﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Buchungssystem.Database
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BuchungssystemEntities : DbContext
    {
        public BuchungssystemEntities()
            : base("name=BuchungssystemEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Buchung> Buchung { get; set; }
        public virtual DbSet<Raum> Raum { get; set; }
        public virtual DbSet<Reservierung> Reservierung { get; set; }
        public virtual DbSet<Tisch> Tisch { get; set; }
        public virtual DbSet<Ware> Ware { get; set; }
        public virtual DbSet<Warengruppe> Warengruppe { get; set; }
    }
}
