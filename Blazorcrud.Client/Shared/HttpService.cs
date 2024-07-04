using Blazorcrud.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Blazorcrud.Client.Shared;

public class HttpService : IHttpService
{
    private HttpClient _httpClient;
    private NavigationManager _navigationManager;
    private ILocalStorageService _localStorageService;
    private IConfiguration _configuration;

    public HttpService(
        HttpClient httpClient,
        NavigationManager navigationManager,
        ILocalStorageService localStorageService,
        IConfiguration configuration
    ) {
        _httpClient = httpClient;
        _navigationManager = navigationManager;
        _localStorageService = localStorageService;
        _configuration = configuration;
        Console.WriteLine("Client.Shared.HttpService, HttpService():");
    }

    public async Task<T> Get<T>(string uri)
    {
        Console.WriteLine($"Client.Shared.HttpService, Get: {uri}");
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        Console.WriteLine($"Client.Shared.HttpService, request: {request.GetType().ToString()}");
        return await sendRequest<T>(request);
    }

    public async Task<T> GetGuid<T>(string uri)
    {
        Console.WriteLine($"Client.Shared.HttpService, GetGuid: {uri}");
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        Console.WriteLine($"Client.Shared.HttpService, request: {request.GetType().ToString()}");
        return await sendRequest<T>(request);
    }

    public async Task Post(string uri, object value)
    {
        Console.WriteLine($"Client.Shared.HttpService, Post: {uri}");
        var request = createRequest(HttpMethod.Post, uri, value);
        await sendRequest(request);
    }

    public async Task<T> Post<T>(string uri, object value)
    {
        Console.WriteLine($"Client.Shared.HttpService, Post<T>: {uri}");
        var request = createRequest(HttpMethod.Post, uri, value);
        return await sendRequest<T>(request);
    }

    public async Task Put(string uri, object value)
    {
        Console.WriteLine($"Client.Shared.HttpService, Put: {uri}");
        var request = createRequest(HttpMethod.Put, uri, value);
        await sendRequest(request);
    }

    public async Task<T> Put<T>(string uri, object value)
    {
        Console.WriteLine($"Client.Shared.HttpService, Put<T>: {uri}");
        var request = createRequest(HttpMethod.Put, uri, value);
        return await sendRequest<T>(request);
    }

    public async Task Delete(string uri)
    {
        Console.WriteLine($"Client.Shared.HttpService, Del: {uri}");
        var request = createRequest(HttpMethod.Delete, uri);
        await sendRequest(request);
    }

    public async Task<T> Delete<T>(string uri)
    {
        Console.WriteLine($"Client.Shared.HttpService, Del<T>: {uri}");
        var request = createRequest(HttpMethod.Delete, uri);
        return await sendRequest<T>(request);
    }

    // helper methods

    private HttpRequestMessage createRequest(HttpMethod method, string uri, object value = null)
    {
        var request = new HttpRequestMessage(method, uri);
        if (value != null)
            request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
        return request;
    }

    private async Task sendRequest(HttpRequestMessage request)
    {
        await addJwtHeader(request);

        // send request
        using var response = await _httpClient.SendAsync(request);

        // auto logout on 401 response
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            _navigationManager.NavigateTo("user/logout");
            return;
        }

        await handleErrors(response);
    }

    private async Task<T> sendRequest<T>(HttpRequestMessage request)
    {
        await addJwtHeader(request);
        
        // send request
        using var response = await _httpClient.SendAsync(request);

        // auto logout on 401 response
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            _navigationManager.NavigateTo("user/logout");
            return default;
        }

        await handleErrors(response);

        var options = new JsonSerializerOptions();
        options.PropertyNameCaseInsensitive = true;
        options.Converters.Add(new StringConverter());
        return await response.Content.ReadFromJsonAsync<T>(options);
    }

    private async Task addJwtHeader(HttpRequestMessage request)
    {
        // add jwt auth header if user is logged in and request is to the api url
        var user = await _localStorageService.GetItem<User>("user");
        var isApiUrl = !request.RequestUri.IsAbsoluteUri;
        if (user != null && isApiUrl)
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);
    }

    private async Task handleErrors(HttpResponseMessage response)
    {
        // throw exception on error response
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            throw new Exception(error["message"]);
        }
    }
}