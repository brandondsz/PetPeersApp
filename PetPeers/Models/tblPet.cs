//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PetPeers.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblPet
    {
        public long PetId { get; set; }
        public string PetName { get; set; }
        public Nullable<int> Age { get; set; }
        public string Place { get; set; }
        public long PetOwnerId { get; set; }
        public Nullable<System.DateTime> CreatedDateTime { get; set; }
        public Nullable<bool> IsSold { get; set; }
    
        public virtual tblUser tblUser { get; set; }
    }
}
