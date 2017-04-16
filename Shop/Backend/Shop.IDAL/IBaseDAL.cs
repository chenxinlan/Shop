using Shop.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.IDAL
{
    public interface IBaseDAL<T> where T: BaseModel,new()
    {
        int Add(T model);
        void AddList(List<T> model);
        int Del(int? id);
        int Update(T model);
        T GetModel(string Name);
        List<T> GetModels();
    }
}
