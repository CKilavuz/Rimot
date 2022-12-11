using System.Threading.Tasks;

namespace Rimot.Desktop.Core.Interfaces
{
    public interface IChatClientService
    {
        Task StartChat(string requesterID, string organizationName);
    }
}
