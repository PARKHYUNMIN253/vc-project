using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BizOneShot.Light.Models.ViewModels
{
    internal class BasicSurveyViewModels
    {
    }

    public class QuesMasterViewModel
    {
        public int QuestionSn { get; set; } // QUESTION_SN (Primary key)
        public string RegistrationNo { get; set; } // REGISTRATION_NO
        public int BasicYear { get; set; } // BASIC_YEAR
        public string SaveStatus { get; set; } // SAVE_STATUS
        public string Status { get; set; } // STATUS
        public string SubmitType { get; set; } // Submit 방식
        public QuesWriterViewModel QuesWriter { get; set; } // 작성자 정보
    }

    public class QuesWriterViewModel
    {
        public int QuestionSn { get; set; } // QUESTION_SN (Primary key)
        public string CompNm { get; set; } // COMP_NM
        public string Name { get; set; } // Name
        public string DeptNm { get; set; } // DEPT_NM
        public string Position { get; set; } // POSITION
        public string TelNo { get; set; } // TEL_NO
        public string Email { get; set; } // EMAIL
        public string RegId { get; set; } // REG_ID
        public DateTime? RegDt { get; set; } // REG_DT
        public string UpdId { get; set; } // UPD_ID
        public DateTime? UpdDt { get; set; } // UPD_DT
    }

    public class QuestionDropDownModel
    {
        public string SnStatus { get; set; } // QUESTION_SN (Primary key)
        public int? BasicYear { get; set; } // BASIC_YEAR
    }

    public class QuesCompanyInfoViewModel
    {
        public int QuestionSn { get; set; } // QUESTION_SN (Primary key)
        public string Status { get; set; } // STATUS
        public string CompNm { get; set; } // COMP_NM
        public string EngCompNm { get; set; } // ENG_COMP_NM
        public string TelNo { get; set; } // TEL_NO
        public string FaxNo { get; set; } // FAX_NO
        public string Name { get; set; } // NAME
        public string Email { get; set; } // EMAIL
        public string RegistrationNo { get; set; } // REGISTRATION_NO
        public string CoRegistrationNo { get; set; } // CO_REGISTRATION_NO
        public string PublishDt { get; set; } // PUBLISH_DT
        public string HomeUrl { get; set; } // HOME_URL
        public string CompAddr { get; set; } // COMP_ADDR
        public string FactoryAddr { get; set; } // FACTORY_ADDR
        public string LabAddr { get; set; } // LAB_ADDR
        public string FacPossessYn { get; set; } // FAC_POSSESS_YN
        public string RndYn { get; set; } // RND_YN
        public string ProductNm1 { get; set; } // PRODUCT_NM1
        public string ProductNm2 { get; set; } // PRODUCT_NM2
        public string ProductNm3 { get; set; } // PRODUCT_NM3
        public bool MarketPublic { get; set; } // MARKET_PUBLIC
        public bool MarketCivil { get; set; } // MARKET_CIVIL
        public bool MarketConsumer { get; set; } // MARKET_CONSUMER
        public bool MarketForeing { get; set; } // MARKET_FOREING
        public bool MarketEtc { get; set; } // MARKET_ETC
        public string CompType { get; set; } // COMP_TYPE
        public string ResidentType { get; set; } // RESIDENT_TYPE
        public bool CertiVenture { get; set; } // CERTI_VENTURE
        public bool CertiRnd { get; set; } // CERTI_RND
        public bool CertiMainbiz { get; set; } // CERTI_MAINBIZ
        public bool CertiInnobiz { get; set; } // CERTI_INNOBIZ
        public bool CertiRoot { get; set; } // CERTI_ROOT
        public bool CertiGreen { get; set; } // CERTI_GREEN
        public bool CertiWoman { get; set; } // CERTI_WOMAN
        public bool CertiSocial { get; set; } // CERTI_SOCIAL
        public bool CertiEtc { get; set; } // CERTI_ETC

        public string CompLeaseYn { get; set; } // COMP_LEASE_YN
        public string FactoryLeaseYn { get; set; } // FACTORY_LEASE_YN
        public string LabLeaseYn { get; set; } // LAB_LEASE_YN
        public string StandardCode1 { get; set; } // STANDARD_CODE1
        public string StandardCode2 { get; set; } // STANDARD_CODE2
        public string StandardCode3 { get; set; } // STANDARD_CODE3
        public string MainSellMarket { get; set; } // MAIN_SELL_MARKET

        public string CompHistory { get; set; } // COMP_HISTORY
        public string NumSn { get; set; } // NUM_SN

        public string MarketEtcText { get; set; } // MARKET_ETC_TEXT
        public string CertiEtcText { get; set; } // CERTI_ETC_TEXT
        public string ResidenteEtcText { get; set; } // RESIDENTE_ETC_TEXT

        public string RegId { get; set; } // REG_ID
        public DateTime? RegDt { get; set; } // REG_DT
        public string UpdId { get; set; } // UPD_ID
        public DateTime? UpdDt { get; set; } // UPD_DT
        public string SubmitType { get; set; } // Submit 방식

        public string PublishYear { get; set; }
    }

    public class QuesCompExtentionViewModel
    {
        public int QuestionSn { get; set; } // QUESTION_SN (Primary key)
        public string Status { get; set; } // STATUS
        public string PresidentNm { get; set; } // PRESIDENT_NM
        public string BirthDate { get; set; } // BIRTH_DATE
        public string AcademicDegree { get; set; } // ACADEMIC_DEGREE
        public string Major { get; set; } // MAJOR
        public string CareerComp1 { get; set; } // CAREER_COMP1
        public string Job1 { get; set; } // JOB1
        public string CareerComp2 { get; set; } // CAREER_COMP2
        public string Job2 { get; set; } // JOB2
        public string CareerComp3 { get; set; } // CAREER_COMP3
        public string Job3 { get; set; } // JOB3
        public string CareerBasicYear { get; set; } // CAREER_BASIC_YEAR
        public string CareerBasicMonth { get; set; } // CAREER_BASIC_MONTH
        public int? TotalYear { get; set; } // TOTAL_YEAR
        public int? TotalMonth { get; set; } // TOTAL_MONTY
        public string HistotyDate10 { get; set; } // HISTOTY_DATE10
        public string HistotyDate9 { get; set; } // HISTOTY_DATE9
        public string HistotyDate8 { get; set; } // HISTOTY_DATE8
        public string HistotyDate7 { get; set; } // HISTOTY_DATE7
        public string HistotyDate6 { get; set; } // HISTOTY_DATE6
        public string HistotyDate5 { get; set; } // HISTOTY_DATE5
        public string HistotyDate4 { get; set; } // HISTOTY_DATE4
        public string HistotyDate3 { get; set; } // HISTOTY_DATE3
        public string HistotyDate2 { get; set; } // HISTOTY_DATE2
        public string HistotyDate1 { get; set; } // HISTOTY_DATE1
        public string HistoryContent10 { get; set; } // HISTORY_CONTENT10
        public string HistoryContent9 { get; set; } // HISTORY_CONTENT9
        public string HistoryContent8 { get; set; } // HISTORY_CONTENT8
        public string HistoryContent7 { get; set; } // HISTORY_CONTENT7
        public string HistoryContent6 { get; set; } // HISTORY_CONTENT6
        public string HistoryContent5 { get; set; } // HISTORY_CONTENT5
        public string HistoryContent4 { get; set; } // HISTORY_CONTENT4
        public string HistoryContent3 { get; set; } // HISTORY_CONTENT3
        public string HistoryContent2 { get; set; } // HISTORY_CONTENT2
        public string HistoryContent1 { get; set; } // HISTORY_CONTENT1
        public string RegId { get; set; } // REG_ID
        public DateTime? RegDt { get; set; } // REG_DT
        public string UpdId { get; set; } // UPD_ID
        public DateTime? UpdDt { get; set; } // UPD_DT
        public string SubmitType { get; set; } // Submit 방식
    }

    public class QuesViewModel
    {
        public int QuestionSn { get; set; } // QUESTION_SN (Primary key)
        public string Status { get; set; } // STATUS
    }

    public class BizCheck02ViewModel
    {
        public int QuestionSn { get; set; } // QUESTION_SN (Primary key)
        public string SubmitType { get; set; } // Submit 방식
        public string Status { get; set; } // STATUS
        public IList<QuesCheckListViewModel> BizPurpose { get; set; }
        public IList<QuesCheckListViewModel> Leadership { get; set; }
    }

    public class BizCheck03ViewModel
    {
        public int QuestionSn { get; set; } // QUESTION_SN (Primary key)
        public string SubmitType { get; set; } // Submit 방식
        public string Status { get; set; } // STATUS
        public IList<QuesCheckListViewModel> LeaderReliability { get; set; }
    }

    public class BizCheck04ViewModel
    {
        public int QuestionSn { get; set; } // QUESTION_SN (Primary key)
        public string SubmitType { get; set; } // Submit 방식
        public string Status { get; set; } // STATUS
        public IList<QuesCheckListViewModel> WorkEnv { get; set; }
        public QuesYearListViewModel TotalEmp { get; set; }
        public QuesYearListViewModel MoveEmp { get; set; }
        public QuesYearListViewModel NewEmp { get; set; }
    }

    public class QuesCheckListViewModel
    {
        public int QuestionSn { get; set; } // QUESTION_SN (Primary key)
        public int CheckListSn { get; set; } // CHECK_LIST_SN (Primary key)
        public string Title { get; set; } // TITLE
        public string Content1 { get; set; } // CONTENT
        public string Content2 { get; set; } // CONTENT
        public bool AnsVal { get; set; } // ANS_VAL
    }

    public class QuesYearListViewModel
    {
        public int QuestionSn { get; set; } // QUESTION_SN (Primary key)
        public int CheckListSn { get; set; } // CHECK_LIST_SN (Primary key)
        public int? BasicYear { get; set; } // BASIC_YEAR
        public string D { get; set; } // D
        public string D451 { get; set; } // D-1
        public string D452 { get; set; } // D-2
        public string D453 { get; set; } // D-3
        public string SmallClassCd { get; set; } // SMALL_CLASS_CD
        public string DetailCd { get; set; } // DETAIL_CD
        public string RegId { get; set; } // REG_ID
        public DateTime? RegDt { get; set; } // REG_DT
        public string UpdId { get; set; } // UPD_ID
        public DateTime? UpdDt { get; set; } // UPD_DT
    }


    public class BizCheck05ViewModel
    {
        public int QuestionSn { get; set; } // QUESTION_SN (Primary key)
        public string SubmitType { get; set; } // Submit 방식
        public string Status { get; set; } // STATUS
        public IList<QuesCheckListViewModel> InfoSystem { get; set; }
    }


    public class BizCheck06ViewModel
    {
        public int QuestionSn { get; set; } // QUESTION_SN (Primary key)
        public string SubmitType { get; set; } // Submit 방식
        public string Status { get; set; } // STATUS
        public QuesYearListViewModel TotalEmp { get; set; } //전체 임직원
        public QuesYearListViewModel RndEmp { get; set; } // 연구개발 인력
        public QuesYearListViewModel DoctorEmp { get; set; } //박사급
        public QuesYearListViewModel MasterEmp { get; set; } //석사급
        public QuesYearListViewModel CollegeEmp { get; set; } //학사급
        public QuesYearListViewModel TechEmp { get; set; } //기능사급
        public QuesYearListViewModel HighEmp { get; set; } //고졸이하
    }

    public class BizCheck07ViewModel
    {
        public int QuestionSn { get; set; } // QUESTION_SN (Primary key)
        public string SubmitType { get; set; } // Submit 방식
        public string Status { get; set; } // STATUS
        public IList<QuesCheckListViewModel> BizCapa { get; set; }
        public QuesYearListViewModel BizResult { get; set; }
        public QuesYearListViewModel BizResultCnt { get; set; }
    }

    public class BizCheck08ViewModel
    {
        public int QuestionSn { get; set; } // QUESTION_SN (Primary key)
        public string SubmitType { get; set; } // Submit 방식
        public string Status { get; set; } // STATUS
        public IList<QuesCheckListViewModel> ProducEquip { get; set; }
        public IList<QuesCheckListViewModel> ProcessControl { get; set; }
    }

    public class BizCheck09ViewModel
    {
        public int QuestionSn { get; set; } // QUESTION_SN (Primary key)
        public string SubmitType { get; set; } // Submit 방식
        public string Status { get; set; } // STATUS
        public IList<QuesCheckListViewModel> QualityControl { get; set; }
    }

    public class BizCheck10ViewModel
    {
        public int QuestionSn { get; set; } // QUESTION_SN (Primary key)
        public string SubmitType { get; set; } // Submit 방식
        public string Status { get; set; } // STATUS
        public IList<QuesCheckListViewModel> MarketingPlan { get; set; }
    }

    public class BizCheck11ViewModel
    {
        public int QuestionSn { get; set; } // QUESTION_SN (Primary key)
        public string SubmitType { get; set; } // Submit 방식
        public string Status { get; set; } // STATUS
        public IList<QuesCheckListViewModel> CustomerMng { get; set; }
    }

    public class BizCheck12ViewModel
    {
        public int QuestionSn { get; set; } // QUESTION_SN (Primary key)
        public string SubmitType { get; set; } // Submit 방식
        public string Status { get; set; } // STATUS
        public IList<QuesCheckListViewModel> HRMng { get; set; }
        public IList<QuesCheckListViewModel> HRMaintenance { get; set; }
    }


    public class BizCheck13ViewModel
    {
        public int QuestionSn { get; set; } // QUESTION_SN (Primary key)
        public string SubmitType { get; set; } // Submit 방식
        public string Status { get; set; } // STATUS
        public QuesYearListViewModel RegPatent { get; set; } //등록된 특허
        public QuesYearListViewModel RegUtilityModel { get; set; } // 등록된 실용신안
        public QuesYearListViewModel ApplyPatent { get; set; } //출원중인 특허
        public QuesYearListViewModel ApplyUtilityModel { get; set; } //출원중인 실용신안
        public QuesYearListViewModel Etc { get; set; } //기타
        public QuesYearListViewModel TotalEmp { get; set; } //전체임직원
    }

    public class FinanceCheckViewModel
    {
        public int QuestionSn { get; set; } // QUESTION_SN (Primary key)
        public int BasicYear { get; set; }  // 기준년도
        public string SubmitType { get; set; } // Submit 방식
        public string Status { get; set; } // STATUS
        public int SaveStatus { get; set; } // SaveStatus -> 숫자들어감, 페이지를 나타내는 값이 아닌가 생각됨
        public FinanceCompositionViewModel Current { get; set; }  // 당기
        public FinanceCompositionViewModel Previous { get; set; } // 전기
    }

    public class FinanceCompositionViewModel
    {
        public int CompSn { get; set; } // COMP_SN (Primary key). 기업식별자
        public int FnYear { get; set; } // FN_YEAR (Primary key). 회계기준년도
        public decimal? FpACa { get; set; } // FP_A_CA. 유동자산
        public decimal? FpAQa { get; set; } // FP_A_QA. 당좌자산
        public decimal? FpATraderecv { get; set; } // FP_A_TRADERECV. 매출채권
        public decimal? FpAIntent { get; set; } // FP_A_INTENT. 재고자산
        public decimal? FpAFixasset { get; set; } // FP_A_FIXASSET. 비유동자산
        public decimal? FpATangible { get; set; } // FP_A_TANGIBLE. 투자자산
        public decimal? FpAIntangible { get; set; } // FP_A_INTANGIBLE. 무형자산
        public decimal? FpARndcost { get; set; } // FP_A_RNDCOST. 개발비
        public decimal? FpANoncurrentasset { get; set; } // FP_A_NONCURRENTASSET. 기타비유동자산
        public decimal? FpASum { get; set; } // FP_A_SUM. 자산총계
        public decimal? FpLCurrent { get; set; } // FP_L_CURRENT. 유동부채
        public decimal? FpLLongterm { get; set; } // FP_L_LONGTERM. 비유동부채
        public decimal? FpLSum { get; set; } // FP_L_SUM. 부채총계
        public decimal? FpCStock { get; set; } // FP_C_STOCK. 자본금
        public decimal? FpCSurplus { get; set; } // FP_C_SURPLUS. 자본잉여금
        public decimal? FpCAdjust { get; set; } // FP_C_ADJUST. 자본조정
        public decimal? FpCOthercomp { get; set; } // FP_C_OTHERCOMP. 기타포괄손익누계
        public decimal? FpCRelatedearning { get; set; } // FP_C_RELATEDEARNING. 이익잉여금
        public decimal? FpCSum { get; set; } // FP_C_SUM. 자본총계
        public decimal? CiSales { get; set; } // CI_SALES. 매출액
        public decimal? CiCostofsales { get; set; } // CI_COSTOFSALES. 매출원가
        public decimal? CiGrosspoint { get; set; } // CI_GROSSPOINT. 매출총이익
        public decimal? CiAdminexpanses { get; set; } // CI_ADMINEXPANSES. 판관비
        public decimal? CiWages { get; set; } // CI_WAGES. 인건비(임원급여,급여,상여)
        public decimal? CiRental { get; set; } // CI_RENTAL. 임차료
        public decimal? CiDepexp { get; set; } // CI_DEPEXP. 감가상각비
        public decimal? CiAmoexp { get; set; } // CI_AMOEXP. 무형자산상각비
        public decimal? CiTax { get; set; } // CI_TAX. 세금과공과
        public decimal? CiOrdevexp { get; set; } // CI_ORDEVEXP. 경상개발비
        public decimal? CiResearch { get; set; } // CI_RESEARCH. 연구비
        public decimal? CiDci { get; set; } // CI_DCI. 개발비상각액
        public decimal? CiOpincome { get; set; } // CI_OPINCOME. 영업이익
        public decimal? CiOthergains { get; set; } // CI_OTHERGAINS. 영업외수익
        public decimal? CiIntincome { get; set; } // CI_INTINCOME. 이자수익
        public decimal? CiOtherloses { get; set; } // CI_OTHERLOSES. 영업외비용
        public decimal? CiIntexpanses { get; set; } // CI_INTEXPANSES. 이자비용
        public decimal? CiPlt { get; set; } // CI_PLT. 법인세비용차감전손익, 세전이익?
        public decimal? CiInctaxexp { get; set; } // CI_INCTAXEXP. 법인세비용
        public decimal? CiProfit { get; set; } // CI_PROFIT. 당기순이익
        public decimal? McRaw { get; set; } // MC_RAW. 원재료비
        public decimal? McPart { get; set; } // MC_PART. 부재료비
        public decimal? McWages { get; set; } // MC_WAGES. 노무비
        public decimal? McOverhead { get; set; } // MC_OVERHEAD. 경비
        public decimal? McRent { get; set; } // MC_RENT. 지급임차료
        public decimal? McTax { get; set; } // MC_TAX. 세금과공과
        public decimal? McRndexp { get; set; } // MC_RNDEXP. 경상연구개발비
        public decimal? McDepexp { get; set; } // MC_DEPEXP. 감가상각비
        public decimal? McYeartotal { get; set; } // MC_YEARTOTAL. 당기총제조비용
        public decimal? McBegin { get; set; } // MC_BEGIN. 기초재공품대체액
        public decimal? McFromother { get; set; } // MC_FROMOTHER. 타계정에서대체액
        public decimal? McTotal { get; set; } // MC_TOTAL. 합계
        public decimal? McEnd { get; set; } // MC_END. 기말재공품대체액
        public decimal? McToother { get; set; } // MC_TOOTHER. 타계정으로대체액
        public decimal? McFinishgoodscost { get; set; } // MC_FINISHGOODSCOST. 당기제품제조원가
        public decimal? FpAInvest { get; set; } // FP_A_INVEST. 유형자산
        public decimal? CiWageBorder { get; set; } // CI_WAGE_BORDER. 임원급여
        public decimal? CiWageMain { get; set; } // CI_WAGE_MAIN. 급여
        public decimal? CiWageBonus { get; set; } // CI_WAGE_BONUS. 상여
        public decimal? CiWageAllowances { get; set; } // CI_WAGE_ALLOWANCES. 제수당
        public decimal? CiWageOther { get; set; } // CI_WAGE_OTHER. 잡급
        public decimal? CiWageRetirepay { get; set; } // CI_WAGE_RETIREPAY. 퇴직급여
    }

    public class OrgCheck01ViewModel
    {
        public int QuestionSn { get; set; } // QUESTION_SN (Primary key)
        public string SubmitType { get; set; } // Submit 방식
        public string Status { get; set; } // STATUS
        public OrgCompositionViewModel Management { get; set; } //기획/관리
        public OrgCompositionViewModel Produce { get; set; } // 생산/생산관리
        public OrgCompositionViewModel RND { get; set; } // 연구개발/연구지원
        public OrgCompositionViewModel Salse { get; set; } // 마케팅기획 / 판매영업
        public string BossType { get; set; }    // 라디오버튼을 위한 bosstype 추가
        public int? OfficerSumCount { get; set; } // OFFICER_COUNT
        public int? ChiefSumCount { get; set; } // CHIEF_COUNT
        public int? StaffSumCount { get; set; } // STAFF_COUNT
        public int? BeginnerSumCount { get; set; } // BEGINNER_COUNT
        public int? TotalSumCount { get; set; } // BEGINNER_COUNT
        public string CorrectValue { get; set; } // BizCheck04에서 입력해둔 값을 저장할 변수
    }

    public class OrgCompositionViewModel
    {
        public string Dept1 { get; set; } // DEPT1
        public string Dept2 { get; set; } // DEPT2
        public int OfficerCount { get; set; } // OFFICER_COUNT
        public int ChiefCount { get; set; } // CHIEF_COUNT
        public int StaffCount { get; set; } // STAFF_COUNT
        public int BeginnerCount { get; set; } // BEGINNER_COUNT
        public int PartialSum { get; set; } // 부문별 합계
    }
}