using System.ComponentModel;

namespace Trader.Models;

public class Traders
{
    public Guid Id { get; set; }
    [DisplayName("Full Name")]
    public string Name { get; set; }
    public string Email { get; set; }
}
