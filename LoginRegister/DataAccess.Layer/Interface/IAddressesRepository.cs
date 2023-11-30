using Azure;
using DataAccess.Layer.DTOS;
using DataAccess.Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Layer.Interface
{
    public interface IAddressesRepository
    {
        Task<Adresses> AddressPost(Adresses adresses);
        Task<Adresses> GetAll(string id);
       // Task<Adresses> UserAddressPost(UsersAddressDTO usersAddressDTO);
        Task<Adresses> UserAddressUpdate(Adresses adresses);
        Task<Adresses> AddressDelete(int id);

    }
}
