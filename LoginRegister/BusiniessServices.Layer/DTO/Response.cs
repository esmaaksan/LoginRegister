using BusiniessServices.Layer.ErrorMessages;
using DataAccess.Layer.Entities;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusiniessServices.Layer.DTO
{
    public class Response<T>
    {
       

        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string? ErrorMessage { get; set; }
        
        public static Response<T> Succes(T data)
        {
            return new Response<T>
            {
                Data = data,
                IsSuccess = true
            };
            
        }
        public static Response<T> Succes()
        {

            return new Response<T>
            {
                IsSuccess = true
            };

        }
        public static Response<T> Fail(T data)
        {
            
            return new Response<T>
            {
                Data = data,
                ErrorMessage= "Bir hata oluştu!"
            };
        }
        public static Response<T> Fail()
        {
            return new Response<T>
            {
                
            };
        }

    }
   
}
