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
    // SC_QA
    public class ScQa
    {
        public int UsrQaSn { get; set; } // USR_QA_SN (Primary key)
        public string QuestionId { get; set; } // QUESTION_ID
        public string AnswerId { get; set; } // ANSWER_ID
        public DateTime? AskDt { get; set; } // ASK_DT
        public string Subject { get; set; } // SUBJECT
        public string Question { get; set; } // QUESTION
        public DateTime? AnsDt { get; set; } // ANS_DT
        public string Answer { get; set; } // ANSWER
        public string AnsYn { get; set; } // ANS_YN

        // Foreign keys
        public virtual VcUsrInfo VcUsrInfo_AnswerId { get; set; } // FK_SC_USR_TO_SC_QA2
        public virtual VcUsrInfo VcUsrInfo_QuestionId { get; set; } // FK_SC_USR_TO_SC_QA
    }

}
