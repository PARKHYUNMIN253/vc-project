using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.WebModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BizOneShot.Light.Dao.Repositories
{
    public interface IScFinancialIndexTRepository : IRepository<ScFinancialIndexT>
    {
        ScFinancialIndexT Insert(ScFinancialIndexT sit);
        Task<ScFinancialIndexT> getScFinancialIndexTAsync(Expression<Func<ScFinancialIndexT, bool>> where);

        Task<int> getLatestYearUnderTarget(int CompSn, int Year);
    }

    public class ScFinancialIndexTRepository : RepositoryBase<ScFinancialIndexT>, IScFinancialIndexTRepository
    {
        public ScFinancialIndexTRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public ScFinancialIndexT Insert(ScFinancialIndexT sit)
        {
            return DbContext.ScFinancialIndexTs.Add(sit);
        }

        public async Task<ScFinancialIndexT> getScFinancialIndexTAsync(Expression<Func<ScFinancialIndexT, bool>> where)
        {
            var scFinancialIndexT = await DbContext.ScFinancialIndexTs.Where(where).SingleOrDefaultAsync();
            return scFinancialIndexT;
        }

        public async Task<int> getLatestYearUnderTarget(int CompSn, int Year)   // 최신년도 가져오기
        {
            int retY = 0;
            List<ScFinancialIndexT> scFinancialIndextT = await DbContext.ScFinancialIndexTs.Where(i => i.CompSn == CompSn).OrderByDescending(i => i.Year).ToListAsync();
            foreach (ScFinancialIndexT t in scFinancialIndextT)
            {
                int cy = Int32.Parse(t.Year);
                if (cy > retY && cy <= Year) retY = cy;
            }
            return retY;
        }
    }
}
