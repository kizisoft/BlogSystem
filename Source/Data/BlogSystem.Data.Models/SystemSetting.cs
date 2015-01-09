﻿namespace BlogSystem.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class SystemSetting
    {
        [Key]
        public string Name { get; set; }

        public string Value { get; set; }
    }
}