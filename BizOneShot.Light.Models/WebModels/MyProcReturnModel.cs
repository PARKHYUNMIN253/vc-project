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
    public class MyProcReturnModel
    {
        public Int32 COMP_SN { get; set; }
        public String YEAR { get; set; }
        public String PRDNO { get; set; }
        public String PRDNO_YM { get; set; }
        public Decimal? RESERCH_AMT { get; set; }
        public Decimal? CURRENT_SALE { get; set; }
        public Decimal? PREV_SALE { get; set; }
        public Decimal? CURRENT_EARNING { get; set; }
        public Decimal? PREV_EARNING { get; set; }
        public Decimal? OPERATING_EARNING { get; set; }
        public Decimal? TOTAL_CAPITAL { get; set; }
        public Decimal? CURRENT_ASSET { get; set; }
        public Decimal? INVENTORY_ASSET { get; set; }
        public Decimal? CURRENT_LIABILITY { get; set; }
        public Decimal? TOTAL_LIABILITY { get; set; }
        public Decimal? TOTAL_ASSET { get; set; }
        public Decimal? NON_OPER_EAR { get; set; }
        public Decimal? INTERST_COST { get; set; }
        public Decimal? SALES_CREDIT { get; set; }
        public Decimal? VALUE_ADDED { get; set; }
        public Decimal? MATERIAL_COST { get; set; }
        public Decimal? QT_EMP { get; set; }
        public DateTime? INSERT_DTS { get; set; }
        public String INSERT_ID { get; set; }
        public DateTime? MODIFY_DTS { get; set; }
        public String MODIFY_ID { get; set; }
    }

}
