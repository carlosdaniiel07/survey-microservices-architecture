using AutoMapper;
using Survey.Microservices.Architecture.Domain.UseCases.v1.Survey.CreateSurvey;
using SurveyEntity = Survey.Microservices.Architecture.Domain.Entities.v1.Survey;

namespace Survey.Microservices.Architecture.Application.UseCases.v1.Survey.CreateSurvey
{
    public class CreateSurveyProfile : Profile
    {
        public CreateSurveyProfile()
        {
            CreateMap<CreateSurveyRequest, SurveyEntity>();
            CreateMap<SurveyEntity, CreateSurveyResponse>();
        }
    }
}
