using ThriftShop.Models;

namespace ThriftShop.Client.Areas.Customer.Models.AccountModel
{
    public class AccountCollection
    {
        public UserInfo UserInfo { get; set; }
        public UserAccount UserAccount { get; set; }
    }
}
