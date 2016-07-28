using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.WebModels;
using PagedList;
using PagedList.EntityFramework;

namespace BizOneShot.Light.Dao.Repositories
{
    public interface IScMakRepository : IRepository<ScMak>
    {
        Task<ScMak> insert(ScMak scMak);
        Task<IList<ScMak>> GetMaklistByIdAsync(int MakDt);

        IList<ScMak> GetMaklistById(int MakDt);
        ScMak GetMakById(int MakDt);
        Task<int> getMaxYear();
    }

    public class ScMakRepository : RepositoryBase<ScMak>, IScMakRepository
    {
        public ScMakRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public ScMak GetMakById(int MakDt)
        {
            IList<ScMak> list = GetMaklistById(MakDt);
            return list.FirstOrDefault();
        }

        public IList<ScMak> GetMaklistById(int MakDt)
        {
            return DbContext.ScMaks.Where(ci => ci.MakDt == MakDt).ToList();
        }

        public async Task<IList<ScMak>> GetMaklistByIdAsync(int MakDt)
        {
            return await DbContext.ScMaks.Where(ci => ci.MakDt == MakDt).ToListAsync();
        }

        public async Task<int> getMaxYear()
        {
            return await DbContext.ScMaks.MaxAsync(p => p.MakDt);
        }

        public async Task<ScMak> insert(ScMak scMak)
        {
            return await Task.Run(() => DbContext.ScMaks.Add(scMak));
        }
    }
}