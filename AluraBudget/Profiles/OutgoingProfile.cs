using AluraBudget.Data.DTO.OutgoingDto;
using AluraBudget.Models;
using AutoMapper;

namespace AluraBudget.Profiles
{
    public class OutgoingProfile: Profile
    {
        public OutgoingProfile()
        {
            CreateMap<CreateOutgoingDto,Outgoing>();
            CreateMap<Outgoing,ReadOutgoingDto>();
            CreateMap<UpdateOutgoingDto,Outgoing>();
        }
    }
}
