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
    public interface IScUsrRepository : IRepository<VcUsrInfo>
    {
        Task<IList<VcUsrInfo>> GetScUsrById(string loginId);
        VcUsrInfo Insert(VcUsrInfo scUsr);
        //Task<VcUsrInfo> GetMentorInfoById(Expression<Func<VcUsrInfo, bool>> where);
        Task<VcUsrInfo> getScUsrByCompSn(Expression<Func<VcUsrInfo, bool>> where);
        int UserPasswordReset(VcUsrInfo scUsr);

        Task<VcUsrInfo> getUsrInfo(string loginId);

        Task<VcBaInfo> getBaInfo(string loginId);

        // add Loy
        Task<VcUsrInfo> getUsrInfoByTcmsKey(int tcmsLoginKey);
    }


    public class ScUsrRepository : RepositoryBase<VcUsrInfo>, IScUsrRepository
    {
        public ScUsrRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public async Task<IList<VcUsrInfo>> GetScUsrById(string loginId)
        {
            var usrInfo = await DbContext.VcUsrInfoes.Where(ci => ci.LoginId == loginId).ToListAsync();
            return usrInfo;
        }

        public VcUsrInfo Insert(VcUsrInfo scUsr)
        {
            return DbContext.VcUsrInfoes.Add(scUsr);
        }

        //public async Task<ScUsr> GetMentorInfoById(string loginId)
        //{
        //    var scusr = await this.DbContext.ScUsrs
        //        .Include(i => i.ScUsrResume)
        //        //.Include(i => i.ScUsrResume.ScFileInfo)
        //        .Include(i => i.ScMentorMappiings.Select(s => s.ScBizWork))
        //        .Where(ci => ci.LoginId == loginId && ci.Status == "N").FirstOrDefaultAsync();
        //    return scusr;
        //}

        /// <summary>
        ///     맨토정보 가져오기(Eager 로딩, include ScUsrResume, ScMentorMappiings.Select(s => s.ScBizWork)
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        
        //public async Task<VcUsrInfo> GetMentorInfoById(Expression<Func<VcUsrInfo, bool>> where)
        //{
        //    var scusr = await DbContext.VcUsrInfoes
        //        .Include(i => i.ScUsrResume)
        //        //.Include(i => i.ScUsrResume.ScFileInfo)
        //        .Include(i => i.VcMentorMappings.Select(s => s.ScBizWork))
        //        .Where(where).FirstOrDefaultAsync();
        //    return scusr;
        //}

        // PW를 디폴트 값으로 초기화 시키는 부분
        public int UserPasswordReset(VcUsrInfo scUsr) 
        {
            // pw 초기화
            var commandString =
                string.Format("UPDATE SC_USR SET LOGIN_PW='9FCEFC0080D894E83CA7D360CE5CCD9EAD2C5D8A80A10F9FA9698510AABA865A' WHERE LOGIN_ID ='mentorL'");

            //string.Format("SELECT LOGIN_PW FROM SC_USR WHERE SC_USR.LOGIN_ID");

            // update SC_USR set LOGIN_PW='[sha256을 통해 암호화된 pw]' where LOGIN_ID='[변경하고자 하는 user의 ID]';

            return DbContext.Database.ExecuteSqlCommand(commandString);

        }

        public async Task<VcUsrInfo> getScUsrByCompSn(Expression<Func<VcUsrInfo, bool>> where)
        {
            return await DbContext.VcUsrInfoes.Where(where).SingleOrDefaultAsync();
        }


        public async Task<VcUsrInfo> getUsrInfo (string loginId)
        {
            return await DbContext.VcUsrInfoes.
                Where(vc => vc.LoginId == loginId).SingleOrDefaultAsync();
        }

        public async Task<VcBaInfo> getBaInfo(string loginId)
        {
            var id = int.Parse(loginId);

            return await DbContext.VcBaInfoes.
                Where(bc => bc.TcmsLoginKey == id).SingleOrDefaultAsync();
        }

        public async Task<VcUsrInfo> getUsrInfoByTcmsKey(int tcmsLoginKey)
        {
            return await DbContext.VcUsrInfoes.Where(vc => vc.TcmsLoginKey == tcmsLoginKey).SingleOrDefaultAsync();
        }

    }
}