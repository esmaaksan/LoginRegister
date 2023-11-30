using Azure;
using DataAccess.Layer.Context;
using DataAccess.Layer.DTOS;
using DataAccess.Layer.Entities;
using DataAccess.Layer.Interface;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Layer.Concrete
{
    public class AddressesRepository : IAddressesRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IValidator<Adresses> _validator;

        public AddressesRepository(AppDbContext appDbContext, IValidator<Adresses> validator)
        {
            _appDbContext = appDbContext;
            _validator = validator;
        }

        public async Task<Adresses> AddressDelete(int id)
        {

            //var veri1 = await _appDbContext.adresses.Include(a=>a.user).SingleOrDefaultAsync(a=>a.Id == id);
            //if (veri1!=null)
            //{
            //    string a = veri1.user.Id;
            //    var veri = await _appDbContext.adresses.SingleOrDefaultAsync(p =>p.UserId==a);
            //    if(veri!=null)
            //    {
            //        _appDbContext.adresses.Remove(veri);
            //        await _appDbContext.SaveChangesAsync();
                    
            //    }
            //}
            //return veri1;
            
           throw new NotImplementedException();
            
        }

        public async Task<Adresses> AddressPost(Adresses adresses)
        {
            await _appDbContext.adresses.AddAsync(adresses);               
            var EnSonEklenen = _appDbContext.adresses.Where(a => a.UserId == adresses.UserId).SingleOrDefault(a => a.IsActivite);                
            if (EnSonEklenen != null)     
            {   
                EnSonEklenen.IsActivite = false; 
            }     
            await _appDbContext.SaveChangesAsync();
               
            
            return adresses;
        }

        public async Task<Adresses> GetAll(string id)
        {
           //var veri= await _appDbContext.adresses.Include(a=>a.user).OrderBy(a=>a.IsActivite).LastOrDefaultAsync(a=>a.UserId==id);
           // return veri;
           throw new NotImplementedException();
        }

        public async Task<Adresses> UserAddressUpdate(Adresses adresses)
        {
            //var veri2 = await _appDbContext.adresses.FirstOrDefaultAsync(p=>p.UserId == id);
            _appDbContext.adresses.Update(adresses);
            await _appDbContext.SaveChangesAsync();
            return adresses;

        }
    }
}
