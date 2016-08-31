using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.WebModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BizOneShot.Light.Dao.Repositories
{
    public interface IVcBaInfoRepository : IRepository<VcBaInfo>
    {
        Task<VcBaInfo> getVcBaInfoById(Expression<Func<VcBaInfo, bool>> where);
        Task<VcBaInfo> getVcBaInfoByBaSn(Expression<Func<VcBaInfo, bool>> where);
        VcBaInfo Insert(VcBaInfo vcBaInfo);
        Task<VcBaInfo> insertToAsync(VcBaInfo vcBainfo);
        // ADD LOY 0627
        Task<IList<VcBaInfo>> getVcBaInfoByLoginKey(Expression<Func<VcBaInfo, bool>> where);
    }

    public class VcBaInfoRepository : RepositoryBase<VcBaInfo>, IVcBaInfoRepository
    {
        public VcBaInfoRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public async Task<VcBaInfo> getVcBaInfoById(Expression<Func<VcBaInfo, bool>> where)
        {
            return await DbContext.VcBaInfoes.Where(where).SingleOrDefaultAsync();
        }

        public VcBaInfo Insert(VcBaInfo vcBaInfo)
        {
            return DbContext.VcBaInfoes.Add(vcBaInfo);
        }

        public async Task<IList<VcBaInfo>> getVcBaInfoByLoginKey(Expression<Func<VcBaInfo, bool>> where)
        {
            return await DbContext.VcBaInfoes.Where(where).ToListAsync();
        }

        public async Task<VcBaInfo> insertToAsync(VcBaInfo vcBainfo)
        {
            return await Task.Run(() => DbContext.VcBaInfoes.Add(vcBainfo));
        }

        public async Task<VcBaInfo> getVcBaInfoByBaSn(Expression<Func<VcBaInfo, bool>> where)
        {
            return await DbContext.VcBaInfoes.Where(where).SingleOrDefaultAsync();
        }
    }
}
