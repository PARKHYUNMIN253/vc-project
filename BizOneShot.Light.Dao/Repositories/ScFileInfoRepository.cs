using System;
using BizOneShot.Light.Dao.Infrastructure;
using BizOneShot.Light.Models.WebModels;
using System.Threading.Tasks;

namespace BizOneShot.Light.Dao.Repositories
{
    public interface IScFileInfoRepository : IRepository<ScFileInfo>
    {
        Task<ScFileInfo> getFileInfoByFileSn(string fileSn);
    }

    public class ScFileInfoRepository : RepositoryBase<ScFileInfo>, IScFileInfoRepository
    {
        public ScFileInfoRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public Task<ScFileInfo> getFileInfoByFileSn(string fileSn)
        {
            throw new NotImplementedException();
        }
    }
}