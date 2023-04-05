using CRUD.Models;

namespace CRUD.Interface
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllRows();
        T GetRowById(int id);
        Task<tbl_user?> ChkCredentials(string mail,string pwd);
        void Insert(T obj);
        void Update(T obj);
        void Delete(int id);
        Task<int> Save();
    }
}
