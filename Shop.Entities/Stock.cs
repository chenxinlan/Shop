using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Entities
{
    /// <summary>
    /// 库存表
    /// </summary>
    public class Stock: BaseModel
    {
        public Stock()
        {
            if (string.IsNullOrEmpty(this.id) || (string.IsNullOrWhiteSpace(this.id)))
            {
                this.id = Guid.NewGuid().ToString("N");
            }
        }

        /// <summary>
        /// 商品名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 商品分类
        /// </summary>
        public int commodityType { get; set; }

        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal price { get; set; }

        /// <summary>
        /// 商品数量
        /// </summary>
        public int amount { get; set; }


    }
}
