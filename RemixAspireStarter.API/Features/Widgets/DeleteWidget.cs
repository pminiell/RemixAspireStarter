using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RemixAspireStarter.Data;
using RemixAspireStarter.Data.Models;

namespace RemixAspireStarter.API.Features.Widgets;

public class DeleteWidget
{
  public record class Command(int Id) : IRequest<Result>;

  public record class Result(int Id, DateTime DeletedAt);

  public class Validator : AbstractValidator<Command>
  {
    public Validator()
    {
      RuleFor(x => x.Id).GreaterThan(0);
    }
  }

  public class Handler : IRequestHandler<Command, Result>
  {
    private readonly ApplicationDbContext _db;

    public Handler(ApplicationDbContext db)
    {
      _db = db;
    }
    public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
    {
      var widgetsExist = await _db.Widgets.AnyAsync(w => w.Id == command.Id, cancellationToken: cancellationToken);
      if (!widgetsExist)
      {
        throw new Exception($"Cannot delete, the widget could not be found:{command.Id}");
      }
      Widget widgetToDelete = await _db.Widgets.FirstAsync(w => w.Id == command.Id, cancellationToken: cancellationToken);
      _db.Widgets.Remove(widgetToDelete);
      await _db.SaveChangesAsync(cancellationToken);
      return new Result(widgetToDelete.Id, DateTime.UtcNow);
    }
  }
}

