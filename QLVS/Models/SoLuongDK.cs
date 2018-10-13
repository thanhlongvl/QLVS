﻿namespace QLVS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SoLuongDK")]
    public partial class SoLuongDK
    {
        [StringLength(20)]
        [Display(Name = "Mã số")]
        public string ID { get; set; }

        [StringLength(20)]
        [Display(Name = "Mã đại lý")]
        public string MaDaiLy { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày đăng kí")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime NgayDK { get; set; }

        [Column("SoLuongDK")]
        [Required(ErrorMessage = "Vui lòng nhập vào số lượng đăng kí")]
        [Display(Name = "Số lượng đăng kí")]
        public int SoLuongDK1 { get; set; }

        public bool? Flag { get; set; }

        public virtual DaiLy DaiLy { get; set; }
    }
}
