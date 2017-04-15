using Shop.IDAL;
using System;
using System.Collections.Generic;
using System.Text;
using Shop.Entities;
using Shop.EFramework;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Shop.PostgreSQLDAL
{
    public class StockDAL : IStockDAL
    {
        protected readonly ShopDbContext _dbContext;

        public StockDAL(ShopDbContext dbContext){
             _dbContext = dbContext;
        }

        public int Add(Stock model)
        {
            _dbContext.Set<Stock>().Add(model);
            return _dbContext.SaveChanges();
        }

        public void AddList(List<Stock> model)
        {
            throw new NotImplementedException();
        }

        public int Del(int id)
        {
            throw new NotImplementedException();
        }

        public Stock GetModel(string ID)
        {
            throw new NotImplementedException();
        }

        public List<Stock> GetModels()
        {
            return _dbContext.Set<Stock>().ToList();
        }

        public void Update(Stock model)
        {
            throw new NotImplementedException();
        }
    }
}
