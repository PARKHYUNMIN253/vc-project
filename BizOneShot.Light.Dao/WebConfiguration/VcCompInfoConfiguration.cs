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
    // VC_COMP_INFO
    internal partial class VcCompInfoConfiguration : EntityTypeConfiguration<VcCompInfo>
    {
        public VcCompInfoConfiguration()
            : this("dbo")
        {
        }
 
        public VcCompInfoConfiguration(string schema)
        {
            ToTable(schema + ".VC_COMP_INFO");
            HasKey(x => x.CompSn);

            Property(x => x.CompSn).HasColumnName("COMP_SN").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.RegistrationNo).HasColumnName("REGISTRATION_NO").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(10);
            Property(x => x.CompNm).HasColumnName("COMP_NM").IsOptional().HasColumnType("nvarchar").HasMaxLength(70);
            Property(x => x.OwnEmail).HasColumnName("OWN_EMAIL").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(40);
            Property(x => x.OwnTelNo).HasColumnName("OWN_TEL_NO").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(30);
            Property(x => x.OwnNm).HasColumnName("OWN_NM").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(50);
            Property(x => x.RegDt).HasColumnName("REG_DT").IsOptional().HasColumnType("datetime");
            Property(x => x.UpdDt).HasColumnName("UPD_DT").IsOptional().HasColumnType("datetime");
            Property(x => x.LoginId).HasColumnName("LOGIN_ID").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(25);
            Property(x => x.TcmsLoginKey).HasColumnName("TCMS_LOGIN_KEY").IsRequired().HasColumnType("int");

            // Foreign keys
            HasOptional(a => a.VcUsrInfo).WithMany(b => b.VcCompInfoes).HasForeignKey(c => c.LoginId); // FK_VC_COMP_INFO_VC_USR_INFO
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
