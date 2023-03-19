using Microsoft.VisualBasic;
using System;

namespace TicketSystem.Models.Entities;

public class CommentEntity
{
    public int Id { get; set; }
    public string Comment { get; set; } = null!;
    public DateTime CreatedAt { get; set; }

    public int TicketId { get; set; }
    public TicketEntity Ticket { get; set; } = null!;

    public int UserId { get; set; }
    public UserEntity User { get; set; } = null!;
}