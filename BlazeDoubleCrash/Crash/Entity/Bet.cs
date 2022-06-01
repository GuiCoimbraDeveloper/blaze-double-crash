using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crash.Entity
{
    public class Bet
    {
        public string id { get; set; }
        public double? cashed_out_at { get; set; }
        public double? amount { get; set; }
        public string currency_type { get; set; }
        public User user { get; set; }
        public string win_amount { get; set; }
        public string status { get; set; }
    }
}
