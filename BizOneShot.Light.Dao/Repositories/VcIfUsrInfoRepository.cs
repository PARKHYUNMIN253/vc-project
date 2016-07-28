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
    public interface IVcIfUsrInfoRepository : IRepository<VcIfUsrInfo>
    {
        void insert(VcIfUsrInfo vcIfUsrInfo);
        VcIfUsrInfo insertUsr(VcIfUsrInfo vcIfUsrInfo);
        Task<VcIfUsrInfo> insertToAsync(VcIfUsrInfo vcIfUsrInfo);
        Task<VcIfUsrInfo> getVcIfUsrInfoByInfId(string infId);
    }

    public class VcIfUsrInfoRepository : RepositoryBase<VcIfUsrInfo>, IVcIfUsrInfoRepository
    {
        public VcIfUsrInfoRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public void insert(VcIfUsrInfo vcIfUsrInfo)
        {
            DbContext.VcIfUsrInfoes.Add(vcIfUsrInfo);
        }

        public async Task<VcIfUsrInfo> insertToAsync(VcIfUsrInfo vcIfUsrInfo)
        {
            return await Task.Run(() => DbContext.VcIfUsrInfoes.Add(vcIfUsrInfo));
        }

        public VcIfUsrInfo insertUsr(VcIfUsrInfo vcIfUsrInfo)
        {
            return DbContext.VcIfUsrInfoes.Add(vcIfUsrInfo);
        }



        // infid로 해당 테이블 객체 조회
        public async Task<VcIfUsrInfo> getVcIfUsrInfoByInfId(string infId)
        {
            return await DbContext.VcIfUsrInfoes.Where(bw => bw.InfId == infId).SingleOrDefaultAsync();
        }

    }
}
