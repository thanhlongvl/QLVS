﻿namespace QLVS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Giai")]
    public partial class Giai
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Giai()
        {
            KetQuaSoXoes = new HashSet<KetQuaSoXo>();
        }

        [Key]
        [StringLength(20)]
        [Display(Name = "Mã giải")]
        public string MaGiai { get; set; }


        [StringLength(30)]
        [Display(Name = "Tên giải")]
        public string TenGiai { get; set; }

        [Display(Name = "Số tiền nhận")]
        public decimal? SoTienNhan { get; set; }

        [Display(Name = "Trạng thái")]
        public bool? Flag { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KetQuaSoXo> KetQuaSoXoes { get; set; }
    }
}
