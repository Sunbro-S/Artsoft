using Domain.Interfaces;
using ProfileConnectionLib.ConnectionServices.DtoModels.CheckUserExists;
using ProfileConnectionLib.ConnectionServices.Interfaces;

namespace Infastracted.Connections;

public class CheckUser : ICheckUser
{
    private readonly IProfileConnectionServcie _profileConnectionServcie;

    public CheckUser(IProfileConnectionServcie profileConnectionServcie)
    {
        _profileConnectionServcie = profileConnectionServcie;
    }
    
    public async Task CheckUserExistAsync(Guid userId)
    {
        await _profileConnectionServcie.CheckUserExistAsync(new CheckUserExistProfileApiRequest
        {
            UserId = userId
        });
    }
}