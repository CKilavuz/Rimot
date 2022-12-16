﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rimot.Shared.Models
{
    public class BrandingInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [StringLength(25)]
        public string Product { get; set; } = "Remotely";

        public byte[] Icon { get; set; }

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
