using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Entity
{

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=InsuranceConection")
        {
        }

        public virtual DbSet<Brunch> Brunches { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Contract> Contracts { get; set; }
        public virtual DbSet<InsuredEvent> InsuredEvents { get; set; }
        public virtual DbSet<TypeOfInsurance> TypeOfInsurances { get; set; }
        public virtual DbSet<Worker> Workers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brunch>()
                .HasMany(e => e.Workers)
                .WithRequired(e => e.Brunch)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<Client>()
                .HasMany(e => e.Contracts)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Contract>()
                .HasMany(e => e.InsuredEvents)
                .WithRequired(e => e.Contract)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<InsuredEvent>()
                .Property(e => e.Sum)
                .HasPrecision(19, 4);

            modelBuilder.Entity<InsuredEvent>()
                .Property(e => e.Status)
                .IsFixedLength()
                .IsUnicode(false);


            modelBuilder.Entity<TypeOfInsurance>()
                .Property(e => e.InsuranceType)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TypeOfInsurance>()
                .HasMany(e => e.Contracts)
                .WithRequired(e => e.TypeOfInsurance)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Worker>()
                .Property(e => e.Sex)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Worker>()
                .HasMany(e => e.Contracts)
                .WithRequired(e => e.Worker)
                .HasForeignKey(e => e.InsuranceAgentID)
                .WillCascadeOnDelete(false);

        }
    }
}
