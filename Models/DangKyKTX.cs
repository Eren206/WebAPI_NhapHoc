﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace testKetNoi.Models
{
    public partial class DangKyKTX
    {
        public string SoCCCD { get; set; }
        public string MaPhong { get; set; }
        public string GhiChu { get; set; }

        public virtual KTX MaPhongNavigation { get; set; }
        public virtual SinhVien SoCCCDNavigation { get; set; }
    }
}