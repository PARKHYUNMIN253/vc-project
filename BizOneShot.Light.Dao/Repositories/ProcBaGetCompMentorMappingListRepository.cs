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
    public interface IProcBaGetCompMentorMappingListRepository : IRepository<ProcBaGetCompMentorMappingListReturnModel>
    {
        Task<IList<ProcBaGetCompMentorMappingListReturnModel>> baGetCompMentorMappingList(object[] parameters);
    }

    public class ProcBaGetCompMentorMappingListRepository : RepositoryBase<ProcBaGetCompMentorMappingListReturnModel>, IProcBaGetCompMentorMappingListRepository
    {
        public ProcBaGetCompMentorMappingListRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public async Task<IList<ProcBaGetCompMentorMappingListReturnModel>> baGetCompMentorMappingList(object[] parameters)
        {
            return await DbContext.Database.SqlQuery<ProcBaGetCompMentorMappingListReturnModel>("proc_baGetCompMentorMappingList @BA_SN", parameters).ToListAsync();
        }
    }
}
