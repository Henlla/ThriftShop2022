using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThriftShop.DataAccess.IRepository;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IProduct Product { get; }
        IUserInfo UserInfo { get; }
        IAdmin Admin { get; }
        void Save();
    }
}
