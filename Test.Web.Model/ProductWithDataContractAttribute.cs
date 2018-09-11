using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Test.Web.Model
{
    [DataContract]
    public class ProductWithDataContractAttribute
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        public int ProductCode { get; set; }  // omitted by default
    }
}
