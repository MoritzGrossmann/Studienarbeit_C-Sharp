﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Stundenplan.Database
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class StundenplanEntities : DbContext
    {
        public StundenplanEntities()
            : base("name=StundenplanEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<studenplan_stunden> studenplan_stunden { get; set; }
        public virtual DbSet<stundenplan> stundenplans { get; set; }
        public virtual DbSet<stundenplan_fach> stundenplan_fach { get; set; }
    }
}
