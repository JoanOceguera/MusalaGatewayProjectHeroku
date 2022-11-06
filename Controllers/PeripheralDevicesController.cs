using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusalaGatewayProject.Models;
using MusalaGatewayProject.Models.Dto;
using MusalaGatewayProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusalaGatewayProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeripheralDevicesController : ControllerBase
    {
        private readonly IPeripheralDeviceRepository _peripheralDeviceRepository;
        protected ResponseDto _response;

        public PeripheralDevicesController(IPeripheralDeviceRepository peripheralDeviceRepository)
        {
            _peripheralDeviceRepository = peripheralDeviceRepository;
            _response = new ResponseDto();
        }

        // GET: api/PeripheralDevices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PeripheralDeviceDto>>> GetPeripheralDevices()
        {
            try
            {
                var peripheralDevices = await _peripheralDeviceRepository.GetPeripheralDevices();
                _response.Result = peripheralDevices;
                _response.DisplayMessage = "A list of devices";
            }
            catch (Exception e)
            {

                _response.IsSuccess = false;
                _response.DisplayMessage = "An error occurred";
                _response.ErrorMessages = new List<string> { e.ToString() };
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        // GET: api/PeripheralDevices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PeripheralDeviceDto>> GetPeripheralDevices(int id)
        {
            var device = await _peripheralDeviceRepository.GetPeripheralDevice(id);
            if (device == null)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "The device not exists";
                return NotFound(_response);
            }
            _response.Result = device;
            _response.DisplayMessage = "Data of a device";
            return Ok(_response);
        }

        // PUT: api/PeripheralDevices/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutPeripheralDevices(int id, PeripheralDeviceDto peripheralDeviceDto)
        {
            if (id != peripheralDeviceDto.UID)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "The id of the selected device and the new device are different" };
                return BadRequest(_response);
            }

            if (await _peripheralDeviceRepository.CapacityAvailable(peripheralDeviceDto.GatewayId))
            {
                try
                {
                    PeripheralDeviceDto peripheralDeviceModel = await _peripheralDeviceRepository.PutPeripheralDevice(peripheralDeviceDto);
                    _response.DisplayMessage = "Data updated";
                    _response.Result = peripheralDeviceModel;
                    return Ok(_response);
                }
                catch (DbUpdateConcurrencyException e)
                {
                    _response.IsSuccess = false;

                    if (!PeripheralDeviceExists(id))
                    {
                        _response.DisplayMessage = "The device not exists";
                        return NotFound(_response);
                    }
                    else
                    {
                        _response.DisplayMessage = "An error occurred while updating";
                        _response.ErrorMessages = new List<string> { e.ToString() };
                        return BadRequest(_response);
                    }
                }
            }
            else
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "The gateway selected has full capacity";
                return BadRequest(_response);
            }            
        }

        // POST: api/PeripheralDevices
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PeripheralDeviceDto>> PostPeripheralDevices(PeripheralDeviceDto peripheralDeviceDto)
        {
            if (await _peripheralDeviceRepository.CapacityAvailable(peripheralDeviceDto.GatewayId))
            {
                try
                {
                    await _peripheralDeviceRepository.PostPeripheralDevice(peripheralDeviceDto);
                    _response.DisplayMessage = "Device added";
                    _response.Result = peripheralDeviceDto;
                    return CreatedAtAction("GetPeripheralDevices", new { id = peripheralDeviceDto.UID } , _response);
                }
                catch (DbUpdateException e)
                {
                    _response.IsSuccess = false;
                    if (PeripheralDeviceExists(peripheralDeviceDto.UID))
                    {
                        _response.DisplayMessage = "UID mismatch";
                        return Conflict(_response);
                    }
                    else
                    {
                        _response.DisplayMessage = "An error occurred while adding";
                        _response.ErrorMessages = new List<string> { e.ToString() };
                        return BadRequest(_response);
                    }
                }
            }
            else
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { "The gateway selected has full capacity" };
                return BadRequest(_response);
            }            
        }

        // DELETE: api/PeripheralDevices/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePeripheralDevices(int id)
        {
            try
            {
                bool isDeleted = await _peripheralDeviceRepository.DeletePeripheralDevice(id);
                if (isDeleted)
                {
                    _response.Result = isDeleted;
                    _response.DisplayMessage = "Device deleted";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "An error ocurred while deleting the device";
                    return BadRequest(_response);
                }
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "An error ocurred while deleting the device";
                _response.ErrorMessages = new List<string>() { e.ToString() };
                return BadRequest(_response);

            }
        }

        bool PeripheralDeviceExists(int id)
        {
            return _peripheralDeviceRepository.GetPeripheralDevices().Result.Any(e => e.UID == id);
        }
    }
}
