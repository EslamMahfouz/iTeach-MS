//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Teach.EDM
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblStage
    {
        public tblStage()
        {
            this.tblClasses = new HashSet<tblClass>();
        }
    
        public int idStage { get; set; }
        public string nameStage { get; set; }
    
        public virtual ICollection<tblClass> tblClasses { get; set; }
    }
}
