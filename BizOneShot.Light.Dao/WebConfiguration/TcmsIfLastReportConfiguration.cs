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
    // TCMS_IF_LAST_REPORT
    internal partial class TcmsIfLastReportConfiguration : EntityTypeConfiguration<TcmsIfLastReport>
    {
        public TcmsIfLastReportConfiguration()
            : this("dbo")
        {
        }
 
        public TcmsIfLastReportConfiguration(string schema)
        {
            ToTable(schema + ".TCMS_IF_LAST_REPORT");
            HasKey(x => x.InfId);

            Property(x => x.InfId).HasColumnName("INF_ID").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(25).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.CompLoginKey).HasColumnName("COMP_LOGIN_KEY").IsOptional().HasColumnType("int");
            Property(x => x.BaLoginKey).HasColumnName("BA_LOGIN_KEY").IsOptional().HasColumnType("int");
            Property(x => x.MentorLoginKey).HasColumnName("MENTOR_LOGIN_KEY").IsOptional().HasColumnType("int");
            Property(x => x.NumSn).HasColumnName("NUM_SN").IsOptional().HasColumnType("nvarchar").HasMaxLength(3);
            Property(x => x.SubNumSn).HasColumnName("SUB_NUM_SN").IsOptional().HasColumnType("nvarchar").HasMaxLength(2);
            Property(x => x.ConCode).HasColumnName("CON_CODE").IsOptional().HasColumnType("nvarchar").HasMaxLength(5);
            Property(x => x.File1).HasColumnName("FILE_1").IsOptional().HasColumnType("nvarchar").HasMaxLength(1500);
            Property(x => x.File2).HasColumnName("FILE_2").IsOptional().HasColumnType("nvarchar").HasMaxLength(1500);
            Property(x => x.File3).HasColumnName("FILE_3").IsOptional().HasColumnType("nvarchar").HasMaxLength(1500);
            Property(x => x.File4).HasColumnName("FILE_4").IsOptional().HasColumnType("nvarchar").HasMaxLength(1500);
            Property(x => x.RegDt).HasColumnName("REG_DT").IsOptional().HasColumnType("datetime");
            Property(x => x.InfDt).HasColumnName("INF_DT").IsOptional().HasColumnType("datetime");
            Property(x => x.File5).HasColumnName("FILE_5").IsOptional().HasColumnType("nvarchar").HasMaxLength(1500);
            Property(x => x.InsertYn).HasColumnName("INSERT_YN").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
