// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier
// TargetFrameworkVersion = 4.51
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Threading;

namespace BizOneShot.Light.Models.WebModels
{
    // TCMS_MENTORING_REPORT_SELECT_VIEW
    public class TcmsMentoringReportSelectView
    {
        public int? Idx { get; set; } // idx
        public string BaNm { get; set; } // ba_nm
        public int MentorSn { get; set; } // mentor_sn
        public int BaSn { get; set; } // ba_sn
        public string MentorName { get; set; } // mentor_name
        public int MentorKey { get; set; } // mentor_key
        public string CompNm { get; set; } // comp_nm
        public int ReportSn { get; set; } // REPORT_SN
        public int BizWorkSn { get; set; } // BIZ_WORK_SN
        public string MentorId { get; set; } // MENTOR_ID
        public int? CompSn { get; set; } // COMP_SN
        public DateTime? MentoringDt { get; set; } // MENTORING_DT
        public string MentoringStHr { get; set; } // MENTORING_ST_HR
        public string MentoringEdHr { get; set; } // MENTORING_ED_HR
        public string MentoringPlace { get; set; } // MENTORING_PLACE
        public string Attendee { get; set; } // ATTENDEE
        public string MentorAreaCd { get; set; } // MENTOR_AREA_CD
        public string MentoringSubject { get; set; } // MENTORING_SUBJECT
        public string MentoringContents { get; set; } // MENTORING_CONTENTS
        public DateTime? SubmitDt { get; set; } // SUBMIT_DT
        public string Status { get; set; } // STATUS
        public string RegId { get; set; } // REG_ID
        public DateTime? RegDt { get; set; } // REG_DT
        public string UpdId { get; set; } // UPD_ID
        public DateTime? UpdDt { get; set; } // UPD_DT
        public string NumSn { get; set; } // NUM_SN
        public string SubNumSn { get; set; } // SUB_NUM_SN
        public string ConCode { get; set; } // CON_CODE
    }

}
