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
    // RPT_MASTER
    public class RptMaster
    {
        public int QuestionSn { get; set; } // QUESTION_SN (Primary key)
        public string NumSn { get; set; } // NUM_SN (Primary key)
        public int BasicYear { get; set; } // BASIC_YEAR (Primary key)
        public int CompSn { get; set; } // COMP_SN
        public string MentorId { get; set; } // MENTOR_ID
        public int? SaveStatus { get; set; } // SAVE_STATUS
        public string Status { get; set; } // STATUS
        public DateTime? RegDt { get; set; } // REG_DT
        public string RegId { get; set; } // REG_ID
        public DateTime? UpdDt { get; set; } // UPD_DT
        public string UpdId { get; set; } // UPD_ID

        // Foreign keys
        public virtual QuesMaster QuesMaster { get; set; } // FK_QUES_MASTER_TO_RPT_MASTER
        public virtual VcCompInfo VcCompInfo { get; set; } // FK_SC_COMP_INFO_TO_RPT_MASTER
        public virtual VcUsrInfo VcUsrInfo { get; set; } // FK_SC_USR_TO_RPT_MASTER
    }

}
