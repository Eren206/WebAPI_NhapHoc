﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace testKetNoi.Models
{
    public partial class DongPhuc
    {
        public DongPhuc()
        {
            MuaDongPhuc = new HashSet<MuaDongPhuc>();
        }

        public string MaDongPhuc { get; set; }
        public string TenDongPhuc { get; set; }
        public decimal DonGia { get; set; }

        public virtual ICollection<MuaDongPhuc> MuaDongPhuc { get; set; }
    }
}