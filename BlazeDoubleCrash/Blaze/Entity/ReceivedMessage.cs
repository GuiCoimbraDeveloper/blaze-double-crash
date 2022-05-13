using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Blaze.Entity
{
    public class ReceivedMessage
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("payload")]
        public Payload Payload { get; set; }

        public override string ToString()
        {
            return $"{Id} pay: ${Payload.Id} ${Payload.Color} ${Payload.Roll} ${Payload.CreatedAt} ${Payload.UpdatedAt} ${Payload.Status}";
        }
    }
}
