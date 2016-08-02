using AutoMapper;
using BizOneShot.Light.Models.CustomModels;
using BizOneShot.Light.Models.WebModels;
using BizOneShot.Light.Services;
using BizOneShot.Light.Web.ComLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BizOneShot.Light.Web.Controllers
{
    public class ApiController : BaseController
    {
        private readonly IVcIfTableService _vcIfTableService;
        private readonly IScUsrService _scUsrService;
        private readonly IVcCompInfoService _vcCompInfoService;
        private readonly IQuesMasterService _quesMasterService;
        private readonly IVcBaInfoService _vcBaInfoService;

        // add Loy
        private readonly IQuesCompInfoService _quesCompInfoService;

        // GET: Api
        public ActionResult Index()
        {
            return View();
        }

        // Constructor...
        public ApiController
        ( IVcIfTableService _vcIfTableService,
          IScUsrService _scUsrService,
          IVcCompInfoService _vcCompInfoService,
          IQuesMasterService _quesMasterService,
          IVcBaInfoService _vcBaInfoService,
          IQuesCompInfoService _quesCompInfoService
        )
        {
            this._vcIfTableService = _vcIfTableService;
            this._scUsrService = _scUsrService;
            this._vcCompInfoService = _vcCompInfoService;
            this._quesMasterService = _quesMasterService;
            this._vcBaInfoService = _vcBaInfoService;
            this._quesCompInfoService = _quesCompInfoService;
        }

        // 데이터 넣기 전에 DB정립 필요
        // 항목 정리 필요
        // DB항목 바꾸면 문제가 생길지 체크 필요


        // 1. VcIfUsrInfo
        [HttpPost]
        public async Task<JsonResult> vc_if_001(VcIfUsrInfo vcIfUsrInfo)
        {
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");

            VcUsrInfo vcUsrInfo = new VcUsrInfo();  // Master Table 객체, 새 값이 들어갈 때 사용한다
            string status = "S";
            string data = "";
            string infId = vcIfUsrInfo.InfId;

            // 객체의 필수값 field 확인
            // if 들어갈 때, infId는 미존재하면 안되므로 
            if (infId != null)
            {
                vcIfUsrInfo.InsertYn = "N";
                vcIfUsrInfo.InsertStatus = "N";
                await _vcIfTableService.insertVcIfUsrInfo(vcIfUsrInfo); // 1 | null | null | N | N

                if (vcIfUsrInfo.TcmsLoginKey == 0 || vcIfUsrInfo.LoginId == null)
                {
                    // infid를 통해 해당 객체 조회해오기
                    var ifObj = await _vcIfTableService.getVcIfUsrInfoByInfId(infId);
                    ifObj.InsertStatus = "E";
                    await _vcIfTableService.SaveDbContextAsync();

                    // 에러 난 내용을 JsonResult에 넣어서 return
                    status = "E";
                    data = "{\"status\":\"" + status + "\"}";
                    return Json(data);
                }
                else
                {
                    var usrSelect = await _scUsrService.selectScUsrByTcms(vcIfUsrInfo.TcmsLoginKey.ToString());

                    var ifObj = await _vcIfTableService.getVcIfUsrInfoByInfId(infId);

                    if (usrSelect == null) // 처음 넣는 값이면
                    {
                        // 최상단에 선언해 둔, VcUsrInfo를 사용한다
                        vcUsrInfo.LoginId = vcIfUsrInfo.LoginId;
                        vcUsrInfo.UsrType = vcIfUsrInfo.UsrType;

                        vcUsrInfo.RegDt = DateTime.Now;
                        vcUsrInfo.UpdDt = DateTime.Now;

                        vcUsrInfo.TypeYn = vcIfUsrInfo.TypeYn;

                        vcUsrInfo.Name = vcIfUsrInfo.Name;
                        vcUsrInfo.Email = vcIfUsrInfo.Email;
                        vcUsrInfo.TelNo = vcIfUsrInfo.TelNo;
                        vcUsrInfo.MbNo = vcIfUsrInfo.MbNo;
                        vcUsrInfo.PostNo = vcIfUsrInfo.PostNo;
                        vcUsrInfo.Addr1 = vcIfUsrInfo.Addr1;
                        vcUsrInfo.FaxNo = vcIfUsrInfo.FaxNo;
                        vcUsrInfo.TcmsLoginKey = vcIfUsrInfo.TcmsLoginKey;
                        vcUsrInfo.AgreeYn = "N";

                        ifObj.InsertYn = "Y";
                        ifObj.InsertStatus = "E";
                        await _vcIfTableService.SaveDbContextAsync();

                        _scUsrService.insertVcUsrInfo(vcUsrInfo); // add 후 savedbcontext까지 완료된 method

                        // interface table 식별자들 변경 후 저장
                        ifObj.InsertYn = "Y";
                        ifObj.InsertStatus = "S";
                        await _vcIfTableService.SaveDbContextAsync();

                        // 성공된 값을 json 데이터로 전송
                        status = "S";
                        data = "{\"status\":\"" + status + "\"}";
                        return Json(data);
                    }
                    else // 이미 존재하는 값이면
                    {
                        // 방금 찾았던 객체를 이용한다
                        usrSelect.LoginId = vcIfUsrInfo.LoginId;
                        usrSelect.UsrType = vcIfUsrInfo.UsrType;

                        usrSelect.UpdDt = DateTime.Now;     // update 시각

                        usrSelect.Name = vcIfUsrInfo.Name;
                        usrSelect.Email = vcIfUsrInfo.Email;
                        usrSelect.TelNo = vcIfUsrInfo.TelNo;
                        usrSelect.MbNo = vcIfUsrInfo.MbNo;
                        usrSelect.PostNo = vcIfUsrInfo.PostNo;
                        usrSelect.Addr1 = vcIfUsrInfo.Addr1;
                        usrSelect.FaxNo = vcIfUsrInfo.FaxNo;
                        usrSelect.TcmsLoginKey = vcIfUsrInfo.TcmsLoginKey;

                        ifObj.InsertYn = "Y";
                        ifObj.InsertStatus = "E";
                        await _vcIfTableService.SaveDbContextAsync();

                        _scUsrService.SaveDbContext();       // update된 내용 저장

                        // interface table 식별자들 변경 후 저장
                        ifObj.InsertYn = "Y";
                        ifObj.InsertStatus = "S";
                        await _vcIfTableService.SaveDbContextAsync();

                        // 성공된 값을 json 데이터로 전송
                        status = "S";
                        data = "{\"status\":\"" + status + "\"}";
                        return Json(data);
                    }
                }
            }
            else
            {
                // infId조차 없는 데이터는 인터페이스 테이블에 넣을 수 없다 그러므로
                // 상태를 표시하여 json 데이터만 넘겨주고 종료
                status = "E";
                data = "{\"status\":\"" + status + "\"}";
                return Json(data);
                // 실제 table에 데이터 넣기
            }
        }

        // 2. VcIfCompInfo
        // VC_COMP_INFO안에 값이 들어가면 해당 REGISTRATION_NO와 현재 년도로
        // QUES_MASTER에 한 줄 넣어줘야 한다
        // 
        [HttpPost]
        public async Task<JsonResult> vc_if_002(VcIfCompInfo vcIfCompInfo)
        {
            VcCompInfo vcCompInfo = new VcCompInfo();
            QuesMaster quesMaster = new QuesMaster();

            string status = "S";
            string data = "";
            string infId = vcIfCompInfo.InfId;

            // 객체의 필수값 field 확인
            // if 들어갈 때, infId는 미존재하면 안되므로 
            if (infId != null)
            {
                vcIfCompInfo.InsertYn = "N";
                vcIfCompInfo.InsertStatus = "N";
                await _vcIfTableService.insertVcIfCompInfo(vcIfCompInfo);

                if (vcIfCompInfo.TcmsLoginKey == 0 || vcIfCompInfo.CompNm == null)
                {
                    var ifObj = await _vcIfTableService.getVcIfCompInfoByInfId(infId);
                    ifObj.InsertStatus = "E";
                    await _vcIfTableService.SaveDbContextAsync();

                    // 에러 난 내용을 JsonResult에 넣어서 return
                    status = "E";
                    data = "{\"status\":\"" + status + "\"}";
                    return Json(data);
                }
                else 
                {
                    var SelectComp = await _vcCompInfoService.getVcCompInfoByKey(vcIfCompInfo.TcmsLoginKey);
                    var SelectCompId = await _scUsrService.selectScUsrByTcms(Convert.ToString(vcIfCompInfo.TcmsLoginKey));

                    var ifObj = await _vcIfTableService.getVcIfCompInfoByInfId(infId);

                    if (SelectComp == null) // 없던 놈이면 insert
                    {
                        // vcCompInfo
                        vcCompInfo.LoginId = SelectCompId.LoginId;
                        vcCompInfo.TcmsLoginKey = vcIfCompInfo.TcmsLoginKey;
                        vcCompInfo.RegistrationNo = vcIfCompInfo.RegistrationSn;
                        vcCompInfo.CompNm = vcIfCompInfo.CompNm;
                        vcCompInfo.OwnEmail = vcIfCompInfo.OwnEmail;
                        vcCompInfo.OwnTelNo = vcIfCompInfo.OwnTelNo;
                        vcCompInfo.OwnNm = vcIfCompInfo.OwnNm;
                        vcCompInfo.RegDt = DateTime.Now;
                        vcCompInfo.UpdDt = DateTime.Now;

                        // quesMaster
                        quesMaster.RegistrationNo = vcIfCompInfo.RegistrationSn;
                        quesMaster.BasicYear = DateTime.Now.Year;
                        quesMaster.SaveStatus = 1;
                        quesMaster.Status = "P";

                        ifObj.InsertYn = "Y";
                        ifObj.InsertStatus = "E";
                        await _vcIfTableService.SaveDbContextAsync();

                        _vcCompInfoService.insertVcCompInfo(vcCompInfo);
                        _quesMasterService.insertQuescomp(quesMaster);

                        ifObj.InsertStatus = "S";
                        await _vcIfTableService.SaveDbContextAsync();

                        status = "S";
                        data = "{\"status\":\"" + status + "\"}";
                        return Json(data);
                    }
                    else // 있던 놈이면 Update
                    {
                        SelectComp.LoginId = SelectCompId.LoginId;
                        SelectComp.TcmsLoginKey = vcIfCompInfo.TcmsLoginKey;
                        SelectComp.RegistrationNo = vcIfCompInfo.RegistrationSn;
                        SelectComp.CompNm = vcIfCompInfo.CompNm;
                        SelectComp.OwnEmail = vcIfCompInfo.OwnEmail;
                        SelectComp.OwnTelNo = vcIfCompInfo.OwnTelNo;
                        SelectComp.OwnNm = vcIfCompInfo.OwnNm;
                        SelectComp.UpdDt = DateTime.Now;

                        ifObj.InsertYn = "Y";
                        ifObj.InsertStatus = "E";

                        await _vcIfTableService.SaveDbContextAsync();

                        _vcCompInfoService.SaveDbContext();

                        ifObj.InsertStatus = "S";
                        await _vcIfTableService.SaveDbContextAsync();

                        status = "S";
                        data = "{\"status\":\"" + status + "\"}";
                        return Json(data);
                    }
                }

            }
            else
            {
                status = "E";
                data = "{\"status\":\"" + status + "\"}";
                return Json(data);
            }
        }

        //compinfo 비교
        // 3. VcIfBaInfo
        [HttpPost]
        public async Task<JsonResult> vc_if_003(VcIfBaInfo vcIfBaInfo)
        {
            VcBaInfo vcBaInfo = new VcBaInfo();

            string status = "S";
            string data = "";
            string infId = vcIfBaInfo.InfId;

            // 전달받은 객체를 해당 table에 insert하게 될 부분 
            if (infId != null)
            {
                vcIfBaInfo.InsertYn = "N";
                vcIfBaInfo.InsertStatus = "N";
                await _vcIfTableService.insertVcIfBaInfo(vcIfBaInfo);

                if (vcIfBaInfo.InfId == null || vcIfBaInfo.TcmsLoginKey == 0)
                {
                    var ifObj = await _vcIfTableService.getVcIfBaInfoInfId(infId);
                    ifObj.InsertStatus = "E";
                    await _vcIfTableService.SaveDbContextAsync();

                    // 에러 난 내용을 JsonResult에 넣어서 return
                    status = "E";
                    data = "{\"status\":\"" + status + "\"}";
                    return Json(data);
                }
                else
                {
                    var selectBa = await _vcBaInfoService.getVcBaInfoByKey(vcIfBaInfo.TcmsLoginKey);
                    var ifObj = await _vcIfTableService.getVcIfBaInfoInfId(infId);
                    if (selectBa == null)
                    {
                        vcBaInfo.TcmsLoginKey = vcIfBaInfo.TcmsLoginKey;
                        vcBaInfo.RegistrationNo = vcIfBaInfo.RegistrationSn;
                        vcBaInfo.BaNm = vcIfBaInfo.BaNm;
                        vcBaInfo.BaOwnNm = vcIfBaInfo.BaOwnNm;
                        vcBaInfo.BaEmail = vcIfBaInfo.BaEmail;
                        vcBaInfo.BaTelNo = vcIfBaInfo.BaTelNo;
                        vcBaInfo.RegDt = DateTime.Now;
                        vcBaInfo.UpdDt = DateTime.Now;

                        ifObj.InsertYn = "Y";
                        ifObj.InsertStatus = "E";
                        await _vcIfTableService.SaveDbContextAsync();

                        _vcBaInfoService.insertVcBaInfo(vcBaInfo);

                        // interface table 식별자들 변경 후 저장
                        ifObj.InsertStatus = "S";
                        await _vcIfTableService.SaveDbContextAsync();

                        // 성공된 값을 json 데이터로 전송
                        status = "S";
                        data = "{\"status\":\"" + status + "\"}";
                        return Json(data);
                    }
                    else
                    {
                        selectBa.TcmsLoginKey = vcIfBaInfo.TcmsLoginKey;
                        selectBa.RegistrationNo = vcIfBaInfo.RegistrationSn;
                        selectBa.BaNm = vcIfBaInfo.BaNm;
                        selectBa.BaOwnNm = vcIfBaInfo.BaOwnNm;
                        selectBa.BaEmail = vcIfBaInfo.BaEmail;
                        selectBa.BaTelNo = vcIfBaInfo.BaTelNo;
                        selectBa.UpdDt = DateTime.Now;

                        ifObj.InsertYn = "Y";
                        ifObj.InsertStatus = "E";
                        await _vcIfTableService.SaveDbContextAsync();

                        _vcBaInfoService.SaveDbContext();

                        ifObj.InsertStatus = "S";
                        await _vcIfTableService.SaveDbContextAsync();
                        // 성공된 값을 json 데이터로 전송
                        status = "S";
                        data = "{\"status\":\"" + status + "\"}";
                        return Json(data);
                    }
                }
            }
            else
            {
                // infId조차 없는 데이터는 인터페이스 테이블에 넣을 수 없다 그러므로
                // 상태를 표시하여 json 데이터만 넘겨주고 종료
                status = "E";
                data = "{\"status\":\"" + status + "\"}";
                return Json(data);
                // 실제 table에 데이터 넣기
            }

        }

        // 4. VcIfMentorInfo... 까다로움
        [HttpPost]
        public async Task<JsonResult> vc_if_004(VcIfMentorInfo vcIfMentorInfo)
        {
            VcMentorInfo vcMentorInfo = new VcMentorInfo();

            string status = "S";
            string data = "";
            string infId = vcIfMentorInfo.InfId;

            // object data 체크
            if (infId != null)
            {
                vcIfMentorInfo.InsertYn = "N";
                vcIfMentorInfo.InsertStatus = "N";
                await _vcIfTableService.insertVcIfMentorInfo(vcIfMentorInfo);

                if (vcIfMentorInfo.InfId == null || vcIfMentorInfo.BaLoginKey == 0 || vcIfMentorInfo.MentorLoginKey == 0)
                {
                    var ifObj = await _vcIfTableService.getVcIfMentorInfId(infId);
                    ifObj.InsertStatus = "E";
                    await _vcIfTableService.SaveDbContextAsync();

                    // 에러 난 내용을 JsonResult에 넣어서 return
                    status = "E";
                    data = "{\"status\":\"" + status + "\"}";
                    return Json(data);
                }
                else
                {
                    var selectMentor = await _scUsrService.getMentorInfo(Convert.ToString(vcIfMentorInfo.MentorLoginKey));
                    var selectBa = await _vcBaInfoService.getVcBaInfoByKey(vcIfMentorInfo.BaLoginKey);
                    var ifObj = await _vcIfTableService.getVcIfMentorInfId(infId);

                    if (selectMentor == null)
                    {
                        vcMentorInfo.TcmsLoginKey = vcIfMentorInfo.MentorLoginKey;
                        vcMentorInfo.BaSn = selectBa.BaSn;
                        vcMentorInfo.RegDt = DateTime.Now;
                        vcMentorInfo.UpdDt = DateTime.Now;

                        ifObj.InsertYn = "Y";
                        ifObj.InsertStatus = "E";
                        await _vcIfTableService.SaveDbContextAsync();

                        _scUsrService.insertMentorInfo(vcMentorInfo);

                        // interface table 식별자들 변경 후 저장
                        ifObj.InsertStatus = "S";
                        await _vcIfTableService.SaveDbContextAsync();

                        // 성공된 값을 json 데이터로 전송
                        status = "S";
                        data = "{\"status\":\"" + status + "\"}";
                        return Json(data);
                    }
                    else
                    {
                        selectMentor.TcmsLoginKey = vcIfMentorInfo.MentorLoginKey;
                        selectMentor.BaSn = selectBa.BaSn;
                        selectMentor.UpdDt = DateTime.Now;

                        ifObj.InsertYn = "Y";
                        ifObj.InsertStatus = "E";
                        await _vcIfTableService.SaveDbContextAsync();

                        _scUsrService.SaveDbContext();

                        ifObj.InsertStatus = "S";
                        await _vcIfTableService.SaveDbContextAsync();

                        // 성공된 값을 json 데이터로 전송
                        status = "S";
                        data = "{\"status\":\"" + status + "\"}";
                        return Json(data);
                    }
                }
            }
            else
            {
                // infId조차 없는 데이터는 인터페이스 테이블에 넣을 수 없다 그러므로
                // 상태를 표시하여 json 데이터만 넘겨주고 종료
                status = "E";
                data = "{\"status\":\"" + status + "\"}";
                return Json(data);
                // 실제 table에 데이터 넣기
            }
        }

        // 5. VcIfNumInfo... 무난함
        [HttpPost]
        public async Task<JsonResult> vc_if_005(VcIfNumInfo vcIfNumInfo)
        {
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");

            VcNumMngInfo vcNumMngInfo = new VcNumMngInfo();
            string status = "S";
            string data = "";
            string infId = vcIfNumInfo.InfId;

            // 객체의 필수값 확인 null check
            if (infId != null)
            {
                vcIfNumInfo.InsertYn = "N";
                vcIfNumInfo.InsertStatus = "N";

                await _vcIfTableService.insertNumMngForIf(vcIfNumInfo);

                if (vcIfNumInfo.BizWorkNm == null)
                {
                    var ifObj = await _vcIfTableService.getVcIfNumInfoByInfId(infId);
                    ifObj.InsertStatus = "E";
                    await _vcIfTableService.SaveDbContextAsync();

                    // 에러 난 내용을 JsonResult에 넣어서 return
                    status = "E";
                    data = "{\"status\":\"" + status + "\"}";
                    return Json(data);
                }
                else
                {
                    // 본테이블에 들어가는 부분 구현

                    var checkNumSn = await _scUsrService.getNumInfoAsync("1");
                    var ifObj = await _vcIfTableService.getVcIfNumInfoByInfId(infId);

                    if (checkNumSn == null)
                    {
                        //insert 부분 
                        vcNumMngInfo.BizStDt = vcIfNumInfo.BizStDt;
                        vcNumMngInfo.BizEdDt = vcIfNumInfo.BizEdDt;
                        vcNumMngInfo.BizWorkNm = vcIfNumInfo.BizWorkNm;
                        vcNumMngInfo.BizWorkSummary = vcIfNumInfo.BizWorkSummary;
                        vcNumMngInfo.NumSn = "1";

                        ifObj.InsertYn = "Y";
                        ifObj.InsertStatus = "E";
                        await _vcIfTableService.SaveDbContextAsync();

                        //var vcNumMngInfoMapper = Mapper.Map<VcNumMngInfo>(vcNumMngInfo);
                        int result = await _scUsrService.insertNumMngInfo(vcNumMngInfo);

                        // interface table 식별자들 변경 후 저장
                        ifObj.InsertYn = "Y";
                        ifObj.InsertStatus = "S";
                        await _vcIfTableService.SaveDbContextAsync();

                        // 해당 데이터 전송의 결과 값을 response로 리턴
                        data = "{\"status\":\"" + status + "\"}";

                        return Json(data);
                    }
                    else
                    {
                        //update 부분 
                        checkNumSn.BizStDt = vcIfNumInfo.BizStDt;
                        checkNumSn.BizEdDt = vcIfNumInfo.BizEdDt;
                        checkNumSn.BizWorkNm = vcIfNumInfo.BizWorkNm;
                        checkNumSn.BizWorkSummary = vcIfNumInfo.BizWorkSummary;
                        checkNumSn.NumSn = "1";

                        await _scUsrService.SaveDbContextAsync();       // update된 내용 저장

                        // interface table 식별자들 변경 후 저장
                        ifObj.InsertYn = "Y";
                        ifObj.InsertStatus = "S";
                        await _vcIfTableService.SaveDbContextAsync();

                        // 해당 데이터 전송의 결과 값을 response로 리턴
                        data = "{\"status\":\"" + status + "\"}";

                        return Json(data);
                    }
                }
            }
            else
            {
                status = "E";
                data = "{\"status\":\"" + status + "\"}";
                return Json(data);
            }

        }

        // 6. vcIfTcmsInfo
        [HttpPost]
        public async Task<JsonResult> vc_if_006(VcIfTcmsInfo vcIfTcmsInfo)
        {
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            VcTcmsInfo vcTcmsInfo = new VcTcmsInfo();
            string status = "S";
            string data = "";
            string infId = vcIfTcmsInfo.InfId;

            // 객체의 필수값 field 확인
            if (infId != null)
            {
                vcIfTcmsInfo.InsertYn = "N";
                vcIfTcmsInfo.InsertStatus = "N";

                await _vcIfTableService.insertTcmsInfoForIf(vcIfTcmsInfo);

                // 중요 key 값들 check
                if (vcIfTcmsInfo.InfId == null || vcIfTcmsInfo.TcmsLoginKey == 0)
                {
                    var ifObj = await _vcIfTableService.getVcIfTcmsInfoByInfId(infId);
                    ifObj.InsertStatus = "E";
                    await _vcIfTableService.SaveDbContextAsync();

                    // 에러 난 내용을 JsonResult에 넣어서 return
                    status = "E";
                    data = "{\"status\":\"" + status + "\"}";
                    return Json(data);

                }
                else
                {
                    // 문제 없어서 본테이블로 저장
                    var checkTcmsLoginKey = await _scUsrService.getTcmsInfo(vcIfTcmsInfo.TcmsLoginKey);
                    var ifObj = await _vcIfTableService.getVcIfTcmsInfoByInfId(infId);

                    if (checkTcmsLoginKey == null)
                    {

                        // VcTcmsInfo에 Insert
                        vcTcmsInfo.TcmsLoginKey = vcIfTcmsInfo.TcmsLoginKey;
                        vcTcmsInfo.TypeYn = "Y";
                        vcTcmsInfo.RegDt = DateTime.Now;

                        ifObj.InsertYn = "Y";
                        ifObj.InsertStatus = "E";
                        await _vcIfTableService.SaveDbContextAsync();

                        //var vcTcmsInfoMapper = Mapper.Map<VcTcmsInfo>(vcTcmsInfo);
                        int result = await _scUsrService.insertTcmsInfo(vcTcmsInfo);

                        // interface table 식별자들 변경 후 저장
                        ifObj.InsertYn = "Y";
                        ifObj.InsertStatus = "S";
                        await _vcIfTableService.SaveDbContextAsync();

                        status = "S";
                        data = "{\"status\":\"" + status + "\"}";
                        // 해당 데이터 전송의 결과 값을 response로 리턴
                        return Json(data);

                    }
                    else
                    {
                        // update 
                        checkTcmsLoginKey.TcmsLoginKey = vcIfTcmsInfo.TcmsLoginKey;
                        checkTcmsLoginKey.RegDt = DateTime.Now;
                        checkTcmsLoginKey.TypeYn = "N";

                        await _scUsrService.SaveDbContextAsync();

                        // interface table 식별자들 변경 후 저장
                        ifObj.InsertYn = "Y";
                        ifObj.InsertStatus = "S";
                        await _vcIfTableService.SaveDbContextAsync();

                        // 성공된 값을 json 데이터로 전송
                        status = "S";
                        data = "{\"status\":\"" + status + "\"}";
                        return Json(data);

                    }
                }
            }
            else
            {
                // infId조차 없는 데이터는 인터페이스 테이블에 넣을 수 없다 그러므로
                // 상태를 표시하여 json 데이터만 넘겨주고 종료
                status = "E";
                data = "{\"status\":\"" + status + "\"}";
                return Json(data);
            }
        }

        // 7. vcIfTcmsInfo 
        [HttpPost]
        public async Task<JsonResult> vc_if_007(VcIfCompMapping vcIfCompMapping)
        {
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            VcCompMapping vcCompMapping = new VcCompMapping();
            string status = "S";
            string data = "";
            string infId = vcIfCompMapping.InfId;

            if (infId != null)
            {
                vcIfCompMapping.InsertYn = "N";
                vcIfCompMapping.InsertStatus = "N";
                await _vcIfTableService.insertCompMappingForIf(vcIfCompMapping);

                if (vcIfCompMapping.InfId == null || vcIfCompMapping.CompLoginKey == 0 ||
                    vcIfCompMapping.BaLoginKey == 0 ||
                    vcIfCompMapping.NumSn == null ||
                    vcIfCompMapping.SubNumSn == null)
                {

                    var ifObj = await _vcIfTableService.getVcIfCompMappingByInfId(infId);
                    ifObj.InsertStatus = "E";
                    await _vcIfTableService.SaveDbContextAsync();

                    // 에러 난 내용을 JsonResult에 넣어서 return
                    status = "E";
                    data = "{\"status\":\"" + status + "\"}";
                    return Json(data);

                }
                else
                {
                    var ifObj = await _vcIfTableService.getVcIfCompMappingByInfId(infId);

                    // Comp_Login_key를 이용하여 CompSn 조회
                    var compLoginKey = vcIfCompMapping.CompLoginKey;
                    var compSn = await _vcCompInfoService.getVcCompInfoByLoginKey(compLoginKey);

                    // Ba_Login_key를 이용하여 BaSn 조회 
                    var baLoginKey = vcIfCompMapping.BaLoginKey;
                    var baSn = await _vcBaInfoService.getVcBaInfoByLoginKey(baLoginKey);

                    var cpMpCodeCheck = Convert.ToString(compSn[0].CompSn) + Convert.ToString(baSn[0].BaSn) + vcIfCompMapping.NumSn+ vcIfCompMapping.SubNumSn + vcIfCompMapping.ConCode;
                    var checkCompMapping = await _vcCompInfoService.getCompMappingByCpMpCode(cpMpCodeCheck);

                    if (checkCompMapping == null)
                    {
                        //insert
                        vcCompMapping.CompSn = compSn[0].CompSn;
                        vcCompMapping.BaSn = baSn[0].BaSn;
                        vcCompMapping.NumSn = vcIfCompMapping.NumSn;
                        vcCompMapping.SubNumSn = vcIfCompMapping.SubNumSn;
                        vcCompMapping.ConCode = vcIfCompMapping.ConCode;
                        vcCompMapping.WriteYn = vcIfCompMapping.WriteYn;
                        vcCompMapping.RegDt = DateTime.Now;
                        //vcCompMapping.TypeYn = "Y";
                        //checkCompMapping.TypeYn = "Y";

                        var cpMpCode = Convert.ToString(compSn[0].CompSn) + Convert.ToString(baSn[0].BaSn) + vcIfCompMapping.NumSn + vcIfCompMapping.SubNumSn + vcIfCompMapping.ConCode;

                        vcCompMapping.CpMpCode = cpMpCode;

                        ifObj.InsertYn = "Y";
                        ifObj.InsertStatus = "E";
                        await _vcIfTableService.SaveDbContextAsync();

                        //var vcCompMappingMapper = Mapper.Map<VcCompMapping>(vcCompMapping);
                        int result = await _vcCompInfoService.insertCompMapping(vcCompMapping);

                        // interface table 식별자들 변경 후 저장
                        ifObj.InsertYn = "Y";
                        ifObj.InsertStatus = "S";
                        await _vcIfTableService.SaveDbContextAsync();

                        status = "S";
                        data = "{\"status\":\"" + status + "\"}";
                        return Json(data);

                    }
                    else
                    {
                        //update
                        checkCompMapping.CompSn = compSn[0].CompSn;
                        checkCompMapping.BaSn = baSn[0].BaSn;
                        checkCompMapping.NumSn = vcIfCompMapping.NumSn;
                        checkCompMapping.SubNumSn = vcIfCompMapping.SubNumSn;
                        checkCompMapping.ConCode = vcIfCompMapping.ConCode;
                        checkCompMapping.WriteYn = vcIfCompMapping.WriteYn;
                        checkCompMapping.RegDt = DateTime.Now;
                        checkCompMapping.TypeYn = "Y";

                        var cpMpCode = Convert.ToString(compSn[0].CompSn) + Convert.ToString(baSn[0].BaSn) + vcIfCompMapping.NumSn + vcIfCompMapping.SubNumSn + vcIfCompMapping.ConCode;

                        vcCompMapping.CpMpCode = cpMpCode;

                        await _scUsrService.SaveDbContextAsync();

                        // interface table 식별자들 변경 후 저장
                        ifObj.InsertYn = "Y";
                        ifObj.InsertStatus = "S";
                        await _vcIfTableService.SaveDbContextAsync();

                        // 성공된 값을 json 데이터로 전송
                        status = "S";
                        data = "{\"status\":\"" + status + "\"}";
                        return Json(data);

                    }

                }

            }
            else
            {
                status = "E";
                data = "{\"status\":\"" + status + "\"}";
                return Json(data);
            }

        }

        // ********* Ques_Comp_Info 테이블 참고 *********
        // 8. vcIfQuesCompInfom ... QuestionSn에 대한 문제 해결이 필요하다
        // 넣어 줄 때, Ques CompInfo를 기준으로 Question_sn을 가져와서 넣어줘야 한다
        [HttpPost]
        public async Task<JsonResult> vc_if_008(VcIfQuesCompInfo vcIfQuesCompInfo)
        {
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");

            QuesCompInfo quesCompInfo = new QuesCompInfo();
            QuesWriter quesWriter = new QuesWriter();   // 추가

            string status = "S";
            string data = "";
            string infId = vcIfQuesCompInfo.InfId;
            int basicYear = DateTime.Now.Year;

            if (infId != null)
            {
                // infId가 존재하는것으로부터 시작
                vcIfQuesCompInfo.InsertYn = "N";
                vcIfQuesCompInfo.InsertStatus = "N";

                await _vcIfTableService.insertQuesCompInfoForIf(vcIfQuesCompInfo); // 일단 insert 되지 않는 상태를 N/N으로 세팅하고 저장

                if (vcIfQuesCompInfo.TcmsLoginKey == 0 || vcIfQuesCompInfo.RegistrationNo == null || vcIfQuesCompInfo.CompNm == null) // 해당 테이블에 꼭 필요한 값 체크
                {
                    // infid를 통해 해당 객체 조회해오기
                    var ifObj = await _vcIfTableService.getVcIfQuesCompInfoByInfId(infId);
                    ifObj.InsertStatus = "E";
                    await _vcIfTableService.SaveDbContextAsync();

                    // 에러 난 내용을 JsonResult에 넣어서 return
                    status = "E";
                    data = "{\"status\":\"" + status + "\"}";
                    return Json(data);
                }
                else // 필수 조건은 충족한다면 이제 실 테이블에 넣기
                {
                    var quesMaster = await _quesMasterService.GetQuesMasterAsyncPro(vcIfQuesCompInfo.RegistrationNo, basicYear);
                    var ifObj = await _vcIfTableService.getVcIfQuesCompInfoByInfId(infId);

                    if (quesMaster == null) // 관련된 object가 null이면 본 테이블에 넣을 questionSn을 가져올 수 없다
                    {
                        // infid를 통해 해당 객체 조회해오기
                        ifObj.InsertStatus = "E";
                        await _vcIfTableService.SaveDbContextAsync();

                        // 에러 난 내용을 JsonResult에 넣어서 return
                        status = "E";
                        data = "{\"status\":\"" + status + "\"}";
                        return Json(data);
                    }

                    // 실제 해당 객체 
                    var checkQuesMaster = await _quesCompInfoService.GetQuesCompInfoAsync(quesMaster.QuestionSn);

                    // 해당하는 questionSn을 가져와야하는데...
                    if (checkQuesMaster == null) // 처음 들어온 값은 insert
                    {
                        quesCompInfo.QuestionSn = quesMaster.QuestionSn;

                        quesCompInfo.CertiEtc = vcIfQuesCompInfo.CertiEtc;
                        quesCompInfo.CertiEtcText = vcIfQuesCompInfo.CertiEtcText;
                        quesCompInfo.CertiGreen = vcIfQuesCompInfo.CertiGreen;
                        quesCompInfo.CertiInnobiz = vcIfQuesCompInfo.CertiInnobiz;
                        quesCompInfo.CertiMainbiz = vcIfQuesCompInfo.CertiMainbiz;
                        quesCompInfo.CertiRnd = vcIfQuesCompInfo.CertiRnd;
                        quesCompInfo.CertiRoot = vcIfQuesCompInfo.CertiRoot;
                        quesCompInfo.CertiSocial = vcIfQuesCompInfo.CertiSocial;
                        quesCompInfo.CertiVenture = vcIfQuesCompInfo.CertiVenture;
                        quesCompInfo.CertiWoman = vcIfQuesCompInfo.CertiWoman;

                        quesCompInfo.CompAddr = vcIfQuesCompInfo.CompAddr;
                        quesCompInfo.CompLeaseYn = vcIfQuesCompInfo.CompLeaseYn;
                        quesCompInfo.CompNm = vcIfQuesCompInfo.CompNm;
                        quesCompInfo.CompType = vcIfQuesCompInfo.CompType;
                        quesCompInfo.CoRegistrationNo = vcIfQuesCompInfo.CoRegistrationNo;

                        quesCompInfo.Email = vcIfQuesCompInfo.Email;
                        quesCompInfo.EngCompNm = vcIfQuesCompInfo.EngCompNm;
                        //quesCompInfo.FacPossessYn = vcIfQuesCompInfo.FacPossessYn;
                        quesCompInfo.FactoryAddr = vcIfQuesCompInfo.FactoryAddr;
                        quesCompInfo.FactoryLeaseYn = vcIfQuesCompInfo.FactoryLeaseYn;
                        quesCompInfo.FaxNo = vcIfQuesCompInfo.FaxNo;
                        quesCompInfo.HomeUrl = vcIfQuesCompInfo.HomeUrl;

                        quesCompInfo.LabAddr = vcIfQuesCompInfo.LabAddr;
                        quesCompInfo.LabLeaseYn = vcIfQuesCompInfo.LabLeaseYn;

                        quesCompInfo.MainSellMarket = vcIfQuesCompInfo.MainSellMarket;
                        quesCompInfo.MarketCivil = vcIfQuesCompInfo.MarketCivil;
                        quesCompInfo.MarketConsumer = vcIfQuesCompInfo.MarketConsumer;
                        quesCompInfo.MarketEtc = vcIfQuesCompInfo.MarketEtc;
                        quesCompInfo.MarketEtcText = vcIfQuesCompInfo.MarketEtcText;
                        quesCompInfo.MarketForeing = vcIfQuesCompInfo.MarketForeign;
                        quesCompInfo.MarketPublic = vcIfQuesCompInfo.MarketPublic;

                        quesCompInfo.Name = vcIfQuesCompInfo.Name;
                        quesCompInfo.NumSn = vcIfQuesCompInfo.NumSn;

                        quesCompInfo.ProductNm1 = vcIfQuesCompInfo.ProductNm1;
                        quesCompInfo.ProductNm2 = vcIfQuesCompInfo.ProductNm2;
                        quesCompInfo.ProductNm3 = vcIfQuesCompInfo.ProductNm3;
                        quesCompInfo.PublishDt = vcIfQuesCompInfo.PublishDt;

                        quesCompInfo.RegistrationNo = vcIfQuesCompInfo.RegistrationNo;
                        quesCompInfo.ResidentEtc = vcIfQuesCompInfo.ResidentEtc;
                        //quesCompInfo.ResidenteEtcText = vcIfQuesCompInfo.ResidentEtcText;
                        quesCompInfo.ResidentType = vcIfQuesCompInfo.ResidentType;
                        //quesCompInfo.RndYn = vcIfQuesCompInfo.RndYn;
                        quesCompInfo.StandardCode1 = vcIfQuesCompInfo.StandardCode1;
                        quesCompInfo.StandardCode2 = vcIfQuesCompInfo.StandardCode2;
                        quesCompInfo.StandardCode3 = vcIfQuesCompInfo.StandardCode3;
                        quesCompInfo.TelNo = vcIfQuesCompInfo.TelNo;
                        quesCompInfo.TcmsLoginKey = vcIfQuesCompInfo.TcmsLoginKey;

                        quesCompInfo.RegDt = DateTime.Now;
                        quesCompInfo.UpdDt = DateTime.Now;

                        var result = await _quesCompInfoService.AddQuesCompInfoAsyncTo(quesCompInfo);

                        // QuesWriter를 insert를 하기위한 과정
                        quesWriter.QuestionSn = quesMaster.QuestionSn;
                        quesWriter.CompNm = vcIfQuesCompInfo.CompNm;
                        quesWriter.Name = vcIfQuesCompInfo.Name;
                        quesWriter.DeptNm = "";
                        quesWriter.Position = "";
                        quesWriter.TelNo = vcIfQuesCompInfo.TelNo;
                        quesWriter.Email = vcIfQuesCompInfo.Email;
                        quesWriter.RegId = vcIfQuesCompInfo.TcmsLoginKey.ToString();
                        quesWriter.RegDt = DateTime.Now;
                        quesWriter.UpdId = vcIfQuesCompInfo.TcmsLoginKey.ToString();
                        quesWriter.UpdDt = DateTime.Now;
                        quesWriter.QuesMaster = quesMaster;

                        _quesMasterService.insertQuesWriters(quesWriter);

                        // interface table 식별자들 변경 후 저장
                        ifObj.InsertYn = "Y";
                        ifObj.InsertStatus = "S";
                        await _vcIfTableService.SaveDbContextAsync();

                        // 성공된 값을 json 데이터로 전송
                        status = "S";
                        data = "{\"status\":\"" + status + "\"}";
                        return Json(data);
                    }
                    else // update를 하는 부분
                    {
                        checkQuesMaster.CertiEtc = vcIfQuesCompInfo.CertiEtc;
                        checkQuesMaster.CertiEtcText = vcIfQuesCompInfo.CertiEtcText;
                        checkQuesMaster.CertiGreen = vcIfQuesCompInfo.CertiGreen;
                        checkQuesMaster.CertiInnobiz = vcIfQuesCompInfo.CertiInnobiz;
                        checkQuesMaster.CertiMainbiz = vcIfQuesCompInfo.CertiMainbiz;
                        checkQuesMaster.CertiRnd = vcIfQuesCompInfo.CertiRnd;
                        checkQuesMaster.CertiRoot = vcIfQuesCompInfo.CertiRoot;
                        checkQuesMaster.CertiSocial = vcIfQuesCompInfo.CertiSocial;
                        checkQuesMaster.CertiVenture = vcIfQuesCompInfo.CertiVenture;
                        checkQuesMaster.CertiWoman = vcIfQuesCompInfo.CertiWoman;

                        checkQuesMaster.CompAddr = vcIfQuesCompInfo.CompAddr;
                        checkQuesMaster.CompLeaseYn = vcIfQuesCompInfo.CompLeaseYn;
                        checkQuesMaster.CompNm = vcIfQuesCompInfo.CompNm;
                        checkQuesMaster.CompType = vcIfQuesCompInfo.CompType;
                        checkQuesMaster.CoRegistrationNo = vcIfQuesCompInfo.CoRegistrationNo;

                        checkQuesMaster.Email = vcIfQuesCompInfo.Email;
                        checkQuesMaster.EngCompNm = vcIfQuesCompInfo.EngCompNm;
                        //quesCompInfo.FacPossessYn = vcIfQuesCompInfo.FacPossessYn;
                        checkQuesMaster.FactoryAddr = vcIfQuesCompInfo.FactoryAddr;
                        checkQuesMaster.FactoryLeaseYn = vcIfQuesCompInfo.FactoryLeaseYn;
                        checkQuesMaster.FaxNo = vcIfQuesCompInfo.FaxNo;
                        checkQuesMaster.HomeUrl = vcIfQuesCompInfo.HomeUrl;

                        checkQuesMaster.LabAddr = vcIfQuesCompInfo.LabAddr;
                        checkQuesMaster.LabLeaseYn = vcIfQuesCompInfo.LabLeaseYn;

                        checkQuesMaster.MainSellMarket = vcIfQuesCompInfo.MainSellMarket;
                        checkQuesMaster.MarketCivil = vcIfQuesCompInfo.MarketCivil;
                        checkQuesMaster.MarketConsumer = vcIfQuesCompInfo.MarketConsumer;
                        checkQuesMaster.MarketEtc = vcIfQuesCompInfo.MarketEtc;
                        checkQuesMaster.MarketEtcText = vcIfQuesCompInfo.MarketEtcText;
                        checkQuesMaster.MarketForeing = vcIfQuesCompInfo.MarketForeign;
                        checkQuesMaster.MarketPublic = vcIfQuesCompInfo.MarketPublic;

                        checkQuesMaster.Name = vcIfQuesCompInfo.Name;
                        checkQuesMaster.NumSn = vcIfQuesCompInfo.NumSn;

                        checkQuesMaster.ProductNm1 = vcIfQuesCompInfo.ProductNm1;
                        checkQuesMaster.ProductNm2 = vcIfQuesCompInfo.ProductNm2;
                        checkQuesMaster.ProductNm3 = vcIfQuesCompInfo.ProductNm3;
                        checkQuesMaster.PublishDt = vcIfQuesCompInfo.PublishDt;

                        checkQuesMaster.RegistrationNo = vcIfQuesCompInfo.RegistrationNo;
                        checkQuesMaster.ResidentEtc = vcIfQuesCompInfo.ResidentEtc;
                        //checkQuesMaster.ResidenteEtcText = vcIfQuesCompInfo.ResidentEtcText;
                        checkQuesMaster.ResidentType = vcIfQuesCompInfo.ResidentType;
                        //quesCompInfo.RndYn = vcIfQuesCompInfo.RndYn;
                        checkQuesMaster.StandardCode1 = vcIfQuesCompInfo.StandardCode1;
                        checkQuesMaster.StandardCode2 = vcIfQuesCompInfo.StandardCode2;
                        checkQuesMaster.StandardCode3 = vcIfQuesCompInfo.StandardCode3;
                        checkQuesMaster.TelNo = vcIfQuesCompInfo.TelNo;
                        checkQuesMaster.TcmsLoginKey = vcIfQuesCompInfo.TcmsLoginKey;

                        checkQuesMaster.UpdDt = DateTime.Now;
                        await _quesCompInfoService.SaveDbContextAsync();

                        // quesWriter의 Update 부분
                        var checkQuesWriter = await _quesMasterService.getQuesWriter(quesMaster.QuestionSn);

                        checkQuesWriter.CompNm = vcIfQuesCompInfo.CompNm;
                        checkQuesWriter.Name = vcIfQuesCompInfo.Name;
                        checkQuesWriter.TelNo = vcIfQuesCompInfo.TelNo;
                        checkQuesWriter.Email = vcIfQuesCompInfo.Email;

                        await _quesMasterService.SaveDbContextAsync();

                        // interface table 식별자들 변경 후 저장
                        ifObj.InsertYn = "Y";
                        ifObj.InsertStatus = "S";
                        await _vcIfTableService.SaveDbContextAsync();

                        // 성공된 값을 json 데이터로 전송
                        status = "S";
                        data = "{\"status\":\"" + status + "\"}";
                        return Json(data);
                    }

                }
            }
            else // infId가 없는 경우 에러메세지만 전송해준다
            {
                status = "E";
                data = "{\"status\":\"" + status + "\"}";
                return Json(data);
            }
        }

        // 자동로그인용 페이지,
        public async Task<ActionResult> AutoLogin(string token)
        {
            HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", "*");

            AutoLoginModel dataModel = new AutoLoginModel();

            if (HttpContext.Request.Cookies[token] != null)
            {
                HttpCookie cookie = HttpContext.Request.Cookies.Get(token);
                dataModel = getLoginData(cookie.Value.ToString());

                if (dataModel != null)
                {
                    VcUsrInfo scUsr = await _scUsrService.SelectScUsr(dataModel.TcmsLoginKey);

                    if (scUsr != null)
                    {
                        base.LogOn(scUsr);
                        switch (scUsr.UsrType)
                        {
                            case Global.Company: //기업
                                return RedirectToAction("index", "Company/Main");
                            case Global.Mentor: //멘토
                                return RedirectToAction("index", "Mentor/Main");
                            case Global.Expert: //전문가
                                return RedirectToAction("index", "Expert/Main");
                            case Global.SysManager: //SCP
                                return RedirectToAction("index", "SysManager/Main");
                            case Global.BizManager: //사업관리자
                                return RedirectToAction("index", "BizManager/Main");
                            default:
                                return Redirect("http://tcms.or.kr");
                        }
                    }
                    else
                    {
                        //return Redirect("http://tcms.or.kr");
                        return RedirectToAction("LoginError", "Error");
                    }
                }
            }

            //return Redirect("http://tcms.or.kr");
            return RedirectToAction("LoginError", "Error");
            // 만약 cookie에 값이 존재한다면
        }


        public AutoLoginModel getLoginData(string token)
        {
            AutoLoginModel myObj = new AutoLoginModel();

            HttpWebRequest myReq = (HttpWebRequest)WebRequest
                .Create("http://tcms.or.kr/chkSso.php?token=" + token);

            myReq.ContentType = "application/json";

            var response = (HttpWebResponse)myReq.GetResponse();

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                var text = sr.ReadToEnd();
                myObj = (AutoLoginModel)js.Deserialize(text, typeof(AutoLoginModel));
            }

            return myObj;   // 필요한 데이터 모델로 만들어서 Return 완료
            // 수정1
            // 수정2
            // 수정3
        }

        // testLogin
        public async Task<ActionResult> testLogin(string tcmsLoginKey)
        {
            VcUsrInfo scUsr = await _scUsrService.SelectScUsr(tcmsLoginKey);

            if (scUsr != null)
            {
                base.LogOn(scUsr);
                switch (scUsr.UsrType)
                {
                    case Global.Company: //기업
                        return RedirectToAction("index", "Company/Main");
                    case Global.Mentor: //멘토
                        return RedirectToAction("index", "Mentor/Main");
                    case Global.Expert: //전문가
                        return RedirectToAction("index", "Expert/Main");
                    case Global.SysManager: //SCP
                        return RedirectToAction("index", "SysManager/Main");
                    case Global.BizManager: //사업관리자
                        return RedirectToAction("index", "BizManager/Main");
                    default:
                        return RedirectToAction("Error500", "Error");
                }
            }
            return RedirectToAction("Error500", "Error");
        }

    }
}