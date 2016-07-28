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
    // VC_COMP_INFO
    [GeneratedCodeAttribute("EF.Reverse.POCO.Generator", "2.15.1.0")]
    public class VcCompInfo
    {
        public int CompSn { get; set; } // COMP_SN (Primary key)
        public string RegistrationNo { get; set; } // REGISTRATION_NO
        public string CompNm { get; set; } // COMP_NM
        public string OwnEmail { get; set; } // OWN_EMAIL
        public string OwnTelNo { get; set; } // OWN_TEL_NO
        public string OwnNm { get; set; } // OWN_NM
        public DateTime? RegDt { get; set; } // REG_DT
        public DateTime? UpdDt { get; set; } // UPD_DT
        public string LoginId { get; set; } // LOGIN_ID
        public int TcmsLoginKey { get; set; } // TCMS_LOGIN_KEY

        // Reverse navigation
        public virtual ICollection<RptFinanceComment> RptFinanceComments { get; set; } // Many to many mapping
        public virtual ICollection<RptMaster> RptMasters { get; set; } // RPT_MASTER.FK_SC_COMP_INFO_TO_RPT_MASTER
        public virtual ICollection<RptMngComment> RptMngComments { get; set; } // Many to many mapping
        public virtual ICollection<ScBizType> ScBizTypes { get; set; } // Many to many mapping
        public virtual ICollection<ScMentoringReport> ScMentoringReports { get; set; } // SC_MENTORING_REPORT.FK_SC_COMP_INFO_TO_SC_MENTORING_REPORT
        public virtual ICollection<ScMentoringTotalReport> ScMentoringTotalReports { get; set; } // SC_MENTORING_TOTAL_REPORT.FK_SC_COMP_INFO_TO_MENTORING_TOTAL_REPORT

        // Foreign keys
        public virtual VcUsrInfo VcUsrInfo { get; set; } // FK_VC_COMP_INFO_VC_USR_INFO
        
        public VcCompInfo()
        {
            RptFinanceComments = new List<RptFinanceComment>();
            RptMasters = new List<RptMaster>();
            RptMngComments = new List<RptMngComment>();
            ScBizTypes = new List<ScBizType>();
            ScMentoringReports = new List<ScMentoringReport>();
            ScMentoringTotalReports = new List<ScMentoringTotalReport>();
        }
    }

}
