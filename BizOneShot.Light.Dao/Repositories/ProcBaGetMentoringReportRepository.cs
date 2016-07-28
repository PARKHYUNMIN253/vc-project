using BizOneShot.Light.Models.WebModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BizOneShot.Light.Dao.Infrastructure;

namespace BizOneShot.Light.Dao.Repositories
{
    public interface IProcBaGetMentoringReportRepository : IRepository<ProcBaGetMentoringReportReturnModel>
    {
        Task<IList<ProcBaGetMentoringReportReturnModel>> getBaMentoringReport(object[] parameters);
    }

    public class ProcBaGetMentoringReportRepository : RepositoryBase<ProcBaGetMentoringReportReturnModel>, IProcBaGetMentoringReportRepository
    {
        public ProcBaGetMentoringReportRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public async Task<IList<ProcBaGetMentoringReportReturnModel>> getBaMentoringReport(object[] parameters)
        {
            return await DbContext.Database.SqlQuery<ProcBaGetMentoringReportReturnModel>("proc_baGetMentoringReport @LOGIN_ID", parameters).ToListAsync();
        }
    }
}
