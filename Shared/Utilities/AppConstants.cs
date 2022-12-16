using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rimot.Shared.Utilities
{
    public class AppConstants
    {
        public const string DefaultProductName = "Rimot";
        public const string DefaultPublisherName = "Rimot Software";
        public const long MaxUploadFileSize = 100_000_000;
        public const int RelayCodeLength = 4;
        public const double ScriptRunExpirationMinutes = 30;

        public const string RimotAscii = @"
  _____                     _          
 |  __ \                   | |           
 | |__) |_   __ ___    ___ |_|_
 |  _  /|_| /'_ ` _ \ / _ \| __/
 | | \ \| |/ | | | | | (_) | | 
 |_|  \_\_||_| |_| |_|\___/\_|
                              ";
    }
}
