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
    public interface IScCavRepository : IRepository<ScCav>
    {
        Task<ScCav> insert(ScCav scCav);
        //Task<ScCav> GetCavinfoById(Expression<Func<ScCav, bool>> where);
        Task<IPagedList<ScCav>> GetPagedListCAVAsync(int page, int pageSize, int CavDt);

        //Task<ScCav> GetCavinfoById(Expression<Func<ScCav, bool>> where);
        IList<ScCav> GetcavlistById(int cavDt);
        Task<IList<ScCav>> GetCavListByIdAsync(int cavDt);
        ScCav GetCavById(int cavDt);
        //Task<ScCav> GetCavinfoById(Func<object, bool> p); 
        Task<int> getMaxYear();
        //Task<ScCav> GetcavInfoById(Expression<Func<ScCav, bool>> where);
        Task<IList<ScCav>> GetCavListLastYear(int maxYear);

    }
    public class ScCavRepository : RepositoryBase<ScCav>, IScCavRepository
    {
        public ScCavRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }


        public IList<ScCav> GetcavlistById(int cavDt)
        {
            //var usrInfo = await DbContext.ScCavs.Where(ci => ci.CavDt == cavDt).ToListAsync();
            //return usrInfo;
            return DbContext.ScCavs.Where(ci => ci.CavDt == cavDt).ToList();
        }


        public async Task<IList<ScCav>> GetCavListByIdAsync(int cavDt)
        {
            //var usrInfo = await DbContext.ScCavs.Where(ci => ci.CavDt == cavDt).ToListAsync();
            //return usrInfo;
            return await DbContext.ScCavs.Where(ci => ci.CavDt == cavDt).ToListAsync();
        }

        public ScCav GetCavById(int cavDt)
        {
            //var usrInfo = await DbContext.ScCavs.Where(ci => ci.CavDt == cavDt).ToListAsync();
            //return usrInfo;
            IList<ScCav> list = GetcavlistById(cavDt);
            return list.FirstOrDefault();
        }

        public async Task<IPagedList<ScCav>> GetPagedListCAVAsync(int page, int pageSize, int CavDt)
        {
            return await DbContext.ScCavs.Where(sq => sq.CavDt == CavDt).ToPagedListAsync(page, CavDt);
        }

        public async Task<IList<ScCav>> GetCavListLastYear(int maxYear)
        {

            return await DbContext.ScCavs.OrderByDescending(i => i.CavDt).Where(i => i.CavDt < maxYear).ToListAsync();
        }

        public async Task<int> getMaxYear ()
        {
            return await DbContext.ScCavs.MaxAsync(p => p.CavDt);
        }
    
        public async Task<ScCav> insert(ScCav scCav)
        {
            return await Task.Run(() => DbContext.ScCavs.Add(scCav));
        }

       
    }
}
