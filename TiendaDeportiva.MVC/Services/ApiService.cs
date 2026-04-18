using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class ApiService
{
    private readonly HttpClient _http;

    public ApiService()
    {
        _http = new HttpClient();
        _http.BaseAddress = new Uri("https://localhost:44300/");
    }

    public async Task<string> Get(string url)
    {
        var response = await _http.GetAsync(url);
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> Post(string url, string json)
    {
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _http.PostAsync(url, content);
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> Put(string url, string json)
    {
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _http.PutAsync(url, content);
        return await response.Content.ReadAsStringAsync();
    }

    public async Task Delete(string url)
    {
        await _http.DeleteAsync(url);
    }
}