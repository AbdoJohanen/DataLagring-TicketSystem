using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TicketSystem.Contexts;
using TicketSystem.Models;
using TicketSystem.Models.Entities;

namespace TicketSystem.Services;

public class TicketService
{
    private readonly static DataContext _context = new();

    public static async Task<TicketEntity> SaveAsync(TicketEntity ticketEntity)
    {
        _context.Add(ticketEntity);
        await _context.SaveChangesAsync();

        return ticketEntity;
    }

    public static async Task<ObservableCollection<TicketEntity>> GetAllAsync()
    {
        var tickets = await _context.Tickets.Include(x => x.User).ThenInclude(user => user.Address).ToListAsync();
        return new ObservableCollection<TicketEntity>(tickets);
    }

    public static async Task<TicketEntity> GetAsync(Func<TicketEntity, bool> predicate)
    {
        var _ticketEntity = await _context.Tickets.FindAsync(predicate);
        if (_ticketEntity != null)
            return _ticketEntity;

        return null!;
    }

    public static async Task<TicketEntity> DeleteAsync(int id)
    {
        var ticket = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == id);
        if (ticket != null)
        {
            _context.Remove(ticket);
            await _context.SaveChangesAsync();
        }

        return null!;
    }

    public static async Task<TicketEntity> UpdateStatusAsync(TicketEntity ticket)
    {
        var _ticketEntity = await _context.Tickets.FirstOrDefaultAsync(x => x.Id == ticket.Id);
        _ticketEntity!.Status= ticket.Status;
        
        _context.Update(_ticketEntity);
        await _context.SaveChangesAsync();

        return null!;
    }
}