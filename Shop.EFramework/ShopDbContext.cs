using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Shop.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.EFramework
{
    public class ShopDbContext: DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) 
            : base(options)
        {

        }

        /// <summary>
        /// 商品库存集
        /// </summary>
        public DbSet<Stock> Stocks { get; set; }

        /// <summary> 
        /// 商品分类集
        /// </summary>
        public DbSet<CommodityType> CommodityTypes { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Stock>(entity =>
            {
                entity.ToTable("Stock");

                entity.Property(e => e.id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar")
                    .HasMaxLength(300);

                entity.Property(e => e.price)
                    .IsRequired()
                    .HasColumnName("price");

                entity.Property(e => e.commodityType)
                    .IsRequired()
                    .HasColumnName("commodityType");
            });

            builder.Entity<CommodityType>(entity =>
            {
                entity.ToTable("CommodityType");

                entity.Property(e => e.id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.HasKey(c => new { c.id, c.commodityTypeId, c.commodityTypeName });
            });
               
 
            
            //启用Guid主键类型扩展
            builder.HasPostgresExtension("uuid-ossp");

            base.OnModelCreating(builder);

        }



    }
}
