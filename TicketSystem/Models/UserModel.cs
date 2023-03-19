using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TicketSystem.Models;

public class UserModel
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string StreetName { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string City { get; set; } = null!;
    public string DisplayName => $"{FirstName} {LastName}";
    public string Address => $"{StreetName}, {PostalCode} {City}";
}
