using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Chat.API.Entities
{
    public class Meet
    {
        public string Id { get; set; }
        public string LastMessage { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<Message> Messages { get; set; }

        public static string MeetId(string One, string Two)
        {
            if (One.CompareTo(Two) > Two.CompareTo(One))
            {
                return $"{Two}_{One}";
            }

            return $"{One}_{Two}";
        }
    }
}