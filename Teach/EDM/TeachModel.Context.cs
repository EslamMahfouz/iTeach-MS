﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TeachEntities : DbContext
    {
        public TeachEntities()
            : base("name=TeachEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<tblClass> tblClasses { get; set; }
        public DbSet<tblStage> tblStages { get; set; }
        public DbSet<tblGroup> tblGroups { get; set; }
        public DbSet<tblAbsence> tblAbsences { get; set; }
        public DbSet<tblRelation> tblRelations { get; set; }
        public DbSet<tblUser> tblUsers { get; set; }
        public DbSet<CenterData> CenterDatas { get; set; }
        public DbSet<tblStudent> tblStudents { get; set; }
    }
}
