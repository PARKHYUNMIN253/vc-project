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
    public class ProcMentorGetDeepenReportListReturnModel
    {
        public Int32 TOTAL_REPORT_SN { get; set; }
        public Int32 BIZ_WORK_SN { get; set; }
        public String MENTOR_ID { get; set; }
        public Int32? COMP_SN { get; set; }
        public DateTime? SUBMIT_DT { get; set; }
        public String STATUS { get; set; }
        public String REG_ID { get; set; }
        public DateTime? REG_DT { get; set; }
        public String UPD_ID { get; set; }
        public DateTime? UPD_DT { get; set; }
        public String NUM_SN { get; set; }
        public String SUB_NUM_SN { get; set; }
        public String DEEPEN_CONTENTS { get; set; }
        public String FINAL_SUBMIT_YN { get; set; }
        public String CON_CODE { get; set; }
        public String COMP_NM { get; set; }
        public Int32 MENTOR_LOGIN_KEY { get; set; }
    }

}
