﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rimot.Server.Models
{
    public enum CircuitEventName
    {
        DisplayMessage,
        UnattendedSessionReady,
        ChatReceived,
        CommandResult,
        DeviceUpdate,
        DownloadFile,
        DownloadFileProgress,
        DeviceWentOffline,
        ScriptResult,
        TransferCompleted,
        PowerShellCompletions,
        RemoteLogsReceived,
    }
}
