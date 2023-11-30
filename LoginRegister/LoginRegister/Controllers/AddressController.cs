using BusiniessServices.Layer.Interface;
using DataAccess.Layer.Context;
using DataAccess.Layer.DTOS;
using DataAccess.Layer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LoginRegister.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IAddressesServices _addressesServices;
        
        public AddressController( IAddressesServices addressesServices, UserManager<User> userManager)
        {
            _userManager= userManager;
            _addressesServices = addressesServices;
        }

        
        [HttpPost("AddressPost")]
        public async Task<IActionResult> AddressPost(Adresses adresses)
        {
            var veri = await _addressesServices.CreateAddress(adresses);
            return Ok(veri);
             
           
        }
        [HttpGet("GetAddress")]
        public async Task<IActionResult> GetAddress(string id)
        {
            var veri = await _addressesServices.GetAll(id);
            return Ok(veri);
        }
        [HttpPost("UsersAddress")]
        public async Task<IActionResult> UsersAddress(Adresses adresses)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "Id").Value;
            var veri = await _addressesServices.UserAddressCreate(userId,adresses);
            return Ok(veri);
        }
        [HttpPut("AddressUpdate")]
        public async Task<IActionResult> AddressUpdate(Adresses adresses)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "Id").Value;
            var veri= await _addressesServices.UserAddressUpdate(userId,adresses);
            return Ok(veri);
        }
        [HttpPut("AddressDelete")]
        public async Task<IActionResult> AddressDelete(int id)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "Id").Value;
            if (userId != null)
            {
                var veri = await _addressesServices.AddressDelete(id);
                return Ok(veri);
            }
            return BadRequest();
            
        }
    }
}
