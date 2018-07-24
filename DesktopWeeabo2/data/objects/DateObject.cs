using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.data.objects {
    public class DateObject {

        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }

        public string toString() {
            return year + "-" + month + "-" + day;
        }

        public DateTime getDateTime() {
            return DateTime.Parse(toString());
        }
    }
}
