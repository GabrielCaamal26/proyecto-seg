using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Infraestructure.Persistence;
using ApplicationCore.Wrappers;
using ApplicationCore.Commands;
using ApplicationCore.Interfaces;
using ApplicationCore.DTOs.Logs;
using System.Text.Json;

namespace Infraestructure.EventHandlers.users
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Response<int>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDashboardService _dashboardService;

        public CreateUserHandler(ApplicationDbContext context, IMapper mapper, IDashboardService dashboardService)
        {
            _context = context;
            _mapper = mapper;
            _dashboardService = dashboardService;
        }
        public async Task<Response<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var u = new CreateUserCommand();
            u.Nombre = request.Nombre;
            u.Ap_Paterno = request.Ap_Paterno;

            var us = _mapper.Map<Domain.Entities.Users>(u);
            await _context.users.AddAsync(us);
            await _context.SaveChangesAsync();

            //Inyección de datos al objetos
            var log = new LogsDto();
            var json = JsonSerializer.Serialize(u);
            log.datos = json;
            log.fecha = DateTime.Now;
            log.response = "200";
            log.nombreFuncion = "CreateCliente";

            //Se llama a traer la función para insertarla en logs
            await _dashboardService.CreateLog(log);
            return new Response<int>(us.Id, "Registro Creado");
        }
    }
}
