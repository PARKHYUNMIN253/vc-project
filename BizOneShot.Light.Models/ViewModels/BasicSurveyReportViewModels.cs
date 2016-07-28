using BizOneShot.Light.Models.WebModels;
using System;
using System.Collections.Generic;

namespace BizOneShot.Light.Models.ViewModels
{
    public class BasicSurveyReportViewModels
    {
    }

    public class BasicSurveyReportViewModel
    {
        public string NumSn { get; set; }
        public int CompSn { get; set; }
        public int BizWorkMngr { get; set; }
        public string BizWorkMngrNm { get; set; }
        public int BizWorkYear { get; set; }
        public int QuestionSn { get; set; }
        public DateTime? QuestionCompleteDt { get; set; }
        public string BizWorkNm { get; set; }
        public string RegistrationNo { get; set; }
        public string OwnNm { get; set; }
        public string CompNm { get; set; }
        public string Status { get; set; } // STATUS
        public string ComCount { get; set; } // 참여기업수
        // T -> 임시저장
        public int TotalCountForMentor { get; set; }    // 보고서 완료 count
        //public int NotYetCountForMentor { get; set; }   // 문진표 미작성 count
        //public int CompleteCount { get; set; }          // 문진표 작성 완료 count
        public string NullCheck { get; set; } //comment nullcheck
        public int? PageNum { get; set; }
        public string BaNm { get; set; }
        //public string mapping_comp_sn { get; set; }
        public string RegDate { get; set; }
        public string ConCode { get; set; } // CON_CODE
        public string MentorSn { get; set; } // CON_CODE
        public string BasicYear { get; set; }
        public string WriteYn { get; set; } // WRITE_YN
        public string MentorNm { get; set; }
        public int MentorLoginKey { get; set; }
    }

    public class OverallSummaryViewModel
    {
        public string SubmitType { get; set; }
        public double AvgTotalPoint { get; set; } //전체 평균점수
        public double CompanyPoint { get; set; } //해당 기업점수
        public string BizCapaType { get; set; } //경영역량 총괄 화살표
        public string HRMngType { get; set; } //인적자원관리 화살표
        public string OrgType { get; set; } //조직분화도 화살표
        public string MarketingType { get; set; } //기술경영, 마케팅 화살표
        public string CustMngType { get; set; } //고객의수, 상품의질, 마케팅 수준 화살표
        public string BasicCapaType { get; set; } //기초역량 화살표
        public string RoolType { get; set; } //전반적제도, 규정관리 체계 화살표
        public OverallSummaryPointViewModel OrgCapa { get; set; } //항목별 역량검토결과 - 조직역량
        public OverallSummaryPointViewModel ProductionCapa { get; set; } //항목별 역량검토결과 - 상품화역량
        public OverallSummaryPointViewModel RiskMngCapa { get; set; } //항목별 역량검토결과 - 위험관리역량
        public IList<CommentViewModel> CommentList { get; set; }
        
    }

    public class RndCostViewModel
    {
        public string SubmitType { get; set; }
        public CheckListViewModel value { get; set; }
        public CheckListViewModel percent { get; set; }
        public IList<CommentViewModel> CommentList { get; set; }
        public IList<CheckListViewModel> CheckList { get; set; }
    }

    public class ProductivityResultViewModel
    {
        public string SubmitType { get; set; }
        public CheckListViewModel BizResultCnt { get; set; }
        public CheckListViewModel BizResultPoint { get; set; }
        public IList<CommentViewModel> CommentList { get; set; }
        public IList<CheckListViewModel> CheckList { get; set; }
    }


    public class RndEmpViewModel
    {
        public string SubmitType { get; set; }
        public CheckListViewModel rndEmpRatio { get; set; }
        public CheckListViewModel rndEmpLevelRatio { get; set; }
        public IList<CommentViewModel> CommentList { get; set; }
        public IList<CheckListViewModel> CheckList { get; set; }
    }

    public class RiskMgmtOrgSatisfactionViewModel
    {
        public string SubmitType { get; set; }
        public CheckListViewModel orgSatisfaction { get; set; }
        public IList<CommentViewModel> CommentList { get; set; }
        public IList<CheckListViewModel> CheckList { get; set; }
    }

    public class OverallSummaryPointViewModel
    {
        public double CompanyPoint { get; set; } //해당기업
        public double AvgBizInCompanyPoint { get; set; } //참여기업 평균
        public double AvgTotalPoint { get; set; } //전체평균
        public double CompanyPoint2 { get; set; } //해당기업
        public double AvgBizInCompanyPoint2 { get; set; } //참여기업 평균
        public double AvgTotalPoint2 { get; set; } //전체평균
        public decimal AvgSMCompanyPoint { get; set; } //중소기업 평균
    }

    public class RegCompanyAverageViewModel //중소기업 평균
    {
        public int CavDt { get; set; } // CAV_DT (Primary key)
        public decimal CavOp { get; set; } // CAV_OP
        public decimal CavRe { get; set; } // CAV_RE
        public decimal CavSg { get; set; } // CAV_SG
        public decimal CavNg { get; set; } // CAV_NG
        public decimal CavQr { get; set; } // CAV_QR
        public decimal CavCr { get; set; } // CAV_CR
        public decimal CavDebt { get; set; } // CAV_DEBT
        public decimal CavIcr { get; set; } // CAV_ICR
        public decimal CavTat { get; set; } // CAV_TAT
        public decimal CavTrt { get; set; } // CAV_TRT
        public decimal CavIt { get; set; } // CAV_IT
        public decimal CavVr { get; set; } // CAV_VR
        public int CavLp { get; set; } // CAV_LP
        public decimal CavCp { get; set; } // CAV_CP
        public decimal CavSmc { get; set; } //CAV_SMC

    }

  
    public class CommentViewModel
    {
        public string DetailCd { get; set; }
        public string Comment { get; set; }
    }

    public class CheckBoxViewModel
    {
        public string DetailCd { get; set; }
        public bool CheckVal { get; set; }
    }

    public class OrgHR01ViewModel
    {
        public string SubmitType { get; set; }
        public IList<CommentViewModel> CommentList { get; set; }
        public IList<CheckListViewModel> CheckList { get; set; }
    }


    public class OrgProductivityViewModel
    {
        public string SubmitType { get; set; }
        public BarChartViewModel Productivity { get; set; } //생산성
        public BarChartViewModel Activity { get; set; } //활동성
        public IList<CommentViewModel> CommentList { get; set; }
        public IList<CheckListViewModel> CheckList { get; set; }
    }

    public class ProductivityProfitabilityViewModel
    {
        public string SubmitType { get; set; }
        public BarChartViewModel Profitability { get; set; } //수익성
        public BarChartViewModel Growth { get; set; } //성장성
        public IList<CommentViewModel> CommentList { get; set; }
        public IList<CheckListViewModel> CheckList { get; set; }
    }

    public class RiskMgmtLiquidityViewModel
    {
        public string SubmitType { get; set; }
        public BarChartViewModel Liquidity { get; set; } //유동성
        public BarChartViewModel Stability { get; set; } //안정성
        public IList<CommentViewModel> CommentList { get; set; }
        public IList<CheckListViewModel> CheckList { get; set; }
    }

    public class BarChartViewModel
    {
        public double Result { get; set; } //결과
        public double Dividend { get; set; } //분자
        public double Divisor { get; set; } // 분모
        public double Company { get; set; }
        public double AvgBizInCompany { get; set; }
        public double AvgTotal { get; set; }
        public decimal AvgSMCompany { get; set; }
    }

    public class CheckListViewModel
    {
        public string Count { get; set; }
        public string DetailCd { get; set; }
        public string Title { get; set; } //항목
        public bool AnsVal { get; set; } //해당기업 문진표
        public string Company { get; set; } //해당기업 
        public string StartUpAvg { get; set; } //창업보육단계평균
        public string GrowthAvg { get; set; } // 보육성장단계 평균
        public string IndependentAvg { get; set; } //자립성장단계 평균
        public string BizInCompanyAvg { get; set; } // 참여기업 평균
        public string TotalAvg { get; set; } //전체평균
        public decimal SMCompanyAvg { get; set; } //중소기업 평균
    }


    public class OrgDividedViewModel
    {
        public int QuestionSn { get; set; } // QUESTION_SN (Primary key)
        public string SubmitType { get; set; } // Submit 방식
        public string Status { get; set; } // STATUS
        public OrgEmpCompositionViewModel Management { get; set; } //기획/관리
        public OrgEmpCompositionViewModel Produce { get; set; } // 생산/생산관리
        public OrgEmpCompositionViewModel RND { get; set; } // 연구개발/연구지원
        public OrgEmpCompositionViewModel Salse { get; set; } // 마케팅기획 / 판매영업
        public int? OfficerSumCount { get; set; } // OFFICER_COUNT
        public int? ChiefSumCount { get; set; } // CHIEF_COUNT
        public int? StaffSumCount { get; set; } // STAFF_COUNT
        public int? BeginnerSumCount { get; set; } // BEGINNER_COUNT
        public int? TotalSumCount { get; set; } //부문별 합계
        public double CompanySum { get; set; } //해당기업 합계
        public double AvgBizInCompanySum { get; set; } //참여기업 합계 평균
        public double AvgTotalSum { get; set; } //전체 합계 평균
        public IList<CommentViewModel> CommentList { get; set; }
        public IList<CheckListViewModel> CheckList { get; set; }
    }

    public class OrgEmpCompositionViewModel
    {
        public string Dept1 { get; set; } // DEPT1
        public string Dept2 { get; set; } // DEPT2
        public int OfficerCount { get; set; } // OFFICER_COUNT
        public int ChiefCount { get; set; } // CHIEF_COUNT
        public int StaffCount { get; set; } // STAFF_COUNT
        public int BeginnerCount { get; set; } // BEGINNER_COUNT
        public int PartialSum { get; set; } // 부문별 합계
        public double Company { get; set; } // 해당기업
        public double AvgBizInCompany { get; set; } // 참여기업 평균
        public double AvgTotal { get; set; } // 전체평균
    }


    public class GrowthStrategyViewModel
    {
        public string SubmitType { get; set; }
        public IList<CommentViewModel> CommentList { get; set; }
    }

    public class RiskMgmtViewModel
    {
        public string SubmitType { get; set; }
        public IList<CommentViewModel> CommentList { get; set; }
        public IList<CheckBoxViewModel> CheckBoxList { get; set; }
        public IList<CheckListViewModel> CheckList { get; set; }
    }

    public class ProductivityRelationViewModel
    {
        public string SubmitType { get; set; }
        public IList<CommentViewModel> CommentList { get; set; }
        public IList<CheckListViewModel> CheckList { get; set; }
        public IList<CommentViewModel> MngComment { get; set; }
        //관리자 입력값
        public ScMak MakCommentDetail { get; set; }
    }

    public class RegCommentViewModel
    {
        public int CompSn { get; set; } // COMP_SN (Primary key). 기업식별자
        public string ExpertId { get; set; } // EXPERT_ID (Primary key). 전문가식별자
        public int BizWorkSn { get; set; } // BIZ_WORK_SN. 사업식별자
        public int BasicYear { get; set; } // BASIC_YEAR (Primary key). 기준년도
        public int BasicMonth { get; set; } // BASIC_MONTH (Primary key). 기준월
        public string Comment { get; set; } // COMMENT. 커맨트
        public DateTime? WriteDt { get; set; } // WRITE_DT. 등록일
        public string ExpertNm { get; set; } // EXPERT_ID (Primary key). 전문가이름
        public string BizWorkNm { get; set; } // EXPERT_ID (Primary key). 사업명
        public string WriteYN { get; set; } //Commant 작성여부 Y:작성, N:미작성
    }

    public class RegMarketStateViewModel //상품화 시장역량 
    {
        public int MakDt { get; set; } // MAK_DT (Primary key)
        public int MaksCa { get; set; } // MAKS_CA
        public int MaksMp { get; set; } // MAKS_MP
        public int MaksTd { get; set; } // MAKS_TD
        public int MaksPs { get; set; } // MAKS_PS
        public int MaksPro { get; set; } // MAKS_PRO
        public int MaksPb { get; set; } // MAKS_PB
        public int MaksMak { get; set; } // MAKS_MAK
        public int MaksCc { get; set; } // MAKS_CC
        public int MakslCa { get; set; } // MAKSL_CA
        public int MakslMp { get; set; } // MAKSL_MP
        public int MakslTd { get; set; } // MAKSL_TD
        public int MakslPs { get; set; } // MAKSL_PS
        public int MakslPro { get; set; } // MAKSL_PRO
        public int MakslPb { get; set; } // MAKSL_PB
        public int MakslMak { get; set; } // MAKSL_MAK
        public int MakslCc { get; set; } // MAKSL_CC
        public int MaksmCa { get; set; } // MAKSM_CA
        public int MaksmMp { get; set; } // MAKSM_MP
        public int MaksmTd { get; set; } // MAKSM_TD
        public int MaksmPs { get; set; } // MAKSM_PS
        public int MaksmPro { get; set; } // MAKSM_PRO
        public int MaksmPb { get; set; } // MAKSM_PB
        public int MaksmMak { get; set; } // MAKSM_MAK
        public int MaksmCc { get; set; } // MAKSM_CC
    }
    //다른 멘토의견 가져오기 ViewModel
    public class SelectMentorCommentViewModel
    {
        public int QuestionSn { get; set; } // QUESTION_SN (Primary key). 문진표순번
        public int BizWorkSn { get; set; } // BIZ_WORK_SN (Primary key). 사업식별자
        public string BizWorkNm { get; set; } // BIZ_WORK_Nm (Primary key). 사업이름
        public int BasicYear { get; set; } // BASIC_YEAR (Primary key). 기준년도
        public string DetailCd { get; set; } // DETAIL_CD (Primary key). 상세코드
        public string Comment { get; set; } // COMMENT. 멘토작성내용
        public int CompSn { get; set; } // COMPSN. 기업식별자
        
    }

    //Navi Bar + 멘토 코멘트 ViewModel 
    public class RptPageListView
    {
        public int QuestionSn { get; set; } // QUESTION_SN
        public int BizWorkSn { get; set; } // BIZ_WORK_SN
        public int? BasicYear { get; set; } // BASIC_YEAR
        public int PageNum { get; set; } // PAGE_NUM
        public string PageName { get; set; } // PAGE_NAME
        public string DetailCd { get; set; } // DETAIL_CD
        public string RequireField { get; set; } // REQUIRE_FIELD
        public string Comment { get; set; } // COMMENT
        public string NullCheck { get; set; } // COMMENT
    }
}