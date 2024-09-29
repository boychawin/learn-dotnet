namespace WebApi.Utils
{
    public static class CookieUtils
    {
        public static void SetTokenCookie(HttpResponse response, string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        public static string GetIpAddress(HttpRequest request)
        {
            if (request.Headers.ContainsKey("X-Forwarded-For"))
                return request.Headers["X-Forwarded-For"];
            else
                return request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
