using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.WebModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizOneShot.Light.Dao.Repositories
{
    public interface ICompMentorBizSurveyCompleteRepository : IRepository<ProcCompMentorBizSurveyCompleteReturnModel>
    {
        Task<IList<ProcCompMentorBizSurveyCompleteReturnModel>> getCompleteObjById(object[] parameters);
    }

    public class CompMentorBizSurveyCompleteRepository : RepositoryBase<ProcCompMentorBizSurveyCompleteReturnModel>, ICompMentorBizSurveyCompleteRepository
    {
        // constructor...
        public CompMentorBizSurveyCompleteRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        // methods...
        public async Task<IList<ProcCompMentorBizSurveyCompleteReturnModel>> getCompleteObjById(object[] parameters)
        {
            return await DbContext.Database.SqlQuery<ProcCompMentorBizSurveyCompleteReturnModel>("proc_compMentorBizSurveyComplete @LOGIN_ID", parameters).ToListAsync();
        }


    }
}
