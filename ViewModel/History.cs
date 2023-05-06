using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja.ViewModel
{
    public class History
    {
        public DateTime fromDateTime { get; set; }
        public DateTime tillDateTime { get; set; }
        public List<Value> values { get; set; }
        public List<Index> indexes { get; set; }
        public List<Standard> standards { get; set; }
    }
}
