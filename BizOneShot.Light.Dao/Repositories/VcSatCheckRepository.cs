using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.WebModels;
using PagedList;
using PagedList.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BizOneShot.Light.Dao.Repositories
{
    public interface IVcSatCheckRepository : IRepository<VcSatCheck>
    {
        Task<VcSatCheck> getVcSatCheckBySatSN(int satSn);
        VcSatCheck insert(VcSatCheck vcs);
    }

    public class VcSatCheckRepository : RepositoryBase<VcSatCheck>, IVcSatCheckRepository
    {
        public VcSatCheckRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public async Task<VcSatCheck> getVcSatCheckBySatSN(int satSn)
        {
            return await DbContext.VcSatChecks.Where(vc => vc.SatSn == satSn).SingleOrDefaultAsync();
        }

        public VcSatCheck insert(VcSatCheck vcs)
        {
            return DbContext.VcSatChecks.Add(vcs);
        }

    }

}
