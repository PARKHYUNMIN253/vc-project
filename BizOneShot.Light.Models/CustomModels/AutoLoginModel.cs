using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizOneShot.Light.Models.CustomModels
{
    public class AutoLoginModel
    {
        public string result { get; set; }
        public string TcmsLoginKey { get; set; }
        public string UsrType { get; set; }
    }

    public class StatusModel
    {
        public string status { get; set; }
    }
}
