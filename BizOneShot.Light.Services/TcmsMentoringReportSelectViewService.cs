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
    public interface ITcmsMentoringReportSelectViewService : IBaseService
    {
        Task<IList<TcmsMentoringReportSelectView>> getMentoringReportInfoes();
        Task<IList<TcmsMentoringReportSelectView>> GetListViewsAsync(string searchType = null, string keyword = null);
        Task<TcmsMentoringReportSelectView> GetCompNmByReportSn(int reportSn);
    }

    public class TcmsMentoringReportSelectViewService : ITcmsMentoringReportSelectViewService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ITcmsMentoringReportSelectViewRepository tcmsMentoringReportSelectViewRepository;

        public TcmsMentoringReportSelectViewService(ITcmsMentoringReportSelectViewRepository tcmsMentoringReportSelectViewRepository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.tcmsMentoringReportSelectViewRepository = tcmsMentoringReportSelectViewRepository;
        }

        public void SaveDbContext()
        {
            unitOfWork.CommitAsync();
        }

        public async Task<int> SaveDbContextAsync()
        {
            return await unitOfWork.CommitAsync();
        }

        public async Task<IList<TcmsMentoringReportSelectView>> getMentoringReportInfoes()
        {
            return await tcmsMentoringReportSelectViewRepository.getMentoringReportInfoes();
        }

        public async Task<IList<TcmsMentoringReportSelectView>> GetListViewsAsync(string searchType = null, string keyword = null)
        {
            if (string.IsNullOrEmpty(searchType) || string.IsNullOrEmpty(keyword))
            {
                return await tcmsMentoringReportSelectViewRepository.getMentoringReportInfoes();
            }
            if (searchType.Equals("0")) // keyword가 포함된 기업명 검색 
            {
                return
                    await
                        tcmsMentoringReportSelectViewRepository.getSearchQuery(
                            cm => cm.CompNm.Contains(keyword));
            }
            if (searchType.Equals("1")) // keyword가 포함된 BA명 검색 
            {
                return
                    await
                        tcmsMentoringReportSelectViewRepository.getSearchQuery(
                            bm => bm.BaNm.Contains(keyword));
            }
            if (searchType.Equals("2")) // keyword가 포함된 멘토명 검색 
            {
                return
                    await
                        tcmsMentoringReportSelectViewRepository.getSearchQuery(
                            bm => bm.MentorName.Contains(keyword));
            }
            return await tcmsMentoringReportSelectViewRepository.getMentoringReportInfoes();
        }

        public async Task<TcmsMentoringReportSelectView> GetCompNmByReportSn(int reportSn)
        {
            return await tcmsMentoringReportSelectViewRepository.GetCompNmByReportSn(reportSn);
        }
    }
}
