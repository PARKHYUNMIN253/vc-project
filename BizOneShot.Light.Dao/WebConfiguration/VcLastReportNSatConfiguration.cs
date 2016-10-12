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
    // VC_LAST_REPORT_N_SAT
    internal partial class VcLastReportNSatConfiguration : EntityTypeConfiguration<VcLastReportNSat>
    {
        public VcLastReportNSatConfiguration()
            : this("dbo")
        {
        }
 
        public VcLastReportNSatConfiguration(string schema)
        {
            ToTable(schema + ".VC_LAST_REPORT_N_SAT");
            HasKey(x => x.SatSn);

            Property(x => x.SatSn).HasColumnName("SAT_SN").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.CompSn).HasColumnName("COMP_SN").IsRequired().HasColumnType("int");
            Property(x => x.MentorSn).HasColumnName("MENTOR_SN").IsRequired().HasColumnType("int");
            Property(x => x.NumSn).HasColumnName("NUM_SN").IsRequired().HasColumnType("nvarchar").HasMaxLength(3);
            Property(x => x.SubNumSn).HasColumnName("SUB_NUM_SN").IsRequired().HasColumnType("nvarchar").HasMaxLength(2);
            Property(x => x.BaSn).HasColumnName("BA_SN").IsRequired().HasColumnType("int");
            Property(x => x.ConCode).HasColumnName("CON_CODE").IsOptional().HasColumnType("nvarchar").HasMaxLength(5);
            Property(x => x.SatisfactionGrade).HasColumnName("SATISFACTION_GRADE").IsOptional().HasColumnType("int");
            Property(x => x.File1).HasColumnName("FILE_1").IsOptional().HasColumnType("nvarchar").HasMaxLength(1500);
            Property(x => x.File2).HasColumnName("FILE_2").IsOptional().HasColumnType("nvarchar").HasMaxLength(1500);
            Property(x => x.File3).HasColumnName("FILE_3").IsOptional().HasColumnType("nvarchar").HasMaxLength(1500);
            Property(x => x.File4).HasColumnName("FILE_4").IsOptional().HasColumnType("nvarchar").HasMaxLength(1500);
            Property(x => x.File5).HasColumnName("FILE_5").IsOptional().HasColumnType("nvarchar").HasMaxLength(1500);
            Property(x => x.RegDt).HasColumnName("REG_DT").IsOptional().HasColumnType("datetime");
            Property(x => x.ConStatus).HasColumnName("CON_STATUS").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.TotalReportSn).HasColumnName("TOTAL_REPORT_SN").IsOptional().HasColumnType("int");
            Property(x => x.Check01).HasColumnName("CHECK01").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.Check02).HasColumnName("CHECK02").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.Check03).HasColumnName("CHECK03").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.Check04).HasColumnName("CHECK04").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.Text01).HasColumnName("TEXT01").IsOptional().HasColumnType("nvarchar").HasMaxLength(150);
            Property(x => x.Text02).HasColumnName("TEXT02").IsOptional().HasColumnType("nvarchar").HasMaxLength(150);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
