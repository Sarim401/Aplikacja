using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja.ViewModel
{
    public class Forecast
    {
        public DateTime fromDateTime { get; set; }
        public DateTime tillDateTime { get; set; }
        public List<object> values { get; set; }
        public List<Index> indexes { get; set; }
        public List<object> standards { get; set; }
    }
}
