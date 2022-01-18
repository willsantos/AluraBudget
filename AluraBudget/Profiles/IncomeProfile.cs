using AluraBudget.Data.DTO.IncomeDto;
using AluraBudget.Models;
using AutoMapper;

namespace AluraBudget.Profiles
{
    public class IncomeProfile: Profile
    {
        public IncomeProfile()
        {
            CreateMap<CreateIncomeDto, Income>();
            CreateMap<Income, ReadIncomeDto>();
            CreateMap<UpdateIncomeDto,Income>();
        }
    }
}
