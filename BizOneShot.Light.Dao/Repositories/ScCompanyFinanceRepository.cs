using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.WebModels;
using System.Linq;

namespace BizOneShot.Light.Dao.Repositories
{
    // 1.29 오후8시 추가
    public interface IScCompanyFinanceRepository : IRepository<ScCompanyFinance>
    {
        ScCompanyFinance Insert(ScCompanyFinance scf);
        Task<ScCompanyFinance> getScCompanyFinanceAsync(Expression<Func<ScCompanyFinance, bool>> where);
        ScCompanyFinance getScCompanyFinance(Expression<Func<ScCompanyFinance, bool>> where);
        ScCompanyFinance getScCompanyFiananceIncludeNull(Expression<Func<ScCompanyFinance, bool>> where);
        Task<int> getMaxYearCompanyFinanceAsync(int compSn);
    }

    class ScCompanyFinanceRepository : RepositoryBase<ScCompanyFinance>, IScCompanyFinanceRepository
    {
        public ScCompanyFinanceRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public ScCompanyFinance getScCompanyFiananceIncludeNull(Expression<Func<ScCompanyFinance, bool>> where)
        {
            return DbContext.ScCompanyFinances.Where(where).DefaultIfEmpty(null).SingleOrDefault();
        }

        public ScCompanyFinance getScCompanyFinance(Expression<Func<ScCompanyFinance, bool>> where)
        {
            return DbContext.ScCompanyFinances.Where(where).SingleOrDefault() ?? new ScCompanyFinance();
        }

        public async Task<ScCompanyFinance> getScCompanyFinanceAsync(Expression<Func<ScCompanyFinance, bool>> where)
        {
            return await DbContext.ScCompanyFinances.Where(where).SingleOrDefaultAsync();
        }

        public ScCompanyFinance Insert(ScCompanyFinance scf)
        {
            return DbContext.ScCompanyFinances.Add(scf);
        }

        public async Task<int> getMaxYearCompanyFinanceAsync(int compSn)
        {
            int retY = 0;
            List<ScCompanyFinance> scCompanyFinance = await DbContext.ScCompanyFinances.Where(i => i.CompSn == compSn).OrderByDescending(i => i.FnYear).ToListAsync();
            foreach (ScCompanyFinance t in scCompanyFinance)
            {
                if (retY < t.FnYear)
                {
                    retY = t.FnYear;
                }
            }
            return retY;
        }
    }
}
