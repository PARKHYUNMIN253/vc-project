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
    // TCMS_COMP_STATUS_SELECT_VIEW
    public class TcmsCompStatusSelectView
    {
        public int? Id { get; set; } // ID
        public string CpMpCode { get; set; } // CP_MP_CODE
        public int CompSn { get; set; } // COMP_SN
        public int BaSn { get; set; } // BA_SN
        public string CompNm { get; set; } // COMP_NM
        public string BaNm { get; set; } // BA_NM
        public string WriteYn { get; set; } // WRITE_YN
        public string RegistrationNo { get; set; } // REGISTRATION_NO
        public string OwnNm { get; set; } // OWN_NM
        public string UsrType { get; set; } // USR_TYPE
        public string ConCode { get; set; } // CON_CODE
        public int? QuestionSn { get; set; } // QUESTION_SN
        public string QStatus { get; set; } // Q_STATUS
        public string RStatus { get; set; } // R_STATUS
        public int? BasicYear { get; set; } // BASIC_YEAR
        public string NumSn { get; set; } // NUM_SN
        public string SubNumSn { get; set; } // SUB_NUM_SN
    }

}
