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
    public interface IProcMentorGetMentoringReportRepository : IRepository<ProcMentorGetMentoringReportReturnModel>
    {
        Task<IList<ProcMentorGetMentoringReportReturnModel>> getMentoring(object[] parameters);
    }

    public class ProcMentorGetMentoringReportRepository : RepositoryBase<ProcMentorGetMentoringReportReturnModel>, IProcMentorGetMentoringReportRepository
    {
        public ProcMentorGetMentoringReportRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public async Task<IList<ProcMentorGetMentoringReportReturnModel>> getMentoring(object[] parameters)
        {
            return await DbContext.Database.SqlQuery<ProcMentorGetMentoringReportReturnModel>("proc_mentorGetMentoringReport @LOGIN_ID, @COMP_SN", parameters).ToListAsync();
        }
    }
}