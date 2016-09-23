using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;
using AutoMapper;
using BizOneShot.Light.Models.ViewModels;
using BizOneShot.Light.Models.WebModels;
using BizOneShot.Light.Models.DareModels;
using BizOneShot.Light.Services;
using BizOneShot.Light.Util.Security;
using BizOneShot.Light.Web.ComLib;
using PagedList;
using BizOneShot.Light.Util.Helper;
using System.Data.SqlClient;

namespace BizOneShot.Light.Web.Areas.BizManager.Controllers
{
    [UserAuthorize(Order = 1)]
    [MenuAuthorize(Roles = UserType.BizManager, Order = 2)]
    public class MentorMngController : BaseController
    {
        private readonly IScUsrService _scUsrService;
        private readonly IScBizWorkService _scBizWorkService;
        private readonly IScMentorMappingService _scMentorMappingService;
        private readonly IProcMngService procBaMentorMapping;
        private readonly IVcBaInfoService vcBaInfoService;
        private readonly IVcMentorMappingService vcMentorMappingService;

        private readonly IProcMngService procMngService;
        private readonly IScMentoringReportService _scMentoringReportService;
        private readonly IVcCompInfoService vcCompInfoService;

        // deepen Report 조회
        private readonly IVcLastReportNSatService vcLastReportNSatService;

        // BA의 WIRTE_YN 조회
        private readonly IScCompMappingService vcCompMappingService;

        private readonly IVcMentorInfoSerivce vcMentorInfoService;

        private readonly IScUsrService _vcUsrService;

        private readonly IVcCompInfoService _vcCompInfoService;

        public MentorMngController(IScUsrService scUsrService,
            IScBizWorkService _scBizWorkService,
            IScMentorMappingService _scMentorMappingService,
            IProcMngService procBaMentorMapping,
            IVcBaInfoService vcBaInfoService,
            IVcMentorMappingService vcMentorMappingService,
            IProcMngService procMngService,
            IScMentoringReportService scMentoringReportService,
            IVcCompInfoService vcCompInfoService,
            IVcLastReportNSatService vcLastReportNSatService,
            IScCompMappingService vcCompMappingService,
            IVcMentorInfoSerivce vcMentorInfoService,
            IScUsrService _vcUsrService,
            IVcCompInfoService _vcCompInfoService)
        {
            this._scUsrService = scUsrService;
            this._scBizWorkService = _scBizWorkService;
            this._scMentorMappingService = _scMentorMappingService;
            this.procBaMentorMapping = procBaMentorMapping;
            this.vcBaInfoService = vcBaInfoService;
            this.vcMentorMappingService = vcMentorMappingService;
            this.procMngService = procMngService;
            this._scMentoringReportService = scMentoringReportService;
            this.vcCompInfoService = vcCompInfoService;
            this.vcLastReportNSatService = vcLastReportNSatService;
            this.vcCompMappingService = vcCompMappingService;
            this.vcMentorInfoService = vcMentorInfoService;
            this._vcUsrService = _vcUsrService;
            this._vcCompInfoService = _vcCompInfoService;
        }

        // GET: BizManager/MentorMng
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> MentorList(string curPage)
        {

            ViewBag.naviLeftMenu = Global.MentorMng;

            SqlParameter loginId = new SqlParameter("LOGIN_ID", Session[Global.LoginID]);   // sql 파라미터 값 설정
            object[] parameters = new object[] { loginId };                                 // 객체에 데이터 삽입

            var obj = await procBaMentorMapping.getBaMentorMapping(parameters);  // 해당 기업과 관련된 리스트데이터 가져오기

            var usrViews = Mapper.Map<List<JoinMentorViewModel>>(obj);

            //return View(usrViews);
            // compSn , baSn, mentorSn 가져와서 Last_report에 존재하면 최종제출 완료 없으면 최종제출 안함
            //var compSn = obj[0].COMP_SN ?? default(int);
            //var baSn = obj[0].BA_SN;
            //var mentorSn = obj[0].MENTOR_SN;

            //var result = await vcLastReportNSatService.checkDeepenSubmit(compSn, baSn, mentorSn);

            // 심화보고서 최종 제출 확인
          
            foreach (var item in usrViews)
            {
                if(item.CompSn != null)
                {
                    var compSn = int.Parse(item.CompSn);
                    var baSn = int.Parse(item.BaSn);
                    var mentorSn = int.Parse(item.MentorSn);
                    var conCode = item.ConCode;

                    var result = await vcLastReportNSatService.checkDeepenSubmitByBa(compSn, baSn, mentorSn, conCode);

                    if (result == null)
                    {
                        item.DeepenComplete = "P";
                        //await procBaMentorMapping.SaveDbContextAsync();
                    }
                    else
                    {
                        item.DeepenComplete = "C";
                        //await procBaMentorMapping.SaveDbContextAsync();
                    }
                }
                
            }

            //SqlParameter loginId2 = new SqlParameter("LOGIN_ID", Session[Global.LoginID]);   // sql 파라미터 값 설정
            //object[] parameters2 = new object[] { loginId2 };                                 // 객체에 데이터 삽입

            //var obj2 = await procBaMentorMapping.getBaMentorMapping(parameters2);  // 해당 기업과 관련된 리스트데이터 가져오기

            //var usrViews2 = Mapper.Map<List<JoinMentorViewModel>>(obj2);

            int pagingSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);

            return View(new StaticPagedList<JoinMentorViewModel>(usrViews.ToPagedList(int.Parse(curPage ?? "1"), pagingSize), int.Parse(curPage ?? "1"), pagingSize, usrViews.Count));
        }

        //[HttpPost]
        //public async Task<ActionResult> MentorList(string BizWorkList, string MentorPartList, string curPage)
        //{
        //    ViewBag.LeftMenu = Global.MentorMng;

        //    string excutorId = null;

        //    //사업담당자 일 경우 담당 사업만 조회
        //    if (Session[Global.UserDetailType].ToString() == "M")
        //    {
        //        excutorId = Session[Global.LoginID].ToString();
        //    }
        //    //사업 DropDown List Data
        //    var listScBizWork = await _scBizWorkService.GetBizWorkList(int.Parse(Session[Global.CompSN].ToString()), excutorId);


        //    var bizWorkDropDown =
        //        Mapper.Map<List<BizWorkDropDownModel>>(listScBizWork);

        //    //사업담당자 일 경우 담당 사업만 조회
        //    if (Session[Global.UserDetailType].ToString() == "M")
        //    {
        //        BizWorkList = listScBizWork.First().BizWorkSn.ToString();
        //    }
        //    else
        //    {
        //        BizWorkDropDownModel title = new BizWorkDropDownModel();
        //        title.BizWorkSn = 0;
        //        title.BizWorkNm = "사업명 선택";
        //        bizWorkDropDown.Insert(0, title);
        //    }

        //    SelectList bizList = new SelectList(bizWorkDropDown, "BizWorkSn", "BizWorkNm");

        //    ViewBag.SelectBizWorkList = bizList;


        //    //멘토 분야 DropDown List Data
        //    var mentorPart = new List<SelectListItem>(){
        //        new SelectListItem { Value = "", Text = "멘토분야", Selected = true },
        //        new SelectListItem { Value = "F", Text = "자금" },
        //        new SelectListItem { Value = "D", Text = "기술개발" },
        //        new SelectListItem { Value = "T", Text = "세무회계" },
        //        new SelectListItem { Value = "L", Text = "법무" },
        //        new SelectListItem { Value = "W", Text = "노무" },
        //        new SelectListItem { Value = "P", Text = "특허" },
        //        new SelectListItem { Value = "M", Text = "마케팅" },
        //        new SelectListItem { Value = "E", Text = "기타" }
        //    };

        //    SelectList mentorPartList = new SelectList(mentorPart, "Value", "Text");

        //    ViewBag.SelectMentorPartList = mentorPartList;

        //    //전문가 리스트 조회
        //    var listMentor = await _scMentorMappingService.GetMentorListAsync(int.Parse(Session[Global.CompSN].ToString()), int.Parse(BizWorkList), MentorPartList);

        //    var usrViews =
        //        Mapper.Map<List<JoinMentorViewModel>>(listMentor);

        //    int pagingSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);
        //    return View(new StaticPagedList<JoinMentorViewModel>(usrViews.ToPagedList(int.Parse(curPage), pagingSize), int.Parse(curPage), pagingSize, usrViews.Count));

        //}

        //public async Task<ActionResult> RegMentor()
        //{
        //    ViewBag.LeftMenu = Global.MentorMng;

        //    string excutorId = null;

        //    //사업담당자 일 경우 담당 사업만 조회
        //    if (Session[Global.UserDetailType].ToString() == "M")
        //    {
        //        excutorId = Session[Global.LoginID].ToString();
        //    }

        //    //사업 DropDown List Data
        //    var listScBizWork = await _scBizWorkService.GetBizWorkList(int.Parse(Session[Global.CompSN].ToString()), excutorId);


        //    var bizWorkDropDown =
        //        Mapper.Map<List<BizWorkDropDownModel>>(listScBizWork);

        //    if (Session[Global.UserDetailType].ToString() == "A")
        //    {
        //        BizWorkDropDownModel title = new BizWorkDropDownModel();
        //        title.BizWorkSn = 0;
        //        title.BizWorkNm = "사업명 선택";
        //        bizWorkDropDown.Insert(0, title);
        //    }

        //    SelectList bizList = new SelectList(bizWorkDropDown, "BizWorkSn", "BizWorkNm");

        //    ViewBag.SelectBizWorkList = bizList;
        //    return View();
        //}

        //[HttpPost]
        //public async Task<ActionResult> RegMentor(JoinMentorViewModel joinMentorViewModel)
        //{
        //    ViewBag.LeftMenu = Global.MentorMng;

        //    string excutorId = null;

        //    //사업담당자 일 경우 담당 사업만 조회
        //    if (Session[Global.UserDetailType].ToString() == "M")
        //    {
        //        excutorId = Session[Global.LoginID].ToString();
        //    }
        //    //사업 DropDown List Data
        //    var listScBizWork = await _scBizWorkService.GetBizWorkList(int.Parse(Session[Global.CompSN].ToString()), excutorId);


        //    var bizWorkDropDown =
        //        Mapper.Map<List<BizWorkDropDownModel>>(listScBizWork);

        //    if (Session[Global.UserDetailType].ToString() == "A")
        //    {
        //        BizWorkDropDownModel title = new BizWorkDropDownModel();
        //        title.BizWorkSn = 0;
        //        title.BizWorkNm = "사업명 선택";
        //        bizWorkDropDown.Insert(0, title);
        //    }

        //    SelectList bizList = new SelectList(bizWorkDropDown, "BizWorkSn", "BizWorkNm");

        //    ViewBag.SelectBizWorkList = bizList;


        //    if (ModelState.IsValid == false)
        //    {
        //        if (joinMentorViewModel.BizWorkSn == 0)
        //        {
        //            ModelState.AddModelError("", "사업명을 선택하지 않았습니다.");
        //            return View(joinMentorViewModel);
        //        }

        //        var scUsr = Mapper.Map<VcUsrInfo>(joinMentorViewModel);
        //        var scCompInfo = Mapper.Map<VcCompInfo>(joinMentorViewModel);

        //        //회원정보 추가 정보 설정
        //        scUsr.RegId = Session[Global.LoginID].ToString();
        //        scUsr.RegDt = DateTime.Now;
        //        scUsr.Status = "N";
        //        scUsr.UsrType = "M";
        //        scUsr.UsrTypeDetail = "E";
        //        scUsr.UseErp = "0";

        //        SHACryptography sha2 = new SHACryptography();
        //        scUsr.LoginPw = sha2.EncryptString(scUsr.LoginPw);

        //        //회사정보 추가 정보 설정
        //        scCompInfo.Status = "N";
        //        scCompInfo.RegId = Session[Global.LoginID].ToString();
        //        scCompInfo.RegDt = DateTime.Now;

        //        //멘토 매핑정보 생성
        //        VcMentorMapping scMentorMappiing = new VcMentorMapping();
        //        scMentorMappiing.BizWorkSn = joinMentorViewModel.BizWorkSn;
        //        scMentorMappiing.MentorId = scUsr.LoginId;
        //        scMentorMappiing.Status = "N";
        //        scMentorMappiing.MngCompSn = int.Parse(Session[Global.CompSN].ToString());
        //        scMentorMappiing.RegId = scUsr.RegId;
        //        scMentorMappiing.RegDt = scUsr.RegDt;
        //        scUsr.ScMentorMappiings.Add(scMentorMappiing);

        //        //저장
        //        scCompInfo.ScUsrs.Add(scUsr);


        //        //bool result = _scUsrService.AddCompanyUser(scCompInfo, scUsr, syUser);
        //        int result = await _scMentorMappingService.AddMentorAsync(scCompInfo);

        //        if (result != -1)
        //            return RedirectToAction("MentorList", "MentorMng");
        //        else
        //        {
        //            ModelState.AddModelError("", "멘토 등록 실패.");
        //            return View(joinMentorViewModel);
        //        }
        //    }
        //    return View(joinMentorViewModel);
        //}

        //public async Task<ActionResult> MentorDetail(string bizWorkSn, string mentorId)
        //{
        //    ViewBag.LeftMenu = Global.MentorMng;

        //    VcMentorMapping vcMentorMapping = await _scMentorMappingService.GetMentor(int.Parse(bizWorkSn), mentorId);
        //    //string a = scMentorMapping.MentorId;
        //    var usrView =
        //        Mapper.Map<JoinMentorViewModel>(vcMentorMapping);

        //    return View(usrView);
        //}

        //[HttpPost]
        //public async Task<JsonResult> DoLoginIdSelect(string LoginId)
        //{
        //    bool result = await _scUsrService.ChkLoginId(LoginId);

        //    if (result.Equals(true))
        //    {
        //        return Json(new { result = true });
        //    }
        //    else
        //    {
        //        return Json(new { result = false });
        //    }

        //}

        public async Task<ActionResult> MentoringReportList(SelectedMentorReportParmModel param, string curPage)
        {
            ViewBag.naviLeftMenu = Global.MentorMng;

            // baLoginKey get
            var baLoginKey = Session[Global.LoginID].ToString();
            // baSn get
            var baSnObj = await vcBaInfoService.getVcBaInfoByKey(int.Parse(baLoginKey));
            // basn -> vcmentorinfo get
            var mentorInfoList = await vcMentorInfoService.getVcMentorInfoListByBaSn(baSnObj.BaSn);
            // foreach (mentor일지같이.. list에 add)
            List<MentoringReportViewModel> mrList = new List<MentoringReportViewModel>();
            if (mentorInfoList.Count > 0)
            {
                foreach (var miObj in mentorInfoList)
                {
                    var mentorIdObj = await _vcUsrService.getUsrInfoByTcmsKey(miObj.TcmsLoginKey);
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
                            var usrInfoObj = await _vcUsrService.getScUsrByLoginId(vObj.MentorId);
                            vObj.MentorNm = usrInfoObj.Name;
                            mrList.Add(vObj);
                        }   
                    }
                }
                //검색조건을 유지하기 위한
                ViewBag.SelectParam = param;

                //맨토링 일지 정보 조회
                int pagingSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);
                //if (curPage == "")
                //{
                //    curPage = "1";
                //}
                //var pList = mrList.ToPagedList(int.Parse(curPage), pagingSize);
                //return View(pList);
                return View(new StaticPagedList<MentoringReportViewModel>(mrList, int.Parse(curPage ?? "1"), pagingSize, mrList.Count));
            }
            else
            {
                return View();
            }
            // 뷰 보이기

            //var mentorId = Session[Global.LoginID].ToString();

            //SqlParameter loginId = new SqlParameter("LOGIN_ID", Session[Global.LoginID].ToString());
            //object[] parameters = new object[] { loginId };
            //var listObj = await procMngService.getBaMentoringReport(parameters);
            //var reportList = listObj;/*.Where(bw => bw.CLASSIFY == "A").ToList();*/

            //ViewBag.SelectCompInfoList = ReportHelper.MakeCompanyListByBa(listObj);                 // 기업명 dropdownlist

            ////검색조건을 유지하기 위한
            //ViewBag.SelectParam = param;

            ////맨토링 일지 정보 조회
            //int pagingSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);

            ////맨토링 일지 정보 to 뷰모델 매핑
            //var listTotalReportView =
            //   Mapper.Map<List<MentoringReportViewModel>>(reportList);

            //return View(new StaticPagedList<MentoringReportViewModel>(listTotalReportView, int.Parse(curPage ?? "1"), pagingSize, listTotalReportView.Count));

        }

        [HttpPost]
        public async Task<ActionResult> MentoringReportList(string curPage)
        {
            ViewBag.naviLeftMenu = Global.MentorMng;

            var mentorId = Session[Global.LoginID].ToString();

            SqlParameter loginId = new SqlParameter("LOGIN_ID", Session[Global.LoginID].ToString());
            object[] parameters = new object[] { loginId };
            var listObj = await procMngService.getBaMentoringReport(parameters);
            //var reportList = listObj.Where(bw => bw.CLASSIFY == "A");

            ViewBag.SelectCompInfoList = ReportHelper.MakeCompanyListByBa(listObj);                 // 기업명 dropdownlist

            //검색조건을 유지하기 위한
            //ViewBag.SelectParam = param;

            //맨토링 일지 정보 조회
            int pagingSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);

            //맨토링 일지 정보 to 뷰모델 매핑
            var listTotalReportView =
               Mapper.Map<List<MentoringReportViewModel>>(listObj);

            return View(new StaticPagedList<MentoringReportViewModel>(listTotalReportView.ToPagedList(int.Parse(curPage), pagingSize), int.Parse(curPage ?? "1"), pagingSize, listTotalReportView.Count));

        }



        public async Task<ActionResult> MentoringReportDetail(int reportSn, SelectedMentorReportParmModel selectParam)
        {
            ViewBag.naviLeftMenu = Global.MentorMng;

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
            }

            //첨부파일
            var listscFileInfo = scMentoringReport.ScMentoringFileInfoes.Where(mtfi => mtfi.Classify == "A").Select(mtfi => mtfi.ScFileInfo).Where(fi => fi.Status == "N");

            var listFileContentView =
               Mapper.Map<List<FileContent>>(listscFileInfo);

            //멘토링 상세 매핑
            var reportViewModel =
               Mapper.Map<MentoringReportViewModel>(scMentoringReport);

            // 멘토명을 이용한 기업명
            var mentorId = reportViewModel.MentorId;
            var mentorLoginKey = await _scUsrService.SelectScUsrforIf(mentorId);

            SqlParameter loginId = new SqlParameter("LOGIN_ID", mentorLoginKey.TcmsLoginKey);
            object[] parameters = new object[] { loginId };
            var listObj = await procMngService.getCompMapping(parameters);
            ViewBag.CompName = listObj[0].COMP_NM;


            //멘토링상세뷰에 파일정보 추가
            reportViewModel.FileContents = listFileContentView;
            reportViewModel.MentoringPhoto = listMentoringPhotoView;

            //검색조건 유지를 위해
            ViewBag.SelectParam = selectParam;

            return View(reportViewModel);
        }

        public void DownloadResumeFile()
        {
            //System.Collections.Specialized.NameValueCollection col = Request.QueryString;
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

        //public async Task<ActionResult> ResetMentorPw(string bizWorkSn, string mentorId)
        //{
        //    ViewBag.LeftMenu = Global.MentorMng;

        //    ScMentorMappiing scMentorMapping = await _scMentorMappingService.GetMentor(int.Parse(bizWorkSn), mentorId);

        //    var usrView =
        //        Mapper.Map<JoinMentorViewModel>(scMentorMapping);

        //    SHACryptography sha2 = new SHACryptography();

        //    //ScUsr scUsr = await _scUsrService.SelectScUsr(model.ID);
        //    //SHUSER_SyUser syUsr = new SHUSER_SyUser();

        //    //// 특정한 pw를 미리 설정
        //    //model.NewLoginPw = "ab12345678";

        //    //scUsr.LoginPw = sha2.EncryptString(model.NewLoginPw);
        //    //syUsr.SmartPwd = scUsr.LoginPw;
        //    //await _scUsrService.SaveDbContextAsync();
        //    //var rst = _scUsrService.UpdatePassword(syUsr);

        //    //// 누가 누구의 pw를 초기화 시킬 것인지?

        //    //string usrArea;

        //    //if (Session[Global.UserType].ToString().Equals(Global.SysManager))
        //    //{
        //    //    // 시스템 담당자
        //    //    // 시스템 담당자가 -> 사업관리기관, 전문가 회원 pw 초기화
        //    //    usrArea = "SysManager";
        //    //}
        //    //else if (Session[Global.UserType].ToString().Equals(Global.BizManager))
        //    //{
        //    //    // 사업관리기관
        //    //    // 사업관리기관 -> 사업담당자, 멘토 pw 초기화
        //    //    usrArea = "BizManager";
        //    //}


        //    //return View();

        //    return View(usrView);
        //}


        // add Loy
        public async Task<ActionResult> UserPasswordReset(string mentorId)
        {
            //ScMentorMappiing scMentorMapping = await _scMentorMappingService.GetMentor(int.Parse(bizWorkSn), mentorId);
            //ScUsr scUsr = await _scUsrService.SelectScUsr(scMentorMapping.MentorId);
            VcUsrInfo scUsr = await _scUsrService.SelectScUsr(mentorId);

            SHACryptography sha2 = new SHACryptography();
            var rstPw = "a12345678";
            var sha2pw = sha2.EncryptString(rstPw);

            //scUsr.LoginPw = sha2pw;

            await _scUsrService.SaveDbContextAsync();

            //var rst = _scUsrService.UserPasswordReset(scUsr);

            // 초기화 후 list로 돌아가기
            return RedirectToAction("MentorList", "MentorMng", new { area = "BizManager" });

        }

        public async Task<ActionResult> MentorCompMapping(JoinMentorViewModel paramodel, string curPage)
        {
            ViewBag.naviLeftMenu = Global.MentorMng;

            var baLoginKey = Session[Global.LoginID].ToString();
            var baInfo = await _scUsrService.getBaInfoById(baLoginKey);

            // 기업 List
            SqlParameter loginKey = new SqlParameter("TCMS_LOGIN_KEY", Session[Global.LoginID]);
            object[] parameters22 = new object[] { loginKey };

            var obj22 = await procMngService.baGetCompSelectList(parameters22);

            //var obj23 = obj22.Where(bw => bw.MENTOR_SN == null);

            var dropDownCompList22 = Mapper.Map<List<JoinMentorViewModel>>(obj22);

            JoinMentorViewModel title22 = new JoinMentorViewModel();

            title22.CompSn = "0";
            title22.CompNm = "기업선택";
            dropDownCompList22.Insert(0, title22);

            SelectList compSelectList = new SelectList(dropDownCompList22, "CompSn", "CompNm");

            ViewBag.SelectCompList = compSelectList;


            // 멘토 select List
            //SqlParameter loginId2 = new SqlParameter("LOGIN_ID", Session[Global.LoginID]);   // sql 파라미터 값 설정
            //object[] parameters2 = new object[] { loginId2 };                                 // 객체에 데이터 삽입

            //var obj2 = await procBaMentorMapping.getBaMentorMapping(parameters2);  // 해당 기업과 관련된 리스트데이터 가져오기
            var obj2 = await _scUsrService.baGetMappingMentor(baInfo.BaSn);

            var dropDownMentor = Mapper.Map<List<JoinMentorViewModel>>(obj2);
            
            foreach(var item in dropDownMentor)
            {
                var mentorInfo = await _scUsrService.selectScUsrByTcms(item.MentorLoginKey);
                var mentorNm = mentorInfo.Name;

                item.MentorNm = mentorNm;
            }

            JoinMentorViewModel title = new JoinMentorViewModel();
            title.MentorSn = "0";
            title.MentorNm = "멘토 선택";
            dropDownMentor.Insert(0, title);

            SelectList mentorListSelect = new SelectList(dropDownMentor, "MentorSn", "MentorNm");

            ViewBag.SelectMentorList = mentorListSelect;

            // 분야 select List
            var conCodeList = new List<SelectListItem>();
            conCodeList.Add(new SelectListItem { Value = "0", Text = "분야 코드 선택", Selected = true });
            SelectList list = new SelectList(conCodeList, "Value", "Text");
            ViewBag.SelectConCodeList = list;


            // 해당 BA에 매핑된 멘토 SelectList
            SqlParameter baSn = new SqlParameter("BA_SN", baInfo.BaSn.ToString());
            object[] baSnSql = new object[] { baSn };

            var obj = await procMngService.baGetCompMentorMappingList(baSnSql);

            var usrViewsInfo = Mapper.Map<List<JoinMentorViewModel>>(obj);

            // mapping 됐는지 안됐는지 확인
            foreach (var item in usrViewsInfo)
            {
                var itemCompSn = int.Parse(item.CompSn);
                var itemBaSn = int.Parse(item.BaSn);
                var itemNumSn = item.NumSn;
                var itemSubNumSn = item.SubNumSn;
                var itemConCode = item.ConCode;

                var checkMapping = await vcMentorMappingService.checkCompMentorMapping(itemCompSn, itemBaSn, itemNumSn, itemSubNumSn, itemConCode);

                if (checkMapping.Count > 0)
                {
                    item.MappingYn = "Y";
                }
                else
                {
                    item.MappingYn = "N";
                }
            }

            var usrViewsInfos = Mapper.Map<List<JoinMentorViewModel>>(usrViewsInfo);
            var usrViewsInfosOrderby = usrViewsInfo.OrderBy(bw => bw.MappingYn);

            int pagingSize = int.Parse(ConfigurationManager.AppSettings["PagingSize"]);

            return View(new StaticPagedList<JoinMentorViewModel>(usrViewsInfosOrderby.ToPagedList(int.Parse(curPage ?? "1"), pagingSize), int.Parse(curPage ?? "1"), pagingSize, usrViewsInfos.Count));

        }

        [HttpPost]
        public async Task<ActionResult> MentorCompMapping(JoinMentorViewModel paramodel)
        {

            ViewBag.naviLeftMenu = Global.MentorMng;

            // compSn을 이용하여 vc_comp_mapping에서 ba_sn, num_sn 추출

            var compSn = int.Parse(paramodel.CompSn);

            SqlParameter loginId = new SqlParameter("LOGIN_ID", Session[Global.LoginID]);   // sql 파라미터 값 설정
            object[] parameters = new object[] { loginId };                                 // 객체에 데이터 삽입

            var obj = await procBaMentorMapping.getBaMentorNonMapping(parameters);          // 해당 기업과 관련된 리스트데이터 가져오기
            var baSn = obj[0].BA_SN;

            var compMappingInfo = await vcCompInfoService.getCompMappingInfosList(compSn, baSn);

            var numSn = compMappingInfo[0].NumSn;

            VcMentorMapping mentorMapping = new VcMentorMapping();
            mentorMapping.CompSn = int.Parse(paramodel.CompSn);
            mentorMapping.MentorSn = int.Parse(paramodel.MentorSn);
            mentorMapping.BaSn = compMappingInfo[0].BaSn;
            mentorMapping.NumSn = compMappingInfo[0].NumSn;
            mentorMapping.SubNumSn = compMappingInfo[0].SubNumSn;
            mentorMapping.ConCode = paramodel.ConCode;
            mentorMapping.RegDt = DateTime.Now;
            mentorMapping.UpdDt = DateTime.Now;

            // 해당 기업의 매핑이 처음인지 아닌지 확인
            // 해당 BA가 WRITE_YN을 가지고 있을 때 Y에 대한 권한을 주는 과정
            var checkMapping = await vcMentorMappingService.getCheckMapping(compSn, baSn, numSn);
            var checkBaWriterList = await vcCompInfoService.checkBaWriterList(compSn, baSn, numSn);
            var checkBaWiret = checkBaWriterList.Where(bw => bw.WriteYn == "Y").ToList();

            if(checkBaWiret.Count != 0)
            {
                // BA가 작성 권한이 있다는 이야기
                if(checkMapping.Count == 0)
                {
                    // 작성 권한이 있는 BA가 첫번째로 할당해주는 멘토에게 작성 권한 부여
                    mentorMapping.WriteYn = "Y";
                }
                else
                {
                    mentorMapping.WriteYn = "N";
                }

            }
            else
            {
                mentorMapping.WriteYn = "N";
            }


            vcMentorMappingService.insertMentorMapping(mentorMapping);

            //await _scUsrService.SaveDbContextAsync();

            return RedirectToAction("MentorCompMapping", "MentorMng", new { area = "BizManager" });
        }


        //public async Task<ActionResult> MentorCompMappings(JoinMentorViewModel paramodel)
        //{
        //    ViewBag.naviLeftMenu = Global.MentorMng;

        //    // compSn을 이용하여 vc_comp_mapping에서 ba_sn, num_sn 추출

        //    var compSn = int.Parse(paramodel.CompSn);

        //    SqlParameter loginId = new SqlParameter("LOGIN_ID", Session[Global.LoginID]);   // sql 파라미터 값 설정
        //    object[] parameters = new object[] { loginId };                                 // 객체에 데이터 삽입

        //    var obj = await procBaMentorMapping.getBaMentorNonMapping(parameters);          // 해당 기업과 관련된 리스트데이터 가져오기
        //    var baSn = obj[0].BA_SN;

        //    var compMappingInfo = await vcCompInfoService.getCompMappingInfosList(compSn, baSn);

        //    var numSn = compMappingInfo[0].NumSn;

        //    VcMentorMapping mentorMapping = new VcMentorMapping();
        //    mentorMapping.CompSn = int.Parse(paramodel.CompSn);
        //    mentorMapping.MentorSn = int.Parse(paramodel.MentorSn);
        //    mentorMapping.BaSn = compMappingInfo[0].BaSn;
        //    mentorMapping.NumSn = compMappingInfo[0].NumSn;
        //    mentorMapping.SubNumSn = compMappingInfo[0].SubNumSn;
        //    mentorMapping.ConCode = paramodel.ConCode;
        //    mentorMapping.RegDt = DateTime.Now;
        //    mentorMapping.UpdDt = DateTime.Now;

        //    // 해당 기업의 매핑이 처음인지 아닌지 확인
        //    // 해당 BA가 WRITE_YN을 가지고 있을 때 Y에 대한 권한을 주는 과정
        //    var checkMapping = await vcMentorMappingService.getCheckMapping(compSn, baSn, numSn);
        //    var checkBaWriter = await vcCompInfoService.checkBaWriter(compSn, baSn, numSn, mentorMapping.ConCode);

        //    if (checkBaWriter.WriteYn == "Y")
        //    {
        //        if (checkMapping == null)
        //        {
        //            mentorMapping.WriteYn = "Y";
        //        }
        //        else
        //        {
        //            mentorMapping.WriteYn = "N";
        //        }
        //    }
        //    else
        //    {
        //        mentorMapping.WriteYn = "N";
        //    }

        //    vcMentorMappingService.insertMentorMapping(mentorMapping);

        //    await _scUsrService.SaveDbContextAsync();

        //    return RedirectToAction("MentorCompMapping", "MentorMng", new { area = "BizManager" });
        //}

        [HttpPost]
        public async Task<JsonResult> GetCompNm(int compSn)
        {

            var baLoginKey = Session[Global.LoginID].ToString();
            var baInfo = await _scUsrService.getBaInfoById(baLoginKey);

            var compInfo = await vcCompInfoService.getCompMappingInfosList(compSn, baInfo.BaSn);

            var dropDwonConCode = Mapper.Map<IList<JoinMentorViewModel>>(compInfo);

            JoinMentorViewModel title = new JoinMentorViewModel();
            title.ConCode = "분야 코드 선택";

            dropDwonConCode.Insert(0, title);

            SelectList conCodeListSelect = new SelectList(dropDwonConCode, "ConCode", "ConCode");

            return Json(conCodeListSelect);

        }

        [HttpPost]
        public async Task<JsonResult> CheckCompMapping(int compSn, string conCode)
        {

            var checkMapping = await vcMentorMappingService.checkCompConCodeMapping(compSn, conCode);

            string status = "Y";


            if (checkMapping.Count > 0)
            {
                // 이미 매핑된 기업이 있을경우
                status = "N";
            }else
            {
                // 이미 매핑된 기업이 없을 경우
                status = "Y";
            }

            return Json(status);

        }


    }
}