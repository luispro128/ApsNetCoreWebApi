using AutoMapper;
using SmartSchool.WebAPI.Models;
using SmartSchool.WebAPI.Dtos;

namespace SmartSchool.WebAPI.Helpers
{
    public class SmartSchoolProfile: Profile
    {
        public SmartSchoolProfile()
        {
            CreateMap<Aluno, AlunoDto>()
                .ForMember(
                    dest => dest.Nome,
                    opt => opt.MapFrom(scr => $"{scr.Nome} {scr.Sobrenome}")
                )
                .ForMember(
                    dest => dest.Idade,
                    opt => opt.MapFrom(scr => scr.DataNasc.GetCurrentAge())
                );
            
            CreateMap<AlunoDto, Aluno>();
            CreateMap<Aluno, AlunoRegistrarDto>().ReverseMap();
        }
    }
}