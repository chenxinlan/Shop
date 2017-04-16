using Shop.Entities;
using Shop.IDAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.BLL
{
    public class CommodityTypeBLL : BaseBLL<CommodityType>
    {
        private ICommodityTypeDAL _Dal;

        public CommodityTypeBLL()
               :base() {

            base.Dal = _Dal as ICommodityTypeDAL;

            if (_Dal == null)
            {

            }
        }

        //自己独立的话可以扩展.
        //也可以重写
    }
}
