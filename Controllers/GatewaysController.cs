using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusalaGatewayProject.Models.Dto;
using MusalaGatewayProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MusalaGatewayProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GatewaysController : ControllerBase
    {
        private readonly IGatewayRepository _gatewayRepository;
        protected ResponseDto _response;

        public GatewaysController(IGatewayRepository gatewayRepository)
        {
            _gatewayRepository = gatewayRepository;
            _response = new ResponseDto();
        }

        // GET: api/Gateways
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GatewayDto>>> GetGateways()
        {
            try
            {
                var gateways = await _gatewayRepository.GetGateways();
                _response.Result = gateways;
                _response.DisplayMessage = "A list of gateways";
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

        // GET: api/Gateways/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GatewayDto>> GetGateway(string id)
        {
            var gateway = await _gatewayRepository.GetGateway(id);
            if (gateway == null)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "The gateway not exists";
                return NotFound(_response);
            }
            _response.Result = gateway;
            _response.DisplayMessage = "Data of a gateway";
            return Ok(_response);
        }

        // PUT: api/Gateways/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutGateway(string id, GatewayDto gatewayDto)
        {
            if (id != gatewayDto.GatewayId.ToString())
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "The id of the selected gateway and the new gateway are different";
                return BadRequest(_response);
            }
            if (gatewayDto.Devices != null && gatewayDto.Devices.Count > 10)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "A gateway must have 10 devices at maximun";
                return BadRequest(_response);
            }

            Regex validateIPv4Regex = new Regex("^(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");

            if (validateIPv4Regex.IsMatch(gatewayDto.Address))
            {
                try
                {
                    if (gatewayDto.Devices != null)
                    {
                        foreach (var device in gatewayDto.Devices)
                        {
                            device.GatewayId = gatewayDto.GatewayId;
                        }
                    }                    
                    GatewayDto gatewayModel = await _gatewayRepository.PutGateway(gatewayDto);
                    _response.DisplayMessage = "Data updated";
                    _response.Result = gatewayModel;
                    return Ok(_response);
                }
                catch (DbUpdateConcurrencyException e)
                {
                    _response.IsSuccess = false;

                    if (!GatewayExists(id))
                    {
                        _response.DisplayMessage = "The gateway not exists";
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
                _response.DisplayMessage = "The address must be a valid Ipv4 address";
                return BadRequest(_response);
            }

        }

        // POST: api/Gateways
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<GatewayDto>> PostGateway(GatewayDto gatewayDto)
        {
            if (gatewayDto.Devices != null && gatewayDto.Devices.Count > 10)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "A gateway must have 10 devices at maximun";
                return BadRequest(_response);
            }
            gatewayDto.GatewayId = Guid.NewGuid().ToString().ToUpper();
            if (gatewayDto.Devices != null)
            {
                foreach (var device in gatewayDto.Devices)
                {
                    if (device.GatewayId == null)
                        device.GatewayId = gatewayDto.GatewayId;
                }
            }
            Regex validateIPv4Regex = new Regex("^(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$");

            if (validateIPv4Regex.IsMatch(gatewayDto.Address))
            {
                try
                {
                    await _gatewayRepository.PostGateway(gatewayDto);
                    _response.DisplayMessage = "Gateway added";
                    _response.Result = gatewayDto;
                    return CreatedAtAction("GetGateway", new { id = gatewayDto.GatewayId }, _response);
                }
                catch (DbUpdateException e)
                {
                    _response.IsSuccess = false;
                    if (GatewayExists(gatewayDto.GatewayId))
                    {
                        _response.DisplayMessage = "Id mismatch";
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
                _response.DisplayMessage = "The address must be a valid Ipv4 address";
                return BadRequest(_response);
            }

        }

        // DELETE: api/Gateways/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteGateway(string id)
        {
            try
            {
                bool isDeleted = await _gatewayRepository.DeleteGateway(id);
                if (isDeleted)
                {
                    _response.Result = isDeleted;
                    _response.DisplayMessage = "Gateway deleted";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSuccess = false;
                    _response.DisplayMessage = "An error ocurred while deleting the gateway";
                    return BadRequest(_response);
                }
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.DisplayMessage = "An error ocurred while deleting the gateway";
                _response.ErrorMessages = new List<string>() { e.ToString() };
                return BadRequest(_response);

            }
        }

        bool GatewayExists(string id)
        {
            return _gatewayRepository.GetGateways().Result.Any(e => e.GatewayId == id);
        }
    }
}
