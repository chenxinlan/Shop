using Shop.Entities;
using Shop.IDAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.BLL
{
    public class StockBLL: BaseBLL<Stock>
    {
        private IStockDAL _Dal;
        
        public StockBLL()
               :base() {

            base.Dal = _Dal as IStockDAL;

            if (_Dal == null)
            {

            }
        }

        //自己独立的话可以扩展.
        //也可以重写
    }
}
