using CostAnalysisAPI.Models;

namespace CostAnalysisAPI.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        UserService GetById(int id);
    }
}