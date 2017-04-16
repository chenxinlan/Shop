using Shop.Entities;
using Shop.IDAL;
using Shop.PostgreSQLDAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.BLL
{
    public class StockBLL: BaseBLL<Stock>
    {
        private StockDAL _Dal;
        
        public StockBLL()
               :base() {

            base.Dal = _Dal as StockDAL;

            if (_Dal == null)
            {

            }
        }

        //自己独立的话可以扩展.
        //也可以重写

        public IEnumerable<Stock> SelectByOther(int? currentPage, int? pageSize, string name, int? amount)
        {
            return _Dal.GetModelByOther(currentPage, pageSize, name, amount);
        }
    }
}
