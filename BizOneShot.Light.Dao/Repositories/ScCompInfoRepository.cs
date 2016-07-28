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
    public interface IScCompInfoRepository : IRepository<VcCompInfo>
    {
        IList<VcCompInfo> GetScCompInfoByName(string compNm);
        VcCompInfo Insert(VcCompInfo compInfo);
        Task<VcCompInfo> GetCompInfoAsync(Expression<Func<VcCompInfo, bool>> where);
    }



    public class ScCompInfoRepository : RepositoryBase<VcCompInfo>, IScCompInfoRepository
    {
        public ScCompInfoRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IList<VcCompInfo> GetScCompInfoByName(string compNm)
        {
            var compInfos = DbContext.VcCompInfoes.Where(ci => ci.CompNm == compNm).ToList();

            return compInfos;
        }

        public async Task<VcCompInfo> GetCompInfoAsync(Expression<Func<VcCompInfo, bool>> where)
        {
            return await DbContext.VcCompInfoes.Where(where).SingleOrDefaultAsync();
        }

        public override void Update(VcCompInfo compInfo)
        {
            compInfo.OwnEmail = "test@test.com";

            base.Update(compInfo);
        }

        public VcCompInfo Insert(VcCompInfo compInfo)
        {
            return DbContext.VcCompInfoes.Add(compInfo);
        }
    }
}