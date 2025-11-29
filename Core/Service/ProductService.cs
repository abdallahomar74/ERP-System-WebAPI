using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.InventoryModule;
using ServiceAbstraction;
using Shared.DataTransferObjects.ProductModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService(IMapper _mapper,IUnitOfWork _unitOfWork ) : IProductService
    {
        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            var Products = await _unitOfWork.GetRepository<Product>().GetAllAsync();
            return  _mapper.Map<IEnumerable<Product>,IEnumerable<ProductDto>>(Products);  
        }

        public async Task<ProductDto?> GetProductByIdAsync(Guid id)
        {
            var Product = await _unitOfWork.GetRepository<Product>().GetByIdAsync(id);
            if (Product is null)
            {
                return null;
            }
            return _mapper.Map<Product,ProductDto>(Product!);
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto)
        {
            var Repo =  _unitOfWork.GetRepository<Product>();

            var existingProducts = await Repo.GetAllAsync();
            if (existingProducts.Any(p => p.SKU == createProductDto.SKU))
                throw new InvalidOperationException("SKU already exists.");
            
            var product = _mapper.Map<CreateProductDto,Product>(createProductDto);
            await  Repo.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();

            return  _mapper.Map<Product,ProductDto>(product);
        }
        public async Task<bool> UpdateProductAsync(Guid id, UpdateProductDto updateProductDto)
        {
            var repo = _unitOfWork.GetRepository<Product>();

            var product = await repo.GetByIdAsync(id);
            if (product is null)
                return false;

            _mapper.Map(updateProductDto, product);
            product.UpdatedAt = DateTime.UtcNow;

            repo.Update(product);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteProductAsync(Guid id)
        {
            var repo = _unitOfWork.GetRepository<Product>();
            var product = await repo.GetByIdAsync(id);
            if (product is null)
                return false;

            repo.Delete(product);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

    }
}
