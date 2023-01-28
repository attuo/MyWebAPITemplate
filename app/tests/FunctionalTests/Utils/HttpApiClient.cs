using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace MyWebAPITemplate.Tests.FunctionalTests.Utils;
public class HttpApiClient
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _serializeOptions;

    public HttpApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _serializeOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    }

    public string ResourceName { get; set; } = String.Empty;

    public async Task<HttpStatusCode> Get(string? path = "")
    {
        var response = await _httpClient.GetAsync($"{ResourceName}/{path}");
        return response.StatusCode;
    }

    public async Task<(HttpStatusCode StatusCode, TResponse? ResponseModel)> Get<TResponse>(string? path = "")
    {
        var response = await _httpClient.GetAsync($"{ResourceName}/{path}");
        return await CreateResponse<TResponse>(response);
    }

    public async Task<(HttpStatusCode StatusCode, TResponse? ResponseModel)> Post<TBody, TResponse>(TBody model, string? path = "")
    {
        var response = await _httpClient.PostAsJsonAsync($"{ResourceName}/{path}", model);
        return await CreateResponse<TResponse>(response);
    }

    public async Task<(HttpStatusCode StatusCode, TResponse? ResponseModel)> Put<TBody, TResponse>(TBody model, string? path = "")
    {
        var response = await _httpClient.PutAsJsonAsync($"{ResourceName}/{path}", model);
        return await CreateResponse<TResponse>(response);
    }

    public async Task<HttpStatusCode> Delete(string? path = "")
    {
        var response = await _httpClient.DeleteAsync($"{ResourceName}/{path}");
        return response.StatusCode;
    }

    private async Task<(HttpStatusCode StatusCode, TResponse? ResponseModel)> CreateResponse<TResponse>(HttpResponseMessage responseMessage)
    {
        var responseBody = await responseMessage.Content.ReadAsStringAsync();
        return (
            responseMessage.StatusCode,
            responseBody == null ? default : JsonSerializer.Deserialize<TResponse>(responseBody, _serializeOptions));
    }
}