using Alura.ListaLeitura.Seguranca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.HttpClients
{
    public class LoginResut
    {

        public bool Succeeded { get; set;  }
        public string Token { get; set; }
    }
    
    public class AuthApiClient
    {
        private readonly HttpClient _httpClient;

        public AuthApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }
        public async Task<LoginResut> PostLoginAsync(LoginModel model)
        {

            var resposta = await _httpClient.PostAsJsonAsync("login", model);
            return new LoginResut
            {
                Succeeded = resposta.IsSuccessStatusCode,
                Token = await resposta.Content.ReadAsStringAsync()
            };


        }
    }
}
