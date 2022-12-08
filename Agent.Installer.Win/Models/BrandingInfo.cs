using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rimot.Agent.Installer.Win.Models
{
    public class BrandingInfo
    {
        public string Product { get; set; } = "Rimot";

        public string Icon { get; set; }

        public byte TitleForegroundRed { get; set; } = 255;

        public byte TitleForegroundGreen { get; set; } = 255;

        public byte TitleForegroundBlue { get; set; } = 255;

        public byte TitleBackgroundRed { get; set; } = 255;

        public byte TitleBackgroundGreen { get; set; } = 0;

        public byte TitleBackgroundBlue { get; set; } = 0;

        public byte ButtonForegroundRed { get; set; } = 255;

        public byte ButtonForegroundGreen { get; set; } = 255;

        public byte ButtonForegroundBlue { get; set; } = 255;
    }
}
