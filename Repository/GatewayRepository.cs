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
    public class GatewayRepository : IGatewayRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GatewayRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> DeleteGateway(string Id)
        {
            try
            {
                Gateway gateway = await _context.Gateways.FindAsync(Id);
                if (gateway == null)
                {
                    return false;
                }
                _context.Remove(gateway);
                _context.Entry(gateway).State = EntityState.Deleted;
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<GatewayDto> GetGateway(string Id)
        {
            Gateway gateway = await _context.Gateways.Include(devices => devices.Devices).Where( OID => OID.GatewayId == Id).SingleAsync();

            return _mapper.Map<GatewayDto> (gateway);
        }

        public async Task<List<GatewayDto>> GetGateways()
        {
            List<Gateway> gateways = await _context.Gateways.Include(devices => devices.Devices).ToListAsync();
            return _mapper.Map<List<GatewayDto>>(gateways);
        }

        public async Task<GatewayDto> PostGateway(GatewayDto gatewayDto)
        {
            Gateway gateway = _mapper.Map<GatewayDto, Gateway>(gatewayDto);
            foreach (var device in gateway.Devices)
            {
                if (await _context.Devices.FindAsync(device.UID) != null)
                {
                    _context.Devices.Update(device);
                    _context.Entry(device).State = EntityState.Modified;
                }
                else
                {
                    await _context.Devices.AddAsync(device);
                    _context.Entry(device).State = EntityState.Added;
                }
            }
            _context.Entry(gateway).State = EntityState.Added;
            await _context.Gateways.AddAsync(gateway);
            await _context.SaveChangesAsync();
            return _mapper.Map<Gateway, GatewayDto>(gateway);

        }

        public async Task<GatewayDto> PutGateway(GatewayDto gatewayDto)
        {
            Gateway gateway = _mapper.Map<GatewayDto, Gateway>(gatewayDto);
            foreach (var device in gateway.Devices)
            {
                if (await _context.Devices.FindAsync(device.UID) != null)
                {
                    _context.Devices.Update(device);
                    _context.Entry(device).State = EntityState.Modified;
                }
                else
                {
                    await _context.Devices.AddAsync(device);
                    _context.Entry(device).State = EntityState.Added;
                }
            }
            _context.Entry(gateway).State = EntityState.Modified;
            _context.Gateways.Update(gateway);
            await _context.SaveChangesAsync();
            return _mapper.Map<Gateway, GatewayDto>(gateway);
        }
    }
}
