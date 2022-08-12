using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThriftShop.DataAccess.Data;
using ThriftShop.DataAccess.IRepository;
using ThriftShop.DataAccess.Repository.Services.Generic_Imp;
using ThriftShop.Models;

namespace ThriftShop.DataAccess.Services
{
    public class StockService : Repository<Stock>, IStock
    {
        private readonly ApplicationDbContext _db;
        public StockService(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public int DecrementCount(Stock stock, int count)
        {
            stock.Count -=  count;
            return stock.Count;
        }

        public int IncrementCount(Stock stock, int count)
        {
            stock.Count += count;
            return stock.Count;
        }

        public async Task<Stock> Update(Stock obj)
        {
            var objFromDb = await _db.Stocks.FirstOrDefaultAsync(u => u.ProductId == obj.ProductId);
            if(objFromDb != null)
            {
                objFromDb.Count = obj.Count;
            }
            return obj;
        }


    }
}
