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
    public interface IScCompanyFinanceService : IBaseService
    {
        Task<ScCompanyFinance> AddScCompanyFinanceAsync(ScCompanyFinance scf);
        Task<ScCompanyFinance> getScCompanyFinanceAsync(int CompSn, int FnYear);
        ScCompanyFinance getScCompanyFinance(int CompSn, int FnYear);
        ScCompanyFinance getScCompanyFinanceIncludeNull(int CompSn, int FnYear);
        void modifyScCompanyFinanceAsync(ScCompanyFinance scf);
        Task<int> getMaxYearCompanyFinanceAsync(int compSn);
    }

    class ScCompanyFinanceService : IScCompanyFinanceService
    {
        private readonly IScCompanyFinanceRepository _scCompanyRepository;
        private readonly IUnitOfWork unitOfWork;

        public ScCompanyFinanceService(IScCompanyFinanceRepository _scCompanyRepository, IUnitOfWork unitOfWork)
        {
            this._scCompanyRepository = _scCompanyRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ScCompanyFinance> AddScCompanyFinanceAsync(ScCompanyFinance scf)
        {
            var rstScCompanyFinance = _scCompanyRepository.Insert(scf);

            if (rstScCompanyFinance != null)
            {
                await SaveDbContextAsync();
            }
            return rstScCompanyFinance;
        }

        public void SaveDbContext()
        {
            unitOfWork.Commit();
        }

        public async Task<int> SaveDbContextAsync()
        {
            return await unitOfWork.CommitAsync();
        }

        public async Task<ScCompanyFinance> getScCompanyFinanceAsync(int CompSn, int FnYear)
        {
            var rst = await _scCompanyRepository.getScCompanyFinanceAsync(scf => scf.CompSn == CompSn && scf.FnYear == FnYear);
            return rst;
        }

        public ScCompanyFinance getScCompanyFinance(int CompSn, int FnYear)
        {
            var rst = _scCompanyRepository.getScCompanyFinance(scf => scf.CompSn == CompSn && scf.FnYear == FnYear);
            return rst;
        }

        public ScCompanyFinance getScCompanyFinanceIncludeNull(int CompSn, int FnYear)
        {
            var rst = _scCompanyRepository.getScCompanyFiananceIncludeNull(scf => scf.CompSn == CompSn && scf.FnYear == FnYear);
            return rst;
        }

        public void modifyScCompanyFinanceAsync(ScCompanyFinance scf)
        {
            _scCompanyRepository.Update(scf);
            SaveDbContext();
        }

        public async Task<int> getMaxYearCompanyFinanceAsync(int compSn)
        {
            return await _scCompanyRepository.getMaxYearCompanyFinanceAsync(compSn);
        }
    }
}
