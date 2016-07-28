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
    public interface IScFinancialIndexTService : IBaseService
    {
        Task<ScFinancialIndexT> AddScFinancialIndexTAsync(ScFinancialIndexT sit);
        Task<ScFinancialIndexT> getScFinancialIndexTAsync(int CompSn, string Year);
        Task<ScFinancialIndexT> getScFinancialIndexTCheckAsync(int CompSn, string Year);
    }


    public class ScFinancialIndexTService : IScFinancialIndexTService
    {
        private readonly IScFinancialIndexTRepository _scFinancialIndexRepository;
        private readonly IUnitOfWork unitOfWork;

        public ScFinancialIndexTService(IScFinancialIndexTRepository _scFinancialIndexRepository, IUnitOfWork unitOfWork)
        {
            this._scFinancialIndexRepository = _scFinancialIndexRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ScFinancialIndexT> AddScFinancialIndexTAsync(ScFinancialIndexT sit)
        {
            var rstFinancialIndexT = _scFinancialIndexRepository.Insert(sit);

            if (rstFinancialIndexT != null)
            {
                await SaveDbContextAsync();
            }
            return rstFinancialIndexT;
        }

        public async Task<ScFinancialIndexT> getScFinancialIndexTCheckAsync(int CompSn, string Year)        // 특정 년도의 ScFinancialIndexT만을 찾을 때
        {
            var rst = await _scFinancialIndexRepository.getScFinancialIndexTAsync(sit => sit.CompSn == CompSn && sit.Year == Year);
            return rst;
        }

        public async Task<ScFinancialIndexT> getScFinancialIndexTAsync(int CompSn, string Year)         // 해당하는 재무정보중 가장 최근의 재무정보를 가져올 때
        {
            var tYear = await getLatestYearUnderTarget(CompSn, Int32.Parse(Year));
            var rst = await _scFinancialIndexRepository.getScFinancialIndexTAsync(sit => sit.CompSn == CompSn && sit.Year == tYear.ToString());
            return rst;
        }

        public void SaveDbContext()
        {
            unitOfWork.Commit();
        }

        public async Task<int> getLatestYearUnderTarget(int CompSn, int Year)
        {
            int ret = await _scFinancialIndexRepository.getLatestYearUnderTarget(CompSn, Year);
            return ret;
        }

        public async Task<int> SaveDbContextAsync()
        {
            return await unitOfWork.CommitAsync();
        }

    }
}
