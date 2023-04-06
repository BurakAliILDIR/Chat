using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Chat.API.Entities
{
    public class Meet
    {
        public IEnumerable<Message> Messages { get; set; }

        public static string MeetId(string One, string Two)
        {
            if (One.CompareTo(Two) > Two.CompareTo(One))
            {
                return Two + One;
            }
            else
            {
                return One + Two;
            }
        }
    }
}