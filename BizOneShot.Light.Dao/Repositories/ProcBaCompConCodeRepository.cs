using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.WebModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizOneShot.Light.Dao.Repositories
{
    public interface IProcBaCompConCodeRepository : IRepository<ProcBaCompConCodeReturnModel>
    {
        Task<IList<ProcBaCompConCodeReturnModel>> getBaCompConcode(object[] parameters);
    }

    public class ProcBaCompConCodeRepository : RepositoryBase<ProcBaCompConCodeReturnModel>, IProcBaCompConCodeRepository
    {
        public ProcBaCompConCodeRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }


        public async Task<IList<ProcBaCompConCodeReturnModel>> getBaCompConcode(object[] parameters)
        {
            return await DbContext.Database.SqlQuery<ProcBaCompConCodeReturnModel>("proc_baCompConCode @BA_SN, @COMP_SN", parameters).ToListAsync();
        }

    }
}
