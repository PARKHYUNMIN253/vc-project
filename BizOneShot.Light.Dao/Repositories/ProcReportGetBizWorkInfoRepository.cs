using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.WebModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizOneShot.Light.Dao.Repositories
{
    public interface IProcReportGetBizWorkInfoRepository : IRepository<ProcReportGetBizWorkInfoReturnModel>
    {
        Task<IList<ProcReportGetBizWorkInfoReturnModel>> getBizWorkInfoByBizWorkSn(object[] parameters);
    }

    public class ProcReportGetBizWorkInfoRepository : RepositoryBase<ProcReportGetBizWorkInfoReturnModel>, IProcReportGetBizWorkInfoRepository
    {
        public ProcReportGetBizWorkInfoRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public async Task<IList<ProcReportGetBizWorkInfoReturnModel>> getBizWorkInfoByBizWorkSn(object[] parameters)
        {
            return await DbContext.Database.SqlQuery<ProcReportGetBizWorkInfoReturnModel>("proc_reportGetBizWorkInfo @NUM_SN", parameters).ToListAsync();
        }
    }
}
