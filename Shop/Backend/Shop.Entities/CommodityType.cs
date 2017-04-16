using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Entities
{
    /// <summary>
    /// 分类表
    /// </summary>
    public class CommodityType: BaseModel
    {
        public CommodityType()
        {
            if (string.IsNullOrEmpty(this.id) || (string.IsNullOrWhiteSpace(this.id)))
            {
                this.id = Guid.NewGuid().ToString("N");
            }
        }

        /// <summary>
        /// 分类编号(不能重复)
        /// </summary>
        public int commodityTypeId { get; set; }

        /// <summary>
        /// 分类名称(不能重复)
        /// </summary>
        public int commodityTypeName { get; set; }
    }
}
