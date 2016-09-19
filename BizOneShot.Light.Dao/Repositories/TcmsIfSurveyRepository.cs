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
    public interface ITcmsIfSurveyRepository : IRepository<TcmsIfSurvey>
    {
        Task<IList<TcmsIfSurvey>> getTcmsIfSurvey();
        void Insert(TcmsIfSurvey tcmsIfSurvey);
        Task<TcmsIfSurvey> getTcmsIfSurveyByInfId(string infId);

        IList<TcmsIfSurvey> unAsyncGetTcmsIfSurvey();
    }


    public class TcmsIfSurveyRepository : RepositoryBase<TcmsIfSurvey>, ITcmsIfSurveyRepository
    {
        public TcmsIfSurveyRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public async Task<IList<TcmsIfSurvey>> getTcmsIfSurvey()
        {
            return await DbContext.TcmsIfSurveys.ToListAsync();
        }

        public async Task<TcmsIfSurvey> getTcmsIfSurveyByInfId(string infId)
        {
            return await DbContext.TcmsIfSurveys.Where(tis => tis.InfId == infId).SingleOrDefaultAsync();
        }

        public IList<TcmsIfSurvey> unAsyncGetTcmsIfSurvey()
        {
            return DbContext.TcmsIfSurveys.ToList();
        }

        public void Insert(TcmsIfSurvey tcmsIfSurvey)
        {
            DbContext.TcmsIfSurveys.Add(tcmsIfSurvey);
        }
    }
}
