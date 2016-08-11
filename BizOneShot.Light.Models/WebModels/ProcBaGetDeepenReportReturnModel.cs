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
    public class ProcBaGetDeepenReportReturnModel
    {
        public Int32 SAT_SN { get; set; }
        public Int32 COMP_SN { get; set; }
        public Int32 MENTOR_SN { get; set; }
        public String NUM_SN { get; set; }
        public String SUB_NUM_SN { get; set; }
        public Int32 BA_SN { get; set; }
        public String CON_CODE { get; set; }
        public Int32? SATISFACTION_GRADE { get; set; }
        public String FILE_1 { get; set; }
        public String FILE_2 { get; set; }
        public String FILE_3 { get; set; }
        public String FILE_4 { get; set; }
        public String FILE_5 { get; set; }
        public DateTime? REG_DT { get; set; }
        public String CON_STATUS { get; set; }
        public Int32? TOTAL_REPORT_SN { get; set; }
        public String CHECK01 { get; set; }
        public String CHECK02 { get; set; }
        public String CHECK03 { get; set; }
        public String CHECK04 { get; set; }
        public String TEXT01 { get; set; }
        public String TEXT02 { get; set; }
        public String COMP_NM { get; set; }
        public Int32 BA_LOGIN_KEY { get; set; }
        public DateTime? SUBMIT_DT { get; set; }
    }

}
