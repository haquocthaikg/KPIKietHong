using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPIKietHong.Interface
{
    public interface IHeThong<T>
    {
        Task<IEnumerable<T>> GetList(string api);
        Task<T> GetList(int id, string api);
        Task<bool> Create(T item, string api);
        Task<bool> Update(T item, string api);
        Task<bool> Delete(int param,int param1, string api);
    }
}
