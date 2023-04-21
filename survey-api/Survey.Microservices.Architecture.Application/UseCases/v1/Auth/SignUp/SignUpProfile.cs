using AutoMapper;
using Survey.Microservices.Architecture.Domain.Entities.v1;
using Survey.Microservices.Architecture.Domain.UseCases.v1.Auth.SignUp;

namespace Survey.Microservices.Architecture.Application.UseCases.v1.Auth.SignUp
{
    public class SignUpProfile : Profile
    {
        public SignUpProfile()
        {
            CreateMap<SignUpRequest, User>();
        }
    }
}
