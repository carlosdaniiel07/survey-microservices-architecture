using AutoMapper;
using Survey.Microservices.Architecture.Domain.UseCases.v1.Survey.GetAllSurveys;
using SurveyEntity = Survey.Microservices.Architecture.Domain.Entities.v1.Survey;

namespace Survey.Microservices.Architecture.Application.UseCases.v1.Survey.GetAllSurveys
{
    public class GetAllSurveysProfile : Profile
    {
        public GetAllSurveysProfile()
        {
            CreateMap<SurveyEntity, GetAllSurveysResponse.Survey>();
        }
    }
}
