using ThriftShop.API;
using ThriftShop.Models;

namespace ThriftShop.Client.Areas.Customer.Models.MailSendContract
{
    public class ContractMail
    {
        public UserInfo InfoUser { get; set; }
        public EmailModel EmailModel {get;set; }
    }
}
