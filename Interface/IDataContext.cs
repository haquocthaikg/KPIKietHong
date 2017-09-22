using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPIKietHong.Interface
{
    public interface IDataContext<T>
    {
        Task<IEnumerable<T>> GetList(string api);
        Task<IEnumerable<T>> GetListBy(int id,string api);
        Task<T> GetList(int id, string api);
        Task<bool> Create(T item, string api);
        Task<bool> Update(int id, T item, string api);
        Task<bool> Delete(int? id, string api);
    }
}
