using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.WebModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizOneShot.Light.Dao.Repositories
{
    public interface IProcMentorGetDeepenReportRepository : IRepository<ProcMentorGetDeepenReportReturnModel>
    {
        Task<IList<ProcMentorGetDeepenReportReturnModel>> getDeepenReport(object[] parameters);
    }

    public class ProcMentorGetDeepenReportRepository : RepositoryBase<ProcMentorGetDeepenReportReturnModel>, IProcMentorGetDeepenReportRepository
    {
        public ProcMentorGetDeepenReportRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }


        public async Task<IList<ProcMentorGetDeepenReportReturnModel>> getDeepenReport(object[] parameters)
        {
            return await DbContext.Database.SqlQuery<ProcMentorGetDeepenReportReturnModel>("proc_mentorGetDeepenReport @LOGIN_ID, @COMP_SN", parameters).ToListAsync();
        }
    }
}
