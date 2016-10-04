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
    }
}
