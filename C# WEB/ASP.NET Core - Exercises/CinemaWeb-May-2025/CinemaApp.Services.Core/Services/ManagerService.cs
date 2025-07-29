namespace CinemaApp.Services.Core.Services
{
    using System;
    using CinemaApp.Data.Models;
    using CinemaApp.Data.Repository.Interfaces;
    using CinemaApp.Services.Core.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class ManagerService : IManagerService
    {
        private readonly IManagerRepository _managerRepository;

        public ManagerService(IManagerRepository managerRepository)
        {
            _managerRepository = managerRepository;
        }

        public async Task<bool> ExistsByIdAsync(string? id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return await _managerRepository
                    .GetAllAttached()
                    .AnyAsync(m => m.Id.ToString().ToLower() == id.ToLower());
            }

            return false;
        }
        public async Task<bool> ExistsByUserIdAsync(string? userId)
        {
           if (!String.IsNullOrWhiteSpace(userId))
            {
                return await _managerRepository
                    .GetAllAttached()
                    .AnyAsync(m => m.UserId.ToLower() == userId.ToLower());
            }

            return false;
        }

        public async Task<Guid?> GetIdByUserIdAsync(string? userId)
        {
            Guid? managerId = null;
            if (!String.IsNullOrWhiteSpace(userId))
            {
                Manager? manager = await _managerRepository
                    .FirstOrDefaultAsync(m => m.UserId.ToLower() == userId.ToLower());
                if (manager != null)
                {
                    managerId = manager.Id;
                }
            }

            return managerId;
        }

    }
}
