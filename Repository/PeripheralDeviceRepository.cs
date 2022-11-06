using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MusalaGatewayProject.Data;
using MusalaGatewayProject.Models;
using MusalaGatewayProject.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusalaGatewayProject.Repository
{
    public class PeripheralDeviceRepository : IPeripheralDeviceRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PeripheralDeviceRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> DeletePeripheralDevice(int Id)
        {
            try
            {
                PeripheralDevice peripheralDevice = await _context.Devices.FindAsync(Id);
                if (peripheralDevice == null)
                {
                    return false;
                }
                _context.Entry(peripheralDevice).State = EntityState.Deleted;
                _context.Remove(peripheralDevice);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<PeripheralDeviceDto> GetPeripheralDevice(int Id)
        {
            PeripheralDevice peripheralDevice = await _context.Devices.Include(gateway => gateway.Gateway).Where(OID => OID.UID == Id).SingleAsync();

            return _mapper.Map<PeripheralDeviceDto>(peripheralDevice);
        }

        public async Task<List<PeripheralDeviceDto>> GetPeripheralDevices()
        {
            List<PeripheralDevice> peripheralDevices = await _context.Devices.Include(gateway => gateway.Gateway).ToListAsync();
            return _mapper.Map<List<PeripheralDeviceDto>>(peripheralDevices);
        }

        public async Task<PeripheralDeviceDto> PostPeripheralDevice(PeripheralDeviceDto peripheralDeviceDto)
        {
            PeripheralDevice peripheralDevice = _mapper.Map<PeripheralDeviceDto, PeripheralDevice>(peripheralDeviceDto);
            _context.Entry(peripheralDevice).State = EntityState.Added;
            await _context.Devices.AddAsync(peripheralDevice);
            await _context.SaveChangesAsync();
            return _mapper.Map<PeripheralDevice, PeripheralDeviceDto>(peripheralDevice);

        }

        public async Task<PeripheralDeviceDto> PutPeripheralDevice(PeripheralDeviceDto peripheralDeviceDto)
        {
            PeripheralDevice peripheralDevice = _mapper.Map<PeripheralDeviceDto, PeripheralDevice>(peripheralDeviceDto);
            _context.Entry(peripheralDevice).State = EntityState.Modified;
            _context.Devices.Update(peripheralDevice);
            await _context.SaveChangesAsync();
            return _mapper.Map<PeripheralDevice, PeripheralDeviceDto>(peripheralDevice);
        }

        public async Task<bool> CapacityAvailable(string GatewayId)
        {
            if (await _context.Devices.Where(gateway => gateway.GatewayId == GatewayId).CountAsync() >= 10)
                return false;
            else
                return true;
        }
    }
}
