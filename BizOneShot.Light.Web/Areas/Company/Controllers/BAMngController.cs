using AutoMapper;
using BizOneShot.Light.Models.ViewModels;
using BizOneShot.Light.Services;
using BizOneShot.Light.Web.ComLib;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BizOneShot.Light.Web.Areas.Company.Controllers
{
    public class BAMngController : BaseController
    {
        private readonly IVcCompInfoService vcCompInfoService;
        private readonly IVcMentorMappingService vcMentorMappingService;
        private readonly ICompBaMngService compBaMngService;
        private readonly IScUsrService vcUsrInfoService;

        public BAMngController(IVcCompInfoService vcCompInfoService, IVcMentorMappingService vcMentorMappingService, ICompBaMngService compBaMngService, IScUsrService vcUsrInfoService)
        {
            this.vcCompInfoService = vcCompInfoService;
            this.vcMentorMappingService = vcMentorMappingService;
            this.compBaMngService = compBaMngService;
            this.vcUsrInfoService = vcUsrInfoService;
        }

        // GET: Company/BAService
        // BA메인 화면
        public ActionResult Index()
        {
            return View();
        }

        // 매핑된 리스트를 보여주는 첫 화면
        //public async Task<ActionResult> BASupervise()
        //{
        //    // 여기에서 보일 데이터

        //    // BA명 | BA 전화번호 | BA EMAIL | 컨설팅코드 | 보고서 담당 멘토

        //    ViewBag.naviLeftMenu = Global.BAMng;
        //    var vcCompInfo = await vcCompInfoService.getVcCompInfoById(Session[Global.LoginID].ToString());
        //    SqlParameter compSn = new SqlParameter("COMP_SN", vcCompInfo.CompSn);
        //    object[] parameters = new object[] { compSn };                                 // 객체에 데이터 삽입

        //    var obj = await compBaMngService.getCompMentorMapping(parameters);  // 해당 기업과 관련된 리스트데이터 가져오기

        //    var usrViews = Mapper.Map<List<BASuperviseViewModel>>(obj);

        //    return View(usrViews);
        //}
        //새로 추가 2016-07-13
        public async Task<ActionResult> BASupervise()
        {
            // 여기에서 보일 데이터

            // BA명 | BA 전화번호 | BA EMAIL | 담당 멘토 | 컨설팅 코드
            ViewBag.naviLeftMenu = Global.BAMng;
            var vcCompInfo = await vcCompInfoService.getVcCompInfoById(Session[Global.LoginID].ToString());
            //var getMapping = await vcCompInfoService.getCompMappingForSn(vcCompInfo.CompSn);

            SqlParameter compSn = new SqlParameter("COMP_SN", vcCompInfo.CompSn);
            object[] parameters = new object[] { compSn };                                 // 객체에 데이터 삽입

            var obj = await compBaMngService.getCompMentorMapping(parameters);  // 해당 기업과 관련된 리스트데이터 가져오기

            var usrViews = Mapper.Map<List<BASuperviseViewModel>>(obj);

            string[] nCheckArray = new string[usrViews.Count];
            
            if(usrViews.Count > 0)
            {
                for(int i = 0; i < nCheckArray.Length; i++)
                {
                    var getInfo = await vcMentorMappingService.GetCompSnForTcms(Convert.ToString(usrViews[i].COMP_SN), Convert.ToString(usrViews[i].BA_SN),Convert.ToString(usrViews[i].NUM_SN), Convert.ToString(usrViews[i].SUB_NUM_SN), usrViews[i].CON_CODE);
                    if (getInfo == null)
                    {
                        nCheckArray[i] = null;
                    }
                    else
                    {
                        usrViews[i].MENTOR_SN = getInfo.MentorSn; //mentorSn 저장
                        var mentorid = await vcUsrInfoService.getMentorInfoBySn(Convert.ToString(usrViews[i].MENTOR_SN));
                        var mentorname = await vcUsrInfoService.selectScUsrByTcms(Convert.ToString(mentorid.TcmsLoginKey));
                        usrViews[i].MENTOR_NM = mentorname.Name; //mentorname 저장
                    }
                }
            }

            return View(usrViews);
        }
        // 해당 멘토의 기본적인 정보를 볼 수 있는 화면
        public async Task<ActionResult> BADeatil(string mentorSn)
        {
            ViewBag.naviLeftMenu = Global.BAMng;
            return View();
        }

        public async Task<ActionResult> MentorDetail(string compSn, string baSn)
        {
            ViewBag.naviLeftMenu = Global.BAMng;

            SqlParameter paramCompSn = new SqlParameter("COMP_SN", compSn);
            SqlParameter paramBaSn = new SqlParameter("BA_SN", baSn);
            object[] parameters = new object[] { paramCompSn, paramBaSn };

            var obj = await compBaMngService.getMentorMappedInfo(parameters);

            var usrViews = Mapper.Map<MentorDetailViewModel>(obj);

            return View(usrViews);
        }

        
        // Mentor 조회시 해당 ba에 할당된 mentor가 없을 경우
        // alert 정보를 리턴하는 액션
        [HttpPost]
        public async Task<JsonResult> MentorExist(string compSn, string baSn)
        {
            // 초기값으로 N 설정
            string status = "N";
            string data = "{\"status\":\"" + status + "\"}";

            SqlParameter paramCompSn = new SqlParameter("COMP_SN", compSn);
            SqlParameter paramBaSn = new SqlParameter("BA_SN", baSn);
            object[] parameters = new object[] { paramCompSn, paramBaSn };

            var obj = await compBaMngService.getMentorMappedInfo(parameters);

            // if 블록에서 조건 충족하면 status를 s로 바꿔 전달
            // 미체크시 N으로 fix
            if (obj != null)
            {
                status = "S";
                data = "{\"status\":\"" + status + "\"}";
            }

            return Json(data);
        }





    }
}