using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.WebModels;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizOneShot.Light.Dao.Repositories
{
    public interface ITcmsIfLastReportRepository : IRepository<TcmsIfLastReport>
    {
        Task<IList<TcmsIfLastReport>> getTcmsIfSurvey();
        void Insert(TcmsIfLastReport tcmsIfLastReport);
        Task<TcmsIfLastReport> getTcmsIfSurveyByInfId(string infId);

        // file들의 절대경로를 update 하기 위해서 조회
        Task<TcmsIfLastReport> getTcmsIfLastReportInfo(int compKey, int baKey, int mentorKey, string conCode);
    }
    public class TcmsIfLastReportRepository : RepositoryBase<TcmsIfLastReport>, ITcmsIfLastReportRepository
    {

        public TcmsIfLastReportRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }


        public async Task<IList<TcmsIfLastReport>> getTcmsIfSurvey()
        {
            return await DbContext.TcmsIfLastReports.ToListAsync();
        }

        public async Task<TcmsIfLastReport> getTcmsIfSurveyByInfId(string infId)
        {
            return await DbContext.TcmsIfLastReports.Where(tis => tis.InfId == infId).SingleOrDefaultAsync();
        }

        public void Insert(TcmsIfLastReport tcmsIfLastReport)
        {
            DbContext.TcmsIfLastReports.Add(tcmsIfLastReport);
        }

        public async Task<TcmsIfLastReport> getTcmsIfLastReportInfo(int compKey, int baKey, int mentorKey, string conCode)
        {

            return await DbContext.TcmsIfLastReports
                .Where(tis => tis.CompLoginKey == compKey)
                .Where(tis => tis.BaLoginKey == baKey)
                .Where(tis => tis.MentorLoginKey == mentorKey)
                .Where(tis => tis.ConCode == conCode)
                .SingleOrDefaultAsync();

        }

    }
}
