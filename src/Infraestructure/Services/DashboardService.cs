using ApplicationCore.Interfaces;
using ApplicationCore.Wrappers;
using Dapper;
using Infraestructure.Persistence;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using System.Net;
using Microsoft.AspNetCore.Http;
using ApplicationCore.DTOs.Logs;
using ApplicationCore.Commands.LogsR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using AutoMapper;

namespace Infraestructure.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public DashboardService(ApplicationDbContext dbContext, ICurrentUserService currentUserService, IMapper mapper)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
            _mapper = mapper;
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

        public async Task<Response<int>> CreateLog(LogsDto request)
        {
            var ipAddress= await GetIp();
            var dd = ipAddress.Message.ToString() ;
            var l = new LogsDto();
            l.ipA = dd;
            l.datos = request.datos;
            l.fecha = request.fecha;
            l.response = request.response;
            l.nombreFuncion = request.nombreFuncion;

            var lo = _mapper.Map<Domain.Entities.logs>(l);
            await _dbContext.AddAsync(lo);
            await _dbContext.SaveChangesAsync();

            return new Response<int>(lo.Id, "Registro Creado");
        }
    }
}
