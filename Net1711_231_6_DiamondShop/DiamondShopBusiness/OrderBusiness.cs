
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using DiamondCommon;
using DiamondShopBusiness;
using DiamondShopData;
using DiamondShopData.Models;
using DiamondShopData.ViewModel;


namespace DiamondShopBusiness
{
    public class OrderBusiness
    {
        //private readonly ProductDAO _unitOfWork.OrderRepository;
        private readonly UnitOfWork _unitOfWork;
        public OrderBusiness()
        {
            //_unitOfWork.OrderRepository = new ProductDAO();
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                var currencies = await _unitOfWork.OrderRepository.GetOrder();
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
                var currency = await _unitOfWork.OrderRepository.GetOrderDtO(id);
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
        public async Task<IBusinessResult> Insert(List<ProductsDTO> products)
        {
            try
            {
                var totalPrice = 0.0;
                foreach (var s in products)
                {
                    var product = await _unitOfWork.ProductRepository.GetByIdAsync(s.ProductId);
                    totalPrice += (double)product.Cost + (double)product.Price;
                }

                var order = new Order
                {
                    CustomerId = 1,
                    PaymentId = 1,
                    Status = "Approved",
                    CreatedDate = DateOnly.FromDateTime(DateTime.Now),
                    TotalPrice = (long)totalPrice
                };
                var result = await _unitOfWork.OrderRepository.CreateAsync(order);
                if (result == 1)
                {
                    List<OrderProduct> list = new List<OrderProduct>();
                    foreach (var product in products)
                    {
                        var orderProduct = new OrderProduct
                        {
                            OrderId = order.Id,
                            ProductId = product.ProductId,
                            Quantity = product.Quantity,
                        };
                        list.Add(orderProduct);
                    }

                    foreach (var i in list)
                    {
                        await _unitOfWork.OrderProductRepository.CreateAsync(i);
                    }
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
        public async Task<IBusinessResult> Update(int id, UpdateOrderDTO dto)
        {
            try
            {
                var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
                //order.SaleStaffId = 3;
                order.Status = dto.Status;
                var result = await _unitOfWork.OrderRepository.UpdateAsync(order);
                if (result != 1)
                {
                    return new BusinessResult(Const.ERROR_DATA_NOT_FOUND, "No data found");
                }
                else
                {
                    return new BusinessResult(Const.SUCCESS_GET, "Success");
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(Const.ERROR_EXCEPTION, ex.Message);
            }
        }
    }
}
