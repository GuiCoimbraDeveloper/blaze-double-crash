using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crash.Entity
{
    public class Payload
    {
        public string id { get; set; }
        public DateTime updated_at { get; set; }
        public string status { get; set; }
        public string crash_point { get; set; }
        public List<Bet> bets { get; set; }
        public string total_eur_bet { get; set; }
        public string total_bets_placed { get; set; }
        public string total_eur_won { get; set; }
    }
}
