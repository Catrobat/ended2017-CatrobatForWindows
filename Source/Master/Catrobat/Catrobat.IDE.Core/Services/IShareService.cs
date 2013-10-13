namespace Catrobat.IDE.Core.Services
{
    public interface IShareService
    {
        void ShareProjectWithMail(string projectName, string mailTo, string subject, string message);

    }
}
