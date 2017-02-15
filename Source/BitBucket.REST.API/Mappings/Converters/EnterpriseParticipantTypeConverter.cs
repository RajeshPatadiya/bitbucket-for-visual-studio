﻿using AutoMapper;
using BitBucket.REST.API.Models.Enterprise;
using BitBucket.REST.API.Models.Standard;

namespace BitBucket.REST.API.Mappings.Converters
{
    public class EnterpriseParticipantTypeConverter : ITypeConverter<EnterpriseParticipant,Participant>
    {
        public Participant Convert(EnterpriseParticipant source, Participant destination, ResolutionContext context)
        {
            return new Participant()
            {
                Approved = source.Approved,
                Role = source.Role,
                User = new UserShort()
                {
                    Links = source.User.Links.MapTo<Links>(),
                    DisplayName = source.User.DisplayName,
                    Username = source.User.Username
                },
            };
        }
    }
}