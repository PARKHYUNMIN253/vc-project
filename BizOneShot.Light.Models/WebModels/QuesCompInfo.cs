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
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Threading;

namespace BizOneShot.Light.Models.WebModels
{
    // QUES_COMP_INFO
    public class QuesCompInfo
    {
        public int QuestionSn { get; set; } // QUESTION_SN (Primary key)
        public string CompNm { get; set; } // COMP_NM
        public string EngCompNm { get; set; } // ENG_COMP_NM
        public string TelNo { get; set; } // TEL_NO
        public string FaxNo { get; set; } // FAX_NO
        public string Name { get; set; } // NAME
        public string Email { get; set; } // EMAIL
        public string RegistrationNo { get; set; } // REGISTRATION_NO
        public string CoRegistrationNo { get; set; } // CO_REGISTRATION_NO
        public DateTime? PublishDt { get; set; } // PUBLISH_DT
        public string HomeUrl { get; set; } // HOME_URL
        public string CompAddr { get; set; } // COMP_ADDR
        public string FactoryAddr { get; set; } // FACTORY_ADDR
        public string LabAddr { get; set; } // LAB_ADDR
        public string ProductNm1 { get; set; } // PRODUCT_NM1
        public string ProductNm2 { get; set; } // PRODUCT_NM2
        public string ProductNm3 { get; set; } // PRODUCT_NM3
        public bool? MarketPublic { get; set; } // MARKET_PUBLIC
        public bool? MarketCivil { get; set; } // MARKET_CIVIL
        public bool? MarketConsumer { get; set; } // MARKET_CONSUMER
        public bool? MarketForeing { get; set; } // MARKET_FOREING
        public bool? MarketEtc { get; set; } // MARKET_ETC
        public string CompType { get; set; } // COMP_TYPE
        public string ResidentType { get; set; } // RESIDENT_TYPE
        public bool? CertiVenture { get; set; } // CERTI_VENTURE
        public bool? CertiRnd { get; set; } // CERTI_RND
        public bool? CertiMainbiz { get; set; } // CERTI_MAINBIZ
        public bool? CertiInnobiz { get; set; } // CERTI_INNOBIZ
        public DateTime? RegDt { get; set; } // REG_DT
        public DateTime? UpdDt { get; set; } // UPD_DT
        public string CompLeaseYn { get; set; } // COMP_LEASE_YN
        public string FactoryLeaseYn { get; set; } // FACTORY_LEASE_YN
        public string LabLeaseYn { get; set; } // LAB_LEASE_YN
        public string StandardCode1 { get; set; } // STANDARD_CODE1
        public string StandardCode2 { get; set; } // STANDARD_CODE2
        public string StandardCode3 { get; set; } // STANDARD_CODE3
        public string MainSellMarket { get; set; } // MAIN_SELL_MARKET
        public bool? CertiRoot { get; set; } // CERTI_ROOT
        public bool? CertiGreen { get; set; } // CERTI_GREEN
        public bool? CertiWoman { get; set; } // CERTI_WOMAN
        public bool? CertiSocial { get; set; } // CERTI_SOCIAL
        public bool? CertiEtc { get; set; } // CERTI_ETC
        public string NumSn { get; set; } // NUM_SN
        public string MarketEtcText { get; set; } // MARKET_ETC_TEXT
        public string CertiEtcText { get; set; } // CERTI_ETC_TEXT
        public string ResidentEtc { get; set; } // RESIDENT_ETC
        public int? TcmsLoginKey { get; set; } // TCMS_LOGIN_KEY

        // Foreign keys
        public virtual QuesMaster QuesMaster { get; set; } // FK_QUES_MASTER_TO_QUES_COMP_INFO
    }

}
