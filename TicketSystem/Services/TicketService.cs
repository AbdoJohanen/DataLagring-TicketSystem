using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TicketSystem.Contexts;
using TicketSystem.Models.Entities;

namespace TicketSystem.Services;
internal class TicketService
{
    public static async Task<TicketEntity> SaveAsync(TicketEntity ticketEntity)
    {
        using var context = new DataContext();
        context.Add(ticketEntity);
        await context.SaveChangesAsync();

        return ticketEntity;
    }

    public static async Task<ObservableCollection<TicketEntity>> GetAllAsync()
    {
        using var context = new DataContext();
        var tickets = await context.Tickets.Include(x => x.Comments).ThenInclude(comment => comment.User).Include(x => x.User).ThenInclude(user => user.Address).ToListAsync();
        return new ObservableCollection<TicketEntity>(tickets);
    }

    public static async Task<TicketEntity> GetAsync(int id)
    {
        using var context = new DataContext();
        var ticketEntity = await context.Tickets.Include(t => t.Comments).ThenInclude(comment => comment.User).Include(x => x.User).ThenInclude(user => user.Address).FirstOrDefaultAsync(t => t.Id == id);
        return ticketEntity!;
    }

    public static async Task<TicketEntity> DeleteAsync(int id)
    {
        using var context = new DataContext();
        var ticket = await context.Tickets.FirstOrDefaultAsync(x => x.Id == id);
        if (ticket != null)
        {
            context.Remove(ticket);
            await context.SaveChangesAsync();
        }
        return ticket!;
    }

    public static async Task<TicketEntity> UpdateStatusAsync(TicketEntity ticket)
    {
        using var context = new DataContext();
        var ticketEntity = await context.Tickets.FirstOrDefaultAsync(x => x.Id == ticket.Id);
        ticketEntity!.Status = ticket.Status;

        context.Update(ticketEntity);
        await context.SaveChangesAsync();

        return ticketEntity;
    }
}
