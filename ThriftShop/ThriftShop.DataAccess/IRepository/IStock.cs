using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThriftShop.DataAccess.Repository.IRepository.GenericInterface;
using ThriftShop.Models;

namespace ThriftShop.DataAccess.IRepository
{
    public interface IStock :IRepository<Stock>
    {
        Task<Stock> Update(Stock obj);
        int IncrementCount(Stock stock, int count);
        int DecrementCount(Stock stock, int count);

    }
}
