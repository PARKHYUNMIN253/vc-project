using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BizOneShot.Light.Models.ViewModels;
using BizOneShot.Light.Services;
using BizOneShot.Light.Web.ComLib;
using PagedList;

namespace BizOneShot.Light.Web.Areas.Mentor.Controllers
{
    [UserAuthorize(Order = 1)]
    [MenuAuthorize(Roles = UserType.Mentor, Order = 2)]
    public class ReportController : BaseController
    {

        private readonly IScCompMappingService scCompMappingService;
        private readonly IScMentorMappingService scMentorMappingService;
        private readonly IRptMasterService rptMasterService;

        private readonly IQuesMasterService _quesMasterService;

        public ReportController(
            IScCompMappingService scCompMappingService,
            IScMentorMappingService scMentorMappingService,
            IRptMasterService rptMasterService,
            IQuesMasterService _quesMasterService)
        {
            this.scCompMappingService = scCompMappingService;
            this.scMentorMappingService = scMentorMappingService;
            this.rptMasterService = rptMasterService;
            this._quesMasterService = _quesMasterService;
        }


        // GET: Mentor/Report
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BasicSurveyReport(string curPage)
        {
            ViewBag.LeftMenu = Global.Report;
            //사업년도 DownDown List Data
            ViewBag.SelectBizWorkYearList = ReportHelper.MakeYear(2015);

            ViewBag.SelectBizWorkList = ReportHelper.MakeBizWorkList(null);
            ViewBag.SelectCompInfoList = ReportHelper.MakeCompanyList(null);
            ViewBag.SelectStatusList = ReportHelper.MakeReportStatusList();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> BasicSurveyReport(BasicSurveyReportViewModel paramModel, string curPage)
        {

            ViewBag.LeftMenu = Global.Report;
            //사업년도 DownDown List Data
            //ViewBag.SelectBizWorkYearList = ReportHelper.MakeYear(2015);

            var bizWorkYearDropDown = ReportHelper.MakeYear(2015);
            SelectList bizWorkYear = new SelectList(bizWorkYearDropDown, "Value", "Text");
            ViewBag.SelectBizWorkYearList = bizWorkYear;

            var mentorId = Session[Global.LoginID].ToString();
            if (string.IsNullOrEmpty(paramModel.Status))
                paramModel.Status = "";

            //사업 DropDown List Data 
            var listScMentorMapping = await scMentorMappingService.GetMentorMappingListByMentorId(mentorId, paramModel.BizWorkYear);
            var listScBizWork = listScMentorMapping.Select(mmp => mmp.ScBizWork).ToList();
            ViewBag.SelectBizWorkList = ReportHelper.MakeBizWorkList(listScBizWork);

            var listScCompMapping = await scCompMappingService.GetCompMappingListByMentorId(mentorId, "A", paramModel.BizWorkSn, paramModel.BizWorkYear);
            var listScCompInfo = listScCompMapping.Select(cmp => cmp.ScCompInfo).ToList();
            ViewBag.SelectCompInfoList = ReportHelper.MakeCompanyList(listScCompInfo);
            ViewBag.SelectStatusList = ReportHelper.MakeReportStatusList();

            var scCompMapping = await scCompMappingService.GetCompMappingAsync(paramModel.BizWorkSn, paramModel.CompSn);
            //scCompMapping.ScCompInfo.

            //var statusForCom = await sc

            //기초역량 보고서 조회
            int pagingSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);

            //// 문진표 작성 완료 후 count
            //var countTest = await _quesMasterService.GetQuesMasterAsync(paramModel.QuestionSn);

            var rptMsters = await rptMasterService.GetRptMasterList(int.Parse(curPage ?? "1"), pagingSize, mentorId, paramModel.BizWorkYear, paramModel.BizWorkSn, paramModel.CompSn, paramModel.Status);
            //var rptMaters2 = scCompMapping.ScCompInfo.RptMasters.Where(rm => rm.BasicYear == paramModel.BizWorkYear && rm.BizWorkSn == paramModel.BizWorkSn && rm.CompSn == paramModel.CompSn).SingleOrDefault();

            // QuesMaster -> stauts ="C"인 애들만 mapping
            var quesMaster = await _quesMasterService.GetQuesMasterAsync(paramModel.RegistrationNo, paramModel.BizWorkYear);
            var quesMasterP = await _quesMasterService.GetQuesMasterAsyncPro(paramModel.RegistrationNo, paramModel.BizWorkYear);


            //paramModel.CompleteCount = 0;
            //paramModel.NotYetCountForMentor = 0;
            //paramModel.TotalCountForMentor = 0;

            // 추가적으로 for 문 돌리거나 그냥 viewModel에 direct로 박아주는 방법
            //if(quesMaster.Status == "C")
            //{
            //    paramModel.CompleteCount++;
            //}
            //else
            //{
            //    paramModel.NotYetCountForMentor++;
            //}

            // 전체 기업 수 count
            //var a = quesMaster.Status;
            //var b = quesMaster.QuesCompInfo;

            //paramModel.Status = quesMaster.Status;
            paramModel.TotalCountForMentor = listScCompMapping.Count;
            // var a = rptMsters.Subset;


            //뷰모델 맵핑
            var rptMasterListView = Mapper.Map<List<BasicSurveyReportViewModel>>(rptMsters);

            return View(new StaticPagedList<BasicSurveyReportViewModel>(rptMasterListView, int.Parse(curPage ?? "1"), pagingSize, rptMsters.TotalItemCount));

        }




        #region 드롭다운박스 처리 controller
        [HttpPost]
        public async Task<JsonResult> GetBizWorkNm(int Year)
        {
            if (Year == 0) Year = 2015;

            var mentorId = Session[Global.LoginID].ToString();

            //사업 DropDown List Data
            var listScMentorMapping = await scMentorMappingService.GetMentorMappingListByMentorId(mentorId, Year);
            var listScBizWork = listScMentorMapping.Select(mmp => mmp.ScBizWork).ToList();

            var bizList = ReportHelper.MakeBizWorkList(listScBizWork);

            return Json(bizList);
        }

        [HttpPost]
        public async Task<JsonResult> GetCompanyNm(int BizWorkSn, int Year)
        {
            if (Year == 0) Year = 2015;

            var mentorId = Session[Global.LoginID].ToString();

            var listScCompMapping = await scCompMappingService.GetCompMappingListByMentorId(mentorId, "A", BizWorkSn, Year);
            var listScCompInfo = listScCompMapping.Select(cmp => cmp.ScCompInfo).ToList();

            var a = listScCompInfo.Count;

            // Mentor와 mapping 된 전체 comp count
            // var ComCount = listScCompMapping.Count;
            // listScCompMapping.ScCompInfo.CompNm -> 기업명
            var bizList = ReportHelper.MakeCompanyList(listScCompInfo);

            return Json(bizList);
        }

        //// Add Loy
        //public  IList<SelectListItem> MakeBizYear(int startYear)
        //{
        //    var bizWorkYearDropDown = new List<SelectListItem>();

        //    //bizWorkYearDropDown.Add(new SelectListItem { Value= "0", Text = "" })

        //        return bizWorkYearDropDown;
        //}

        #endregion


        public ActionResult JoinComCount()
        {
            return View();
        }

        // listScCompMapping을 통해서 


        // 멘토와 mapping되어있는 모든 기업들 count
        [HttpPost]
        public async Task<JsonResult> GetTotalCount(int BizWorkSn, int Year)
        {
            if (Year == 0) Year = 2015;

            var mentorId = Session[Global.LoginID].ToString();

            // 멘토에 mapping되어있는 comp 가져오기
            var listScCompMapping = await scCompMappingService.GetCompMappingListByMentorId(mentorId, "A", BizWorkSn, Year);
            var listScCompInfo = listScCompMapping.Select(cmp => cmp.ScCompInfo).ToList();
            // 가져온 comp 회사정보에서 문진표 status 확인 하는 과정 추가

            // Mentor와 mapping 된 전체 comp count
            var ComCount = listScCompInfo.Count;
            // listScCompMapping.ScCompInfo.CompNm -> 기업명

            return Json(ComCount);
        }


        // GetNotYetCount


        // GetCompleteCount


    }
}