using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.WebModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizOneShot.Light.Dao.Repositories
{
    public interface IVcIfCompMappingRepository : IRepository<VcIfCompMapping>
    {
        void insert(VcIfCompMapping vcIfCompMapping);
        Task<VcIfCompMapping> inertToAsync(VcIfCompMapping vcIfCompMapping);
        //getVcIfCompMappingByInfId
        Task<VcIfCompMapping> getVcIfCompMappingByInfId(string infId);
    }

    public class VcIfCompMappingRepository : RepositoryBase<VcIfCompMapping>, IVcIfCompMappingRepository
    {
        public VcIfCompMappingRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public void insert(VcIfCompMapping vcIfCompMapping)
        {
            DbContext.VcIfCompMappings.Add(vcIfCompMapping);
        }
        public async Task<VcIfCompMapping> inertToAsync(VcIfCompMapping vcIfCompMapping)
        {
            return await Task.Run(() => DbContext.VcIfCompMappings.Add(vcIfCompMapping));
        }

        public async Task<VcIfCompMapping> getVcIfCompMappingByInfId(string infId)
        {
            return await DbContext.VcIfCompMappings.Where(bw => bw.InfId == infId).SingleOrDefaultAsync();
        }
    }
}
