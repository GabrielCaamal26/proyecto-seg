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

namespace Infraestructure.EventHandlers.users
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Response<int>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateUserHandler(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var u = new CreateUserCommand();
            u.Nombre = request.Nombre;
            u.Ap_Paterno = request.Ap_Paterno;

            var us = _mapper.Map<Domain.Entities.Users>(u);
            await _context.users.AddAsync(us);
            await _context.SaveChangesAsync();
            return new Response<int>(us.Id, "Registro Creado");
        }
    }
}
