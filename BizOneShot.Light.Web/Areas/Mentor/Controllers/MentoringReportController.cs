using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.Mvc;
using System.IO;
using BizOneShot.Light.Web.ComLib;
using BizOneShot.Light.Models.ViewModels;
using System.Threading.Tasks;
using BizOneShot.Light.Models.WebModels;
using BizOneShot.Light.Services;
using BizOneShot.Light.Util.Helper;
using BizOneShot.Light.Util.Security;
using PagedList;
using AutoMapper;
using System.Data.SqlClient;

namespace BizOneShot.Light.Web.Areas.Mentor.Controllers
{

    [UserAuthorize(Order = 1)]
    [MenuAuthorize(Roles = UserType.Mentor, Order = 2)]
    public class MentoringReportController : BaseController
    {
        private readonly IScCompMappingService _scCompMappingService;
        private readonly IScMentorMappingService _scMentorMappingService;
        private readonly IScMentoringTotalReportService _scMentoringTotalReportService;
        private readonly IScMentoringTrFileInfoService _scMentoringTrFileInfoService;

        private readonly IScMentoringReportService _scMentoringReportService;
        private readonly IScMentoringFileInfoService _scMentoringFileInfoService;
        private readonly IProcMngService procMngService;

        // add Loy
        private readonly IScUsrService _vcUsrService;

        // add Loy 20160706
        private readonly IVcMentorMappingService _vcMentorMappingService;
        // add Loy 20160726 멘토링 일지 관련 추가
        private readonly IVcCompInfoService _vcCompInfoService;

        public MentoringReportController(IScCompMappingService scCompMappingService
            , IScMentorMappingService scMentorMappingService
            , IScMentoringTotalReportService scMentoringTotalReportService
            , IScMentoringTrFileInfoService scMentoringTrFileInfoService

            , IScMentoringReportService scMentoringReportService
            , IScMentoringFileInfoService scMentoringFileInfoService
            , IProcMngService procMngService
            , IScUsrService vcUsrService
            , IVcMentorMappingService vcMentorMappingService
            , IVcCompInfoService _vcCompInfoService)
        {
            this._scCompMappingService = scCompMappingService;
            this._scMentorMappingService = scMentorMappingService;
            this._scMentoringTotalReportService = scMentoringTotalReportService;
            this._scMentoringTrFileInfoService = scMentoringTrFileInfoService;

            this._scMentoringReportService = scMentoringReportService;
            this._scMentoringFileInfoService = scMentoringFileInfoService;
            this.procMngService = procMngService;
            this._vcUsrService = vcUsrService;
            this._vcMentorMappingService = vcMentorMappingService;

            this._vcCompInfoService = _vcCompInfoService;
        }

        [HttpPost]
        public async Task<JsonResult> DeleteMentoringTotalReport(string[] totalReportSns)
        {
            ViewBag.LeftMenu = Global.MentoringReport;

            await _scMentoringTotalReportService.DeleteMentoringTotalReport(totalReportSns);

            return Json(new { result = true });
        }

        public async Task<ActionResult> MentoringTotalReportDetail(int totalReportSn, SelectedMentorTotalReportParmModel selectParam)
        {
            ViewBag.LeftMenu = Global.MentoringReport;

            var scMentoringTotalReport = await _scMentoringTotalReportService.GetMentoringTotalReportById(totalReportSn);

            var listscFileInfo = scMentoringTotalReport.ScMentoringTrFileInfoes.Select(mtfi => mtfi.ScFileInfo).Where(fi => fi.Status == "N");

            var listFileContent =
               Mapper.Map<List<FileContent>>(listscFileInfo);

            var totalReportViewModel =
               Mapper.Map<MentoringTotalReportViewModel>(scMentoringTotalReport);

            totalReportViewModel.FileContents = listFileContent;

            //검색조건 유지를 위해
            ViewBag.SelectParam = selectParam;

            return View(totalReportViewModel);
        }

        //public async Task<ActionResult> MentoringTotalReportList(SelectedMentorTotalReportParmModel param, string curPage)
        //{
        //    ViewBag.LeftMenu = Global.MentoringReport;

        //    var mentorId = Session[Global.LoginID].ToString();

        //    //사업 DropDown List Data
        //    var bizWorkDropDown = await MakeBizWork(mentorId, param.BizWorkYear);
        //    SelectList bizList = new SelectList(bizWorkDropDown, "BizWorkSn", "BizWorkNm");
        //    ViewBag.SelectBizWorkList = bizList;

        //    //기업 DropDwon List Data
        //    var compInfoDropDown = await MakeBizComp(mentorId, param.BizWorkSn, param.BizWorkYear);
        //    SelectList compInfoList = new SelectList(compInfoDropDown, "CompSn", "CompNm");
        //    ViewBag.SelectCompInfoList = compInfoList;

        //    //사업년도 DownDown List Data
        //    var bizWorkYearDropDown = MakeBizYear(2015);
        //    SelectList bizWorkYear = new SelectList(bizWorkYearDropDown, "Value", "Text");
        //    ViewBag.SelectBizWorkYearList = bizWorkYear;

        //    //검색조건을 유지하기 위한
        //    ViewBag.SelectParam = param;

        //    //실제 쿼리
        //    int pagingSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);

        //    var pagedListscMentoringTotalReport = await _scMentoringTotalReportService.GetPagedListMentoringTotalReportAsync(int.Parse(curPage ?? "1"), pagingSize, mentorId, param.BizWorkYear, param.BizWorkSn, param.CompSn);

        //    //맨토링 종합 레포트 정보 조회
        //    var listTotalReportView =
        //       Mapper.Map<List<MentoringTotalReportViewModel>>(pagedListscMentoringTotalReport);

        //    return View(new StaticPagedList<MentoringTotalReportViewModel>(listTotalReportView, int.Parse(curPage ?? "1"), pagingSize, pagedListscMentoringTotalReport.TotalItemCount));

        //}
        //#endregion


        #region 멘토 일지
        public async Task<ActionResult> MentoringReportList(SelectedMentorReportParmModel param, string curPage)
        {

            ViewBag.naviLeftMenu = Global.MentoringReport;

            var mentorLoginKey = Session[Global.LoginID].ToString(); // 로그인 키다

            var mentorIdObj = await _vcUsrService.getUsrInfoByTcmsKey(int.Parse(mentorLoginKey));
            var mentorId = mentorIdObj.LoginId;
            // 추가...
            var scMentoringReportObj = await _scMentoringReportService.getMentoringReportListById(mentorId);

            if (scMentoringReportObj.Count > 0)
            {
                var viewObj = Mapper.Map<List<MentoringReportViewModel>>(scMentoringReportObj);

                foreach (var vObj in viewObj)
                {
                    var compNmObj = await _vcCompInfoService.getVcCompInfoByCompSn(vObj.CompSn);
                    vObj.CompNm = compNmObj.CompNm;
                }

                ViewBag.SelectParam = param;

                //맨토링 일지 정보 조회
                int pagingSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);
                //var pagedListscMentoringReport = await _scMentoringReportService.GetPagedListMentoringReportAsync(int.Parse(curPage ?? "1"), pagingSize, mentorId, param.BizWorkYear, param.BizWorkSn, param.CompSn);

                return View(new StaticPagedList<MentoringReportViewModel>(viewObj, int.Parse(curPage ?? "1"), pagingSize, viewObj.Count));
            }
            else
            {
                return View();
            }                
            
            //// 추가 블록 끝...
            //SqlParameter loginId = new SqlParameter("LOGIN_ID", Session[Global.LoginID].ToString());
            //object[] parameters = new object[] { loginId };
            //var compList = await procMngService.getCompMapping(parameters);

            //// 기업명 List
            //var compDropDown = Mapper.Map<List<MentoringReportViewModel>>(compList);

            //MentoringReportViewModel title = new MentoringReportViewModel();
            //title.CompSn = 0;
            //title.CompNm = "기업 선택";
            //compDropDown.Insert(0, title);

            //SelectList compListSelect = new SelectList(compDropDown, "CompSn", "CompNm");

            //ViewBag.SelectCompList = compListSelect;
            

            //// 매핑 되어있지 않은 Mentor는 compSn이 없기때문에 우선은 단순 View만 보여줌
            //if(compList.Count > 0)
            //{
            //    List<ProcMentorGetMentoringReportReturnModel> ieList = new List<ProcMentorGetMentoringReportReturnModel>();
            //    foreach (var compEach in compList)
            //    {
            //        SqlParameter loginId2 = new SqlParameter("LOGIN_ID", Session[Global.LoginID].ToString());
            //        SqlParameter compSn = new SqlParameter("COMP_SN", compEach.COMP_SN);
            //        object[] parameters2 = new object[] { loginId2, compSn };
            //        var compSnList = await procMngService.getMentoringReport(parameters2);
            //        foreach (var obj in compSnList)
            //        { 
            //            ieList.Add(obj);
            //        }
            //    }

            //    var mentoringList = ieList;

            //    var listTotalReportView = Mapper.Map<List<MentoringReportViewModel>>(mentoringList);
            //    // MENTOR_ID, COMP_SN을 추출하여 해당 파일 LIST 조회
            //    //SqlParameter loginId2 = new SqlParameter("LOGIN_ID", Session[Global.LoginID].ToString());
            //    //SqlParameter compSn = new SqlParameter("COMP_SN", compList[0].COMP_SN);
            //    //object[] parameters2 = new object[] { loginId2, compSn };
            //    //var compSnList = await procMngService.getMentoringReport(parameters2);
            //    //var mentoringList = compSnList.Where(bw => bw.CLASSIFY == "A");

            //    //var listTotalReportView = Mapper.Map<List<MentoringReportViewModel>>(mentoringList);

            //    //검색조건을 유지하기 위한+
            //    ViewBag.SelectParam = param;

            //    //맨토링 일지 정보 조회
            //    int pagingSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);
            //    //var pagedListscMentoringReport = await _scMentoringReportService.GetPagedListMentoringReportAsync(int.Parse(curPage ?? "1"), pagingSize, mentorId, param.BizWorkYear, param.BizWorkSn, param.CompSn);

            //    return View(new StaticPagedList<MentoringReportViewModel>(listTotalReportView, int.Parse(curPage ?? "1"), pagingSize, listTotalReportView.Count));

            //}
            //else
            //{
            //    return View();
            //}
        }

        [HttpPost]
        public async Task<JsonResult> DeleteMentoringReport(string[] reportSns)
        {
            ViewBag.LeftMenu = Global.MentoringReport;

            await _scMentoringReportService.DeleteMentoringReport(reportSns);

            return Json(new { result = true });
        }

        public async Task<ActionResult> RegMentoringReport()
        {
            ViewBag.naviLeftMenu = Global.MentoringReport;

            SqlParameter loginId = new SqlParameter("LOGIN_ID", Session[Global.LoginID].ToString());
            object[] parameters = new object[] { loginId };
            var compList = await procMngService.getCompMapping(parameters);

            // 기업명 List
            var compDropDown = Mapper.Map<List<MentoringReportViewModel>>(compList);

            MentoringReportViewModel title = new MentoringReportViewModel();
            title.CompSn = 0;
            title.CompNm = "기업 선택";
            compDropDown.Insert(0, title);

            SelectList compListSelect = new SelectList(compDropDown, "CompSn", "CompNm");

            ViewBag.SelectCompList = compListSelect;

            // 멘토링 분야 ( con_code )
            //var conCodeDropDown = Mapper.Map<List<MentoringReportViewModel>>(compList);

            //MentoringReportViewModel title2 = new MentoringReportViewModel();
            //title2.ConCode = "분야 선택";

            //conCodeDropDown.Insert(0, title2);

            //SelectList ConCodeListSelect = new SelectList(conCodeDropDown, "ConCode", "ConCode");
            //ViewBag.SelectConCodeList = ConCodeListSelect;

            // MENTOR_ID, COMP_SN을 추출하여 해당 파일 LIST 조회

            if (compList.Count != 0)
            {

                //SqlParameter loginId2 = new SqlParameter("LOGIN_ID", Session[Global.LoginID].ToString());
                //SqlParameter compSn = new SqlParameter("COMP_SN", compList[0].COMP_SN);

                //object[] parameters2 = new object[] { loginId2, compSn };
                //var compSnList = await procMngService.getMentoringReport(parameters2);
                //var mentoringTitleList = compSnList.Where(bw => bw.CLASSIFY == "A").Select(bw => bw.MENTORING_SUBJECT).ToList();

            }

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegMentoringReport(MentoringReportViewModel dataRequestViewModel, IEnumerable<HttpPostedFileBase> files)
        {
            ViewBag.naviLeftMenu = Global.MentoringReport;

            var mentorId = Session[Global.LoginID].ToString();

            SqlParameter loginId = new SqlParameter("LOGIN_ID", Session[Global.LoginID]);   // sql 파라미터 값 설정
            object[] parameters = new object[] { loginId };                                 // 객체에 데이터 삽입

            var obj = await procMngService.getCompMapping(parameters);  // 해당 기업과 관련된 리스트데이터 가져오기

            var usrViews = Mapper.Map<List<MentoringReportViewModel>>(obj);

            //if (ModelState.IsValid)
            //{
            var scMentoringReport = Mapper.Map<ScMentoringReport>(dataRequestViewModel);
            //var comp = usrViews[0].CompSn;

            scMentoringReport.MentorId = mentorId;
            scMentoringReport.RegId = mentorId;
            scMentoringReport.RegDt = DateTime.Now;
            scMentoringReport.Status = "N";
            scMentoringReport.CompSn = scMentoringReport.CompSn;

            var compInfo = await _vcCompInfoService.getVcCompInfoByCompSn(scMentoringReport.CompSn ?? default(int));


            // ConCode, NumSn, SubNumSn 자동 할당

            // MentorId로 해당 기업의 num_sn, sub_num_sn을 가져온다
            SqlParameter compSn2 = new SqlParameter("COMP_SN", scMentoringReport.CompSn);
            SqlParameter loginId2 = new SqlParameter("LOGIN_ID", Session[Global.LoginID].ToString());

            object[] parameters2 = new object[] { compSn2, loginId2 };

            var compList = await procMngService.getMentorCompNumSn(parameters2);

            var mentorLoginKey = Session[Global.LoginID].ToString();
            var mentorInfo = await _vcUsrService.getMentorInfoById(mentorLoginKey);

            var baSn = mentorInfo.BaSn;

            //var conCodeInfo = await _vcMentorMappingService.getConcodeInfo(dataRequestViewModel.CompSn, baSn, mentorInfo.MentorSn, compList[0].NUM_SN, compList[0].SUB_NUM_SN);
            scMentoringReport.NumSn = compList[0].NUM_SN;
            scMentoringReport.SubNumSn = compList[0].SUB_NUM_SN;
            scMentoringReport.ConCode = compList[0].CON_CODE;

            //첨부파일
            if (files != null)
            {
                var fileHelper = new FileHelper();
                foreach (var file in files)
                {
                    if (file != null)
                    {

                        //var compSn = usrViews[cnt].CompSn;

                        var savedFileName = fileHelper.GetUploadFileName(file);

                        var subDirectoryPath = Path.Combine(FileType.Mentoring_Report.ToString(), DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString());

                        var savedFilePath = Path.Combine(subDirectoryPath, savedFileName);

                        var tcmsLoginKey = Session[Global.LoginID].ToString();
                        var tcmsLoginKeyCon = int.Parse(tcmsLoginKey);

                        var mentorUsrInfo = await _vcUsrService.getUsrInfoByTcmsKey(tcmsLoginKeyCon);

                        var mentorIdInfo = mentorUsrInfo.LoginId;

                        var scFileInfo = new ScFileInfo { FileNm = Path.GetFileName(file.FileName), FilePath = savedFilePath, Status = "N", RegId = mentorIdInfo, RegDt = DateTime.Now };

                        var scMentoringFileInfo = new ScMentoringFileInfo { ScFileInfo = scFileInfo };

                        //파일타입에 따라 재정의해서 넣어야 함(첨부파일, 사진)
                        scMentoringFileInfo.Classify = fileHelper.HasImageFile(file) ? "P" : "A";

                        scMentoringReport.RegId = mentorIdInfo;
                        scMentoringReport.MentorId = mentorIdInfo;

                        scMentoringReport.ScMentoringFileInfoes.Add(scMentoringFileInfo);

                        await fileHelper.UploadFile(file, subDirectoryPath, savedFileName);

                    }
                }
            }

            //저장
            int result = await _scMentoringReportService.AddScMentoringReportAsync(scMentoringReport);

            if (result != -1)
                return RedirectToAction("MentoringReportList", "MentoringReport");
            else
            {
                ModelState.AddModelError("", "자료요청 등록 실패.");
                return View(dataRequestViewModel);
            }
            // }
            //ModelState.AddModelError("", "입력값 검증 실패.");
            //return View(dataRequestViewModel);
        }

        public async Task<ActionResult> MentoringReportDetail(int reportSn, SelectedMentorReportParmModel selectParam)
        {
            ViewBag.naviLeftMenu = Global.MentoringReport;

            var scMentoringReport = await _scMentoringReportService.GetMentoringReportById(reportSn);

            //멘토링 사진
            var listscMentoringImageInfo = scMentoringReport.ScMentoringFileInfoes.Where(mtfi => mtfi.Classify == "P").Select(mtfi => mtfi.ScFileInfo).Where(fi => fi.Status == "N");

            //사진추가
            var listMentoringPhotoView =
              Mapper.Map<List<FileContent>>(listscMentoringImageInfo);

            FileHelper fileHelper = new FileHelper();

            foreach (var mentoringPhoto in listMentoringPhotoView)
            {
                mentoringPhoto.FileBase64String = await fileHelper.GetPhotoString(mentoringPhoto.FilePath);
                // resize/
                mentoringPhoto.FileFullPath = await fileHelper.GetPhotoStringFullsize(mentoringPhoto.FilePath);
            }

            //첨부파일
            var listscFileInfo = scMentoringReport.ScMentoringFileInfoes.Where(mtfi => mtfi.Classify == "A").Select(mtfi => mtfi.ScFileInfo).Where(fi => fi.Status == "N");

            var listFileContentView =
               Mapper.Map<List<FileContent>>(listscFileInfo);

            //멘토링 상세 매핑
            var reportViewModel =
               Mapper.Map<MentoringReportViewModel>(scMentoringReport);

            //멘토링상세뷰에 파일정보 추가
            reportViewModel.FileContents = listFileContentView;
            reportViewModel.MentoringPhoto = listMentoringPhotoView;

            // 기업명 넘기기
            SqlParameter loginId3 = new SqlParameter("LOGIN_ID", Session[Global.LoginID].ToString());
            object[] parameters3 = new object[] { loginId3 };
            var compInfo = await procMngService.getCompMapping(parameters3);
            ViewBag.CompName = compInfo[0].COMP_NM;

            //검색조건 유지를 위해
            ViewBag.SelectParam = selectParam;

            return View(reportViewModel);
        }

        //public async Task<ActionResult> ModifyMentoringReport(int reportSn)
        //{
        //    ViewBag.LeftMenu = Global.MentoringReport;

        //    var mentorId = Session[Global.LoginID].ToString();

        //    //사업 DropDown List Data
        //    var bizWorkDropDown = await MakeBizWork(mentorId, 0);
        //    SelectList bizList = new SelectList(bizWorkDropDown, "BizWorkSn", "BizWorkNm");
        //    ViewBag.SelectBizWorkList = bizList;

        //    //기업 DropDwon List Data
        //    var compInfoDropDown = await MakeBizComp(mentorId, 0, 0);
        //    SelectList compInfoList = new SelectList(compInfoDropDown, "CompSn", "CompNm");
        //    ViewBag.SelectCompInfoList = compInfoList;


        //    //실제 데이터
        //    var scMentoringReport = await _scMentoringReportService.GetMentoringReportById(reportSn);

        //    //멘토링 사진
        //    var listscMentoringImageInfo = scMentoringReport.ScMentoringFileInfoes.Where(mtfi => mtfi.Classify == "P").Select(mtfi => mtfi.ScFileInfo).Where(fi => fi.Status == "N");

        //    //사진추가
        //    var listMentoringPhotoView =
        //      Mapper.Map<List<FileContent>>(listscMentoringImageInfo);

        //    //첨부파일
        //    var listscFileInfo = scMentoringReport.ScMentoringFileInfoes.Where(mtfi => mtfi.Classify == "A").Select(mtfi => mtfi.ScFileInfo).Where(fi => fi.Status == "N");

        //    var listFileContentView =
        //       Mapper.Map<List<FileContent>>(listscFileInfo);

        //    //멘토링 상세 매핑
        //    var reportViewModel =
        //       Mapper.Map<MentoringReportViewModel>(scMentoringReport);

        //    //멘토링상세뷰에 파일정보 추가
        //    reportViewModel.FileContents = listFileContentView;
        //    reportViewModel.MentoringPhoto = listMentoringPhotoView;

        //    return View(reportViewModel);
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ModifyMentoringReport(MentoringReportViewModel dataRequestViewModel, string deleteFileSns, IEnumerable<HttpPostedFileBase> files)
        {
            ViewBag.LeftMenu = Global.MentoringReport;

            var mentorId = Session[Global.LoginID].ToString();

            if (ModelState.IsValid)
            {
                var scMentoringReport = await _scMentoringReportService.GetMentoringReportById(dataRequestViewModel.ReportSn);

                scMentoringReport.Attendee = dataRequestViewModel.Attendee;
                scMentoringReport.BizWorkSn = dataRequestViewModel.BizWorkSn;
                scMentoringReport.CompSn = dataRequestViewModel.CompSn;
                scMentoringReport.MentorAreaCd = dataRequestViewModel.MentorAreaCd;
                scMentoringReport.MentoringContents = dataRequestViewModel.MentoringContents;
                scMentoringReport.MentoringDt = dataRequestViewModel.MentoringDt;
                scMentoringReport.MentoringEdHr = dataRequestViewModel.MentoringEdHr;
                scMentoringReport.MentoringPlace = dataRequestViewModel.MentoringPlace;
                scMentoringReport.MentoringStHr = dataRequestViewModel.MentoringStHr;
                scMentoringReport.MentoringSubject = dataRequestViewModel.MentoringSubject;
                scMentoringReport.SubmitDt = dataRequestViewModel.SubmitDt;

                scMentoringReport.UpdId = mentorId;
                scMentoringReport.UpdDt = DateTime.Now;

                //삭제파일 상태 업데이트

                if (!string.IsNullOrEmpty(deleteFileSns.Trim()))
                {
                    foreach (var deleteFileSn in deleteFileSns.Split(',').AsEnumerable())
                    {
                        scMentoringReport.ScMentoringFileInfoes.Select(mtfi => mtfi.ScFileInfo).Where(fi => fi.FileSn == int.Parse(deleteFileSn)).FirstOrDefault().Status = "D";
                    }
                }

                //첨부파일
                if (files != null)
                {
                    var fileHelper = new FileHelper();
                    foreach (var file in files)
                    {
                        if (file != null)
                        {
                            var savedFileName = fileHelper.GetUploadFileName(file);

                            var subDirectoryPath = Path.Combine(FileType.Mentoring_Report.ToString(), DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString());

                            var savedFilePath = Path.Combine(subDirectoryPath, savedFileName);

                            var scFileInfo = new ScFileInfo { FileNm = Path.GetFileName(file.FileName), FilePath = savedFilePath, Status = "N", RegId = Session[Global.LoginID].ToString(), RegDt = DateTime.Now };

                            var scMentoringFileInfo = new ScMentoringFileInfo { ScFileInfo = scFileInfo };

                            //파일타입에 따라 재정의해서 넣어야 함(첨부파일, 사진)
                            scMentoringFileInfo.Classify = fileHelper.HasImageFile(file) ? "P" : "A";

                            scMentoringReport.ScMentoringFileInfoes.Add(scMentoringFileInfo);

                            await fileHelper.UploadFile(file, subDirectoryPath, savedFileName);
                        }
                    }
                }

                //수정
                await _scMentoringReportService.ModifyScMentoringReportAsync(scMentoringReport);

                return RedirectToAction("MentoringReportList", "MentoringReport");

            }
            ModelState.AddModelError("", "입력값 검증 실패.");
            return View(dataRequestViewModel);
        }

        #endregion

        #region 드롭다운박스 처리 controller
        //[HttpPost]
        //public async Task<JsonResult> getBizWork(int bizWorkYear)
        //{
        //    var mentorId = Session[Global.LoginID].ToString();

        //    var bizList = await MakeBizWork(mentorId, bizWorkYear);

        //    return Json(bizList);
        //}

        //[HttpPost]
        //public async Task<JsonResult> getBizComp(int bizWorkSn, int bizWorkYear)
        //{
        //    var mentorId = Session[Global.LoginID].ToString();

        //    var compInfoList = await MakeBizComp(mentorId, bizWorkSn, bizWorkYear);

        //    return Json(compInfoList);
        //}
        #endregion

        #region 멘토링 관련 드롭다운리스트
        public IList<SelectListItem> MakeBizYear(int startYear)
        {

            //사업년도
            var bizWorkYearDropDown = new List<SelectListItem>();

            bizWorkYearDropDown.Add(new SelectListItem { Value = "0", Text = "사업년도 선택", Selected = true });

            for (int year = startYear; year <= DateTime.Now.Year; year++)
            {
                bizWorkYearDropDown.Add(
                    new SelectListItem
                    {
                        Value = year.ToString(),
                        Text = year.ToString()
                    });
            }

            return bizWorkYearDropDown;
        }

        //public async Task<IList<BizWorkDropDownModel>> MakeBizWork(string mentorId, int bizWorkYear)
        //{

        //    //사업 DropDown List Data
        //    var listScMentorMapping = await _scMentorMappingService.GetMentorMappingListByMentorId(mentorId, bizWorkYear);
        //    var listScBizWork = listScMentorMapping.Select(mmp => mmp.ScBizWork);//.ToList();

        //    var bizWorkDropDown =
        //        Mapper.Map<List<BizWorkDropDownModel>>(listScBizWork);

        //    //사업드롭다운 타이틀 추가
        //    BizWorkDropDownModel titleBizWork = new BizWorkDropDownModel
        //    {
        //        BizWorkSn = 0,
        //        BizWorkNm = "사업명 선택"
        //    };

        //    bizWorkDropDown.Insert(0, titleBizWork);

        //    return bizWorkDropDown;
        //}


        //public async Task<IList<CompInfoDropDownModel>> MakeBizComp(string mentorId, int bizWorkSn, int bizWorkYear)
        //{

        //    //기업 DropDwon List Data
        //    var listScCompMapping = await _scCompMappingService.GetCompMappingListByMentorId(mentorId, "A", bizWorkSn, bizWorkYear);
        //    var listScCompInfo = listScCompMapping.Select(cmp => cmp.ScCompInfo);//.ToList();

        //    var compInfoDropDown =
        //        Mapper.Map<List<CompInfoDropDownModel>>(listScCompInfo);

        //    //기업 드롭다운 타이틀 추가
        //    CompInfoDropDownModel titleCompInfo = new CompInfoDropDownModel
        //    {
        //        CompSn = 0,
        //        CompNm = "기업명 선택"
        //    };

        //    compInfoDropDown.Insert(0, titleCompInfo);

        //    return compInfoDropDown;
        //}
        #endregion


        #region 파일 다운로드
        public void DownloadReportFile()
        {
            string fileNm = Request.QueryString["FileNm"];
            string filePath = Request.QueryString["FilePath"];

            string archiveName = fileNm;

            var files = new List<FileContent>();

            var file = new FileContent
            {
                FileNm = fileNm,
                FilePath = filePath
            };
            files.Add(file);

            new FileHelper().DownloadFile(files, archiveName);
        }

        public async Task DownloadTRReportFileMulti(string totalReportSn)
        {

            string archiveName = "download.zip";

            //Eager Loading 방식
            var listscMentoringTrFileInfo = await _scMentoringTrFileInfoService.GetMentoringTrFileInfo(int.Parse(totalReportSn));

            var listScFileInfo = new List<ScFileInfo>();
            foreach (var scMentoringTrFileInfo in listscMentoringTrFileInfo)
            {
                listScFileInfo.Add(scMentoringTrFileInfo.ScFileInfo);
            }

            var files = Mapper.Map<IList<FileContent>>(listScFileInfo);

            new FileHelper().DownloadFile(files, archiveName);

        }

        public async Task DownloadReportFileMulti(string reportSn)
        {

            string archiveName = "download.zip";

            //Eager Loading 방식
            var listscMentoringFileInfo = await _scMentoringFileInfoService.GetMentoringFileInfo(int.Parse(reportSn));

            var listScFileInfo = new List<ScFileInfo>();
            foreach (var scMentoringFileInfo in listscMentoringFileInfo)
            {
                listScFileInfo.Add(scMentoringFileInfo.ScFileInfo);
            }

            var files = Mapper.Map<IList<FileContent>>(listScFileInfo);

            new FileHelper().DownloadFile(files, archiveName);

        }
        #endregion

         public async Task<ActionResult> CompList(string curPage)
        {

            ViewBag.naviLeftMenu = Global.MentoringReport;

            SqlParameter loginId = new SqlParameter("LOGIN_ID", Session[Global.LoginID]);   // sql 파라미터 값 설정
            object[] parameters = new object[] { loginId };                                 // 객체에 데이터 삽입

            var obj = await procMngService.getCompMapping(parameters);  // 해당 기업과 관련된 리스트데이터 가져오기

            var usrViews = Mapper.Map<List<JoinMentorViewModel>>(obj);

            int pagingSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);

            return View(new StaticPagedList<JoinMentorViewModel>(usrViews.ToPagedList(int.Parse(curPage ?? "1"), pagingSize), int.Parse(curPage ?? "1"), pagingSize, usrViews.Count));

        }

        // 이미지 파일 원본 크기로 하기위한 과정
        [HttpPost]
        public async Task<JsonResult> ResizeImage(HttpContext context)
        {

            HttpRequest request = context.Request;

            if (!string.IsNullOrEmpty(request.QueryString["id"]))
            {

                //byte[] imageByte = new byte[(int)context.Response.];

            }

            return Json(request);

        }
    }
}