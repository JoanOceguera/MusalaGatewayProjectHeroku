using MusalaGatewayProject.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusalaGatewayProject.Repository
{
    public interface IPeripheralDeviceRepository
    {
        Task<List<PeripheralDeviceDto>> GetPeripheralDevices();

        Task<PeripheralDeviceDto> GetPeripheralDevice(int Id);

        Task<PeripheralDeviceDto> PostPeripheralDevice(PeripheralDeviceDto peripheralDeviceDto);

        Task<PeripheralDeviceDto> PutPeripheralDevice(PeripheralDeviceDto peripheralDeviceDto);

        Task<bool> DeletePeripheralDevice(int Id);

        Task<bool> CapacityAvailable(string GatewayId);
    }
}
