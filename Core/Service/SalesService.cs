using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.InventoryModule;
using DomainLayer.Models.SalesModule;
using Service.Specifications;
using ServiceAbstraction;
using Shared.DataTransferObjects.SalesModule;

namespace Service
{
    public class SalesService(IUnitOfWork _unitOfWork, IMapper _mapper) : ISalesService
    {
        public async Task<SalesOrderDto> CreateSalesOrderAsync(CreateOrderDto createSalesOrderDto)
        {
            #region Validation

            var errors = new List<ValidationError>();

            if (createSalesOrderDto.OrderItems is null || !createSalesOrderDto.OrderItems.Any())
            {
                errors.Add(new ValidationError
                {
                    Field = nameof(createSalesOrderDto.OrderItems),
                    Errors = ["Order must contain at least one item."]
                });
            }

            if (createSalesOrderDto.OrderItems is not null &&
                createSalesOrderDto.OrderItems.Any(i => i.Quantity < 1))
            {
                errors.Add(new ValidationError
                {
                    Field = nameof(CreateOrderItemDto.Quantity),
                    Errors = ["Quantity must be at least 1."]
                });
            }

            if (errors.Any())
                throw new InvalidOrderException(errors);

            #endregion

            #region Merge Duplicate Products

            var mergedItems = createSalesOrderDto.OrderItems
                .GroupBy(i => i.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    Quantity = g.Sum(i => i.Quantity),
                })
                .ToList();

            #endregion

            #region Validate Customer

            var customerRepo = _unitOfWork.GetRepository<Customer>();

            var customer = await customerRepo.GetByIdAsync(createSalesOrderDto.CustomerId);

            if (customer is null || !customer.IsActive)
                throw new CustomerNotFoundException(createSalesOrderDto.CustomerId);

            #endregion

            #region Get Products

            var productIds = mergedItems
                .Select(i => i.ProductId)
                .ToList();

            var spec = new ProductSpecifications(productIds);

            var productRepo = _unitOfWork.GetRepository<Product>();

            var products = await productRepo.GetAllAsync(spec);

            var missingIds = productIds.Except(products.Select(p => p.Id));

            if (missingIds.Any())
                throw new ProductNotFoundException(missingIds.First());

            #endregion

            #region Create Order Items

            var orderItems = new List<SalesOrderItem>();

            decimal totalAmount = 0;

            var productDict = products.ToDictionary(p => p.Id);

            foreach (var item in mergedItems)
            {
                var product = productDict[item.ProductId];

                var unitPrice = product.SalePrice;

                var lineTotal = item.Quantity * unitPrice;

                totalAmount += lineTotal;

                orderItems.Add(new SalesOrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = unitPrice,
                    LineTotal = lineTotal
                });
            }

            #endregion

            #region Create Order

            var order = new SalesOrder
            {
                Id = Guid.NewGuid(),
                OrderNumber = $"SO-{DateTime.UtcNow.Ticks}",
                CustomerId = createSalesOrderDto.CustomerId,
                Status = OrderStatus.Draft,
                TotalAmount = totalAmount,
                CreatedAt = DateTime.UtcNow,
                SalesOrderItems = orderItems
            };

            #endregion

            #region Save

            var orderRepo = _unitOfWork.GetRepository<SalesOrder>();

            await orderRepo.AddAsync(order);

            await _unitOfWork.SaveChangesAsync();

            #endregion

            return _mapper.Map<SalesOrderDto>(order);
        }

        public Task<SalesOrderDto> GetSalesOrderByIdAsync(Guid orderId)
        {
            throw new NotImplementedException();
        }
    }
}