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
    // VC_BA_INFO
    internal partial class VcBaInfoConfiguration : EntityTypeConfiguration<VcBaInfo>
    {
        public VcBaInfoConfiguration()
            : this("dbo")
        {
        }
 
        public VcBaInfoConfiguration(string schema)
        {
            ToTable(schema + ".VC_BA_INFO");
            HasKey(x => x.BaSn);

            Property(x => x.BaSn).HasColumnName("BA_SN").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.RegistrationNo).HasColumnName("REGISTRATION_NO").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(10);
            Property(x => x.TcmsLoginKey).HasColumnName("TCMS_LOGIN_KEY").IsRequired().HasColumnType("int");
            Property(x => x.BaNm).HasColumnName("BA_NM").IsOptional().HasColumnType("nvarchar").HasMaxLength(70);
            Property(x => x.BaOwnNm).HasColumnName("BA_OWN_NM").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(50);
            Property(x => x.BaEmail).HasColumnName("BA_EMAIL").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(70);
            Property(x => x.BaTelNo).HasColumnName("BA_TEL_NO").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(20);
            Property(x => x.RegDt).HasColumnName("REG_DT").IsOptional().HasColumnType("datetime");
            Property(x => x.UpdDt).HasColumnName("UPD_DT").IsOptional().HasColumnType("datetime");
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
