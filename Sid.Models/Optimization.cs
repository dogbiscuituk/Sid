﻿namespace Sid.Models
{
    using System.ComponentModel;

    public enum  Optimization
    {
        [Description("Default")]
        Default,
        [Description("High Quailty")]
        HighQuality,
        [Description("High Speed")]
        HighSpeed
    }
}
