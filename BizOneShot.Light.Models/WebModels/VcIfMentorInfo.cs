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
    // VC_IF_MENTOR_INFO
    public class VcIfMentorInfo
    {
        public string InfId { get; set; } // INF_ID
        public int BaLoginKey { get; set; } // BA_LOGIN_KEY
        public int MentorLoginKey { get; set; } // MENTOR_LOGIN_KEY
        public DateTime? InfDt { get; set; } // INF_DT
        public string InsertYn { get; set; } // INSERT_YN
        public string InsertStatus { get; set; } // INSERT_STATUS
    }

}
