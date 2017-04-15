using Shop.Entities;
using Shop.IDAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.BLL
{
    public class BaseBLL<T> where T : BaseModel, new()
    {
        public IBaseDAL<T> Dal;

        public BaseBLL() {
            
        }

        public virtual int Add(T model) {
            return Dal.Add(model);
        }
    }
}
