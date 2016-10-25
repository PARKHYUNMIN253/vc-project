using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.WebModels;

namespace BizOneShot.Light.Dao.Repositories
{
    public interface IScMentoringFileInfoRepository : IRepository<ScMentoringFileInfo>
    {
        Task<IList<ScMentoringFileInfo>> GetMentoringFileInfo(Expression<Func<ScMentoringFileInfo, bool>> where);

        // 멘토링관련 파일 삭제
        int deleteMentoringReport(int reportSn);
    }


    public class ScMentoringFileInfoRepository : RepositoryBase<ScMentoringFileInfo>, IScMentoringFileInfoRepository
    {
        public ScMentoringFileInfoRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public async Task<IList<ScMentoringFileInfo>> GetMentoringFileInfo(
            Expression<Func<ScMentoringFileInfo, bool>> where)
        {
            return await DbContext.ScMentoringFileInfoes
                .Include(mtfi => mtfi.ScFileInfo)
                .Where(where).ToListAsync();
        }

        public int deleteMentoringReport(int reportSn)
        {
            var commandString = string.Format("DELETE FROM SC_MENTORING_FILE_INFO where REPORT_SN='" + reportSn + "'");

            return DbContext.Database.ExecuteSqlCommand(commandString);
        }
    }
}