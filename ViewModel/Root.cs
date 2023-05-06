using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplikacja.ViewModel
{
    public class Root
    {
        public Current current { get; set; }
        public List<History> history { get; set; }
        public List<Forecast> forecast { get; set; }
    }
}
