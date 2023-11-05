using Talabat.Core.Entities.IdentityEntities;

namespace Talabat.Sevices.IServices.ITokenServices
{
    public interface ITokenServices
    {
        string CreateToken(AppUser appUser);
    }
}
