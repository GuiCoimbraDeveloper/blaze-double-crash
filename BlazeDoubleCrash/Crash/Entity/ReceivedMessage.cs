using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Crash.Entity
{
    public class ReceivedMessage
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("payload")]
        public Payload Payload { get; set; }

        public override string ToString()
        {
            return $"{Id} id:{Payload.id} crash_point:{Payload.crash_point} status:{Payload.status} total_bets_placed:{Payload.total_bets_placed} total_eur_bet:{Payload.total_eur_bet} total_eur_won:{Payload.total_eur_won} updated_at:{Payload.updated_at}";
        }
    }
}
