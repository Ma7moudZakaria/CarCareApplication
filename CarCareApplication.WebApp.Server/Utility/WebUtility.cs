using Microsoft.AspNetCore.Http;

namespace CarCareApplication.WebApp.Server.Utility
{
    public class WebUtility
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WebUtility(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUrl()
            => $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host.Value}/";
    }
}
