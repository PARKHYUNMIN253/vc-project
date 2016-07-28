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
    public interface IVcIfBaInfoRepository : IRepository<VcIfBaInfo>
    {
        void insert(VcIfBaInfo vcIfBaInfo);
        Task<VcIfBaInfo> insertBa(VcIfBaInfo vcIfBaInfo);
        Task<VcIfBaInfo> getVcIfBaInfoByInfId(string InfId);
    }
    public class VcIfBaInfoRepository : RepositoryBase<VcIfBaInfo>, IVcIfBaInfoRepository
    {
        public VcIfBaInfoRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public async Task<VcIfBaInfo> getVcIfBaInfoByInfId(string InfId)
        {
            return await DbContext.VcIfBaInfoes.Where(bi => bi.InfId == InfId).SingleOrDefaultAsync();
        }

        public void insert(VcIfBaInfo vcIfBaInfo)
        {
            DbContext.VcIfBaInfoes.Add(vcIfBaInfo);
        }

        public async Task<VcIfBaInfo> insertBa(VcIfBaInfo vcIfBaInfo)
        {
            return await Task.Run(() => DbContext.VcIfBaInfoes.Add(vcIfBaInfo));
        }
    }
}
