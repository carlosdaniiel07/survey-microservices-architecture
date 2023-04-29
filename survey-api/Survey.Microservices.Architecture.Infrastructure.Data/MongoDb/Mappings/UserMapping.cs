using MongoDB.Bson.Serialization;
using Survey.Microservices.Architecture.Domain.Entities.v1;

namespace Survey.Microservices.Architecture.Infrastructure.Data.MongoDb.Mappings
{
    public class UserMapping : IMapping<User>
    {
        public BsonClassMap<User> RegisterMap()
        {
            return BsonClassMap.RegisterClassMap<User>(classMap =>
            {
                classMap.MapMember(user => user.Name).SetElementName("user");
                classMap.MapMember(user => user.Email).SetElementName("email");
                classMap.MapMember(user => user.Password).SetElementName("password");
                classMap.MapMember(user => user.IsActive).SetElementName("isActive");
            });
        }
    }
}
