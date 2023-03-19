using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketSystem.Models.Entities;

public class AddressEntity
{
    public int Id { get; set; }

    [Column(TypeName = "nvarchar(100)")]
    public string StreetName { get; set; } = null!;

    [Column(TypeName = "char(5)")]
    public string PostalCode { get; set; } = null!;

    [Column(TypeName = "nvarchar(100)")]
    public string City { get; set; } = null!;

     public ICollection<UserEntity> Users { get; set; } = new HashSet<UserEntity>();
}
