
using BusiniessServices.Layer.DTO;
using DataAccess.Layer.DTOS;
using DataAccess.Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusiniessServices.Layer.Interface
{
    public interface IAddressesServices
    {
        Task<Adresses> CreateAddress(Adresses adresses);
        Task<Adresses> GetAll(string id);
        Task<Adresses> UserAddressCreate(string token, Adresses adresses);
        Task<Adresses> UserAddressUpdate(string token, Adresses adresses);
        Task<Adresses> AddressDelete(int id);
    }
}
