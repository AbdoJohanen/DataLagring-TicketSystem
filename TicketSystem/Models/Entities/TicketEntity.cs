using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TicketSystem.Models.Entities;

public class TicketEntity
{
    public int Id { get; set; }

    [StringLength(20)]
    public string Status { get; set; } = null!;

    [StringLength(50)]
    public string Subject { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime CreatedAt { get; set; }


    public int UserId { get; set; }
    public UserEntity User { get; set; } = null!;

    public ICollection<CommentEntity> Comments { get; set; } = new HashSet<CommentEntity>();
}
