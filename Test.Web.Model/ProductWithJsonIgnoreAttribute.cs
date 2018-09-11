using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Web.Model
{
    public class ProductWithJsonIgnoreAttribute
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        [JsonIgnore]
        public int ProductCode { get; set; } // omitted
    }
}
