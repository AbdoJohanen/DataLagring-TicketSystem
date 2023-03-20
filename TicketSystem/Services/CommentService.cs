using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TicketSystem.Contexts;
using TicketSystem.Models.Entities;

namespace TicketSystem.Services;

internal class CommentService
{
    public static async Task<CommentEntity> SaveAsync(CommentEntity commentEntity)
    {
        using var context = new DataContext();
        var ticketEntity = await context.Tickets.Include(x => x.Comments).ThenInclude(comment => comment.User).FirstOrDefaultAsync(x => x.Id == commentEntity.TicketId);

        if (ticketEntity != null)
        {
            ticketEntity.Comments.Add(commentEntity);
            await context.SaveChangesAsync();
        }

        return commentEntity;
    }
}
