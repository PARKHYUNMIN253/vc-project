using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.WebModels;
using PagedList;
using PagedList.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BizOneShot.Light.Dao.Repositories
{
    public interface ITcmsMentoringReportSelectViewRepository : IRepository<TcmsMentoringReportSelectView>
    {
        Task<IList<TcmsMentoringReportSelectView>> getMentoringReportInfoes();
        Task<IList<TcmsMentoringReportSelectView>> getSearchQuery(Expression<Func<TcmsMentoringReportSelectView, bool>> where);
        Task<TcmsMentoringReportSelectView> GetCompNmByReportSn(int reportSn);
    }

    public class TcmsMentoringReportSelectViewRepository : RepositoryBase<TcmsMentoringReportSelectView>, ITcmsMentoringReportSelectViewRepository
    {
        public TcmsMentoringReportSelectViewRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public async Task<TcmsMentoringReportSelectView> GetCompNmByReportSn(int reportSn)
        {
            return await DbContext.TcmsMentoringReportSelectViews.Where(rs => rs.ReportSn == reportSn).SingleOrDefaultAsync();
        }

        public async Task<IList<TcmsMentoringReportSelectView>> getMentoringReportInfoes()
        {
            return await DbContext.TcmsMentoringReportSelectViews.OrderBy(cm => cm.CompNm).ToListAsync();
        }

        public async Task<IList<TcmsMentoringReportSelectView>> getSearchQuery(Expression<Func<TcmsMentoringReportSelectView, bool>> where)
        {
            return await DbContext.TcmsMentoringReportSelectViews.Where(where).ToListAsync();
        }
    }
}
