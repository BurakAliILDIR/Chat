using System.Text.Json.Serialization;
using AutoMapper.Configuration.Annotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Chat.API.CQRS.Meet.GetMessage
{
    public class GetMessageQueryRequest : IRequest<GetMessageQueryResponse>
    {
        public string ReceiveId { get; set; }
        public int Page { get; set; } = 0;
        public int PageSize { get; set; } = 10;
    }
}