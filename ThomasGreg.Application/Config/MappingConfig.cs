using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThomasGreg.Domain.Entities;
using ThomasGreg.Domain.Models;

namespace ThomasGreg.Application.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ClienteViewModel, Cliente>();
                config.CreateMap<Cliente, ClienteViewModel>();

                config.CreateMap<LogradouroViewModel, Logradouro>();
                config.CreateMap<Logradouro, LogradouroViewModel>();
            });
            return mappingConfig;
        }
    }
}
