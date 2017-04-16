using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shop.API
{
    [Route("api/v1/[controller]")]
    public class stockController : Controller
    {

        /// <summary>
        /// GET: api/stock
        ///获取库存所有数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Stock> Get(int? currentPage,int? pageSize,string name,int ? amount)
        {
            List<Stock> lists = new List<Stock>() {
                new Stock(){ id="1",name="苹果",commodityType=1,price=3,amount=5},
                new Stock(){ id="2",name="苹果2",commodityType=1,price=3,amount=7},
                new Stock(){ id="3",name="苹果444",commodityType=1,price=3,amount=7},
                new Stock(){ id="4",name="苹果",commodityType=1,price=3,amount=5},
                new Stock(){ id="5",name="苹果2",commodityType=1,price=3,amount=7},
                new Stock(){ id="6",name="苹果444",commodityType=1,price=3,amount=7},
                new Stock(){ id="7",name="苹果",commodityType=1,price=3,amount=5},
                new Stock(){ id="8",name="苹果2",commodityType=1,price=3,amount=7},
                new Stock(){ id="9",name="苹果444",commodityType=1,price=3,amount=7},
                new Stock(){ id="10",name="苹果",commodityType=1,price=3,amount=5},
                new Stock(){ id="11",name="苹果2",commodityType=1,price=3,amount=7},
                new Stock(){ id="12",name="苹果444",commodityType=1,price=3,amount=7}
            };

            if (currentPage == null && pageSize == null && string.IsNullOrEmpty(null) && amount == null)
            {

                //返回全部
            }
            else if (currentPage != null && pageSize != null) {

                //分页查询,再筛选条件.
            }

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
            return new Stock() { };
        }

        /// <summary>
        /// 新建一个库存
        /// </summary>
        /// <param name="value"></param>
        // POST api/v1/stock/
        [HttpPost]
        public Stock Post([FromBody]Stock value)
        {
            return new Stock() { };
        }

        /// <summary>
        /// 更新库存信息
        /// </summary>
        /// <param name="value">库存信息</param>
        // PUT api/v1/stock/
        [HttpPut]
        public int Put([FromBody]Stock value)
        {
            return 1;
        }

        /// <summary>
        /// 删除库存信息
        /// </summary>
        /// <param name="id"></param>
        // DELETE api/v1/stock/5
        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            return 1;
        }
    }
}
