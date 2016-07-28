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
    // RPT_MENTOR_COMMENT
    public class RptMentorComment
    {
        public int QuestionSn { get; set; } // QUESTION_SN (Primary key)
        public string NumSn { get; set; } // NUM_SN (Primary key)
        public int BasicYear { get; set; } // BASIC_YEAR (Primary key)
        public string DetailCd { get; set; } // DETAIL_CD (Primary key)
        public string Comment { get; set; } // COMMENT

        // Foreign keys
        public virtual RptCheckList RptCheckList { get; set; } // FK_RPT_CHECK_LIST_TO_RPT_MENTOR_COMMENT
    }

}
