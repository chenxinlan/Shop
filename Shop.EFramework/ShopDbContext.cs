using Microsoft.EntityFrameworkCore;
using Shop.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.EFramework
{
    public class ShopDbContext: DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
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





    }
}
