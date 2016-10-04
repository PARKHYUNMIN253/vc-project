using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.WebModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizOneShot.Light.Dao.Repositories
{
    public interface ITcmsMentoringReportSelectViewRepository : IRepository<TcmsMentoringReportSelectView>
    {
        Task<IList<TcmsMentoringReportSelectView>> getMentoringReportInfoes();
    }

    public class TcmsMentoringReportSelectViewRepository : RepositoryBase<TcmsMentoringReportSelectView>, ITcmsMentoringReportSelectViewRepository
    {
        public TcmsMentoringReportSelectViewRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public async Task<IList<TcmsMentoringReportSelectView>> getMentoringReportInfoes()
        {
            return await DbContext.TcmsMentoringReportSelectViews.ToListAsync();
        }
    }
}
