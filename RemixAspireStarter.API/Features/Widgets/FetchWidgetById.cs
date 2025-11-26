using Microsoft.EntityFrameworkCore;
using RemixAspireStarter.Data;
using MediatR;

namespace RemixAspireStarter.API.Features.Widgets;


public class FetchWidgetById
{

  public record class Query(int Id) : IRequest<Result>;
  public record class Result(int Id, string Name, int Quantity);

  public class Handler : IRequestHandler<Query, Result>
  {
    private readonly ApplicationDbContext _db;

    public Handler(ApplicationDbContext db)
    {
      _db = db;
    }
    public async Task<Result> Handle(Query query, CancellationToken cancellationToken)
    {
      var widget = await _db.Widgets.FirstOrDefaultAsync(w => w.Id == query.Id, cancellationToken);
      return widget != null
        ? new Result(widget.Id, widget.Name, widget.Quantity)
        : throw new KeyNotFoundException($"Widget with ID {query.Id} not found.");
    }
  }
}
