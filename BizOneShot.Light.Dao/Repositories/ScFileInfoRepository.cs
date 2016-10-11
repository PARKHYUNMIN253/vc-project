using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.WebModels;
using PagedList;
using PagedList.EntityFramework;


namespace BizOneShot.Light.Dao.Repositories
{
    public interface IScFileInfoRepository : IRepository<ScFileInfo>
    {
        Task<ScFileInfo> getFileInfoByFileSn(int fileSn);
        ScFileInfo getFileInfoByFileSnNA(int fileSn);
    }

    public class ScFileInfoRepository : RepositoryBase<ScFileInfo>, IScFileInfoRepository
    {
        public ScFileInfoRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public async Task<ScFileInfo> getFileInfoByFileSn(int fileSn)
        {
            return await DbContext.ScFileInfoes.Where(obj => obj.FileSn == fileSn).SingleAsync();
        }

        public ScFileInfo getFileInfoByFileSnNA(int fileSn)
        {
            return DbContext.ScFileInfoes.Where(obj => obj.FileSn == fileSn).Single();
        }
    }
}