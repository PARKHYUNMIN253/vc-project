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
        Task<TcmsIfSurvey> getTcmsIfSurveyByInfId(string infId);
        void Insert(TcmsIfSurvey tcmsIfSurvey);

        IList<TcmsIfSurvey> unAsyncGetTcmsIfSurvey();
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

        public async Task<TcmsIfSurvey> getTcmsIfSurveyByInfId(string infId)
        {
            return await tcmsIfSurveyRepository.getTcmsIfSurveyByInfId(infId);
        }

        public void Insert(TcmsIfSurvey tcmsIfSurvey)
        {
            tcmsIfSurveyRepository.Insert(tcmsIfSurvey);
        }

        public IList<TcmsIfSurvey> unAsyncGetTcmsIfSurvey()
        {
            return tcmsIfSurveyRepository.unAsyncGetTcmsIfSurvey();
        }

        public void SaveDbContext()
        {
            unitOfWork.Commit();
        }

        public async Task<int> SaveDbContextAsync()
        {
            return await unitOfWork.CommitAsync();
        }
    }
}
