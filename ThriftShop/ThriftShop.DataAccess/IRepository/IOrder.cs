using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThriftShop.DataAccess.Repository.IRepository.GenericInterface;
using ThriftShop.Models;

namespace ThriftShop.DataAccess.IRepository
{
    public interface IOrder:IRepository<Order>
    {
        Task<Order> Update(Order obj);
    }
}
