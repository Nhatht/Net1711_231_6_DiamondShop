
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiamondCommon;
using DiamondShopData;

namespace DiamondShopBusiness
{
    public class OrderProductBusiness
    {
        private readonly UnitOfWork _unitOfWork;
        public OrderProductBusiness()
        {
            //_unitOfWork.ProductRepository = new ProductDAO();
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                var currencies = await _unitOfWork.OrderProductRepository.GetAllAsync();
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
        public async Task<IBusinessResult> GetByOrderId(int orderId)
        {
            try
            {
                var currency = await _unitOfWork.OrderProductRepository.GetByOrderId(orderId);
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
    }
}
