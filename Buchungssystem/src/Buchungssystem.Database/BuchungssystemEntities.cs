using Buchungssystem.Domain;

namespace Buchungssystem.Database
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class BuchungssystemEntities : DbContext
    {
        // Der Kontext wurde für die Verwendung einer BuchungssystemEntities-Verbindungszeichenfolge aus der 
        // Konfigurationsdatei ('App.config' oder 'Web.config') der Anwendung konfiguriert. Diese Verbindungszeichenfolge hat standardmäßig die 
        // Datenbank 'Buchungssystem.Database.BuchungssystemEntities' auf der LocalDb-Instanz als Ziel. 
        // 
        // Wenn Sie eine andere Datenbank und/oder einen anderen Anbieter als Ziel verwenden möchten, ändern Sie die BuchungssystemEntities-Zeichenfolge 
        // in der Anwendungskonfigurationsdatei.
        public BuchungssystemEntities()
            : base("name=BuchungssystemEntities")
        {
        }

        // Fügen Sie ein 'DbSet' für jeden Entitätstyp hinzu, den Sie in das Modell einschließen möchten. Weitere Informationen 
        // zum Konfigurieren und Verwenden eines Code First-Modells finden Sie unter 'http://go.microsoft.com/fwlink/?LinkId=390109'.

        public virtual DbSet<Warengruppe> Warengruppen { get; set; }

        public virtual DbSet<Ware> Waren { get; set; }

        public virtual DbSet<Raum> Raeume { get; set; }

        public virtual DbSet<Tisch> Tische { get; set; }

        public virtual DbSet<BuchungsStatus> BuchungsStati { get; set; }

        public virtual DbSet<Buchung> Buchungen { get; set; }

        public virtual DbSet<Reservierung> Reservierungen { get; set; }
    }
}