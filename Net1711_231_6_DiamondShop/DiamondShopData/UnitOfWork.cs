using DiamondShopData.Models;
using DiamondShopData.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiamondShopData
{
    public class UnitOfWork
    {
        private ProductRepository _product;
        private CustomerRepository _customer;
        private Net17112316DiamondShopContext _unitOfWorkContext;
        public UnitOfWork()
        {
            _unitOfWorkContext ??= new Net17112316DiamondShopContext();
        }

        public ProductRepository ProductRepository
        {
            get
            {
                return _product ??= new ProductRepository(_unitOfWorkContext);
            }
        }
        //Customer
        public CustomerRepository CustomerRepository
        {
            get
            {
                return _customer ??= new CustomerRepository(_unitOfWorkContext);
            }
        }

    }
}
