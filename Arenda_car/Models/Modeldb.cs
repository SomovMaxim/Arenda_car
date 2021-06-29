using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Arenda_car.Models
{
    public partial class Modeldb : DbContext
    {
        public Modeldb()
            : base("name=Modeldb")
        {
        }

        public virtual DbSet<Car> Car { get; set; }
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<Contract> Contract { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .Property(e => e.vin)
                .IsUnicode(false);

            modelBuilder.Entity<Car>()
                .Property(e => e.state_number)
                .IsUnicode(false);

            modelBuilder.Entity<Car>()
                .Property(e => e.brand)
                .IsUnicode(false);

            modelBuilder.Entity<Car>()
                .Property(e => e.category)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Car>()
                .Property(e => e.age)
                .IsUnicode(false);

            modelBuilder.Entity<Car>()
                .Property(e => e.body)
                .IsUnicode(false);

            modelBuilder.Entity<Car>()
                .Property(e => e.color_body)
                .IsUnicode(false);

            modelBuilder.Entity<Car>()
                .Property(e => e.power_engine)
                .IsUnicode(false);

            modelBuilder.Entity<Car>()
                .Property(e => e.type_engine)
                .IsUnicode(false);

            modelBuilder.Entity<Car>()
                .Property(e => e.price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Car>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Clients>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Clients>()
                .Property(e => e.address)
                .IsUnicode(false);

            modelBuilder.Entity<Clients>()
                .Property(e => e.passport)
                .IsUnicode(false);

            modelBuilder.Entity<Contract>()
                .Property(e => e.name_client)
                .IsUnicode(false);

            modelBuilder.Entity<Contract>()
                .Property(e => e.passport)
                .IsUnicode(false);

            modelBuilder.Entity<Contract>()
                .Property(e => e.price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Users>()
                .Property(e => e.login)
                .IsUnicode(false);

            modelBuilder.Entity<Users>()
                .Property(e => e.password)
                .IsUnicode(false);
        }
    }
}
