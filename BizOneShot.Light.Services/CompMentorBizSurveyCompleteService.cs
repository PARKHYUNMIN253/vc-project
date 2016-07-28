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
    public interface ICompMentorBizSurveyCompleteService : IBaseService
    {
        Task<IList<ProcCompMentorBizSurveyCompleteReturnModel>> getCompleteObjById(object[] parameters);
    }

    public class CompMentorBizSurveyCompleteService : ICompMentorBizSurveyCompleteService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ICompMentorBizSurveyCompleteRepository compMentorBizSurveyCompleteRepository;

        // constructor...
        public CompMentorBizSurveyCompleteService(IUnitOfWork unitOfWork, ICompMentorBizSurveyCompleteRepository compMentorBizSurveyCompleteRepository)
        {
            this.unitOfWork = unitOfWork;
            this.compMentorBizSurveyCompleteRepository = compMentorBizSurveyCompleteRepository;
        }

        // methods...
        public async Task<IList<ProcCompMentorBizSurveyCompleteReturnModel>> getCompleteObjById(object[] parameters)
        {
            return await compMentorBizSurveyCompleteRepository.getCompleteObjById(parameters);
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
