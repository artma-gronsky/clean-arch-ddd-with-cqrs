using BuperDinner.Application.Authentication.Common;
using BuperDinner.Contracts.Authentications;
using Mapster;

namespace BuperDinner.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
        .Map(dest => dest.Token, src => src.Token)
        .Map(dest => dest, src => src.User);
    }
}