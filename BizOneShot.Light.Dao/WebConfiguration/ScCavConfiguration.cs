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
    // SC_CAV
    internal partial class ScCavConfiguration : EntityTypeConfiguration<ScCav>
    {
        public ScCavConfiguration()
            : this("dbo")
        {
        }
 
        public ScCavConfiguration(string schema)
        {
            ToTable(schema + ".SC_CAV");
            HasKey(x => x.CavDt);

            Property(x => x.CavDt).HasColumnName("CAV_DT").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.CavOp).HasColumnName("CAV_OP").IsRequired().HasColumnType("decimal").HasPrecision(8,3);
            Property(x => x.CavRe).HasColumnName("CAV_RE").IsRequired().HasColumnType("decimal").HasPrecision(8,3);
            Property(x => x.CavSg).HasColumnName("CAV_SG").IsRequired().HasColumnType("decimal").HasPrecision(8,3);
            Property(x => x.CavNg).HasColumnName("CAV_NG").IsRequired().HasColumnType("decimal").HasPrecision(8,3);
            Property(x => x.CavQr).HasColumnName("CAV_QR").IsRequired().HasColumnType("decimal").HasPrecision(8,3);
            Property(x => x.CavCr).HasColumnName("CAV_CR").IsRequired().HasColumnType("decimal").HasPrecision(8,3);
            Property(x => x.CavDebt).HasColumnName("CAV_DEBT").IsRequired().HasColumnType("decimal").HasPrecision(8,3);
            Property(x => x.CavIcr).HasColumnName("CAV_ICR").IsRequired().HasColumnType("decimal").HasPrecision(8,3);
            Property(x => x.CavTat).HasColumnName("CAV_TAT").IsRequired().HasColumnType("decimal").HasPrecision(8,3);
            Property(x => x.CavTrt).HasColumnName("CAV_TRT").IsRequired().HasColumnType("decimal").HasPrecision(8,3);
            Property(x => x.CavIt).HasColumnName("CAV_IT").IsRequired().HasColumnType("decimal").HasPrecision(8,3);
            Property(x => x.CavVr).HasColumnName("CAV_VR").IsRequired().HasColumnType("decimal").HasPrecision(8,3);
            Property(x => x.CavLp).HasColumnName("CAV_LP").IsRequired().HasColumnType("int");
            Property(x => x.CavCp).HasColumnName("CAV_CP").IsRequired().HasColumnType("decimal").HasPrecision(8,3);
            Property(x => x.CavSmc).HasColumnName("CAV_SMC").IsRequired().HasColumnType("decimal").HasPrecision(8,3);
            Property(x => x.RegId).HasColumnName("REG_ID").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(25);
            Property(x => x.RegDt).HasColumnName("REG_DT").IsOptional().HasColumnType("datetime");
            Property(x => x.UpdId).HasColumnName("UPD_ID").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(25);
            Property(x => x.UpdDt).HasColumnName("UPD_DT").IsOptional().HasColumnType("datetime");
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
