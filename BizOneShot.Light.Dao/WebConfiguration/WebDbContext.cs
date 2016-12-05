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
    public partial class WebDbContext : DbContext, IWebDbContext
    {
        public DbSet<CtWebLog> CtWebLogs { get; set; } // CT_WEB_LOG
        public DbSet<MServiceDomain> MServiceDomains { get; set; } // M_SERVICE_DOMAIN
        public DbSet<NPsGungu> NPsGungus { get; set; } // N_PS_GUNGU
        public DbSet<QuesCheckList> QuesCheckLists { get; set; } // QUES_CHECK_LIST
        public DbSet<QuesCompExtention> QuesCompExtentions { get; set; } // QUES_COMP_EXTENTION
        public DbSet<QuesCompHistory> QuesCompHistories { get; set; } // QUES_COMP_HISTORY
        public DbSet<QuesCompInfo> QuesCompInfoes { get; set; } // QUES_COMP_INFO
        public DbSet<QuesMaster> QuesMasters { get; set; } // QUES_MASTER
        public DbSet<QuesOgranAnalysis> QuesOgranAnalysis { get; set; } // QUES_OGRAN_ANALYSIS
        public DbSet<QuesResult1> QuesResult1 { get; set; } // QUES_RESULT1
        public DbSet<QuesResult2> QuesResult2 { get; set; } // QUES_RESULT2
        public DbSet<QuesWriter> QuesWriters { get; set; } // QUES_WRITER
        public DbSet<RptCheckList> RptCheckLists { get; set; } // RPT_CHECK_LIST
        public DbSet<RptFinanceComment> RptFinanceComments { get; set; } // RPT_FINANCE_COMMENT
        public DbSet<RptMaster> RptMasters { get; set; } // RPT_MASTER
        public DbSet<RptMentorCheck> RptMentorChecks { get; set; } // RPT_MENTOR_CHECK
        public DbSet<RptMentorComment> RptMentorComments { get; set; } // RPT_MENTOR_COMMENT
        public DbSet<RptMentorRadio> RptMentorRadios { get; set; } // RPT_MENTOR_RADIO
        public DbSet<RptMngCode> RptMngCodes { get; set; } // RPT_MNG_CODE
        public DbSet<RptMngComment> RptMngComments { get; set; } // RPT_MNG_COMMENT
        public DbSet<ScBizType> ScBizTypes { get; set; } // SC_BIZ_TYPE
        public DbSet<ScCav> ScCavs { get; set; } // SC_CAV
        public DbSet<ScCompanyFinance> ScCompanyFinances { get; set; } // SC_COMPANY_FINANCE
        public DbSet<ScFaq> ScFaqs { get; set; } // SC_FAQ
        public DbSet<ScFileInfo> ScFileInfoes { get; set; } // SC_FILE_INFO
        public DbSet<ScFinancialIndexT> ScFinancialIndexTs { get; set; } // SC_FINANCIAL_INDEX_T
        public DbSet<ScForm> ScForms { get; set; } // SC_FORM
        public DbSet<ScFormFile> ScFormFiles { get; set; } // SC_FORM_FILE
        public DbSet<ScMak> ScMaks { get; set; } // SC_MAK
        public DbSet<ScMentoringFileInfo> ScMentoringFileInfoes { get; set; } // SC_MENTORING_FILE_INFO
        public DbSet<ScMentoringReport> ScMentoringReports { get; set; } // SC_MENTORING_REPORT
        public DbSet<ScMentoringTotalReport> ScMentoringTotalReports { get; set; } // SC_MENTORING_TOTAL_REPORT
        public DbSet<ScMentoringTrFileInfo> ScMentoringTrFileInfoes { get; set; } // SC_MENTORING_TR_FILE_INFO
        public DbSet<ScNtc> ScNtcs { get; set; } // SC_NTC
        public DbSet<ScQa> ScQas { get; set; } // SC_QA
        public DbSet<ScQcl> ScQcls { get; set; } // SC_QCL
        public DbSet<ScReqDoc> ScReqDocs { get; set; } // SC_REQ_DOC
        public DbSet<ScReqDocFile> ScReqDocFiles { get; set; } // SC_REQ_DOC_FILE
        public DbSet<ScUsrResume> ScUsrResumes { get; set; } // SC_USR_RESUME
        public DbSet<SyDareDbInfo> SyDareDbInfoes { get; set; } // SY_DARE_DB_INFO
        public DbSet<sys_DatabaseFirewallRule> sys_DatabaseFirewallRules { get; set; } // database_firewall_rules
        public DbSet<TcmsCompStatusSelectView> TcmsCompStatusSelectViews { get; set; } // TCMS_COMP_STATUS_SELECT_VIEW
        public DbSet<TcmsIfLastReport> TcmsIfLastReports { get; set; } // TCMS_IF_LAST_REPORT
        public DbSet<TcmsIfSurvey> TcmsIfSurveys { get; set; } // TCMS_IF_SURVEY
        public DbSet<TcmsMentoringReportSelectView> TcmsMentoringReportSelectViews { get; set; } // TCMS_MENTORING_REPORT_SELECT_VIEW
        public DbSet<VcBaInfo> VcBaInfoes { get; set; } // VC_BA_INFO
        public DbSet<VcBaTypeInfo> VcBaTypeInfoes { get; set; } // VC_BA_TYPE_INFO
        public DbSet<VcBizWork> VcBizWorks { get; set; } // VC_BIZ_WORK
        public DbSet<VcCompInfo> VcCompInfoes { get; set; } // VC_COMP_INFO
        public DbSet<VcCompMapping> VcCompMappings { get; set; } // VC_COMP_MAPPING
        public DbSet<VcIfBaInfo> VcIfBaInfoes { get; set; } // VC_IF_BA_INFO
        public DbSet<VcIfCompInfo> VcIfCompInfoes { get; set; } // VC_IF_COMP_INFO
        public DbSet<VcIfCompMapping> VcIfCompMappings { get; set; } // VC_IF_COMP_MAPPING
        public DbSet<VcIfMentorInfo> VcIfMentorInfoes { get; set; } // VC_IF_MENTOR_INFO
        public DbSet<VcIfNumInfo> VcIfNumInfoes { get; set; } // VC_IF_NUM_INFO
        public DbSet<VcIfQuesCompInfo> VcIfQuesCompInfoes { get; set; } // VC_IF_QUES_COMP_INFO
        public DbSet<VcIfTcmsInfo> VcIfTcmsInfoes { get; set; } // VC_IF_TCMS_INFO
        public DbSet<VcIfUsrInfo> VcIfUsrInfoes { get; set; } // VC_IF_USR_INFO
        public DbSet<VcLastReportNSat> VcLastReportNSats { get; set; } // VC_LAST_REPORT_N_SAT
        public DbSet<VcMentorInfo> VcMentorInfoes { get; set; } // VC_MENTOR_INFO
        public DbSet<VcMentorMapping> VcMentorMappings { get; set; } // VC_MENTOR_MAPPING
        public DbSet<VcNumMngInfo> VcNumMngInfoes { get; set; } // VC_NUM_MNG_INFO
        public DbSet<VcSatCheck> VcSatChecks { get; set; } // VC_SAT_CHECK
        public DbSet<VcTcmsInfo> VcTcmsInfoes { get; set; } // VC_TCMS_INFO
        public DbSet<VcUsrInfo> VcUsrInfoes { get; set; } // VC_USR_INFO
        
        static WebDbContext()
        {
            System.Data.Entity.Database.SetInitializer<WebDbContext>(null);
        }

        public WebDbContext()
            : base("Name=WebDbContext")
        {
            InitializePartial();
        }

        public WebDbContext(string connectionString) : base(connectionString)
        {
            InitializePartial();
        }

        public WebDbContext(string connectionString, System.Data.Entity.Infrastructure.DbCompiledModel model) : base(connectionString, model)
        {
            InitializePartial();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new CtWebLogConfiguration());
            modelBuilder.Configurations.Add(new MServiceDomainConfiguration());
            modelBuilder.Configurations.Add(new NPsGunguConfiguration());
            modelBuilder.Configurations.Add(new QuesCheckListConfiguration());
            modelBuilder.Configurations.Add(new QuesCompExtentionConfiguration());
            modelBuilder.Configurations.Add(new QuesCompHistoryConfiguration());
            modelBuilder.Configurations.Add(new QuesCompInfoConfiguration());
            modelBuilder.Configurations.Add(new QuesMasterConfiguration());
            modelBuilder.Configurations.Add(new QuesOgranAnalysisConfiguration());
            modelBuilder.Configurations.Add(new QuesResult1Configuration());
            modelBuilder.Configurations.Add(new QuesResult2Configuration());
            modelBuilder.Configurations.Add(new QuesWriterConfiguration());
            modelBuilder.Configurations.Add(new RptCheckListConfiguration());
            modelBuilder.Configurations.Add(new RptFinanceCommentConfiguration());
            modelBuilder.Configurations.Add(new RptMasterConfiguration());
            modelBuilder.Configurations.Add(new RptMentorCheckConfiguration());
            modelBuilder.Configurations.Add(new RptMentorCommentConfiguration());
            modelBuilder.Configurations.Add(new RptMentorRadioConfiguration());
            modelBuilder.Configurations.Add(new RptMngCodeConfiguration());
            modelBuilder.Configurations.Add(new RptMngCommentConfiguration());
            modelBuilder.Configurations.Add(new ScBizTypeConfiguration());
            modelBuilder.Configurations.Add(new ScCavConfiguration());
            modelBuilder.Configurations.Add(new ScCompanyFinanceConfiguration());
            modelBuilder.Configurations.Add(new ScFaqConfiguration());
            modelBuilder.Configurations.Add(new ScFileInfoConfiguration());
            modelBuilder.Configurations.Add(new ScFinancialIndexTConfiguration());
            modelBuilder.Configurations.Add(new ScFormConfiguration());
            modelBuilder.Configurations.Add(new ScFormFileConfiguration());
            modelBuilder.Configurations.Add(new ScMakConfiguration());
            modelBuilder.Configurations.Add(new ScMentoringFileInfoConfiguration());
            modelBuilder.Configurations.Add(new ScMentoringReportConfiguration());
            modelBuilder.Configurations.Add(new ScMentoringTotalReportConfiguration());
            modelBuilder.Configurations.Add(new ScMentoringTrFileInfoConfiguration());
            modelBuilder.Configurations.Add(new ScNtcConfiguration());
            modelBuilder.Configurations.Add(new ScQaConfiguration());
            modelBuilder.Configurations.Add(new ScQclConfiguration());
            modelBuilder.Configurations.Add(new ScReqDocConfiguration());
            modelBuilder.Configurations.Add(new ScReqDocFileConfiguration());
            modelBuilder.Configurations.Add(new ScUsrResumeConfiguration());
            modelBuilder.Configurations.Add(new SyDareDbInfoConfiguration());
            modelBuilder.Configurations.Add(new sys_DatabaseFirewallRuleConfiguration());
            modelBuilder.Configurations.Add(new TcmsCompStatusSelectViewConfiguration());
            modelBuilder.Configurations.Add(new TcmsIfLastReportConfiguration());
            modelBuilder.Configurations.Add(new TcmsIfSurveyConfiguration());
            modelBuilder.Configurations.Add(new TcmsMentoringReportSelectViewConfiguration());
            modelBuilder.Configurations.Add(new VcBaInfoConfiguration());
            modelBuilder.Configurations.Add(new VcBaTypeInfoConfiguration());
            modelBuilder.Configurations.Add(new VcBizWorkConfiguration());
            modelBuilder.Configurations.Add(new VcCompInfoConfiguration());
            modelBuilder.Configurations.Add(new VcCompMappingConfiguration());
            modelBuilder.Configurations.Add(new VcIfBaInfoConfiguration());
            modelBuilder.Configurations.Add(new VcIfCompInfoConfiguration());
            modelBuilder.Configurations.Add(new VcIfCompMappingConfiguration());
            modelBuilder.Configurations.Add(new VcIfMentorInfoConfiguration());
            modelBuilder.Configurations.Add(new VcIfNumInfoConfiguration());
            modelBuilder.Configurations.Add(new VcIfQuesCompInfoConfiguration());
            modelBuilder.Configurations.Add(new VcIfTcmsInfoConfiguration());
            modelBuilder.Configurations.Add(new VcIfUsrInfoConfiguration());
            modelBuilder.Configurations.Add(new VcLastReportNSatConfiguration());
            modelBuilder.Configurations.Add(new VcMentorInfoConfiguration());
            modelBuilder.Configurations.Add(new VcMentorMappingConfiguration());
            modelBuilder.Configurations.Add(new VcNumMngInfoConfiguration());
            modelBuilder.Configurations.Add(new VcSatCheckConfiguration());
            modelBuilder.Configurations.Add(new VcTcmsInfoConfiguration());
            modelBuilder.Configurations.Add(new VcUsrInfoConfiguration());

            OnModelCreatingPartial(modelBuilder);
        }

        public static DbModelBuilder CreateModel(DbModelBuilder modelBuilder, string schema)
        {
            modelBuilder.Configurations.Add(new CtWebLogConfiguration(schema));
            modelBuilder.Configurations.Add(new MServiceDomainConfiguration(schema));
            modelBuilder.Configurations.Add(new NPsGunguConfiguration(schema));
            modelBuilder.Configurations.Add(new QuesCheckListConfiguration(schema));
            modelBuilder.Configurations.Add(new QuesCompExtentionConfiguration(schema));
            modelBuilder.Configurations.Add(new QuesCompHistoryConfiguration(schema));
            modelBuilder.Configurations.Add(new QuesCompInfoConfiguration(schema));
            modelBuilder.Configurations.Add(new QuesMasterConfiguration(schema));
            modelBuilder.Configurations.Add(new QuesOgranAnalysisConfiguration(schema));
            modelBuilder.Configurations.Add(new QuesResult1Configuration(schema));
            modelBuilder.Configurations.Add(new QuesResult2Configuration(schema));
            modelBuilder.Configurations.Add(new QuesWriterConfiguration(schema));
            modelBuilder.Configurations.Add(new RptCheckListConfiguration(schema));
            modelBuilder.Configurations.Add(new RptFinanceCommentConfiguration(schema));
            modelBuilder.Configurations.Add(new RptMasterConfiguration(schema));
            modelBuilder.Configurations.Add(new RptMentorCheckConfiguration(schema));
            modelBuilder.Configurations.Add(new RptMentorCommentConfiguration(schema));
            modelBuilder.Configurations.Add(new RptMentorRadioConfiguration(schema));
            modelBuilder.Configurations.Add(new RptMngCodeConfiguration(schema));
            modelBuilder.Configurations.Add(new RptMngCommentConfiguration(schema));
            modelBuilder.Configurations.Add(new ScBizTypeConfiguration(schema));
            modelBuilder.Configurations.Add(new ScCavConfiguration(schema));
            modelBuilder.Configurations.Add(new ScCompanyFinanceConfiguration(schema));
            modelBuilder.Configurations.Add(new ScFaqConfiguration(schema));
            modelBuilder.Configurations.Add(new ScFileInfoConfiguration(schema));
            modelBuilder.Configurations.Add(new ScFinancialIndexTConfiguration(schema));
            modelBuilder.Configurations.Add(new ScFormConfiguration(schema));
            modelBuilder.Configurations.Add(new ScFormFileConfiguration(schema));
            modelBuilder.Configurations.Add(new ScMakConfiguration(schema));
            modelBuilder.Configurations.Add(new ScMentoringFileInfoConfiguration(schema));
            modelBuilder.Configurations.Add(new ScMentoringReportConfiguration(schema));
            modelBuilder.Configurations.Add(new ScMentoringTotalReportConfiguration(schema));
            modelBuilder.Configurations.Add(new ScMentoringTrFileInfoConfiguration(schema));
            modelBuilder.Configurations.Add(new ScNtcConfiguration(schema));
            modelBuilder.Configurations.Add(new ScQaConfiguration(schema));
            modelBuilder.Configurations.Add(new ScQclConfiguration(schema));
            modelBuilder.Configurations.Add(new ScReqDocConfiguration(schema));
            modelBuilder.Configurations.Add(new ScReqDocFileConfiguration(schema));
            modelBuilder.Configurations.Add(new ScUsrResumeConfiguration(schema));
            modelBuilder.Configurations.Add(new SyDareDbInfoConfiguration(schema));
            modelBuilder.Configurations.Add(new sys_DatabaseFirewallRuleConfiguration(schema));
            modelBuilder.Configurations.Add(new TcmsCompStatusSelectViewConfiguration(schema));
            modelBuilder.Configurations.Add(new TcmsIfLastReportConfiguration(schema));
            modelBuilder.Configurations.Add(new TcmsIfSurveyConfiguration(schema));
            modelBuilder.Configurations.Add(new TcmsMentoringReportSelectViewConfiguration(schema));
            modelBuilder.Configurations.Add(new VcBaInfoConfiguration(schema));
            modelBuilder.Configurations.Add(new VcBaTypeInfoConfiguration(schema));
            modelBuilder.Configurations.Add(new VcBizWorkConfiguration(schema));
            modelBuilder.Configurations.Add(new VcCompInfoConfiguration(schema));
            modelBuilder.Configurations.Add(new VcCompMappingConfiguration(schema));
            modelBuilder.Configurations.Add(new VcIfBaInfoConfiguration(schema));
            modelBuilder.Configurations.Add(new VcIfCompInfoConfiguration(schema));
            modelBuilder.Configurations.Add(new VcIfCompMappingConfiguration(schema));
            modelBuilder.Configurations.Add(new VcIfMentorInfoConfiguration(schema));
            modelBuilder.Configurations.Add(new VcIfNumInfoConfiguration(schema));
            modelBuilder.Configurations.Add(new VcIfQuesCompInfoConfiguration(schema));
            modelBuilder.Configurations.Add(new VcIfTcmsInfoConfiguration(schema));
            modelBuilder.Configurations.Add(new VcIfUsrInfoConfiguration(schema));
            modelBuilder.Configurations.Add(new VcLastReportNSatConfiguration(schema));
            modelBuilder.Configurations.Add(new VcMentorInfoConfiguration(schema));
            modelBuilder.Configurations.Add(new VcMentorMappingConfiguration(schema));
            modelBuilder.Configurations.Add(new VcNumMngInfoConfiguration(schema));
            modelBuilder.Configurations.Add(new VcSatCheckConfiguration(schema));
            modelBuilder.Configurations.Add(new VcTcmsInfoConfiguration(schema));
            modelBuilder.Configurations.Add(new VcUsrInfoConfiguration(schema));
            return modelBuilder;
        }

        partial void InitializePartial();
        partial void OnModelCreatingPartial(DbModelBuilder modelBuilder);
    }
}
