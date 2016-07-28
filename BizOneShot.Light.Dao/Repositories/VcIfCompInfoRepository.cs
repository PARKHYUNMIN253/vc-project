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
    public interface IVcIfCompInfoRepository : IRepository<VcIfCompInfo>
    {
        void insert(VcIfCompInfo vcIfCompInfo);
        Task<VcIfCompInfo> insertComp(VcIfCompInfo vcIfCompInfo);
        Task<VcIfCompInfo> getVcIfCompInfoByInfId(string infId);
    }


    public class VcIfCompInfoRepository : RepositoryBase<VcIfCompInfo>, IVcIfCompInfoRepository
    {
        public VcIfCompInfoRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public async Task<VcIfCompInfo> getVcIfCompInfoByInfId(string infId)
        {
            return await DbContext.VcIfCompInfoes.Where(ci => ci.InfId == infId).SingleOrDefaultAsync();
        }

        public void insert(VcIfCompInfo vcIfCompInfo)
        {
            DbContext.VcIfCompInfoes.Add(vcIfCompInfo);
        }

        public async Task<VcIfCompInfo> insertComp(VcIfCompInfo vcIfCompInfo)
        {
            return await Task.Run(() => DbContext.VcIfCompInfoes.Add(vcIfCompInfo));
        }
    }
}
