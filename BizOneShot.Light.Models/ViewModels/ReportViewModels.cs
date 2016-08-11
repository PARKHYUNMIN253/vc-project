using System;
using System.Collections.Generic;

namespace BizOneShot.Light.Models.ViewModels
{
    internal class ReportViewModels
    {
    }

    public class QuarterModel
    {
        public int Year { get; set; } // quarter
        public int Quarter { get; set; } // display quarter
    }

    public class BizInCompanyStatsViewModel
    {
        public string BizWorkNm { get; set; } // BIZ_WORK_NM. 사업명
        public DateTime? BizWorkStDt { get; set; } // BIZ_WORK_ST_DT. 사업시작일
        public DateTime? BizWorkEdDt { get; set; } // BIZ_WORK_ED_DT. 사업종료일

        public string SumSales { get; set; } // 참여기업 매출 총합
        public string AvgSales { get; set; } // 참여기업 매출 평균
        public string SumEmploy { get; set; } // 참여기업 총 고용인원
        public string AvgEmploy { get; set; } // 참여기업 평균 고용인원
        public string Display { get; set; } // Display 
        public string StartYear { get; set; } // 시작년
        public string EndYear { get; set; } // 종료년
        public string StartMonth { get; set; } // 시작월
        public string EndMonth { get; set; } // 종료월
        public string StartQuarter { get; set; } // 시작분기
        public string EndQuarter { get; set; } // 종료분기

        public IList<CompnayStatsViewModel> compnayStatsListViewModel { get; set; } //기업매출 및 고용인원
    }

    public class CompnayStatsViewModel
    {
        public string CompNm { get; set; } // COMP_NM. 회사명
        public string SumSales { get; set; } // 참여기업 기간 매출 총합
        public string CntEmploy { get; set; } // 참여기업 고용인원
        public string AvgSales { get; set; } // 참여기업 기간 매출 평균
        public string LastSales { get; set; } // 참여기업 종료 매출 
        public string BeforeSales { get; set; } // 참여기업 종료 전(월, 분기, 년) 매출 
    }
    public class VcLastReportNSatViewModel
    {
        public int SatSn { get; set; } // SAT_SN (Primary key)
        public string CompSn { get; set; } // COMP_SN
        public string MentorSn { get; set; } // MENTOR_SN
        public string NumSn { get; set; } // NUM_SN
        public string SubNumSn { get; set; } // SUB_NUM_SN
        public string BaSn { get; set; } // BA_SN
        public string ConCode { get; set; } // CON_CODE
        public string ConStatus { get; set; } // CON_STATUS
        public int SatisfactionGrade { get; set; } // SATISFACTION_GRADE
        public string File1 { get; set; } // FILE_1
        public string File2 { get; set; } // FILE_2
        public string File3 { get; set; } // FILE_3
        public string File4 { get; set; } // FILE_4
        public string File5 { get; set; } // FILE_5
        public string TotalReportSn { get; set; } // TOTAL_REPORT_SN
        public DateTime RegDt { get; set; } // REG_DT
    }

    public class VcLastReportViewModel
    {
        public int SAT_SN { get; set; }
        public string COMP_SN { get; set; }
        public string MENTOR_SN { get; set; }
        public string NUM_SN { get; set; }
        public string SUB_NUM_SN { get; set; }
        public string BA_SN { get; set; }
        public string CON_CODE { get; set; }
        public int SATISFACTION_GRADE { get; set; }
        public string FILE_1 { get; set; }
        public string FILE_2 { get; set; }
        public string FILE_3 { get; set; }
        public string FILE_4 { get; set; }
        public string FILE_5 { get; set; }
        public DateTime? REG_DT { get; set; }
        public string CON_STATUS { get; set; }
        public int TOTAL_REPORT_SN { get; set; }
        public DateTime? SUBMIT_DT { get; set; }
        public string BA_NM { get; set; }
        public string MENTOR_ID { get; set; }
        public string mentor_name { get; set; }
    }

    public class VcSatCheckViewModel
    {
        public int SatSn { get; set; } // SAT_SN (Primary key)
        public string Check01 { get; set; } // CHECK01
        public string Check02 { get; set; } // CHECK02
        public string Check03 { get; set; } // CHECK03
        public string Check04 { get; set; } // CHECK04
        public string Check05 { get; set; } // CHECK05
        public string Check06 { get; set; } // CHECK06
        public string Check07 { get; set; } // CHECK07
        public string Check08 { get; set; } // CHECK08
        public string Check09 { get; set; } // CHECK09
        public string Check10 { get; set; } // CHECK10
        public string Check11 { get; set; } // CHECK11
        public string Check12 { get; set; } // CHECK12
        public string Check13 { get; set; } // CHECK13
        public string Check14 { get; set; } // CHECK14
        public string Check15 { get; set; } // CHECK15
        public string Check16 { get; set; } // CHECK16
        public string Check17 { get; set; } // CHECK17
        public string Check18 { get; set; } // CHECK18
        public string Check19 { get; set; } // CHECK19
        public string Check20 { get; set; } // CHECK20
        public string Check21 { get; set; } // CHECK21
        public string Check22 { get; set; } // CHECK22
        public string Check23 { get; set; }
        public string Check24 { get; set; }
        public string Text01 { get; set; } // TEXT01
        public string Text02 { get; set; } // TEXT02
        public string CompSn { get; set; } // COMP_SN
        public string MentorSn { get; set; } // MENTOR_SN
        public string NumSn { get; set; } // NUM_SN
        public string SubNumSn { get; set; } // SUB_NUM_SN
        public string TotalReportSn { get; set; }
        public DateTime UpdDt { get; set; } // UPD_DT
        
    }
    #region 기업지원통계

    public class MentoringCompanyStatsViewModel
    {
        public string BizWorkNm { get; set; } // BIZ_WORK_NM. 사업명
        public DateTime? BizWorkStDt { get; set; } // BIZ_WORK_ST_DT. 사업시작일
        public DateTime? BizWorkEdDt { get; set; } // BIZ_WORK_ED_DT. 사업종료일

        public string StartYear { get; set; } // 시작년
        public string EndYear { get; set; } // 종료년
        public string StartMonth { get; set; } // 시작월
        public string EndMonth { get; set; } // 종료월

        public int SumMentoringDays { get; set; } //전체 멘통링 일수
        public double AvgMentoringDays { get; set; } //메토링 일수 평균(전체 멘토링 일수 / 기업수)

        public int SumMentoring_F { get; set; } //자금 일수
        public int SumMentoring_D { get; set; } //기술개발 일수
        public int SumMentoring_P { get; set; } //특허 일수
        public int SumMentoring_M { get; set; } //마케팅 일수
        public int SumMentoring_L { get; set; } //법무 일수
        public int SumMentoring_T { get; set; } //세무회계 일수
        public int SumMentoring_W { get; set; } //노무 일수
        public int SumMentoring_E { get; set; } //기타 일수

        public string Display { get; set; } // Display 

        public IList<MentoringStatByCompanyViewModel> MentoringStatByCompanyViewModel { get; set; } // 기업별 멘토링 통계
    }

    public class MentoringStatByCompanyViewModel
    {
        public int ComSn { get; set; }
        public string CompNm { get; set; } // COMP_NM. 회사명
        public int SumMentoringDays { get; set; } //기간 멘토일 일수
        public double AvgMentoringDays { get; set; } //기간 월 평균

        public int SumMentoring_F { get; set; } //자금 일수
        public int SumMentoring_D { get; set; } //기술개발 일수
        public int SumMentoring_P { get; set; } //특허 일수
        public int SumMentoring_M { get; set; } //마케팅 일수
        public int SumMentoring_L { get; set; } //법무 일수
        public int SumMentoring_T { get; set; } //세무회계 일수
        public int SumMentoring_W { get; set; } //노무 일수
        public int SumMentoring_E { get; set; } //기타 일수
    }

    public class MentoringStatsByCompanyGroupModel
    {
        public int CompSn { get; set; }
        public string ComNm { get; set; }
        public string MentoringAreaCd { get; set; }
        public int Count { get; set; }
    }


    public class MentoringMentorStatsViewModel
    {
        public string BizWorkNm { get; set; } // BIZ_WORK_NM. 사업명
        public DateTime? BizWorkStDt { get; set; } // BIZ_WORK_ST_DT. 사업시작일
        public DateTime? BizWorkEdDt { get; set; } // BIZ_WORK_ED_DT. 사업종료일

        public string StartYear { get; set; } // 시작년
        public string EndYear { get; set; } // 종료년
        public string StartMonth { get; set; } // 시작월
        public string EndMonth { get; set; } // 종료월

        public int SumMentoringDays { get; set; } //전체 멘통링 일수
        public double AvgMentoringDays { get; set; } //메토링 일수 평균(전체 멘토링 일수 / 맨토수)

        public int SumMentoringCount { get; set; } //전체 맨토링 횟수
        public double AvgMentoringCount { get; set; } //맨토링 횟수 평균(전체맨토링 횟수 / 맨토수)

        public int SumMentoringHours { get; set; } //전체 맨토링 시간
        public double AvgMentoringHours { get; set; } //맨토링 시간 평균(전체맨토링 시간 / 맨토수)

        public string Display { get; set; } // Display 

        public IList<MentoringStatByMentorViewModel> ListMentoringStatByMentorViewModel { get; set; } // 기업별 멘토링 통계
    }

    public class MentoringStatByMentorViewModel
    {
        public string LoginId { get; set; } //멘토 아이디
        public string Name { get; set; } // 멘토이름
        public string UsrTypeDetail { get; set; } // 멘토분야
        public string UsrTypeDetailName { get; set; } // 멘토분야이름

        public int CountMentoringComp { get; set; } //기간 멘토링 기업수
        public int SumMentoringDays { get; set; } //기간 멘토일 일수
        public int SumMentoringCount { get; set; } //기산 맨토링 횟수
        public int SumMentoringHours { get; set; } //기간 맨토링 시간
    }

    public class MentoringStatsByMentorGroupModel
    {
        public string LoginId { get; set; } //멘토 아이디
        public DateTime MentoringDt { get; set; } //멘토링일시
        public int Count { get; set; } //멘토링 일시별 멘토링 횟수
        public int SumMentoringHours { get; set; } //멘토링 일시별 멘토링 시간
    }

    public class MentoringStatsByMentorCompGroupModel
    {
        public string LoginId { get; set; } //멘토 아이디
        public int CompSn { get; set; } //멘토링일시
        public int Count { get; set; } //멘토링 일시별 멘토링 횟수
    }


    public class MentoringAreaStatsViewModel
    {
        public string BizWorkNm { get; set; } // BIZ_WORK_NM. 사업명
        public DateTime? BizWorkStDt { get; set; } // BIZ_WORK_ST_DT. 사업시작일
        public DateTime? BizWorkEdDt { get; set; } // BIZ_WORK_ED_DT. 사업종료일

        public string StartYear { get; set; } // 시작년
        public string EndYear { get; set; } // 종료년
        public string StartMonth { get; set; } // 시작월
        public string EndMonth { get; set; } // 종료월

        public int SumMentoring_F { get; set; } //자금 일수
        public int SumMentoring_D { get; set; } //기술개발 일수
        public int SumMentoring_P { get; set; } //특허 일수
        public int SumMentoring_M { get; set; } //마케팅 일수
        public int SumMentoring_L { get; set; } //법무 일수
        public int SumMentoring_T { get; set; } //세무회계 일수
        public int SumMentoring_W { get; set; } //노무 일수
        public int SumMentoring_E { get; set; } //기타 일수

        public double AvgMentoring_F { get; set; } //자금 일수
        public double AvgMentoring_D { get; set; } //기술개발 일수
        public double AvgMentoring_P { get; set; } //특허 일수
        public double AvgMentoring_M { get; set; } //마케팅 일수
        public double AvgMentoring_L { get; set; } //법무 일수
        public double AvgMentoring_T { get; set; } //세무회계 일수
        public double AvgMentoring_W { get; set; } //노무 일수
        public double AvgMentoring_E { get; set; } //기타 일수

        public string Display { get; set; } // Display 
    }

    public class MentoringStatsByAreaGroupModel
    {
        public string MentoringAreaCd { get; set; }
        public int Count { get; set; }
    }

    #endregion
}