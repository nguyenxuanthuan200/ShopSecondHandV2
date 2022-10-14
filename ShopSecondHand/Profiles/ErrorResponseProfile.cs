using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopSecondHand.Data.Common;

namespace VoiceAPI.Profiles
{
    public class ErrorResponseProfile : Profile
    {
        public ErrorResponseProfile()
        {
            CreateMap(typeof(GenericResult<>), typeof(ErrorResponse)).ReverseMap();
            CreateMap<GenericResult, ErrorResponse>().ReverseMap();
        }
    }
}
