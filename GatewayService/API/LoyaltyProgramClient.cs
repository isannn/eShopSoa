using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LoyaltyProgramService.Model;
using Nancy.Json;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;

namespace GatewayService.API
{
    public class LoyaltyProgramClient
    {
        private static readonly AsyncRetryPolicy ExponentialRetryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(
                3, attempt => TimeSpan.FromMilliseconds(100 * Math.Pow(2, attempt)));

        private string _hostName;

        public LoyaltyProgramClient(string loyalProgramMicroserviceHostName)
        {
            this._hostName = loyalProgramMicroserviceHostName;
        }

        public async Task<HttpResponseMessage> RegisterUser(LoyaltyProgramUser newUser)
        {
            return await ExponentialRetryPolicy.ExecuteAsync(() => DoRegisterUser(newUser));
        }

        public async Task<HttpResponseMessage> UpdateUser(LoyaltyProgramUser user)
        {
            return await ExponentialRetryPolicy.ExecuteAsync(() => DoUpdateUser(user));
        }

        private async Task<HttpResponseMessage> DoRegisterUser(LoyaltyProgramUser newUser)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"http://{this._hostName}");
                var response = await client.PostAsync("/users/"
                    , new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json"));
                ThrowOnTransientFailure(response);
                return response;
            }
        }

        private async Task<HttpResponseMessage> DoUpdateUser(LoyaltyProgramUser user)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"http://{this._hostName}");
                var response = await client.PutAsync($"/users/{user.Id}"
                    , new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json"));
                ThrowOnTransientFailure(response);
                return response;
            }
        }

        private static void ThrowOnTransientFailure(HttpResponseMessage response)
        {
            if (((int)response.StatusCode) < 200 || ((int)response.StatusCode) > 499) throw new Exception(response.StatusCode.ToString());
        }
    }
}
