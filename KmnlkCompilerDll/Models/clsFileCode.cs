using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KmnlkCompilerDll.Models
{
    public class clsFileCode
    {
        public List<string> codes { set; get; }
        public int type { set; get; }

        public clsFileCode()
        {
            codes = new List<string>();
        }
        public clsFileCode(List<string> codes,int type)
        {
            this.codes = codes;
            this.type = type;
        }
    }
}
