using Microsoft.EntityFrameworkCore;
using RemixAspireStarter.Data;
using MediatR;

namespace RemixAspireStarter.API.Features.Widgets;


public class FetchWidgets
{

  public record class Query : IRequest<IEnumerable<Result>>;
  public record class Result(int Id, string Name, int Quantity);

  public class Handler : IRequestHandler<Query, IEnumerable<Result>>
  {
    private readonly ApplicationDbContext _db;

    public Handler(ApplicationDbContext db)
    {
      _db = db;
    }
    public async Task<IEnumerable<Result>> Handle(Query query, CancellationToken cancellationToken)
    {
      var widgets = await _db.Widgets.OrderBy(w => w.Id).ToListAsync(cancellationToken);
      return widgets.Select(w => new Result(w.Id, w.Name, w.Quantity));
    }
  }
}
