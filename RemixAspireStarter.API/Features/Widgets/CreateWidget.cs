using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RemixAspireStarter.Data;
using RemixAspireStarter.Data.Models;

namespace RemixAspireStarter.API.Features.Widgets;

public class CreateWidget
{
  public record class Command(int Id, string Name, int Quantity) : IRequest<Result>;

  public record class Result(int Id, string Name, int Quantity);


  public class MapperProfile : Profile
  {

    public MapperProfile()
    {
      CreateMap<Command, Widget>();
      CreateMap<Widget, Result>();
    }
  }

  public class Validator : AbstractValidator<Command>
  {
    public Validator()
    {
      RuleFor(x => x.Id).GreaterThan(0);
      RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
      RuleFor(x => x.Quantity).GreaterThan(0);
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

    public async Task<Result> Handle(Command command, CancellationToken cancellationToken = default)
    {

      var widgetsExist = await _db.Widgets.AnyAsync(w => w.Id == command.Id, cancellationToken: cancellationToken);
      if (widgetsExist)
      {
        throw new Exception($"The widget already exists :{command.Id}");
      }
      Widget newWidget = _mapper.Map<Widget>(command);
      _db.Widgets.Add(newWidget);
      await _db.SaveChangesAsync(cancellationToken);
      return _mapper.Map<Result>(newWidget);
    }
  }
}

