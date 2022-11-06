using AutoMapper;
using MusalaGatewayProject.Models;
using MusalaGatewayProject.Models.Dto;
using System;
using System.Linq;

namespace MusalaGatewayProject
{
    public class MappingConfiguration
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<GatewayDto, Gateway>();
                config.CreateMap<Gateway, GatewayDto>();
                config.CreateMap<PeripheralDeviceDto, PeripheralDevice>();
                config.CreateMap<PeripheralDevice, PeripheralDeviceDto>();
            });

            return mappingConfiguration;
        }


    }
}
