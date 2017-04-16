using Shop.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.IDAL
{
    public interface IStockDAL: IBaseDAL<Stock>
    {
        //扩展
        IEnumerable<Stock> GetModelByOther(int? currentPage, int? pageSize, string name, int? amount);
    }
}
