﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace testKetNoi.Models
{
    public partial class HoSo
    {
        public HoSo()
        {
            HoSoSinhVien = new HashSet<HoSoSinhVien>();
        }

        public string MaHoSo { get; set; }
        public string Ten { get; set; }
        public string MoTa { get; set; }

        public virtual ICollection<HoSoSinhVien> HoSoSinhVien { get; set; }
    }
}