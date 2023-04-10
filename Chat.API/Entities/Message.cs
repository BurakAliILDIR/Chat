using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Chat.API.Entities
{
    public class Message
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string MeetId { get; init; }
        public string ReceiverId { get; init; }
        public string SenderId { get; init; }
        public string Text { get; init; }
        public DateTime CreatedAt { get; init; }


        public Message(string receiverId, string senderId, string text)
        {
            MeetId = GetId(receiverId, senderId);

            ReceiverId = receiverId;
            SenderId = senderId;
            Text = text;
            CreatedAt = DateTime.UtcNow;
        }

        public static string GetId(string receiverId, string senderId)
        {
            if (receiverId.CompareTo(senderId) > senderId.CompareTo(receiverId))
                return $"{senderId}_{receiverId}";
            else
                return $"{receiverId}_{senderId}";
        }
    }
}