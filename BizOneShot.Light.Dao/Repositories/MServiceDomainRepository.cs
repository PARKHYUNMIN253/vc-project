using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.WebModels;
using System.Data.Entity;

namespace BizOneShot.Light.Dao.Repositories
{

    public interface IMServiceDomainRepository : IRepository<MServiceDomain>
    {
        Task<MServiceDomain> getDomainService(string domainName);
    }
    public class MServiceDomainRepository : RepositoryBase<MServiceDomain>, IMServiceDomainRepository
    {
        public MServiceDomainRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public async Task<MServiceDomain> getDomainService(string domainName)
        {
            return await DbContext.MServiceDomains.Where(t => t.DomainName == domainName).SingleOrDefaultAsync();
        }
    }
}
