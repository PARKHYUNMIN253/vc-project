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
    public interface IVcIfQuesCompInfoRepository : IRepository<VcIfQuesCompInfo>
    {
        void insert(VcIfQuesCompInfo vcIfQuesCompInfo);
        Task<VcIfQuesCompInfo> inertToAsync(VcIfQuesCompInfo vcIfQuesCompInfo);
        Task<VcIfQuesCompInfo> getVcIfQuesCompInfoByInfId(string infId);
    }

    public class VcIfQuesCompInfoRepository : RepositoryBase<VcIfQuesCompInfo>, IVcIfQuesCompInfoRepository
    {
        public VcIfQuesCompInfoRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public void insert(VcIfQuesCompInfo vcIfQuesCompInfo)
        {
            DbContext.VcIfQuesCompInfoes.Add(vcIfQuesCompInfo);
        }
        public async Task<VcIfQuesCompInfo> inertToAsync(VcIfQuesCompInfo vcIfQuesCompInfo)
        {
            return await Task.Run(() => DbContext.VcIfQuesCompInfoes.Add(vcIfQuesCompInfo));
        }

        public async Task<VcIfQuesCompInfo> getVcIfQuesCompInfoByInfId(string infId)
        {
            return await DbContext.VcIfQuesCompInfoes.Where(vc => vc.InfId == infId).SingleOrDefaultAsync();
        }
    }
}
