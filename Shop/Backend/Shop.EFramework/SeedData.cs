using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shop.EFramework;
using Shop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Backend.Shop.EFramework
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ShopDbContext(serviceProvider.GetRequiredService<DbContextOptions<ShopDbContext>>()))
            {
                if (context.Stocks.Any())
                {
                    return;   // 已经初始化过数据，直接返回
                }
                Guid id = Guid.NewGuid();
                //增加一个部门
                context.Stocks.AddRange(
                   new Stock
                   {
                       id = id.ToString(),
                       name = "苹果",
                       commodityType=1,
                       price = 5,
                       amount=5
                   },
                   new Stock
                   {
                       id = id.ToString(),
                       name = "草莓",
                       commodityType = 1,
                       price = 5,
                       amount = 5
                   },
                   new Stock
                   {
                       id = id.ToString(),
                       name = "诺基亚",
                       commodityType = 2,
                       price = 1000,
                       amount = 5
                   }
                );
               
                context.SaveChanges();
            }
        }
    }
}

