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
    }
}
