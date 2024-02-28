using ApplicationCore.Commands.LogsR;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Mappers
{
    public class LogsProfile : Profile
    {
        public LogsProfile() {
            CreateMap<CreateLogsCommand, logs>()
                .ForMember(x => x.Id, y => y.Ignore());
        }
    }
}
