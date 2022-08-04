namespace Common.Helper.EmailHelper
{
    public interface ISendMail
    {
        void Send(string mailTo, string body, string subject, bool supportHtml = false);
    }
}