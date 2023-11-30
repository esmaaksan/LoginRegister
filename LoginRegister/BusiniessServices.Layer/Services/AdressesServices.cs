using Azure;
using BusiniessServices.Layer.Interface;
using DataAccess.Layer.Context;
using DataAccess.Layer.DTOS;
using DataAccess.Layer.Entities;
using DataAccess.Layer.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusiniessServices.Layer.Services
{
    public class AdressesServices : IAddressesServices
    {
        private readonly IAddressesRepository _usersRepository;
        private readonly UserManager<User> _userManager;
        
        public AdressesServices(IAddressesRepository usersRepository, UserManager<User> userManager)
        {
            _usersRepository = usersRepository;
            _userManager = userManager;
            
        }


        public async Task<Adresses> CreateAddress(Adresses adresses)
        {
            var veri= await _usersRepository.AddressPost(adresses);
            return veri;
        }

        public async Task<Adresses> GetAll(string id)
        {
            var veri= await _usersRepository.GetAll(id);
            return veri;
        }


        public async Task<Adresses> UserAddressCreate(string token, Adresses adresses)
        {
            User user = await _userManager.FindByIdAsync(token);
            if (user != null)
            {
              var veri=  await _usersRepository.AddressPost(adresses);
                return veri;
            }
            return adresses;
            
        }

        public async Task<Adresses> UserAddressUpdate(string token, Adresses adresses)
        {
            User user = await _userManager.FindByIdAsync(token);
            
            if (user != null)
            {
                var veri = await _usersRepository.UserAddressUpdate(adresses);
                return veri;
            }
            return adresses;
        }

       public async Task<Adresses> AddressDelete( int id)
        {

                return await _usersRepository.AddressDelete(id);
 
        }
    }
}
