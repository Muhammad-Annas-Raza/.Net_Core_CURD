using CRUD.Interface;
using CRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD.DAL
{
    public class Gen_Repo<T> : IRepository<T> where T : class
    {
        private MyDbContext _context = null;
        private DbSet<T> table = null;
        public Gen_Repo(MyDbContext context)
        {
            this._context = context;
            this.table = _context.Set<T>();
        }

        public async Task<tbl_user?> ChkCredentials(string mail, string pwd)
        {
           tbl_user? Verified_row = await _context.tbl_user.Where(m => m.user_email == mail && m.user_password == pwd).FirstOrDefaultAsync();
            return Verified_row;
        }

        public async void Delete(int id)
        {
            T? row = await GetRowById(id);
            if (row != null)
            {
              _context.Remove(row);
             //_context.Entry(row).State = EntityState.Deleted;
            }

        }

        public async Task<IEnumerable<T>> GetAllRows()
        {
            return await table.ToListAsync();
            
        }

        public async Task<T?> GetRowById(int id)
        {
            return await table.FindAsync(id);           
        }

        public async void Insert(T obj)
        {
            await table.AddAsync(obj);
            
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }

        public void Update(T obj)
        {
            _context.Attach(obj);
            //_context.Entry(obj).State = EntityState.Modified;


        }

        T IRepository<T>.GetRowById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
