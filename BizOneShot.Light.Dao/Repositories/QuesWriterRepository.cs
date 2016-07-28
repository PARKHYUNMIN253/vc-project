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
    public interface IQuesWriterRepository : IRepository<QuesWriter>
    {
        Task<QuesWriter> insertQuesWriterAsync(QuesWriter quesWriter);
        Task<QuesWriter> getQuesWriter(int questionSn);
    }

    public class QuesWriterRepository : RepositoryBase<QuesWriter>, IQuesWriterRepository
    {
        public QuesWriterRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public async Task<QuesWriter> insertQuesWriterAsync(QuesWriter quesWriter)
        {
            var result = await Task.Run(() => DbContext.QuesWriters.Add(quesWriter));
            return result;
        }

        public async Task<QuesWriter> getQuesWriter(int questionSn)
        {
            return await DbContext.QuesWriters.Where(bw => bw.QuestionSn == questionSn).SingleOrDefaultAsync();
        }
    }
}
