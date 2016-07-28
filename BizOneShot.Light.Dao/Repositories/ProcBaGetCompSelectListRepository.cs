using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.WebModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizOneShot.Light.Dao.Repositories
{
    public interface IProcBaGetCompSelectListRepository : IRepository<ProcBaGetCompSelectListReturnModel>
    {
        Task<IList<ProcBaGetCompSelectListReturnModel>> baGetCompSelectList(object[] parameters);
    }

    public class ProcBaGetCompSelectListRepository : RepositoryBase<ProcBaGetCompSelectListReturnModel>, IProcBaGetCompSelectListRepository
    {
        public ProcBaGetCompSelectListRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }


        public async Task<IList<ProcBaGetCompSelectListReturnModel>> baGetCompSelectList(object[] parameters)
        {
            return await DbContext.Database.SqlQuery<ProcBaGetCompSelectListReturnModel>("proc_baGetCompSelectList @TCMS_LOGIN_KEY", parameters).ToListAsync();
        }

    }
}
