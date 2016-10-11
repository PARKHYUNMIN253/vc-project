using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BizOneShot.Light.Web.ComLib
{
    public static class Global
    {
        //파일 다운로드시 필요한 url, 개발될 url에 맞춰 서브 도메인을 바꿔줄 필요가 있다.
        public static readonly string TESTURL = "http://221.144.139.21:26081/";
        public static readonly string VOUCHERURL = "http://voucher.tcms.or.kr/";
        public static readonly string LOCALURL = "http://localhost:38224/";
        public static readonly string VCURLDOWN = VOUCHERURL + "Api/DownloadWithFileSn?";

        public static readonly string TCMSGATE = "http://tcms.or.kr/";
        public static readonly string TCMSTESTGATE = "http://tcms.igarim.com/";

        //세션상수
        public static readonly string LoginID = "LoginID";
        public static readonly string CompSN = "CompSN";
        public static readonly string CompRegistrationNo = "CompRegistrationNo";
        public static readonly string UserNM = "UserNM";
        public static readonly string UserType = "UserType";
        public static readonly string UserTypeVal = "UserTypeEnum";
        public static readonly string UserDetailType = "UserDetailType";
        public static readonly string UserLogo = "UserLogo";
        public static readonly string StartMethod = "Methods";
        public static readonly string AgreeYn = "AgreeYn";
        public static readonly string UseErp = "UseErp";

        //쿠키상수
        public static readonly string ScpSearch = "ScpSearch";

        // 1기수의 기준년도
        public const int BasicYear = 2016;

        //UserType
        //public const string Company = "C";
        //public const string Mentor = "M";
        public const string Expert = "P";
        public const string BizManager = "B";
        public const string SysManager = "S";

        // Voucher user type 
        public const string Company = "C";          // 기업
        public const string Ba = "A";               // BA
        public const string Mentor = "M";           // MENTOR
        public const string TcmsMng = "T";          // TCMS 관리자

        //UserDetailType
        public const string Admin = "A";
        public const string Member = "M";

        //LeftMenu
        //공통
        public const string Cs = "Cs";                          //고객센터
        public const string Report = "Report";                  //보고서
        public const string MyInfo = "MyInfo";                  //내정보관리
        public const string DeepenReport = "DeepenReport";      //심화보고서 **
        public const string inReport = "inReport";              //서브네비를 위한 inReport 상수 **

        //기업회원
        public const string ExpertService = "ExpertService";    //전문가서비스
        public const string BAMng = "BAMng";                    //BA관리 **

        //SysManager   
        public const string ExpertMng = "ExpertMng";            //전문가 관리
        public const string BizMng = "BizMng";                  //사업관리자 관리
        public const string CompMng = "CompMng";                //기업현황

        //BizManager
        public const string MentorMng = "MentorMng";            //멘토 관리
        public const string BizWorkMng = "BizWorkMng";          //사업 관리
        public const string ComMng = "ComMng";                  //기업 관리

        //Mentor
        public const string MentoringReport = "MentoringReport";                //멘토링 보고서
        public const string CapabilityReport = "CapabilityReport";              //기초역량 보고서

        //Expert
        public const string CompanyMng = "CompanyMng";                  //관리기업
    }
}