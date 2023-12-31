﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project_62130516.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Linq;

    public partial class SinhVien
    {
        public SinhVien()
        {
            this.BangDiems = new HashSet<BangDiem>();
        }
        [Key]
        [Display(Name = "Mã số sinh viên")]
        public string MaSV { get; set; }
        [Display(Name = "Tên sinh viên")]
        public string TenSV { get; set; }

        [Display(Name = "Số điện thoại")]
        public string SDT { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Giới tính")]
        public string GioiTinh { get; set; }

        [Display(Name = "Mã lớp")]
        public Nullable<System.Guid> MaLop { get; set; }
        public Nullable<bool> DaXoa { get; set; }
    
        public virtual Lop Lop { get; set; }
        public virtual ICollection<BangDiem> BangDiems { get; set; }
    }
}
