using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.WebModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizOneShot.Light.Dao.Repositories
{
    public interface IVcNumMngInfoRepository : IRepository<VcNumMngInfo>
    {
        void insert(VcNumMngInfo vcNumMngInfo);
        Task<VcNumMngInfo> inertToAsync(VcNumMngInfo vcNumMngInfo);
        Task<VcNumMngInfo> getNumInfoAsync(string numSn);
        Task<VcNumMngInfo> getNumInfoOneAsync();
    }

    public class VcNumMngInfoRepository : RepositoryBase<VcNumMngInfo>, IVcNumMngInfoRepository
    {
        public VcNumMngInfoRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public void insert(VcNumMngInfo vcNumMngInfo)
        {
            DbContext.VcNumMngInfoes.Add(vcNumMngInfo);
        }

        public async Task<VcNumMngInfo> inertToAsync(VcNumMngInfo vcNumMngInfo)
        {
            return await Task.Run(() => DbContext.VcNumMngInfoes.Add(vcNumMngInfo));
        }
        public async Task<VcNumMngInfo> getNumInfoAsync(string numSn)
        {
            return await DbContext.VcNumMngInfoes.Where(bw => bw.NumSn == numSn).SingleOrDefaultAsync();
        }

        public async Task<VcNumMngInfo> getNumInfoOneAsync()
        {
            return await DbContext.VcNumMngInfoes.OrderBy(obj => obj.BizStDt).Take(1).SingleOrDefaultAsync();
        }
    }
}
