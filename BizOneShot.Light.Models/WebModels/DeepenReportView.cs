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
    // DEEPEN_REPORT_VIEW
    public class DeepenReportView
    {
        public int TotalReportSn { get; set; } // TOTAL_REPORT_SN
        public int FileSn { get; set; } // FILE_SN
        public string Classify { get; set; } // CLASSIFY
        public string MentorId { get; set; } // MENTOR_ID
        public int? CompSn { get; set; } // COMP_SN
        public string CompNm { get; set; } // COMP_NM
        public string FileNm { get; set; } // FILE_NM
        public string FilePath { get; set; } // FILE_PATH
        public DateTime? SubmitDt { get; set; } // SUBMIT_DT
        public string Status { get; set; } // STATUS
        public string RegId { get; set; } // REG_ID
        public DateTime? RegDt { get; set; } // REG_DT
        public string MentorSn { get; set; } // MENTOR_SN
        public string BaSn { get; set; } // BA_SN
        public string BaNm { get; set; } // BA_NM
        public string UsrType { get; set; } // USR_TYPE
        public string NumSn { get; set; } // NUM_SN
        public string SubNumSn { get; set; } // SUB_NUM_SN
    }

}
