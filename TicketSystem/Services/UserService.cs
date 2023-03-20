using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TicketSystem.Contexts;
using TicketSystem.Models;
using TicketSystem.Models.Entities;

namespace TicketSystem.Services;

internal class UserService
{
    public static async Task SaveAsync(UserModel user)
    {
        var _userEntity = new UserEntity
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber
        };

        using var _context = new DataContext();
        var _addressEntity = await _context.Addresses.FirstOrDefaultAsync(x => x.StreetName == user.StreetName && x.PostalCode == user.PostalCode && x.City == user.City);
        if (_addressEntity != null)
            _userEntity.AddressId = _addressEntity.Id;
        else
            _userEntity.Address = new AddressEntity
            {
                StreetName = user.StreetName,
                PostalCode = user.PostalCode,
                City = user.City
            };

        _context.Add(_userEntity);
        await _context.SaveChangesAsync();
    }


    public static async Task<ObservableCollection<UserModel>> GetAllAsync()
    {
        using var _context = new DataContext();
        var _users = new ObservableCollection<UserModel>();

        foreach (var _user in await _context.Users.Include(x => x.Address).ToListAsync())
            _users.Add(new UserModel
            {
                Id = _user.Id,
                FirstName = _user.FirstName,
                LastName = _user.LastName,
                Email = _user.Email,
                PhoneNumber = _user.PhoneNumber,
                StreetName = _user.Address.StreetName,
                PostalCode = _user.Address.PostalCode,
                City = _user.Address.City
            });

        return _users;
    }


    public static async Task<UserModel> GetAsync(string email)
    {
        using var _context = new DataContext();
        var _user = await _context.Users.Include(x => x.Address).FirstOrDefaultAsync(x => x.Email == email);
        if (_user != null)
            return new UserModel
            {
                Id = _user.Id,
                FirstName = _user.FirstName,
                LastName = _user.LastName,
                Email = _user.Email,
                PhoneNumber = _user.PhoneNumber,
                StreetName = _user.Address.StreetName,
                PostalCode = _user.Address.PostalCode,
                City = _user.Address.City
            };

        else
            return null!;
    }


    public static async Task UpdateAsync(UserModel user)
    {
        using var _context = new DataContext();
        var _userEntity = await _context.Users.Include(x => x.Address).FirstOrDefaultAsync(x => x.Id == user.Id);
        if (_userEntity != null)
        {
            if (!string.IsNullOrEmpty(user.FirstName))
                _userEntity.FirstName = user.FirstName;

            if (!string.IsNullOrEmpty(user.LastName))
                _userEntity.LastName = user.LastName;

            if (!string.IsNullOrEmpty(user.Email))
                _userEntity.Email = user.Email;

            if (!string.IsNullOrEmpty(user.PhoneNumber))
                _userEntity.PhoneNumber = user.PhoneNumber;

            if (!string.IsNullOrEmpty(user.StreetName) || !string.IsNullOrEmpty(user.PostalCode) || !string.IsNullOrEmpty(user.City))
            {
                var _addressEntity = await _context.Addresses.FirstOrDefaultAsync(x => x.StreetName == user.StreetName && x.PostalCode == user.PostalCode && x.City == user.City);
                if (_addressEntity != null)
                    _userEntity.AddressId = _addressEntity.Id;
                else
                    _userEntity.Address = new AddressEntity
                    {
                        StreetName = user.StreetName,
                        PostalCode = user.PostalCode,
                        City = user.City
                    };
            }

            _context.Update(_userEntity);
            await _context.SaveChangesAsync();

        }
    }

    public static async Task DeleteAsync(string email)
    {
        using var _context = new DataContext();
        var user = await _context.Users.Include(x => x.Address).FirstOrDefaultAsync(x => x.Email == email);
        if (user != null)
        {
            _context.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
