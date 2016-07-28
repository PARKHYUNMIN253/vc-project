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
    public class ProcBaGetReportCompInfoReturnModel
    {
        public String NUM_SN { get; set; }
        public Int32 BA_SN { get; set; }
        public String BA_NM { get; set; }
        public String BA_TEL_NO { get; set; }
        public String BA_EMAIL { get; set; }
        public String CON_CODE { get; set; }
        public Int32 BA_LOGIN_KEY { get; set; }
        public Int32 COMP_SN { get; set; }
        public String COMP_NM { get; set; }
        public String COMP_REGISTRATION_NO { get; set; }
        public Int32 QUESTION_SN { get; set; }
        public Int32 BASIC_YEAR { get; set; }
        public String MENTOR_ID { get; set; }
        public Int32? SAVE_STATUS { get; set; }
        public String STATUS { get; set; }
        public DateTime? REG_DT { get; set; }
        public String REG_ID { get; set; }
    }

}
