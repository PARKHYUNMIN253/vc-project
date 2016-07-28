using BizOneShot.Light.Models.WebModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BizOneShot.Light.Dao.Repositories;
using BizOneShot.Light.Dao.Infrastructure;

namespace BizOneShot.Light.Services
{
    public interface IMServiceDomainService : IBaseService
    {
        Task<MServiceDomain> getServiceByDomainName(string domainName);
    }

    public class MServiceDomainService : IMServiceDomainService
    {
        private readonly IMServiceDomainRepository mServiceDomainRepository;
        private readonly IUnitOfWork unitOfWork;

        public MServiceDomainService(IMServiceDomainRepository mServiceDomainRepository, IUnitOfWork unitOfWork)
        {
            this.mServiceDomainRepository = mServiceDomainRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<MServiceDomain> getServiceByDomainName(string domainName)
        {

            return await mServiceDomainRepository.getDomainService(domainName);
        }

        public void SaveDbContext()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveDbContextAsync()
        {
            throw new NotImplementedException();
        }
    }
}
