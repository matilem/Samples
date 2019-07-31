namespace Aafp.EmailSender.Api.Tasks.Interfaces
{
    public interface ICustomerCorrespondenceTasks
    {
        void LogEmail(string content, string recipient, string subject);
    }
}
