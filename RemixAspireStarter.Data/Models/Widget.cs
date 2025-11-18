using System.ComponentModel.DataAnnotations;

namespace RemixAspireStarter.Data.Models;

public class Widget
{
  public int Id { get; set; }
  [Required]
  public string Name { get; set; } = string.Empty;
  public int Quantity { get; set; } = 0;
}
