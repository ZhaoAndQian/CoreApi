using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Preoff.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CoreTestContext : DbContext
    {
        //string dbconn = string.Empty;
        /// <summary>
        /// 用户类
        /// </summary>
        public virtual DbSet<Tuser> Tuser { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public CoreTestContext(DbContextOptions<CoreTestContext> options) : base(options)
        {

        }
        //public CoreTestContext()
        //{
        //    //var builder = new ConfigurationBuilder()
        //    //    .AddJsonFile("appsettings.json");
        //    //var configuration = builder.Build();
        //    //dbconn = configuration["DBSettings:ConnDBString"];
        //    dbconn = dbstr;
        //}
        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                //optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=CoreTest;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        //               // optionsBuilder.UseSqlServer();
        //            }
        //        }
        //public CoreTestContext(DbContextOptions<CoreTestContext> options)
        //            : base(options)
        //{ }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tuser>(entity =>
            {
                entity.ToTable("TUser");

                entity.Property(e => e.CName)
                    .HasColumnName("C_Name")
                    .HasColumnType("nvarchar(50)");

                entity.Property(e => e.CValue)
                    .HasColumnName("C_Value")
                    .HasColumnType("nvarchar(50)");
            });
        }
    }
}
