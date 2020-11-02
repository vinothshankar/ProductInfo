using AutoMapper;
using ProductInfo.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductInfo.API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, Product>().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<Price, Price>().ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}
