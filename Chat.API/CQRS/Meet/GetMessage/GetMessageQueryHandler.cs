using System.Security.Claims;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Chat.API.CQRS.Meet.GetMessage;

public class GetMessageQueryHandler : IRequestHandler<GetMessageQueryRequest, GetMessageQueryResponse>
{
    private readonly AppDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    public GetMessageQueryHandler(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor, IMapper mapper)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }

    public async Task<GetMessageQueryResponse> Handle(GetMessageQueryRequest request,
        CancellationToken cancellationToken)
    {
        var senderId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);


        var meet = await _dbContext.Meets
            .Include(x =>
                x.Messages
                    .Where(x => x.DeletedAt == null)
                    .OrderByDescending(x => x.CreatedAt)
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize))
            .Where(x =>
                ((x.SenderId == senderId || x.ReceiverId == senderId) &&
                 (x.SenderId == request.ReceiveId ||
                  x.ReceiverId == request.ReceiveId)))
            .FirstOrDefaultAsync();

        if (meet is null) throw new Exception("Not found meet.");

        var meetDto = _mapper.Map<GetMeetDto>(meet);

        meetDto.Receiver = senderId == meet.SenderId ? meet.ReceiverId : meet.SenderId;

        return new GetMessageQueryResponse
        {
            Data = meetDto
        };
    }
}