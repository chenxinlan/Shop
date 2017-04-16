using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Entities;
using Shop.EFramework;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shop.API
{
    [Route("api/v1/[controller]")]
    public class stockController : Controller
    {
        protected readonly ShopDbContext _dbContext;

        public stockController(ShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        

        /// <summary>
        /// GET: api/stock
        ///获取库存所有数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Stock> Get(int? currentPage,int? pageSize,string name,int ? amount)
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
                    if (_dbContext.Stocks != null && _dbContext.Stocks.Count() > 0)
                    {
                        return _dbContext.Stocks.Where(t => t.name == name && t.amount == amount).Skip((cp - 1) * ps).Take(ps).Select(s => s).ToList();
                    }
                }
                else if (string.IsNullOrEmpty(name))
                {
                    if (_dbContext.Stocks != null && _dbContext.Stocks.Count() > 0)
                    {
                        return _dbContext.Stocks.Where(t => t.name == name).Skip((cp - 1) * ps).Take(ps).Select(s => s).ToList();
                    }
                }
                else if (amount != null)
                {
                    if (_dbContext.Stocks != null && _dbContext.Stocks.Count() > 0)
                    {
                        return _dbContext.Stocks.Where(t => t.amount == amount).Skip((cp - 1) * ps).Take(ps).Select(s => s).ToList();
                    }
                }
                else
                {
                    if (_dbContext.Stocks != null && _dbContext.Stocks.Count() > 0)
                    {
                        return _dbContext.Stocks.Skip((cp - 1) * ps).Take(ps).Select(s => s).ToList();
                    }
                }

                //分页查询,再筛选条件.
            }
            return null;
        }

        /// <summary>
        /// 根据id获取实例
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/v1/stock/苹果
        [HttpGet("{id}")]
        public Stock Get(int id)
        {
            if (_dbContext.Stocks != null && _dbContext.Stocks.Count() > 0) {
                try
                {
                    var st = _dbContext.Stocks.SingleOrDefault(m => m.id == id.ToString());
                    if (st == null)
                    {
                        return null;
                    }
                    else
                    {
                        return st;
                    }
                }
                catch (Exception)
                {
                    return null;
                }
                
            }

            return null;
           
        }

        /// <summary>
        /// 新建一个库存
        /// </summary>
        /// <param name="value"></param>
        // POST api/v1/stock/
        [HttpPost]
        public int Post([FromBody]Stock value)
        {
            try
            {
                _dbContext.Set<Stock>().Add(value);
                return _dbContext.SaveChanges();
            }
            catch (Exception)
            {

                return -1;
            }
           
        }

        /// <summary>
        /// 更新库存信息
        /// </summary>
        /// <param name="value">库存信息</param>
        // PUT api/v1/stock/
        [HttpPut]
        public int Put([FromBody]Stock value)
        {
            if (value != null && value.id != null)
            {
                //ModelState.IsValid
                try
                {
                    _dbContext.Update(value);
                    return _dbContext.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return -1;
                }

            }

            return -1;
        }

        /// <summary>
        /// 删除库存信息
        /// </summary>
        /// <param name="id"></param>
        // DELETE api/v1/stock/5
        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            if (id == null)
            {
                return -1;
            }
            if (_dbContext.Stocks != null && _dbContext.Stocks.Count() > 0) {

                var stocks = _dbContext.Stocks.SingleOrDefaultAsync(m => m.id == id.ToString());
                if (stocks == null)
                {
                    return -1;
                }
            }

            return -1;
        }
    }
}
