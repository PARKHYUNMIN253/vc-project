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
    public class ProcMentorGetMentoringReportReturnModel
    {
        public Int32 FILE_SN { get; set; }
        public Int32 REPORT_SN { get; set; }
        public String CLASSIFY { get; set; }
        public String MENTOR_ID { get; set; }
        public Int32? COMP_SN { get; set; }
        public String COMP_NM { get; set; }
        public DateTime? MENTORING_DT { get; set; }
        public String MENTORING_ST_HR { get; set; }
        public String MENTORING_ED_HR { get; set; }
        public String MENTORING_PLACE { get; set; }
        public String ATTENDEE { get; set; }
        public String MENTOR_AREA_CD { get; set; }
        public String MENTORING_SUBJECT { get; set; }
        public String MENTORING_CONTENTS { get; set; }
        public DateTime? SUBMIT_DT { get; set; }
        public String STATUS { get; set; }
        public String FILE_NM { get; set; }
        public String FILE_PATH { get; set; }
        public String REG_ID { get; set; }
        public DateTime? REG_DT { get; set; }
        public Int32 MENTOR_LOGIN_KEY { get; set; }
    }

}
