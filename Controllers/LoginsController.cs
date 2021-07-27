using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using C_TEste.Model;
using C_TesteFrontEnd.Data;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace C_TesteFrontEnd.Controllers
{
    public class LoginsController : Controller
    {
        private readonly C_TesteFrontEndContext _context;
        private readonly IHttpClientFactory _clientFactory;


        public LoginsController(C_TesteFrontEndContext context, IHttpClientFactory clientFactory)
        {
            _context = context;
            _clientFactory = clientFactory;

        }

        // GET: Logins
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ValidadeLogin(Login login)
        {
            var client = _clientFactory.CreateClient("ApiCTeste");

            var loginJson = new StringContent(
                                     JsonSerializer.Serialize(login),
                                     Encoding.UTF8,
                                     "application/json");

            var httpResponse =
                await client.PostAsync("Login/RealizarLogin", loginJson);

            if (httpResponse.IsSuccessStatusCode)
                ViewBag.Login = "true";

            else
                ViewBag.Login = "false";

            return View("SuccessLogin");


        }

    }
}
