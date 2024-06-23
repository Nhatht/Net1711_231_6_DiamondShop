using DiamondShopData.Models;
using DiamondShopData.Repository;
using OrderData.Repository;
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
        private OrderRepository _order;
        private OrderProductRepository _orderProduct;
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
        public OrderRepository OrderRepository
        {
            get
            {
                return _order ??= new OrderRepository(_unitOfWorkContext);
            }
        }
         public OrderProductRepository OrderProductRepository
 {
     get
     {
         return _orderProduct ??= new OrderProductRepository(_unitOfWorkContext);
     }
 }
    }
}
