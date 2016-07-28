using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Dao.Repositories;
using BizOneShot.Light.Models.WebModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizOneShot.Light.Services
{
    public interface ICompBaMngService : IBaseService
    {
        Task<IList<ProcCompMentorMappingReturnModel>> getCompMentorMapping(object[] parameters);
        Task<ProcMentorMappedInfoReturnModel> getMentorMappedInfo(object[] parameters);
    }

    public class CompBaMngService : ICompBaMngService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICompBaMngRepository compBaMngRepository;
        private readonly IMentorMappedInfoRepository mentorMappedInfoRepository;

        // constructor...
        public CompBaMngService(IUnitOfWork unitOfWork, ICompBaMngRepository compBaMngRepository, IMentorMappedInfoRepository mentorMappedInfoRepository)
        {
            this.unitOfWork = unitOfWork;
            this.compBaMngRepository = compBaMngRepository;
            this.mentorMappedInfoRepository = mentorMappedInfoRepository;
        }

        // methods...
        public async Task<IList<ProcCompMentorMappingReturnModel>> getCompMentorMapping(object[] parameters)
        {
            return await compBaMngRepository.getCompMentorMapping(parameters);
        }

        public async Task<ProcMentorMappedInfoReturnModel> getMentorMappedInfo(object[] parameters)
        {
            return await mentorMappedInfoRepository.getMentorMappedInfo(parameters);
        }


        // save methods...
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
