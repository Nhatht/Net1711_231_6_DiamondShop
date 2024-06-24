using DiamondCommon;
using DiamondShopData.ViewModel.ProductDTO;
using DiamondShopData;
using DiamondShopData.DAO;
using DiamondShopData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DiamondShopBusiness
{
    public class ProductBusiness
    {
        //private readonly ProductDAO _unitOfWork.ProductRepository;
        private readonly UnitOfWork _unitOfWork;
        public ProductBusiness()
        {
            //_unitOfWork.ProductRepository = new ProductDAO();
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> GetAll(string? query = null)
        {
            try
            {
                var currencies = await _unitOfWork.ProductRepository.GetAllAsync(query);
                if (currencies == null)
                {
                    return new BusinessResult(-1, "No data found");
                }
                else
                {
                    return new BusinessResult(1, "Success", currencies);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
        public async Task<IBusinessResult> GetById(int id)
        {
            try
            {
                var currency = await _unitOfWork.ProductRepository.GetByIdAsync(id);
                if (currency == null)
                {
                    return new BusinessResult(Const.ERROR_DATA_NOT_FOUND, "No data found");
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_GET, "Success", currency);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
        // create insert method
        public async Task<IBusinessResult> Insert(ProductAddDTO productDto, string url)
        {
            try
            {
                // Convert ProductDTO to Product
                var product = new Product
                {

                    DiamondId = productDto.DiamondId,
                    Name = productDto.Name,
                    Description = productDto.Description,
                    Metal = productDto.Metal,
                    ImageUrl = url,
                    Price = productDto.Price,
                    Cost = productDto.Cost,
                    Size = productDto.Size,
                    Stock = productDto.Stock,
                    IsDeleted = false
                };

                var result = await _unitOfWork.ProductRepository.CreateAsync(product);
                if (result == 1)
                {
                    return new BusinessResult(Const.SUCCESS_GET, "Success");
                }
                else
                {
                    return new BusinessResult(Const.FAILURE, "Fail");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }



        public async Task<IBusinessResult> Update(int id, ProductUpdateDTO productDto, string url)
        {
            if (productDto == null)
            {
                return new BusinessResult(Const.ERROR_INVALID_DATA, "Product cannot be null");
            }
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null)
            {
                return new BusinessResult(Const.ERROR_DATA_NOT_FOUND, "No data found");
            }
            try
            {
                product.DiamondId = productDto.DiamondId;
                product.Name = productDto.Name;
                product.Description = productDto.Description;
                product.Metal = productDto.Metal;
                if(!string.IsNullOrEmpty(url))
                {
                    product.ImageUrl = url;
                }
                product.Price = productDto.Price;
                product.Cost = productDto.Cost;
                product.Size = productDto.Size;
                product.Stock = productDto.Stock;
                product.IsDeleted = productDto.IsDeleted;

                await _unitOfWork.ProductRepository.UpdateAsync(product);
                return new BusinessResult(Const.SUCCESS_GET, "Success");
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }

        public async Task<IBusinessResult> UpdateIsDelete(int id, bool? isDelete)
        {
            var currency = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (currency == null)
            {
                return new BusinessResult(Const.ERROR_DATA_NOT_FOUND, "No data found");
            }
            try
            {
                if (isDelete.HasValue)
                {
                    currency.IsDeleted = isDelete;
                }
                await _unitOfWork.ProductRepository.UpdateAsync(currency);
                return new BusinessResult(Const.SUCCESS_GET, "Success");
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
        public async Task<IBusinessResult> Delete(int id)
        {
            var currency = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (currency == null)
            {
                return new BusinessResult(Const.ERROR_DATA_NOT_FOUND, "No data found");
            }
            try
            {
                await _unitOfWork.ProductRepository.RemoveAsync(currency);
                return new BusinessResult(Const.SUCCESS_GET, "Success");
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
    }
}
