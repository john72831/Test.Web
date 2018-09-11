using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Web.Model
{
    public class ETag
    {
        public string Tag { get; set; }
    }

    public enum ETagMatch
    {
        IfMatch,
        IfNoneMatch
    }
}
