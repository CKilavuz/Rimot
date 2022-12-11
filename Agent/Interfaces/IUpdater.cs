using System;
using System.Threading.Tasks;

namespace Rimot.Agent.Interfaces
{
    public interface IUpdater
    {
        Task BeginChecking();
        Task CheckForUpdates();
        Task InstallLatestVersion();
    }
}
