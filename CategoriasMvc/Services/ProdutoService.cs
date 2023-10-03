﻿using CategoriasMvc.Models;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace CategoriasMvc.Services;

public class ProdutoService : IProdutoService
{
    private readonly IHttpClientFactory _clientFactory;
    private const string apiEndpoint = "/api/v1/produtos/";
    private readonly JsonSerializerOptions _options;
    private ProdutoViewModel produtoVM;
    private IEnumerable<ProdutoViewModel> produtosVM;

    public ProdutoService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions {  PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<ProdutoViewModel>> GetProdutos()
    {
        var client = _clientFactory.CreateClient("ProdutosApi");
        using (var response = await client.GetAsync(apiEndpoint))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                produtosVM = await JsonSerializer
                               .DeserializeAsync<IEnumerable<ProdutoViewModel>>
                               (apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return produtosVM;
    }
    public async Task<ProdutoViewModel> GetProdutoPorId(int id)
    {
        var client = _clientFactory.CreateClient("ProdutosApi");
        using (var response = await client.GetAsync(apiEndpoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                produtoVM = await JsonSerializer
                              .DeserializeAsync<ProdutoViewModel>
                              (apiResponse, _options);

            }
            else
            {
                return null;
            }
        }
        return produtoVM;
    }

    public async  Task<ProdutoViewModel> CriaProduto(ProdutoViewModel produtoVM)
    {
        var client = _clientFactory.CreateClient("ProdutosApi");
        var produto = JsonSerializer.Serialize(produtoVM);
        StringContent content = new StringContent(produto, Encoding.UTF8, "application/json");
        using (var response = await client.PostAsync(apiEndpoint, content))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                produtoVM = await JsonSerializer
                             .DeserializeAsync<ProdutoViewModel>
                             (apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return produtoVM;
    }
    public async  Task<bool> AtualizaProduto(int id, ProdutoViewModel produtoVM)
    {
        var client = _clientFactory.CreateClient("ProdutosApi");

        using (var response = await client.PutAsJsonAsync(apiEndpoint + id, produtoVM))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public async Task<bool> DeletaProduto(int id)
    {
        var client = _clientFactory.CreateClient("ProdutosApi");

        using (var response = await client.DeleteAsync(apiEndpoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
        }
        return false;
    }
}
