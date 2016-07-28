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
    // SC_COMPANY_FINANCE
    public class ScCompanyFinance
    {
        public int CompSn { get; set; } // COMP_SN (Primary key)
        public int FnYear { get; set; } // FN_YEAR (Primary key)
        public decimal? FpACa { get; set; } // FP_A_CA
        public decimal? FpAQa { get; set; } // FP_A_QA
        public decimal? FpATraderecv { get; set; } // FP_A_TRADERECV
        public decimal? FpAIntent { get; set; } // FP_A_INTENT
        public decimal? FpAFixasset { get; set; } // FP_A_FIXASSET
        public decimal? FpATangible { get; set; } // FP_A_TANGIBLE
        public decimal? FpAIntangible { get; set; } // FP_A_INTANGIBLE
        public decimal? FpARndcost { get; set; } // FP_A_RNDCOST
        public decimal? FpANoncurrentasset { get; set; } // FP_A_NONCURRENTASSET
        public decimal? FpASum { get; set; } // FP_A_SUM
        public decimal? FpLCurrent { get; set; } // FP_L_CURRENT
        public decimal? FpLLongterm { get; set; } // FP_L_LONGTERM
        public decimal? FpLSum { get; set; } // FP_L_SUM
        public decimal? FpCStock { get; set; } // FP_C_STOCK
        public decimal? FpCSurplus { get; set; } // FP_C_SURPLUS
        public decimal? FpCAdjust { get; set; } // FP_C_ADJUST
        public decimal? FpCOthercomp { get; set; } // FP_C_OTHERCOMP
        public decimal? FpCRelatedearning { get; set; } // FP_C_RELATEDEARNING
        public decimal? FpCSum { get; set; } // FP_C_SUM
        public decimal? CiSales { get; set; } // CI_SALES
        public decimal? CiCostofsales { get; set; } // CI_COSTOFSALES
        public decimal? CiGrosspoint { get; set; } // CI_GROSSPOINT
        public decimal? CiAdminexpanses { get; set; } // CI_ADMINEXPANSES
        public decimal? CiWages { get; set; } // CI_WAGES
        public decimal? CiRental { get; set; } // CI_RENTAL
        public decimal? CiDepexp { get; set; } // CI_DEPEXP
        public decimal? CiAmoexp { get; set; } // CI_AMOEXP
        public decimal? CiTax { get; set; } // CI_TAX
        public decimal? CiOrdevexp { get; set; } // CI_ORDEVEXP
        public decimal? CiResearch { get; set; } // CI_RESEARCH
        public decimal? CiDci { get; set; } // CI_DCI
        public decimal? CiOpincome { get; set; } // CI_OPINCOME
        public decimal? CiOthergains { get; set; } // CI_OTHERGAINS
        public decimal? CiIntincome { get; set; } // CI_INTINCOME
        public decimal? CiOtherloses { get; set; } // CI_OTHERLOSES
        public decimal? CiIntexpanses { get; set; } // CI_INTEXPANSES
        public decimal? CiPlt { get; set; } // CI_PLT
        public decimal? CiInctaxexp { get; set; } // CI_INCTAXEXP
        public decimal? CiProfit { get; set; } // CI_PROFIT
        public decimal? McRaw { get; set; } // MC_RAW
        public decimal? McPart { get; set; } // MC_PART
        public decimal? McWages { get; set; } // MC_WAGES
        public decimal? McOverhead { get; set; } // MC_OVERHEAD
        public decimal? McRent { get; set; } // MC_RENT
        public decimal? McTax { get; set; } // MC_TAX
        public decimal? McRndexp { get; set; } // MC_RNDEXP
        public decimal? McDepexp { get; set; } // MC_DEPEXP
        public decimal? McYeartotal { get; set; } // MC_YEARTOTAL
        public decimal? McBegin { get; set; } // MC_BEGIN
        public decimal? McFromother { get; set; } // MC_FROMOTHER
        public decimal? McTotal { get; set; } // MC_TOTAL
        public decimal? McEnd { get; set; } // MC_END
        public decimal? McToother { get; set; } // MC_TOOTHER
        public decimal? McFinishgoodscost { get; set; } // MC_FINISHGOODSCOST
        public decimal? FpAInvest { get; set; } // FP_A_INVEST
        public decimal? CiWageBorder { get; set; } // CI_WAGE_BORDER
        public decimal? CiWageMain { get; set; } // CI_WAGE_MAIN
        public decimal? CiWageBonus { get; set; } // CI_WAGE_BONUS
        public decimal? CiWageAllowances { get; set; } // CI_WAGE_ALLOWANCES
        public decimal? CiWageOther { get; set; } // CI_WAGE_OTHER
        public decimal? CiWageRetirepay { get; set; } // CI_WAGE_RETIREPAY
    }

}
