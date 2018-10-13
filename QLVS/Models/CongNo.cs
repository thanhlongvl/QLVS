namespace QLVS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CongNo")]
    public partial class CongNo
    {
        [StringLength(20)]
        public string ID { get; set; }

        [StringLength(20)]
        public string MaDaiLy { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Ngay { get; set; }

        public decimal? SoTienNo { get; set; }

        public bool? Flag { get; set; }

        public virtual DaiLy DaiLy { get; set; }
    }
}
