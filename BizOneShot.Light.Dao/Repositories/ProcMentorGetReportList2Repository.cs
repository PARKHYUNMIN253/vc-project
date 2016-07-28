using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.WebModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizOneShot.Light.Dao.Repositories
{
    public interface IProcMentorGetReportList2Repository : IRepository<ProcMentorGetReportList2ReturnModel>
    {
        Task<IList<ProcMentorGetReportList2ReturnModel>> mentorGetReportList2(object[] parameters);
    }

    public class ProcMentorGetReportList2Repository : RepositoryBase<ProcMentorGetReportList2ReturnModel>, IProcMentorGetReportList2Repository
    {
        public ProcMentorGetReportList2Repository(IDbFactory dbFactory) : base(dbFactory)
        {

        }


        public async Task<IList<ProcMentorGetReportList2ReturnModel>> mentorGetReportList2(object[] parameters)
        {
            return await DbContext.Database.SqlQuery<ProcMentorGetReportList2ReturnModel>("proc_mentorgetreportList2 @TCMS_LOGIN_KEY", parameters).ToListAsync();
        }

    }
}
