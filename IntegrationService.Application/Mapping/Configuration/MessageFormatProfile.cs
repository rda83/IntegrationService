﻿using AutoMapper;
using IntegrationService.Model;

namespace IntegrationService.Application.Mapping.Configuration
{
    class MessageFormatProfile : Profile
    {
        public MessageFormatProfile()
        {
            CreateMap<Data.Entities.MessageFormat, MessageFormat>();
            CreateMap<MessageFormat, Data.Entities.MessageFormat>();
        }
    }
}
