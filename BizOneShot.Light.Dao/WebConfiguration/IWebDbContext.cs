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
    public interface IWebDbContext : IDisposable
    {
        DbSet<CtWebLog> CtWebLogs { get; set; } // CT_WEB_LOG
        DbSet<MServiceDomain> MServiceDomains { get; set; } // M_SERVICE_DOMAIN
        DbSet<NPsGungu> NPsGungus { get; set; } // N_PS_GUNGU
        DbSet<QuesCheckList> QuesCheckLists { get; set; } // QUES_CHECK_LIST
        DbSet<QuesCompExtention> QuesCompExtentions { get; set; } // QUES_COMP_EXTENTION
        DbSet<QuesCompHistory> QuesCompHistories { get; set; } // QUES_COMP_HISTORY
        DbSet<QuesCompInfo> QuesCompInfoes { get; set; } // QUES_COMP_INFO
        DbSet<QuesMaster> QuesMasters { get; set; } // QUES_MASTER
        DbSet<QuesOgranAnalysis> QuesOgranAnalysis { get; set; } // QUES_OGRAN_ANALYSIS
        DbSet<QuesResult1> QuesResult1 { get; set; } // QUES_RESULT1
        DbSet<QuesResult2> QuesResult2 { get; set; } // QUES_RESULT2
        DbSet<QuesWriter> QuesWriters { get; set; } // QUES_WRITER
        DbSet<RptCheckList> RptCheckLists { get; set; } // RPT_CHECK_LIST
        DbSet<RptFinanceComment> RptFinanceComments { get; set; } // RPT_FINANCE_COMMENT
        DbSet<RptMaster> RptMasters { get; set; } // RPT_MASTER
        DbSet<RptMentorCheck> RptMentorChecks { get; set; } // RPT_MENTOR_CHECK
        DbSet<RptMentorComment> RptMentorComments { get; set; } // RPT_MENTOR_COMMENT
        DbSet<RptMentorRadio> RptMentorRadios { get; set; } // RPT_MENTOR_RADIO
        DbSet<RptMngCode> RptMngCodes { get; set; } // RPT_MNG_CODE
        DbSet<RptMngComment> RptMngComments { get; set; } // RPT_MNG_COMMENT
        DbSet<ScBizType> ScBizTypes { get; set; } // SC_BIZ_TYPE
        DbSet<ScCav> ScCavs { get; set; } // SC_CAV
        DbSet<ScCompanyFinance> ScCompanyFinances { get; set; } // SC_COMPANY_FINANCE
        DbSet<ScFaq> ScFaqs { get; set; } // SC_FAQ
        DbSet<ScFileInfo> ScFileInfoes { get; set; } // SC_FILE_INFO
        DbSet<ScFinancialIndexT> ScFinancialIndexTs { get; set; } // SC_FINANCIAL_INDEX_T
        DbSet<ScForm> ScForms { get; set; } // SC_FORM
        DbSet<ScFormFile> ScFormFiles { get; set; } // SC_FORM_FILE
        DbSet<ScMak> ScMaks { get; set; } // SC_MAK
        DbSet<ScMentoringFileInfo> ScMentoringFileInfoes { get; set; } // SC_MENTORING_FILE_INFO
        DbSet<ScMentoringReport> ScMentoringReports { get; set; } // SC_MENTORING_REPORT
        DbSet<ScMentoringTotalReport> ScMentoringTotalReports { get; set; } // SC_MENTORING_TOTAL_REPORT
        DbSet<ScMentoringTrFileInfo> ScMentoringTrFileInfoes { get; set; } // SC_MENTORING_TR_FILE_INFO
        DbSet<ScNtc> ScNtcs { get; set; } // SC_NTC
        DbSet<ScQa> ScQas { get; set; } // SC_QA
        DbSet<ScQcl> ScQcls { get; set; } // SC_QCL
        DbSet<ScReqDoc> ScReqDocs { get; set; } // SC_REQ_DOC
        DbSet<ScReqDocFile> ScReqDocFiles { get; set; } // SC_REQ_DOC_FILE
        DbSet<ScUsrResume> ScUsrResumes { get; set; } // SC_USR_RESUME
        DbSet<SyDareDbInfo> SyDareDbInfoes { get; set; } // SY_DARE_DB_INFO
        DbSet<sys_DatabaseFirewallRule> sys_DatabaseFirewallRules { get; set; } // database_firewall_rules
        DbSet<TcmsCompStatusSelectView> TcmsCompStatusSelectViews { get; set; } // TCMS_COMP_STATUS_SELECT_VIEW
        DbSet<TcmsIfLastReport> TcmsIfLastReports { get; set; } // TCMS_IF_LAST_REPORT
        DbSet<TcmsIfSurvey> TcmsIfSurveys { get; set; } // TCMS_IF_SURVEY
        DbSet<TcmsMentoringReportSelectView> TcmsMentoringReportSelectViews { get; set; } // TCMS_MENTORING_REPORT_SELECT_VIEW
        DbSet<VcBaInfo> VcBaInfoes { get; set; } // VC_BA_INFO
        DbSet<VcBaTypeInfo> VcBaTypeInfoes { get; set; } // VC_BA_TYPE_INFO
        DbSet<VcBizWork> VcBizWorks { get; set; } // VC_BIZ_WORK
        DbSet<VcCompInfo> VcCompInfoes { get; set; } // VC_COMP_INFO
        DbSet<VcCompMapping> VcCompMappings { get; set; } // VC_COMP_MAPPING
        DbSet<VcIfBaInfo> VcIfBaInfoes { get; set; } // VC_IF_BA_INFO
        DbSet<VcIfCompInfo> VcIfCompInfoes { get; set; } // VC_IF_COMP_INFO
        DbSet<VcIfCompMapping> VcIfCompMappings { get; set; } // VC_IF_COMP_MAPPING
        DbSet<VcIfMentorInfo> VcIfMentorInfoes { get; set; } // VC_IF_MENTOR_INFO
        DbSet<VcIfNumInfo> VcIfNumInfoes { get; set; } // VC_IF_NUM_INFO
        DbSet<VcIfQuesCompInfo> VcIfQuesCompInfoes { get; set; } // VC_IF_QUES_COMP_INFO
        DbSet<VcIfTcmsInfo> VcIfTcmsInfoes { get; set; } // VC_IF_TCMS_INFO
        DbSet<VcIfUsrInfo> VcIfUsrInfoes { get; set; } // VC_IF_USR_INFO
        DbSet<VcLastReportNSat> VcLastReportNSats { get; set; } // VC_LAST_REPORT_N_SAT
        DbSet<VcMentorInfo> VcMentorInfoes { get; set; } // VC_MENTOR_INFO
        DbSet<VcMentorMapping> VcMentorMappings { get; set; } // VC_MENTOR_MAPPING
        DbSet<VcNumMngInfo> VcNumMngInfoes { get; set; } // VC_NUM_MNG_INFO
        DbSet<VcSatCheck> VcSatChecks { get; set; } // VC_SAT_CHECK
        DbSet<VcTcmsInfo> VcTcmsInfoes { get; set; } // VC_TCMS_INFO
        DbSet<VcUsrInfo> VcUsrInfoes { get; set; } // VC_USR_INFO

        int SaveChanges();
        System.Threading.Tasks.Task<int> SaveChangesAsync();
        System.Threading.Tasks.Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }

}
