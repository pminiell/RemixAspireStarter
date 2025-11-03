using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RemixAspireStarter.Data;
using RemixAspireStarter.Data.Models;

namespace RemixAspireStarter.API.Features.Widgets;


public static class UpdateWidget
{
  public record Command(int Id, string Name, int Quantity) : IRequest<Result>;

  public record Result(int Id, string Name, int Quantity);

  public class MapperProfile : Profile
  {
    public MapperProfile()
    {
      CreateMap<Command, Widget>();
      CreateMap<Widget, Result>();
    }
  }

  public class Handler : IRequestHandler<Command, Result>
  {
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public Handler(ApplicationDbContext db, IMapper mapper)
    {
      _db = db;
      _mapper = mapper;
    }

    public async Task<Result> Handle(Command command, CancellationToken cancellationToken)
    {
      Widget widgetToUpdate = _mapper.Map<Widget>(command);
      var widget = await _db.Widgets.FindAsync(new object[] { widgetToUpdate.Id }, cancellationToken);
      if (widget != null)
      {
        widget.Name = widgetToUpdate.Name;
        widget.Quantity = widgetToUpdate.Quantity;

        await _db.SaveChangesAsync(cancellationToken);

        return new Result(widget.Id, widget.Name, widget.Quantity);
      }

      throw new KeyNotFoundException($"Widget with ID {command.Id} not found.");
    }
  }
}

