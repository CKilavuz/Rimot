﻿using Microsoft.Extensions.DependencyInjection;
using Rimot.Desktop.Core;
using Rimot.Desktop.Core.Interfaces;
using Rimot.Desktop.Core.Services;
using Rimot.Shared.Utilities;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Rimot.Desktop.XPlat.Services
{
    public class ShutdownServiceLinux : IShutdownService
    {
        public async Task Shutdown()
        {
            Logger.Debug($"Exiting process ID {Environment.ProcessId}.");
            var casterSocket = ServiceContainer.Instance.GetRequiredService<ICasterSocket>();
            await casterSocket.DisconnectAllViewers();
            Environment.Exit(0);
        }
    }
}
