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
    // VC_USR_INFO
    [GeneratedCodeAttribute("EF.Reverse.POCO.Generator", "2.15.1.0")]
    public class VcUsrInfo
    {
        public string LoginId { get; set; } // LOGIN_ID (Primary key)
        public string UsrType { get; set; } // USR_TYPE
        public DateTime? RegDt { get; set; } // REG_DT
        public DateTime? UpdDt { get; set; } // UPD_DT
        public string TypeYn { get; set; } // TYPE_YN
        public string Name { get; set; } // NAME
        public string Email { get; set; } // EMAIL
        public string TelNo { get; set; } // TEL_NO
        public string MbNo { get; set; } // MB_NO
        public string PostNo { get; set; } // POST_NO
        public string Addr1 { get; set; } // ADDR_1
        public string FaxNo { get; set; } // FAX_NO
        public int TcmsLoginKey { get; set; } // TCMS_LOGIN_KEY
        public string AgreeYn { get; set; } // AGREE_YN

        // Reverse navigation
        public virtual ICollection<RptFinanceComment> RptFinanceComments { get; set; } // Many to many mapping
        public virtual ICollection<RptMaster> RptMasters { get; set; } // RPT_MASTER.FK_SC_USR_TO_RPT_MASTER
        public virtual ICollection<ScMentoringReport> ScMentoringReports { get; set; } // SC_MENTORING_REPORT.FK_SC_USR_TO_SC_MENTORING_REPORT
        public virtual ICollection<ScMentoringTotalReport> ScMentoringTotalReports { get; set; } // SC_MENTORING_TOTAL_REPORT.FK_SC_USR_TO_MENTORING_TOTAL_REPORT
        public virtual ICollection<ScQa> ScQas_AnswerId { get; set; } // SC_QA.FK_SC_USR_TO_SC_QA2
        public virtual ICollection<ScQa> ScQas_QuestionId { get; set; } // SC_QA.FK_SC_USR_TO_SC_QA
        public virtual ICollection<ScReqDoc> ScReqDocs_ReceiverId { get; set; } // SC_REQ_DOC.FK_SC_USR_TO_SC_REQ_DOC2
        public virtual ICollection<ScReqDoc> ScReqDocs_SenderId { get; set; } // SC_REQ_DOC.FK_SC_USR_TO_SC_REQ_DOC
        public virtual ICollection<VcCompInfo> VcCompInfoes { get; set; } // VC_COMP_INFO.FK_VC_COMP_INFO_VC_USR_INFO
        public virtual ScUsrResume ScUsrResume { get; set; } // SC_USR_RESUME.FK_SC_USR_TO_SC_USR_RESUME
        
        public VcUsrInfo()
        {
            TypeYn = "Y";
            RptFinanceComments = new List<RptFinanceComment>();
            RptMasters = new List<RptMaster>();
            ScMentoringReports = new List<ScMentoringReport>();
            ScMentoringTotalReports = new List<ScMentoringTotalReport>();
            ScQas_AnswerId = new List<ScQa>();
            ScQas_QuestionId = new List<ScQa>();
            ScReqDocs_ReceiverId = new List<ScReqDoc>();
            ScReqDocs_SenderId = new List<ScReqDoc>();
            VcCompInfoes = new List<VcCompInfo>();
        }
    }

}
