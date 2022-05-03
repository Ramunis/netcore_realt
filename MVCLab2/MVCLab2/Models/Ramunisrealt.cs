using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MVCLab2.Models
{
    public class Ramunisrealt : DbContext
    {
        public Ramunisrealt()            //конструкторы класса
        { }
        public Ramunisrealt(DbContextOptions<Ramunisrealt> options)
        : base(options)
        { }

        public virtual DbSet<myboard> myboard { get; set; }
        public virtual DbSet<buyboard> buyboard { get; set; }
        public virtual DbSet<rentboard> rentboard { get; set; }
        public virtual DbSet<Contracts> Contracts { get; set; }
        public virtual DbSet<Clients> Clients { get; set; }

        public virtual DbSet<Realts> Realts { get; set; }

        public virtual DbSet<Districts> Districts { get; set; }

        public virtual DbSet<Servics> Servics { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=ASUSH510M-K; Database=Ramunisrealt; Trusted_Connection = True;");
                //optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=111; Trusted_Connection = True; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contracts>(entity =>		 //описание  сущностей 
            {
                entity.ToTable("Contracts");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.DZ)
                  .IsRequired()
                  .HasColumnName("DZ");

                entity.Property(e => e.Client).HasColumnName("Client");

                entity.Property(e => e.Realtor).HasColumnName("Realtor");

                entity.Property(e => e.District).HasColumnName("District");

                entity.Property(e => e.Servic).HasColumnName("Servic");

                entity.Property(e => e.Sq)
                   .IsRequired()
                   .HasColumnName("Sq");

                entity.Property(e => e.DS)
                  .IsRequired()
                  .HasColumnName("DS");

                entity.Property(e => e.Term)
                   .IsRequired()
                   .HasColumnName("Term");

                entity.Property(e => e.Price)
                   .IsRequired()
                   .HasColumnName("Price");

                entity.Property(e => e.Pay)
                     .IsRequired()
                     .HasColumnName("Pay")
                     .HasMaxLength(50);

                entity.Property(e => e.Repair)
                     .IsRequired()
                     .HasColumnName("Repair")
                     .HasMaxLength(20);

            });

            modelBuilder.Entity<Clients>(entity =>		 //описание  сущностей 
            {
                entity.ToTable("Clients");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.Username)                   // параметры сущности 
                      .IsRequired()                          //обязательное значение (не пустое)
                      .HasColumnName("Username")                    // имя колонки в таблице БД
                      .HasMaxLength(30);                         //размер поля

                entity.Property(e => e.PW)                  
                     .IsRequired()                          
                     .HasColumnName("PW")                   
                     .HasMaxLength(200);

                entity.Property(e => e.F)
                     .IsRequired()
                     .HasColumnName("F")
                     .HasMaxLength(30);

                entity.Property(e => e.I)
                     .IsRequired()
                     .HasColumnName("I")
                     .HasMaxLength(30);

                entity.Property(e => e.O)
                     .IsRequired()
                     .HasColumnName("O")
                     .HasMaxLength(30);

                entity.Property(e => e.DR)
                  .IsRequired()
                  .HasColumnName("DR");

                entity.Property(e => e.City)
                     .IsRequired()
                     .HasColumnName("City")
                     .HasMaxLength(30);

                entity.Property(e => e.Adres)
                     .IsRequired()
                     .HasColumnName("Adres")
                     .HasMaxLength(50);

                entity.Property(e => e.Phone)
                     .IsRequired()
                     .HasColumnName("Phone")
                     .HasMaxLength(10);

                entity.Property(e => e.Email)
                     .IsRequired()
                     .HasColumnName("Email")
                     .HasMaxLength(20);

            });

            modelBuilder.Entity<Realts>(entity =>		 //описание  сущностей 
            {
                entity.ToTable("Realts");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.Username)                   // параметры сущности 
                      .IsRequired()                          //обязательное значение (не пустое)
                      .HasColumnName("Username")                    // имя колонки в таблице БД
                      .HasMaxLength(30);                         //размер поля

                entity.Property(e => e.PW)
                     .IsRequired()
                     .HasColumnName("PW")
                     .HasMaxLength(200);

                entity.Property(e => e.F)
                     .IsRequired()
                     .HasColumnName("F")
                     .HasMaxLength(30);

                entity.Property(e => e.I)
                     .IsRequired()
                     .HasColumnName("I")
                     .HasMaxLength(30);

                entity.Property(e => e.O)
                     .IsRequired()
                     .HasColumnName("O")
                     .HasMaxLength(30);

                entity.Property(e => e.DR)
                  .IsRequired()
                  .HasColumnName("DR");

                entity.Property(e => e.Salar)
                   .IsRequired()
                   .HasColumnName("Salar");

                entity.Property(e => e.City)
                     .IsRequired()
                     .HasColumnName("City")
                     .HasMaxLength(30);

                entity.Property(e => e.Adres)
                     .IsRequired()
                     .HasColumnName("Adres")
                     .HasMaxLength(50);

                entity.Property(e => e.Phone)
                     .IsRequired()
                     .HasColumnName("Phone")
                     .HasMaxLength(10);

                entity.Property(e => e.Email)
                     .IsRequired()
                     .HasColumnName("Email")
                     .HasMaxLength(20);

            });

            modelBuilder.Entity<Districts>(entity =>		 //описание  сущностей 
            {
                entity.ToTable("Districts");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.District)                   // параметры сущности 
                      .IsRequired()                          //обязательное значение (не пустое)
                      .HasColumnName("District")                    // имя колонки в таблице БД
                      .HasMaxLength(20);                         //размер поля  
            });

            modelBuilder.Entity<Servics>(entity =>		 //описание  сущностей 
            {
                entity.ToTable("Servics");

                entity.Property(e => e.ID).HasColumnName("ID");

                entity.Property(e => e.Servic)                   // параметры сущности 
                      .IsRequired()                          //обязательное значение (не пустое)
                      .HasColumnName("Servic")                    // имя колонки в таблице БД
                      .HasMaxLength(10);                         //размер поля  
            });


            // OnModelCreatingPartial(modelBuilder);
        }
       
    }
}
