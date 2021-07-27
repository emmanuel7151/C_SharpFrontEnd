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
using Newtonsoft.Json.Linq;

namespace C_TesteFrontEnd.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly C_TesteFrontEndContext _context;
        private readonly IHttpClientFactory _clientFactory;
        private const string BaseUrl = "https://localhost:5001/";


        public ProdutosController(C_TesteFrontEndContext context, IHttpClientFactory clientFactory)
        {
            _context = context;
            _clientFactory = clientFactory;

        }

        // GET: Produtos
        public async Task<IActionResult> Index()
        {
            var produto = new List<Produto>();
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"{BaseUrl}Produtos/GetProdutos");

            var _client = _clientFactory.CreateClient("ApiCTeste");
            var response = await _client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                produto = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Produto>>(content);

            }

            return View(produto);
        }

        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var _client = _clientFactory.CreateClient("ApiCTeste");
            var response = await _client.GetAsync($"Produtos/Details?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var produto = Newtonsoft.Json.JsonConvert.DeserializeObject<Produto>(content);

                if (produto == null)
                {
                    return NotFound();
                }
                return View(produto);
            }
            return NotFound();
        }

        // GET: Produtos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Preco,Categoria")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                var client = _clientFactory.CreateClient("ApiCTeste");

                var produtoJson = new StringContent(
                                         JsonSerializer.Serialize(produto),
                                         Encoding.UTF8,
                                         "application/json");

                using var httpResponse =
                    await client.PutAsync("Produtos/Create", produtoJson);

                httpResponse.EnsureSuccessStatusCode();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Produtos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var _client = _clientFactory.CreateClient("ApiCTeste");
            var response = await _client.GetAsync($"Produtos/Details?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var produto = Newtonsoft.Json.JsonConvert.DeserializeObject<Produto>(content);

                if (produto == null)
                {
                    return NotFound();
                }
                return View(produto);
            }
            return NotFound();
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Preco,Categoria")] Produto produto)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var produtoJson = new StringContent(
                                        JsonSerializer.Serialize(produto),
                                        Encoding.UTF8,
                                        "application/json");

                var client = _clientFactory.CreateClient("ApiCTeste");
                using var httpResponse = await client.PutAsync("Produtos/Edit", produtoJson);
                httpResponse.EnsureSuccessStatusCode();

                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        // GET: Produtos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var _client = _clientFactory.CreateClient("ApiCTeste");
            var response = await _client.GetAsync($"Produtos/Details?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var produto = Newtonsoft.Json.JsonConvert.DeserializeObject<Produto>(content);

                if (produto == null)
                {
                    return NotFound();
                }
                return View(produto);

            }
            return NotFound();
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var _client = _clientFactory.CreateClient("ApiCTeste");
            var response = await _client.GetAsync($"Produtos/Details?id={id}");
            var produto = new Produto();
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                produto = Newtonsoft.Json.JsonConvert.DeserializeObject<Produto>(content);

                if (produto == null)
                {
                    return NotFound();
                }
            }
            using var httpResponse = await _client.DeleteAsync($"Produtos/Delete?id={id}");
            httpResponse.EnsureSuccessStatusCode();
            return RedirectToAction(nameof(Index));
        }

        //private bool ProdutoExists(int id)
        //{
        //    return _context.Produto.Any(e => e.Id == id);
        //}
    }
}
