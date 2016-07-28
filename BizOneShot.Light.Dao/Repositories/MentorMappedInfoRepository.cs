using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.WebModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizOneShot.Light.Dao.Repositories
{
    public interface IMentorMappedInfoRepository : IRepository<ProcMentorMappedInfoReturnModel>
    {
        Task<ProcMentorMappedInfoReturnModel> getMentorMappedInfo(object[] parameters);
    }

    public class MentorMappedInfoRepository : RepositoryBase<ProcMentorMappedInfoReturnModel>, IMentorMappedInfoRepository
    {
        public MentorMappedInfoRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public async Task<ProcMentorMappedInfoReturnModel> getMentorMappedInfo(object[] parameters)
        {
            return await
                DbContext.Database.SqlQuery<ProcMentorMappedInfoReturnModel>("proc_mentorMappedInfo @COMP_SN, @BA_SN", parameters).SingleOrDefaultAsync();
        }
    }
}
