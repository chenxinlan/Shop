using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Entities;
using Shop.BLL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Shop.API
{
    [Route("api/v1/[controller]")]
    public class stockController : Controller
    {
        private StockBLL _BLL;
        /// <summary>
        /// GET: api/stock
        ///获取库存所有数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Stock> Get(int? currentPage,int? pageSize,string name,int ? amount)
        {
           return _BLL.SelectByOther(currentPage, pageSize, name, amount);
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
           return _BLL.SelectById(id.ToString());
        }

        /// <summary>
        /// 新建一个库存
        /// </summary>
        /// <param name="value"></param>
        // POST api/v1/stock/
        [HttpPost]
        public int Post([FromBody]Stock value)
        {
            return _BLL.Add(value);
        }

        /// <summary>
        /// 更新库存信息
        /// </summary>
        /// <param name="value">库存信息</param>
        // PUT api/v1/stock/
        [HttpPut]
        public int Put([FromBody]Stock value)
        {
            return _BLL.Update(value);
        }

        /// <summary>
        /// 删除库存信息
        /// </summary>
        /// <param name="id"></param>
        // DELETE api/v1/stock/5
        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            return _BLL.Del(id);
        }
    }
}
