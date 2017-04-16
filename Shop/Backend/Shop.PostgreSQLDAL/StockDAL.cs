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

        public StockDAL(ShopDbContext dbContext)
        {
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

        public int Del(int? id)
        {
            if (id == null)
            {
                return -1;
            }
            var stocks = _dbContext.Stocks.SingleOrDefaultAsync(m => m.id == id.ToString());
            if (stocks == null)
            {
                return -1;
            }

            return 1;
        }

        public Stock GetModel(string Name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Stock> GetModelByOther(int? currentPage, int? pageSize, string name, int? amount)
        {
            if (currentPage == null && pageSize == null && string.IsNullOrEmpty(null) && amount == null)
            {

                //返回全部
                return _dbContext.Stocks.ToList();
            }
            else if (currentPage != null && pageSize != null)
            {
                int cp = currentPage ?? 0;
                int ps = pageSize ?? 0;
                if (!string.IsNullOrEmpty(name) && amount != null)
                {
                    return _dbContext.Stocks.Where(t => t.name == name && t.amount == amount).Skip((cp - 1) * ps).Take(ps).Select(s => s).ToList();
                }
                else if (string.IsNullOrEmpty(name))
                {
                    return _dbContext.Stocks.Where(t => t.name == name).Skip((cp - 1) * ps).Take(ps).Select(s => s).ToList();
                }
                else if (amount != null)
                {
                    return _dbContext.Stocks.Where(t => t.amount == amount).Skip((cp - 1) * ps).Take(ps).Select(s => s).ToList();
                }
                else
                {
                    return _dbContext.Stocks.Skip((cp - 1) * ps).Take(ps).Select(s => s).ToList();
                }

                //分页查询,再筛选条件.
            }
            return null;
        }

        public List<Stock> GetModels()
        {
            return _dbContext.Set<Stock>().ToList();
        }

        public int Update(Stock model)
        {
            if (model != null && model.id != null)
            {
                //ModelState.IsValid
                try
                {
                    _dbContext.Update(model);
                    return _dbContext.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }

            }

            return -1;
        }
    }
}
