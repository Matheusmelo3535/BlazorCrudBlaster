using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using Mysql_blazor2.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;




namespace Mysql_blazor2.Client
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {

        private readonly HttpClient _httpClient;

        public CustomAuthenticationStateProvider(HttpClient httpClient)
        {
           _httpClient = httpClient;
        }
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            User CurrentUser =  await _httpClient.GetFromJsonAsync<User>("User/GetUser");

            if(CurrentUser != null && CurrentUser.Email != null)
            {
                var claim = new Claim(ClaimTypes.Name, CurrentUser.Email);
                var claimIdentity = new ClaimsIdentity(new[] {claim}, "serverAuth");
                var claimPrincipal = new ClaimsPrincipal(claimIdentity);

                return new AuthenticationState(claimPrincipal);

            }
            else
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
    }
}