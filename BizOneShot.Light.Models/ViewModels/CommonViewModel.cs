using System;

namespace BizOneShot.Light.Models.ViewModels
{
    internal class CommonViewModel
    {
    }

    public class FileContent
    {
        public int FileSn { get; set; }
        public string FileNm { get; set; }
        public string FilePath { get; set; }

        public string FileFullPath { get; set; }
        public string FileUrl { get; set; }
        public string FileBase64String { get; set; }
        public string FileType { get; set; }
        public string FileExtension { get; set; }
        public long FileSizeInbytes { get; set; }

        public long FileSizeInKb
        {
            get { return (long) Math.Ceiling((double) FileSizeInbytes/1024); }
        }
    }

    public enum FileType
    {
        Document, //자료(요청)
        Resume, //이력서
        Manual, //매뉴얼
        Mentoring_Report, //맨토링 일지
        Mentoring_Total, //맨토링 종합일지
        DeepenReport    // 심화보고서

    }

    public class TcmsCompStatusSelectViewModel
    {
        public int Id { get; set; } // ID
        public int CompSn { get; set; } // COMP_SN
        public string BaSn { get; set; } // BA_SN
        public string MentorSn { get; set; } // MENTOR_SN
        public string LoginId { get; set; } // LOGIN_ID
        public string UsrType { get; set; } // USR_TYPE
       // public string MappingCompSn { get; set; } // mapping_comp_sn
        public int QuestionSn { get; set; } // QUESTION_SN
        public string QStatus { get; set; } // Q_STATUS
        public string RStatus { get; set; } // r_status
        public string CompNm { get; set; } // COMP_NM
        public string BaNm { get; set; } // BA_NM
        public string Name { get; set; } // NAME
        public string RegistrationNo { get; set; } // REGISTRATION_NO
        public string OwnNm { get; set; } // OWN_NM
        public int BasicYear { get; set; } // BASIC_YEAR
        public string NumSn { get; set; } // NUM_SN
        public string SubNumSn { get; set; } // sub_num_sn
        public int SatisfactionGrade { get; set; } // SATISFACTION_GRADE
        public int SatSn { get; set; } // SAT_SN
        public int TotalReportSn { get; set; } // total_report_SN
        public string ConCode { get; set; } // CON_CODE
        public string CpMpCode { get; set; } // CP_MP_CODE
        public string ConStatus { get; set; } // CON_STATUS
    }
}