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
    // VC_LAST_REPORT_N_SAT
    public class VcLastReportNSat
    {
        public int SatSn { get; set; } // SAT_SN (Primary key)
        public int CompSn { get; set; } // COMP_SN
        public int MentorSn { get; set; } // MENTOR_SN
        public string NumSn { get; set; } // NUM_SN
        public string SubNumSn { get; set; } // SUB_NUM_SN
        public int BaSn { get; set; } // BA_SN
        public string ConCode { get; set; } // CON_CODE
        public int? SatisfactionGrade { get; set; } // SATISFACTION_GRADE
        public string File1 { get; set; } // FILE_1
        public string File2 { get; set; } // FILE_2
        public string File3 { get; set; } // FILE_3
        public string File4 { get; set; } // FILE_4
        public string File5 { get; set; } // FILE_5
        public DateTime? RegDt { get; set; } // REG_DT
        public string ConStatus { get; set; } // CON_STATUS
        public int? TotalReportSn { get; set; } // TOTAL_REPORT_SN
    }

}
