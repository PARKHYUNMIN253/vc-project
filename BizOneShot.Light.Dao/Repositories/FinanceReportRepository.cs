using System.Collections.Generic;
using System.Threading.Tasks;
using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.DareModels;

namespace BizOneShot.Light.Dao.Repositories
{
    // dare 프로시져 호출
    public interface IFinanceReportRepository : IRepository<SHUSER_SboMonthlySalesSelectReturnModel>
    {
        /*
        자체적으로 만든 프로시져
        so 분석이 필요
        일반 재무정보 관련된 부분 

        다래안에도 우리가 만든 프로시져가 있다.
        */

        //현금시재조회
        Task<IList<SHUSER_SboBosLiteMonthlyCashReturnModel>> GetMonthlyCashListAsync(object[] parameters);
        //매출조회 당월, 전월
        Task<IList<SHUSER_SboMonthlySalesSelectReturnModel>> GetMonthlySalesAsync(object[] parameters);
        //매출조회 년 누적
        Task<SHUSER_SboMonthlyYearSalesSelectReturnModel> GetYearTotalSalesAsync(object[] parameters);
        //이익분석
        Task<IList<SHUSER_SboMonthlyCostAnalysisSelectReturnModel>> GetCostAnalysisAsync(object[] parameters);

        //비용분석
        Task<IList<SHUSER_SboMonthlyExpenseCostSelectReturnModel>> GetExpenseCostAsync(object[] parameters);

        //주요매출
        Task<IList<SHUSER_SboMonthlyTaxSalesSelectReturnModel>> GetTaxSalesAsync(object[] parameters);
        //주요지출
        Task<IList<SHUSER_SboMonthlyBankOutSelectReturnModel>> GetBankOutAsync(object[] parameters);

        Task<SHUSER_SboFinancialTab1SalesSelectReturnModel> GetCompanyMonthSalesAsync(object[] parameters);
        Task<SHUSER_SboFinancialTab2SalesSelectReturnModel> GetCompanyQuarterSalesAsync(object[] parameters);
        Task<SHUSER_SboFinancialTab3SalesSelectReturnModel> GetCompanyYearSalesAsync(object[] parameters);
    }


    public class FinanceReportRepository : DareRepositoryBase<SHUSER_SboMonthlySalesSelectReturnModel>,
        IFinanceReportRepository
    {
        public FinanceReportRepository(IDareDbFactory dbFactory) : base(dbFactory)
        {
        }

        public async Task<IList<SHUSER_SboBosLiteMonthlyCashReturnModel>> GetMonthlyCashListAsync(object[] parameters)
        {
            return
                await
                    DareDbContext.Database.SqlQuery<SHUSER_SboBosLiteMonthlyCashReturnModel>(
                        "SBO_BOS_LITE_MONTHLY_CASH @MEMB_BUSNPERS_NO, @CORP_CODE, @BIZ_CD, @SET_YEAR, @SET_MONTH ",
                        parameters).ToListAsync();
        }

        public async Task<IList<SHUSER_SboMonthlySalesSelectReturnModel>> GetMonthlySalesAsync(object[] parameters)
        {
            return
                await
                    DareDbContext.Database.SqlQuery<SHUSER_SboMonthlySalesSelectReturnModel>(
                        "SBO_MONTHLY_SALES_SELECT @MEMB_BUSNPERS_NO, @CORP_CODE, @BIZ_CD, @SET_YEAR, @SET_MONTH ",
                        parameters).ToListAsync();
        }

        public async Task<SHUSER_SboMonthlyYearSalesSelectReturnModel> GetYearTotalSalesAsync(object[] parameters)
        {
            return
                await
                    DareDbContext.Database.SqlQuery<SHUSER_SboMonthlyYearSalesSelectReturnModel>(
                        "SBO_YEAR_SALES_SELECT @MEMB_BUSNPERS_NO, @CORP_CODE, @BIZ_CD, @SET_YEAR, @SET_MONTH ",
                        parameters).SingleOrDefaultAsync();
        }

        public async Task<IList<SHUSER_SboMonthlyCostAnalysisSelectReturnModel>> GetCostAnalysisAsync(
            object[] parameters)
        {
            return
                await
                    DareDbContext.Database.SqlQuery<SHUSER_SboMonthlyCostAnalysisSelectReturnModel>(
                        "SBO_MONTHLY_COST_ANALYSIS_SELECT @MEMB_BUSNPERS_NO, @CORP_CODE, @BIZ_CD, @SET_YEAR, @SET_MONTH ",
                        parameters).ToListAsync();
        }

        public async Task<IList<SHUSER_SboMonthlyExpenseCostSelectReturnModel>> GetExpenseCostAsync(object[] parameters)
        {
            return
                await
                    DareDbContext.Database.SqlQuery<SHUSER_SboMonthlyExpenseCostSelectReturnModel>(
                        "SBO_MONTHLY_EXPENSE_COST_SELECT @MEMB_BUSNPERS_NO, @CORP_CODE, @BIZ_CD, @SET_YEAR, @SET_MONTH ",
                        parameters).ToListAsync();
        }

        public async Task<IList<SHUSER_SboMonthlyTaxSalesSelectReturnModel>> GetTaxSalesAsync(object[] parameters)
        {
            return
                await
                    DareDbContext.Database.SqlQuery<SHUSER_SboMonthlyTaxSalesSelectReturnModel>(
                        "SBO_MONTHLY_TAX_SALES_SELECT @MEMB_BUSNPERS_NO, @CORP_CODE, @BIZ_CD, @SET_YEAR, @SET_MONTH ",
                        parameters).ToListAsync();
        }

        public async Task<IList<SHUSER_SboMonthlyBankOutSelectReturnModel>> GetBankOutAsync(object[] parameters)
        {
            return
                await
                    DareDbContext.Database.SqlQuery<SHUSER_SboMonthlyBankOutSelectReturnModel>(
                        "SBO_MONTHLY_BANK_OUT_SELECT @MEMB_BUSNPERS_NO, @CORP_CODE, @BIZ_CD, @SET_YEAR, @SET_MONTH ",
                        parameters).ToListAsync();
        }

        // 이하 3개는 dare에서 마든 프로시져
        // 월간, 분기, 연간 재무 보고서에 대한 정보는 dare에서 주기때문에
        // dare에서 프로시저를 만들었다.
        // 이 위로 7개를 WEB에서 사용 -> [기업에서 보고서] 재무관리 보고서 부분
        public async Task<SHUSER_SboFinancialTab1SalesSelectReturnModel> GetCompanyMonthSalesAsync(object[] parameters)
        { 
            return await DareDbContext.Database.SqlQuery<SHUSER_SboFinancialTab1SalesSelectReturnModel>("SBO_FINANCIAL_TAB1_SALES_SELECT @MEMB_BUSNPERS_NO, @CORP_CODE, @BIZ_CD, @FR_YM, @TO_YM, @BASE_DT", parameters).SingleOrDefaultAsync();
        }

        public async Task<SHUSER_SboFinancialTab2SalesSelectReturnModel> GetCompanyQuarterSalesAsync(object[] parameters)
        {
            return await DareDbContext.Database.SqlQuery<SHUSER_SboFinancialTab2SalesSelectReturnModel>("SBO_FINANCIAL_TAB2_SALES_SELECT @MEMB_BUSNPERS_NO, @CORP_CODE, @BIZ_CD, @FR_YEAR, @FR_QT, @TO_YEAR, @TO_QT, @BASE_DT", parameters).SingleOrDefaultAsync();
        }

        public async Task<SHUSER_SboFinancialTab3SalesSelectReturnModel> GetCompanyYearSalesAsync(object[] parameters)
        {
            return await DareDbContext.Database.SqlQuery<SHUSER_SboFinancialTab3SalesSelectReturnModel>("SBO_FINANCIAL_TAB3_SALES_SELECT @MEMB_BUSNPERS_NO, @CORP_CODE, @BIZ_CD, @FR_YEAR, @TO_YEAR, @BASE_DT", parameters).SingleOrDefaultAsync();
        }
    }
}