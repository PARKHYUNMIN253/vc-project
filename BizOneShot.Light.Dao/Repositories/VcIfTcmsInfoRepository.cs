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
    public interface IVcIfTcmsInfoRepository : IRepository<VcIfTcmsInfo>
    {
        void insert(VcIfTcmsInfo vcIfTcmsInfo);
        Task<VcIfTcmsInfo> inertToAsync(VcIfTcmsInfo vcIfTcmsInfo);
        //getVcIfTcmsInfoByInfId

        Task<VcIfTcmsInfo> getVcIfTcmsInfoByInfId(string infId);
    }


    public class VcIfTcmsInfoRepository : RepositoryBase<VcIfTcmsInfo>, IVcIfTcmsInfoRepository
    {
        public VcIfTcmsInfoRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }


        public void insert(VcIfTcmsInfo vcIfTcmsInfo)
        {
            DbContext.VcIfTcmsInfoes.Add(vcIfTcmsInfo);
        }

        public async Task<VcIfTcmsInfo> inertToAsync(VcIfTcmsInfo vcIfTcmsInfo)
        {
            return await Task.Run(() => DbContext.VcIfTcmsInfoes.Add(vcIfTcmsInfo));
        }

        public async Task<VcIfTcmsInfo> getVcIfTcmsInfoByInfId(string infId)
        {
            return await DbContext.VcIfTcmsInfoes.Where(bw => bw.InfId == infId).SingleOrDefaultAsync();
        }
    }
}
