using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.WebModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizOneShot.Light.Dao.Repositories
{
    public interface IProcMentorRegDeepenReportRepository : IRepository<ProcMentorRegDeepenReportReturnModel>
    {
        Task<ProcMentorRegDeepenReportReturnModel> getMentorRegDeepenReport(object[] parameters);
    }

    public class ProcMentorRegDeepenReportRepository : RepositoryBase<ProcMentorRegDeepenReportReturnModel>, IProcMentorRegDeepenReportRepository
    {
        public ProcMentorRegDeepenReportRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }


        public async Task<ProcMentorRegDeepenReportReturnModel> getMentorRegDeepenReport(object[] parameters)
        {
            return await DbContext.Database.SqlQuery<ProcMentorRegDeepenReportReturnModel>("proc_mentorRegDeepenReport @TCMS_LOGIN_KEY, @COMP_SN, @CON_CODE", parameters).SingleOrDefaultAsync();
        }
    }
}
