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
    public interface ITcmsIfSurveyService : IBaseService
    {
        Task<IList<TcmsIfSurvey>> getTcmsIfSurvey();
    }


    public class TcmsIfSurveyService : ITcmsIfSurveyService
    {
        private readonly ITcmsIfSurveyRepository tcmsIfSurveyRepository;
        private readonly IUnitOfWork unitOfWork;

        public TcmsIfSurveyService(ITcmsIfSurveyRepository tcmsIfSurveyRepository, IUnitOfWork unitOfWork)
        {
            this.tcmsIfSurveyRepository = tcmsIfSurveyRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IList<TcmsIfSurvey>> getTcmsIfSurvey()
        {
            return await tcmsIfSurveyRepository.getTcmsIfSurvey();
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
