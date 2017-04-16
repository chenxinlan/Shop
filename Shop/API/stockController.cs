using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shop.API
{
    [Route("api/[controller]")]
    public class stockController : Controller
    {

        /// <summary>
        /// GET: api/stock
        ///获取库存所有数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Stock> Get(string name,int amount, int pageSize,int currentPage)
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

            return lists;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
