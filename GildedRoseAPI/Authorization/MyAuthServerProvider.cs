﻿using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;

namespace GildedRoseAPI.Authorization
{
	public class MyAuthServerProvider : OAuthAuthorizationServerProvider
	{
		public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
		{
			context.Validated();
		}
		public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
		{
			var identity = new ClaimsIdentity(context.Options.AuthenticationType);
			if (context.UserName == "admin" && context.Password == "admin")
			{
				identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
				identity.AddClaim(new Claim("username", "admin"));
				identity.AddClaim(new Claim(ClaimTypes.Name, "Merchant1"));
				context.Validated(identity);
			}
			//else if (context.UserName == "user" && context.Password == "user")
			//{
			//	identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
			//	identity.AddClaim(new Claim("username", "user"));
			//	identity.AddClaim(new Claim(ClaimTypes.Name, "Olamide Adek"));
			//	context.Validated(identity);
			//}
			else
			{
				context.SetError("Invalid_grant", "Provided username and password is incorrect");
			}
		}
	}
}