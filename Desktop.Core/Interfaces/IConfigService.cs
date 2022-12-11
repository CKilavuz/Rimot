﻿using Rimot.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rimot.Desktop.Core.Interfaces
{
    public interface IConfigService
    {
        DesktopAppConfig GetConfig();
        void Save(DesktopAppConfig config);
    }
}
