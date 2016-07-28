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
using System.Collections.ObjectModel;
using System.Linq;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using BizOneShot.Light.Models.WebModels;
using System.Threading;
using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption;

namespace BizOneShot.Light.Dao.WebConfiguration
{
    // SC_COMPANY_FINANCE
    internal partial class ScCompanyFinanceConfiguration : EntityTypeConfiguration<ScCompanyFinance>
    {
        public ScCompanyFinanceConfiguration()
            : this("dbo")
        {
        }
 
        public ScCompanyFinanceConfiguration(string schema)
        {
            ToTable(schema + ".SC_COMPANY_FINANCE");
            HasKey(x => new { x.CompSn, x.FnYear });

            Property(x => x.CompSn).HasColumnName("COMP_SN").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.FnYear).HasColumnName("FN_YEAR").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.FpACa).HasColumnName("FP_A_CA").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.FpAQa).HasColumnName("FP_A_QA").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.FpATraderecv).HasColumnName("FP_A_TRADERECV").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.FpAIntent).HasColumnName("FP_A_INTENT").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.FpAFixasset).HasColumnName("FP_A_FIXASSET").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.FpATangible).HasColumnName("FP_A_TANGIBLE").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.FpAIntangible).HasColumnName("FP_A_INTANGIBLE").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.FpARndcost).HasColumnName("FP_A_RNDCOST").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.FpANoncurrentasset).HasColumnName("FP_A_NONCURRENTASSET").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.FpASum).HasColumnName("FP_A_SUM").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.FpLCurrent).HasColumnName("FP_L_CURRENT").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.FpLLongterm).HasColumnName("FP_L_LONGTERM").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.FpLSum).HasColumnName("FP_L_SUM").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.FpCStock).HasColumnName("FP_C_STOCK").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.FpCSurplus).HasColumnName("FP_C_SURPLUS").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.FpCAdjust).HasColumnName("FP_C_ADJUST").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.FpCOthercomp).HasColumnName("FP_C_OTHERCOMP").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.FpCRelatedearning).HasColumnName("FP_C_RELATEDEARNING").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.FpCSum).HasColumnName("FP_C_SUM").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.CiSales).HasColumnName("CI_SALES").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.CiCostofsales).HasColumnName("CI_COSTOFSALES").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.CiGrosspoint).HasColumnName("CI_GROSSPOINT").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.CiAdminexpanses).HasColumnName("CI_ADMINEXPANSES").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.CiWages).HasColumnName("CI_WAGES").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.CiRental).HasColumnName("CI_RENTAL").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.CiDepexp).HasColumnName("CI_DEPEXP").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.CiAmoexp).HasColumnName("CI_AMOEXP").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.CiTax).HasColumnName("CI_TAX").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.CiOrdevexp).HasColumnName("CI_ORDEVEXP").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.CiResearch).HasColumnName("CI_RESEARCH").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.CiDci).HasColumnName("CI_DCI").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.CiOpincome).HasColumnName("CI_OPINCOME").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.CiOthergains).HasColumnName("CI_OTHERGAINS").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.CiIntincome).HasColumnName("CI_INTINCOME").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.CiOtherloses).HasColumnName("CI_OTHERLOSES").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.CiIntexpanses).HasColumnName("CI_INTEXPANSES").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.CiPlt).HasColumnName("CI_PLT").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.CiInctaxexp).HasColumnName("CI_INCTAXEXP").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.CiProfit).HasColumnName("CI_PROFIT").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.McRaw).HasColumnName("MC_RAW").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.McPart).HasColumnName("MC_PART").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.McWages).HasColumnName("MC_WAGES").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.McOverhead).HasColumnName("MC_OVERHEAD").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.McRent).HasColumnName("MC_RENT").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.McTax).HasColumnName("MC_TAX").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.McRndexp).HasColumnName("MC_RNDEXP").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.McDepexp).HasColumnName("MC_DEPEXP").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.McYeartotal).HasColumnName("MC_YEARTOTAL").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.McBegin).HasColumnName("MC_BEGIN").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.McFromother).HasColumnName("MC_FROMOTHER").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.McTotal).HasColumnName("MC_TOTAL").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.McEnd).HasColumnName("MC_END").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.McToother).HasColumnName("MC_TOOTHER").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.McFinishgoodscost).HasColumnName("MC_FINISHGOODSCOST").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.FpAInvest).HasColumnName("FP_A_INVEST").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.CiWageBorder).HasColumnName("CI_WAGE_BORDER").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.CiWageMain).HasColumnName("CI_WAGE_MAIN").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.CiWageBonus).HasColumnName("CI_WAGE_BONUS").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.CiWageAllowances).HasColumnName("CI_WAGE_ALLOWANCES").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.CiWageOther).HasColumnName("CI_WAGE_OTHER").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            Property(x => x.CiWageRetirepay).HasColumnName("CI_WAGE_RETIREPAY").IsOptional().HasColumnType("decimal").HasPrecision(22,4);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
