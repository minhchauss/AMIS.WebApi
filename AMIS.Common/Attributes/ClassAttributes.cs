using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMIS.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
   public class MISARequired:Attribute
    {
        public string MsgError = string.Empty;
        public MISARequired(string msgError)
        {
            MsgError = msgError;
        }
    }
    public class MISAMaxLength : Attribute
    {
        public string MsgError = string.Empty;
        public int Maxlength = 0;
        public MISAMaxLength(int maxlength=0,string msgError="")
        {
            MsgError = msgError;
            Maxlength = maxlength;
        }
    }

    public class MISAValidateEmail : Attribute
    {
        public string MsgError = string.Empty;
        public string RegexEmail = string.Empty;
        public MISAValidateEmail(string regexEmail = "", string msgError = "")
        {
            MsgError = msgError;
            RegexEmail = regexEmail;
        }
    }
    public class MISADuplicate : Attribute
    {
        public string MsgError = string.Empty;
        public MISADuplicate( string msgError)
        {
            MsgError = msgError;
        }
    }
}
