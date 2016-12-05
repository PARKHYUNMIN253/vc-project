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
    // VC_IF_USR_INFO
    internal partial class VcIfUsrInfoConfiguration : EntityTypeConfiguration<VcIfUsrInfo>
    {
        public VcIfUsrInfoConfiguration()
            : this("dbo")
        {
        }
 
        public VcIfUsrInfoConfiguration(string schema)
        {
            ToTable(schema + ".VC_IF_USR_INFO");
            HasKey(x => new { x.InfId, x.LoginId, x.TcmsLoginKey });

            Property(x => x.InfId).HasColumnName("INF_ID").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(25);
            Property(x => x.LoginId).HasColumnName("LOGIN_ID").IsRequired().IsUnicode(false).HasColumnType("varchar").HasMaxLength(25);
            Property(x => x.TcmsLoginKey).HasColumnName("TCMS_LOGIN_KEY").IsRequired().HasColumnType("int");
            Property(x => x.UsrType).HasColumnName("USR_TYPE").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.TypeYn).HasColumnName("TYPE_YN").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.Name).HasColumnName("NAME").IsOptional().HasColumnType("nvarchar").HasMaxLength(50);
            Property(x => x.Email).HasColumnName("EMAIL").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(70);
            Property(x => x.TelNo).HasColumnName("TEL_NO").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(20);
            Property(x => x.MbNo).HasColumnName("MB_NO").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(20);
            Property(x => x.PostNo).HasColumnName("POST_NO").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(6);
            Property(x => x.Addr1).HasColumnName("ADDR_1").IsOptional().HasColumnType("nvarchar").HasMaxLength(75);
            Property(x => x.FaxNo).HasColumnName("FAX_NO").IsOptional().IsUnicode(false).HasColumnType("varchar").HasMaxLength(20);
            Property(x => x.InfDt).HasColumnName("INF_DT").IsOptional().HasColumnType("datetime");
            Property(x => x.InsertYn).HasColumnName("INSERT_YN").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            Property(x => x.InsertStatus).HasColumnName("INSERT_STATUS").IsOptional().IsFixedLength().IsUnicode(false).HasColumnType("char").HasMaxLength(1);
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
