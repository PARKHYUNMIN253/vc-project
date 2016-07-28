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
    public interface IVcTcmsInfoRepository : IRepository<VcTcmsInfo>
    {
        void insert(VcTcmsInfo vcTcmsInfo);
        Task<VcTcmsInfo> inertToAsync(VcTcmsInfo vcTcmsInfo);
        Task<VcTcmsInfo> getTcmsInfo(int tcmsLoginKey);
    }

    public class VcTcmsInfoRepository : RepositoryBase<VcTcmsInfo>, IVcTcmsInfoRepository
    {
        public VcTcmsInfoRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }
        public void insert(VcTcmsInfo vcTcmsInfo)
        {
            DbContext.VcTcmsInfoes.Add(vcTcmsInfo);
        }

        public async Task<VcTcmsInfo> inertToAsync(VcTcmsInfo vcTcmsInfo)
        {
            return await Task.Run(() => DbContext.VcTcmsInfoes.Add(vcTcmsInfo));
        }

        public async Task<VcTcmsInfo> getTcmsInfo(int tcmsLoginKey)
        {
            var loginId = Convert.ToString(tcmsLoginKey);
            return await DbContext.VcTcmsInfoes.Where(bw => bw.TcmsLoginKey == tcmsLoginKey).SingleOrDefaultAsync();
        }
            
    }
}
