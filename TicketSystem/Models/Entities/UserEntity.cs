using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketSystem.Models.Entities;

[Index(nameof(Email), IsUnique = true)]
public class UserEntity
{
    public int Id { get; set; }

    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    public string LastName { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [Column(TypeName = "varchar(10)")]
    public string? PhoneNumber { get; set; }


    public int AddressId { get; set; }
    public AddressEntity Address { get; set; } = null!;

    public ICollection<TicketEntity> Tickets { get; set; } = new HashSet<TicketEntity>();

    public ICollection<CommentEntity> Comments { get; set; } = new HashSet<CommentEntity>();

}
