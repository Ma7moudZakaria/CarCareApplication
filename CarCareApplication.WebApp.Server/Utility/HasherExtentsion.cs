using System;

namespace CarCareApplication.WebApp.Server.Utility
{
    public static class HasherExtentsion
    {
        public static string GetHashContent(this byte[] FileContent)
        => BitConverter.ToString(System.Security.Cryptography.SHA1.Create()
                  .ComputeHash(FileContent)).Replace("-", string.Empty);

    }
}
