﻿namespace BeyondEarthApp.Web.Api.Security
{
    public interface IBasicSecurityService
    {
        bool SetPrincipal(string username, string password);
    }
}
