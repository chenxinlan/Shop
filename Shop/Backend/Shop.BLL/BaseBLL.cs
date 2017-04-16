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

        //增加
        public virtual int Add(T model) {
            return Dal.Add(model);
        }

        //删除
        public virtual int Del(int id)
        {
            return Dal.Del(id);
        }

        //更新
        public virtual int Update(T m)
        {
            return Dal.Update(m);
        }

        //查找全部数据.
        public virtual List<T> Select()
        {
            return Dal.GetModels();
        }

        //查找通过id
        public virtual T SelectById(string id)
        {
            return Dal.GetModel(id);
        }

    }
}
