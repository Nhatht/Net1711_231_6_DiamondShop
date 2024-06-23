using DiamondShopData;
using DiamondShopData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondShopBusiness
{
    public class CustomerBusiness
    {
        //private readonly CustomerDAO _DAO;
        private readonly UnitOfWork _unitOfWork;

        public CustomerBusiness()
        {
            //_DAO = new CustomerDAO();
            _unitOfWork ??= new UnitOfWork();
        }
        public async Task<IBusinessResult> GetAll()
        {
            try
            {
                #region Businees rule
                #endregion
                var customer = await _unitOfWork.CustomerRepository.GetAllAsync();
                if (customer == null)
                {
                    return new BusinessResult(-1, " No customer data");
                }
                else
                {
                    return new BusinessResult(1, "Get customer list success", customer);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }
        public async Task<IBusinessResult> CreateCustomer(Customer customer)
        {
            try
            {
                #region Businees rule
                #endregion
                var cus = await _unitOfWork.CustomerRepository.CreateAsync(customer);
                if (cus == null)
                {
                    return new BusinessResult(-1, "Add fail");
                }
                else
                {
                    return new BusinessResult(1, "Add success", customer);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }
        public async Task<IBusinessResult> DeleteCustomer(int id)
        {
            try
            {
                #region Businees rule
                #endregion
                var getExistedCustomer = _unitOfWork.CustomerRepository.GetById(id);
                if (getExistedCustomer == null)
                {
                    return new BusinessResult(-1, " No customer data by code");
                }
                else
                {
                    getExistedCustomer.IsDeleted = true;
                    var result = await _unitOfWork.CustomerRepository.UpdateAsync(getExistedCustomer);

                    return new BusinessResult(1, "Delete success");
                }


            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }
        public async Task<IBusinessResult> GetById(int id)
        {
            try
            {
                #region Business rule
                #endregion
                var result = await _unitOfWork.CustomerRepository.GetByIdAsync(id);
                if (result == null)
                {
                    return new BusinessResult(-1, "No data");
                }
                else
                {
                    return new BusinessResult(1, "Get success", result);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }
        public async Task<IBusinessResult> Update(Customer customer)
        {

            try
            {
                #region Businees rule
                #endregion
                var cus = await _unitOfWork.CustomerRepository.UpdateAsync(customer);
                if (cus == null)
                {
                    return new BusinessResult(-1, "Update fail");
                }
                else
                {
                    return new BusinessResult(1, "Update success", customer);
                }
            }
            catch (Exception ex)
            {
                return new BusinessResult(-4, ex.Message);
            }
        }


    }
}
