namespace ThriftShop.API
{
    public interface IEmailService
    {
        void SendEmail(EmailModel request);
    }
}
