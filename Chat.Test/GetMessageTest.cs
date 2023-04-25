using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Chat.API;
using Chat.API.CQRS.Meet.GetMessage;
using Chat.API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Chat.Test
{
    public class GetMessageTest
    {
        private readonly Mock<AppDbContext> _mockDbContext;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly Meet _meet;
        private readonly GetMessageQueryRequest _request;

        public GetMessageTest()
        {
            var meetId = new Guid();
            var senderId = new Guid().ToString();
            var receiverId = new Guid().ToString();


            _mockDbContext = new Mock<AppDbContext>();
            _mockMapper = new Mock<IMapper>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();

            _request = new GetMessageQueryRequest()
            {
                Page = 1,
                PageSize = 10,
                ReceiveId = receiverId
            };

            _meet = new Meet()
            {
                Id = new Guid(),
                LastMessage = "Napıyon",
                SenderId = senderId,
                ReceiverId = receiverId,
                CreatedAt = DateTime.UtcNow,
                Messages = new List<Message>()
                {
                    new()
                    {
                        SenderId = senderId,
                        ReceiverId = receiverId,
                        Id = new Guid(),
                        Text = "Napıyon",
                        MeetId = meetId,
                        CreatedAt = DateTime.UtcNow,
                        DeletedAt = null
                    }
                }
            };
        }

        [Fact]
        public void ReturnNofFoundMeet()
        {
            _mockDbContext.Setup(x => x.Meets.Find(1)).Returns(_meet);


            var handler = new GetMessageQueryHandler(_mockDbContext.Object, _mockHttpContextAccessor.Object,
                _mockMapper.Object);


            var result = handler.Handle(_request, CancellationToken.None);
        }
    }
}