using ApplicationCore.Interfaces;
using ApplicationCore.Wrappers;
using Dapper;
using Infraestructure.Persistence;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace Infraestructure.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;

        public DashboardService(ApplicationDbContext dbContext, ICurrentUserService currentUserService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
        }

        public async Task<Response<object>> GetData()
        {
            object list = new();
            list = await _dbContext.users.ToListAsync();
            return new Response<object>(list);
        }

        public async Task<Response<string>> GetIp()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
            var ip= ipAddress?.ToString() ?? "No se pudo obtener la IP";
            return new Response<string>(ip);
        }
    }
}
