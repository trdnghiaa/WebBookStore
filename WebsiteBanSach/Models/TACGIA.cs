//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebsiteBanSach.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class TACGIA
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TACGIA()
        {
            this.THAMGIAs = new HashSet<THAMGIA>();
        }
    
        public int MaTacGia { get; set; }
        public string TenTacGia { get; set; }
        public string DienThoai { get; set; }
        public string DiaChi { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<THAMGIA> THAMGIAs { get; set; }
    }
}
