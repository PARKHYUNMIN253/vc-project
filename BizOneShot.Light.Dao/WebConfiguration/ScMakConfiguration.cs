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
    // SC_MAK
    internal partial class ScMakConfiguration : EntityTypeConfiguration<ScMak>
    {
        public ScMakConfiguration()
            : this("dbo")
        {
        }
 
        public ScMakConfiguration(string schema)
        {
            ToTable(schema + ".SC_MAK");
            HasKey(x => x.MakDt);

            Property(x => x.MakDt).HasColumnName("MAK_DT").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.MaksCa).HasColumnName("MAKS_CA").IsRequired().HasColumnType("int");
            Property(x => x.MaksMp).HasColumnName("MAKS_MP").IsRequired().HasColumnType("int");
            Property(x => x.MaksTd).HasColumnName("MAKS_TD").IsRequired().HasColumnType("int");
            Property(x => x.MaksPs).HasColumnName("MAKS_PS").IsRequired().HasColumnType("int");
            Property(x => x.MaksPro).HasColumnName("MAKS_PRO").IsRequired().HasColumnType("int");
            Property(x => x.MaksPb).HasColumnName("MAKS_PB").IsRequired().HasColumnType("int");
            Property(x => x.MaksMak).HasColumnName("MAKS_MAK").IsRequired().HasColumnType("int");
            Property(x => x.MaksCc).HasColumnName("MAKS_CC").IsRequired().HasColumnType("int");
            Property(x => x.MakslCa).HasColumnName("MAKSL_CA").IsRequired().HasColumnType("int");
            Property(x => x.MakslMp).HasColumnName("MAKSL_MP").IsRequired().HasColumnType("int");
            Property(x => x.MakslTd).HasColumnName("MAKSL_TD").IsRequired().HasColumnType("int");
            Property(x => x.MakslPs).HasColumnName("MAKSL_PS").IsRequired().HasColumnType("int");
            Property(x => x.MakslPro).HasColumnName("MAKSL_PRO").IsRequired().HasColumnType("int");
            Property(x => x.MakslPb).HasColumnName("MAKSL_PB").IsRequired().HasColumnType("int");
            Property(x => x.MakslMak).HasColumnName("MAKSL_MAK").IsRequired().HasColumnType("int");
            Property(x => x.MakslCc).HasColumnName("MAKSL_CC").IsRequired().HasColumnType("int");
            Property(x => x.MaksmCa).HasColumnName("MAKSM_CA").IsRequired().HasColumnType("int");
            Property(x => x.MaksmMp).HasColumnName("MAKSM_MP").IsRequired().HasColumnType("int");
            Property(x => x.MaksmTd).HasColumnName("MAKSM_TD").IsRequired().HasColumnType("int");
            Property(x => x.MaksmPs).HasColumnName("MAKSM_PS").IsRequired().HasColumnType("int");
            Property(x => x.MaksmPro).HasColumnName("MAKSM_PRO").IsRequired().HasColumnType("int");
            Property(x => x.MaksmPb).HasColumnName("MAKSM_PB").IsRequired().HasColumnType("int");
            Property(x => x.MaksmMak).HasColumnName("MAKSM_MAK").IsRequired().HasColumnType("int");
            Property(x => x.MaksmCc).HasColumnName("MAKSM_CC").IsRequired().HasColumnType("int");
            Property(x => x.RegId).HasColumnName("REG_ID").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(25);
            Property(x => x.RegDt).HasColumnName("REG_DT").IsOptional().HasColumnType("datetime");
            Property(x => x.UpdId).HasColumnName("UPD_ID").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(25);
            Property(x => x.UpdDt).HasColumnName("UPD_DT").IsOptional().HasColumnType("datetime");
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
