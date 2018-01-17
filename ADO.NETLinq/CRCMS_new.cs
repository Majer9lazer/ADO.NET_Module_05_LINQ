namespace ADO.NETLinq
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CRCMS_new : DbContext
    {
        public CRCMS_new()
            : base("name=CRCMS_new")
        {
        }

        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Timer> Timers { get; set; }
        public virtual DbSet<TimerArchive> TimerArchives { get; set; }
        public virtual DbSet<TimerInactivity> TimerInactivities { get; set; }
        public virtual DbSet<TimerInactivityArchive> TimerInactivityArchives { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Document>()
                .Property(e => e.HoursMachines)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Document>()
                .Property(e => e.PartsCost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Document>()
                .Property(e => e.WorkCost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Document>()
                .Property(e => e.ConsumablesCost)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Document>()
                .Property(e => e.ComponentHours)
                .HasPrecision(19, 4);
        }
    }
}
