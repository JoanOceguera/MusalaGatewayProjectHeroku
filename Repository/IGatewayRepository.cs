using MusalaGatewayProject.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusalaGatewayProject.Repository
{
    public interface IGatewayRepository
    {
        Task<List<GatewayDto>> GetGateways();

        Task<GatewayDto> GetGateway(string Id);

        Task<GatewayDto> PostGateway(GatewayDto gatewayDto);

        Task<GatewayDto> PutGateway(GatewayDto gatewayDto);

        Task<bool>  DeleteGateway(string Id);


    }
}
