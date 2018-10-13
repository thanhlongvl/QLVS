namespace QLVS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DotPhatHanh")]
    public partial class DotPhatHanh
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        [Display(Name = "Mã đại lý")]
        public string MaDaiLy { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        [Display(Name = "Mã loại vé số")]
        public string MaLoaiVeSo { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [Display(Name = "Ngày nhận")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime NgayNhan { get; set; }

        [Display(Name = "Số lượng phát hành")]
        public int? SoLuong { get; set; }

        [Display(Name = "Số lượng bán được")]
        public int? SLBanDuoc { get; set; }

        [Display(Name = "Tiền thanh toán")]
        public decimal? TienThanhToan { get; set; }

        public bool? Flag { get; set; }

        public virtual DaiLy DaiLy { get; set; }

        public virtual LoaiVeso LoaiVeso { get; set; }
    }
}
