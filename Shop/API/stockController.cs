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

        //初始化数据
        List<Stock> lists = new List<Stock>() {
            new Stock(){ id="1",name="苹果",price=5,amount=6,commodityType=1},
            new Stock(){ id="2",name="苹果2",price=123,amount=612,commodityType=1},
            new Stock(){ id="3",name="芒果",price=123,amount=612,commodityType=1},
            new Stock(){ id="4",name="诺基亚n",price=123,amount=612,commodityType=2},
            new Stock(){ id="5",name="小米n6",price=523,amount=6,commodityType=2},
            new Stock(){ id="6",name="苹果56",price=123,amount=612,commodityType=1},
            new Stock(){ id="7",name="苹果32",price=123,amount=612,commodityType=1},
            new Stock(){ id="8",name="苹果45",price=123,amount=612,commodityType=1},
            new Stock(){ id="9",name="苹果324",price=5,amount=6,commodityType=1},
            new Stock(){ id="10",name="苹果234",price=123,amount=612,commodityType=1},
            new Stock(){ id="11",name="苹果65",price=123,amount=612,commodityType=1},
            new Stock(){ id="12",name="苹果676",price=123,amount=612,commodityType=1},
            new Stock(){ id="14",name="苹342",price=5,amount=6,commodityType=1},
            new Stock(){ id="15",name="苹果76",price=123,amount=612,commodityType=1},
            new Stock(){ id="16",name="苹果4455",price=123,amount=612,commodityType=1},
            new Stock(){ id="17",name="苹果45",price=123,amount=612,commodityType=1}
        };
        

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
                //return _dbContext.Stocks.ToList();
                return lists;
            }
            else if (currentPage != null && pageSize != null)
            {
                int cp = currentPage ?? 0;
                int ps = pageSize ?? 0;
                if (!string.IsNullOrEmpty(name) && amount != null)
                {
                    //if (_dbContext.Stocks != null && _dbContext.Stocks.Count() > 0)
                    //{
                        //return _dbContext.Stocks.Where(t => t.name == name && t.amount == amount).Skip((cp - 1) * ps).Take(ps).Select(s => s).ToList();
                        return lists.Where(t => t.name == name && t.amount == amount).Skip((cp - 1) * ps).Take(ps).Select(s => s).ToList();
                    //}
                }
                else if (string.IsNullOrEmpty(name))
                {
                    //if (_dbContext.Stocks != null && _dbContext.Stocks.Count() > 0)
                    //{
                        //return _dbContext.Stocks.Where(t => t.name == name).Skip((cp - 1) * ps).Take(ps).Select(s => s).ToList();
                        return lists.Where(t => t.name == name).Skip((cp - 1) * ps).Take(ps).Select(s => s).ToList();
                    //}
                }
                else if (amount != null)
                {
                    //if (_dbContext.Stocks != null && _dbContext.Stocks.Count() > 0)
                    //{
                        //return _dbContext.Stocks.Where(t => t.amount == amount).Skip((cp - 1) * ps).Take(ps).Select(s => s).ToList();
                        return lists.Where(t => t.amount == amount).Skip((cp - 1) * ps).Take(ps).Select(s => s).ToList();
                    //}
                }
                else
                {
                    //if (_dbContext.Stocks != null && _dbContext.Stocks.Count() > 0)
                    //{
                        //return _dbContext.Stocks.Skip((cp - 1) * ps).Take(ps).Select(s => s).ToList();
                        return lists.Skip((cp - 1) * ps).Take(ps).Select(s => s).ToList();
                    //}
                }

                //分页查询,再筛选条件.
            }
            //return null;
            return lists;
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
            return lists.SingleOrDefault(m => m.id == id.ToString());

            //if (_dbContext.Stocks != null && _dbContext.Stocks.Count() > 0) {
            //    try
            //    {
            //        var st = _dbContext.Stocks.SingleOrDefault(m => m.id == id.ToString());
            //        if (st == null)
            //        {
            //            return null;
            //        }
            //        else
            //        {
            //            return st;
            //        }
            //    }
            //    catch (Exception)
            //    {
            //        return null;
            //    }

            //}

            //return null;
           
        }

        /// <summary>
        /// 新建一个库存
        /// </summary>
        /// <param name="value"></param>
        // POST api/v1/stock/
        [HttpPost]
        public int Post([FromBody]Stock value)
        {
            lists.Add(value);
            return 1;
            //try
            //{
            //    _dbContext.Set<Stock>().Add(value);
            //    return _dbContext.SaveChanges();
            //}
            //catch (Exception)
            //{

            //    return -1;
            //}

        }

        /// <summary>
        /// 更新库存信息
        /// </summary>
        /// <param name="value">库存信息</param>
        // PUT api/v1/stock/
        [HttpPut]
        public int Put([FromBody]Stock value)
        {
            if (lists.Where(t => t.id == value.id) != null && lists.Where(t => t.id == value.id).Count() > 0) {
                lists.Where(t => t.id == value.id).First().id= value.id;
                lists.Where(t => t.id == value.id).First().name = value.name;
                lists.Where(t => t.id == value.id).First().price = value.price;
                lists.Where(t => t.id == value.id).First().commodityType = value.commodityType;
                lists.Where(t => t.id == value.id).First().amount = value.amount;
            }

            return 1;

            //if (value != null && value.id != null)
            //{
            //    //ModelState.IsValid
            //    try
            //    {
            //        _dbContext.Update(value);
            //        return _dbContext.SaveChanges();
            //    }
            //    catch (DbUpdateConcurrencyException)
            //    {
            //        return -1;
            //    }

            //}

            //return -1;
        }

        /// <summary>
        /// 删除库存信息
        /// </summary>
        /// <param name="id"></param>
        // DELETE api/v1/stock/5
        [HttpDelete("{id}")]
        public int Delete(int id)
        {

            if (lists.Where(t => t.id == id.ToString()) != null && lists.Where(t => t.id == id.ToString()).Count() > 0)
            {
                lists.Remove(lists.Where(t => t.id == id.ToString()).First());
            }

            return 1;

                //if (id == null)
                //{
                //    return -1;
                //}
                //if (_dbContext.Stocks != null && _dbContext.Stocks.Count() > 0) {

                //    var stocks = _dbContext.Stocks.SingleOrDefaultAsync(m => m.id == id.ToString());
                //    if (stocks == null)
                //    {
                //        return -1;
                //    }
                //}

                //return -1;
            }
    }
}
