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
    // VC_SAT_CHECK
    internal partial class VcSatCheckConfiguration : EntityTypeConfiguration<VcSatCheck>
    {
        public VcSatCheckConfiguration()
            : this("dbo")
        {
        }
 
        public VcSatCheckConfiguration(string schema)
        {
            ToTable(schema + ".VC_SAT_CHECK");
            HasKey(x => x.SatSn);

            Property(x => x.SatSn).HasColumnName("SAT_SN").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Check01).HasColumnName("CHECK01").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.Check02).HasColumnName("CHECK02").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.Check03).HasColumnName("CHECK03").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.Check04).HasColumnName("CHECK04").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.Check05).HasColumnName("CHECK05").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.RegDt).HasColumnName("REG_DT").IsOptional().HasColumnType("datetime");
            Property(x => x.UdpDt).HasColumnName("UDP_DT").IsOptional().HasColumnType("datetime");
            Property(x => x.Check06).HasColumnName("CHECK06").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.Check07).HasColumnName("CHECK07").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.Check08).HasColumnName("CHECK08").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.Check09).HasColumnName("CHECK09").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.Check10).HasColumnName("CHECK10").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.Check11).HasColumnName("CHECK11").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.Check12).HasColumnName("CHECK12").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.Check13).HasColumnName("CHECK13").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.Check14).HasColumnName("CHECK14").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.Check15).HasColumnName("CHECK15").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.Check16).HasColumnName("CHECK16").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.Check17).HasColumnName("CHECK17").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.Check18).HasColumnName("CHECK18").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.Check19).HasColumnName("CHECK19").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.Check20).HasColumnName("CHECK20").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.Check21).HasColumnName("CHECK21").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.Check22).HasColumnName("CHECK22").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.Text01).HasColumnName("TEXT01").IsOptional().HasColumnType("nvarchar").HasMaxLength(150);
            Property(x => x.Text02).HasColumnName("TEXT02").IsOptional().HasColumnType("nvarchar").HasMaxLength(150);
            Property(x => x.Check23).HasColumnName("CHECK23").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.Check24).HasColumnName("CHECK24").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
