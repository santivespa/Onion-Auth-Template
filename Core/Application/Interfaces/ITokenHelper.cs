using Domain;

namespace Application.Interfaces
{
    public interface ITokenHelper
    {
        string GenerateToken(User user, IList<string> userRoles);
    }
}
