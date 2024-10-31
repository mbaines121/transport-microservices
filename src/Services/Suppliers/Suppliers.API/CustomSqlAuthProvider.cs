﻿using Azure.Core;
using Azure.Identity;
using Microsoft.Data.SqlClient;

namespace Suppliers.API;

public class CustomSqlAuthProvider : SqlAuthenticationProvider
{
    private static readonly string[] _azureSqlScopes = new[]
    {
        "https://database.windows.net//.default"
    };

    private static readonly TokenCredential _credential = new DefaultAzureCredential();

    public override async Task<SqlAuthenticationToken> AcquireTokenAsync(SqlAuthenticationParameters parameters)
    {
        var tokenRequestContext = new TokenRequestContext(_azureSqlScopes);
        var tokenResult = await _credential.GetTokenAsync(tokenRequestContext, default);
        return new SqlAuthenticationToken(tokenResult.Token, tokenResult.ExpiresOn);
    }

    public override bool IsSupported(SqlAuthenticationMethod authenticationMethod)
    {
        return authenticationMethod.Equals(SqlAuthenticationMethod.ActiveDirectoryManagedIdentity);
    }
}
