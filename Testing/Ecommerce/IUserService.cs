using Microsoft.AspNetCore.Http;

namespace Ecommerce
{
    public interface IUserService
    {
        string GetUserName();
        bool IsAdministrator();
    }

    class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserName()
        {
            return _httpContextAccessor.HttpContext.User.Identity.Name;

        }

        public bool IsAdministrator()
        {
            var user = _httpContextAccessor.HttpContext.User;
            return user.IsInRole("Administrator");
        }
    }
}