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
    public interface ITcmsIfLastReportService : IBaseService
    {
        Task<IList<TcmsIfLastReport>> getTcmsIfSurvey();
        Task<TcmsIfLastReport> getTcmsIfSurveyByInfId(string infId);
        void Insert(TcmsIfLastReport tcmsIfLastReport);

        Task<IList<TcmsIfLastReport>> getTcmsIfLastReportInfo(int compKey, int baKey, int mentorKey, string conCode);

        // 동기식으로 TcmsIfLastReport 부분 모두 가져오는 부분
        IList<TcmsIfLastReport> unAsyncGetTcmsIfLastReportInfo();

        IList<TcmsIfLastReport> getResendObj(int compKey, int baKey, int mentorKey, string numSn, string subNumSn, string conCode);
    }


    public class TcmsIfLastReportService : ITcmsIfLastReportService
    {
        private readonly ITcmsIfLastReportRepository tcmsIfLastReportRepository;
        private readonly IUnitOfWork unitOfWork;

        public TcmsIfLastReportService(ITcmsIfLastReportRepository tcmsIfLastReportRepository, IUnitOfWork unitOfWork)
        {
            this.tcmsIfLastReportRepository = tcmsIfLastReportRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<IList<TcmsIfLastReport>> getTcmsIfSurvey()
        {
            return await tcmsIfLastReportRepository.getTcmsIfSurvey();
        }

        public async Task<TcmsIfLastReport> getTcmsIfSurveyByInfId(string infId)
        {
            return await tcmsIfLastReportRepository.getTcmsIfSurveyByInfId(infId);
        }

        public void Insert(TcmsIfLastReport tcmsIfLastReport)
        {
            tcmsIfLastReportRepository.Insert(tcmsIfLastReport);
        }

        public async Task<IList<TcmsIfLastReport>> getTcmsIfLastReportInfo(int compKey, int baKey, int mentorKey, string conCode)
        {

            return await tcmsIfLastReportRepository.getTcmsIfLastReportInfo(compKey, baKey, mentorKey, conCode);

        }

        public IList<TcmsIfLastReport> unAsyncGetTcmsIfLastReportInfo()
        {

            var listTcmsIfLastReport = tcmsIfLastReportRepository.unAsyncGetTcmsIfLastReportInfo(bw => bw.InfId != null);

            return listTcmsIfLastReport.OrderByDescending(bw => bw.InfId).ToList();

        }

        public IList<TcmsIfLastReport> getResendObj(int compKey, int baKey, int mentorKey, string numSn, string subNumSn, string conCode)
        {
            var resendObj = tcmsIfLastReportRepository.getResendObj(bw => bw.CompLoginKey == compKey 
            && bw.BaLoginKey == baKey 
            && bw.MentorLoginKey == mentorKey 
            && bw.NumSn == numSn 
            && bw.SubNumSn == subNumSn 
            && bw.ConCode == conCode);

            return resendObj.OrderByDescending(bw => bw.InfId).ToList();
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
