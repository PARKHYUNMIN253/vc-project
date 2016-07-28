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
    public interface IVcCompInfoRepository : IRepository<VcCompInfo>
    {
        Task<VcCompInfo> getVcCompInfoById(Expression<Func<VcCompInfo, bool>> where);
        Task<VcCompInfo> GetCompInfoAsync(Expression<Func<VcCompInfo, bool>> where);
        Task<VcCompInfo> getVcCompInfoByCompSn(Expression<Func<VcCompInfo, bool>> where);
        VcCompInfo Insert(VcCompInfo vcCompInfo);
        Task<VcCompInfo> getCompInfo(string RegistrationNo);
        Task<VcCompInfo> insertToAsync(VcCompInfo vcCompInfo);

        // add loy 0627
        Task<IList<VcCompInfo>> getVcCompInfoByLoginKey(Expression<Func<VcCompInfo, bool>> where);
    }




    public class VcCompInfoRepository : RepositoryBase<VcCompInfo>, IVcCompInfoRepository
    {
        public VcCompInfoRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public async Task<VcCompInfo> getCompInfo(string RegistrationNo)
        {
            return await DbContext.VcCompInfoes.Where(vc => vc.RegistrationNo == RegistrationNo).SingleOrDefaultAsync();
        }

        public async Task<VcCompInfo> GetCompInfoAsync(Expression<Func<VcCompInfo, bool>> where)
        {
            return await DbContext.VcCompInfoes.Where(where).SingleAsync();
        }

        public async Task<VcCompInfo> getVcCompInfoByCompSn(Expression<Func<VcCompInfo, bool>> where)
        {
            return await DbContext.VcCompInfoes.Where(where).SingleAsync();
        }

        public async Task<VcCompInfo> getVcCompInfoById(Expression<Func<VcCompInfo, bool>> where)
        {
            return await DbContext.VcCompInfoes.Where(where).SingleOrDefaultAsync();
        }

        public VcCompInfo Insert(VcCompInfo vcCompInfo)
        {
            return DbContext.VcCompInfoes.Add(vcCompInfo);
        }

        public async Task<VcCompInfo> insertToAsync(VcCompInfo vcCompInfo)
        {
            return await Task.Run(() => DbContext.VcCompInfoes.Add(vcCompInfo));
        }

        public async Task<IList<VcCompInfo>> getVcCompInfoByLoginKey(Expression<Func<VcCompInfo, bool>> where)
        {
            return await DbContext.VcCompInfoes.Where(where).ToListAsync();
        }

    }
}
