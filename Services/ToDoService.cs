
using Grpc.Core;
using ToDoGRPC.Data;
using ToDoGRPC.Models;

namespace ToDoGRPC.Services;

public class ToDoService : ToDoGRPC.ToDoService.ToDoServiceBase
{
    private readonly AppDbContext _context;

    public ToDoService(AppDbContext context)=> _context = context;

    public override async Task<CreateToDoResponse> CreateToDo(CreateToDoRequest request, ServerCallContext context)
    {
        if(request.Title == string.Empty || request.Description == string.Empty)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Title or Description cannot be empty"));
        var toDoItem = new ToDoItem
        {
            Title = request.Title,
            Description = request.Description,
        };
        await _context.AddAsync(toDoItem);
        await _context.SaveChangesAsync();

        return await Task.FromResult(new CreateToDoResponse
        {
            Id = toDoItem.Id
        });
    }
}