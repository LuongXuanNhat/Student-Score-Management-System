//------------------------------------------------------------------------------
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
    
    public partial class Lop
    {
        public Lop()
        {
            this.SinhViens = new HashSet<SinhVien>();
        }
    
        public System.Guid MaLop { get; set; }
        public string TenLop { get; set; }
    
        public virtual ICollection<SinhVien> SinhViens { get; set; }
    }
}
