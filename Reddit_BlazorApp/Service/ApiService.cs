using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

using Model;

namespace Reddit_BlazorApp.Data;

public class ApiService
{
    private readonly HttpClient http;
    private readonly IConfiguration configuration;
    private readonly string baseAPI = "";

    public event Action RefreshRequired;

    public ApiService(HttpClient http, IConfiguration configuration)
    {
        this.http = http;
        this.configuration = configuration;
        this.baseAPI = configuration["base_api"];
    }

    public void CallRequestRefresh()
    {
        RefreshRequired?.Invoke();
    }

    //GET metoder
    public async Task<Tr�d[]> GetTr�de()
    {
        string url = $"{baseAPI}tr�de";
        return await http.GetFromJsonAsync<Tr�d[]>(url);
    }

    public async Task<Tr�d> GetTr�d(int tr�dID)
    {
        string url = $"{baseAPI}tr�d/{tr�dID}";
        return await http.GetFromJsonAsync<Tr�d>(url);
    }

    //POST metoder
    public async Task<Tr�d> CreateTr�d(int brugerID, string overskrift, string indhold)
    {
        string url = $"{baseAPI}tr�de";

        // Post JSON to API, save the HttpResponseMessage
        HttpResponseMessage msg = await http.PostAsJsonAsync(url, new { overskrift, indhold, brugerID });

        // Get the JSON string from the response
        string json = msg.Content.ReadAsStringAsync().Result;

        // Deserialize the JSON string to a Comment object
        Tr�d? NyTr�d = JsonSerializer.Deserialize<Tr�d>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true // Ignore case when matching JSON properties to C# properties 
        });

        // Return the new comment 
        CallRequestRefresh();
        return NyTr�d;
    }
    public async Task<Kommentar> CreateKommentar(int tr�dID, int brugerID, string tekst)
    {
        string url = $"{baseAPI}tr�d/{tr�dID}";
     
        // Post JSON to API, save the HttpResponseMessage
        HttpResponseMessage msg = await http.PostAsJsonAsync(url, new { tekst, brugerID });

        // Get the JSON string from the response
        string json = msg.Content.ReadAsStringAsync().Result;

        // Deserialize the JSON string to a Comment object
        Kommentar? NyKommentar = JsonSerializer.Deserialize<Kommentar>(json, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true // Ignore case when matching JSON properties to C# properties 
        });

        // Return the new comment 
        CallRequestRefresh();
        return NyKommentar;
    }

    //PUT metoder
    public async Task<Tr�d> UpVotesTr�d(int tr�dID)
    {
        string url = $"{baseAPI}tr�d/{tr�dID}/upvote";

        // Post JSON to API, save the HttpResponseMessage
        HttpResponseMessage msg = await http.PutAsJsonAsync(url, "");

        // Get the JSON string from the response
        string json = msg.Content.ReadAsStringAsync().Result;

        // Deserialize the JSON string to a Post object
        Tr�d? UpdatedTr�d = JsonSerializer.Deserialize<Tr�d>(json, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true // Ignore case when matching JSON properties to C# properties
        });

        // Return the updated post (vote increased)
        CallRequestRefresh();
        return UpdatedTr�d;
    }

    public async Task<Tr�d> DownVotesTr�d(int tr�dID)
    {
        string url = $"{baseAPI}tr�d/{tr�dID}/downvote";

        // Post JSON to API, save the HttpResponseMessage
        HttpResponseMessage msg = await http.PutAsJsonAsync(url, "");

        // Get the JSON string from the response
        string json = msg.Content.ReadAsStringAsync().Result;

        // Deserialize the JSON string to a Post object
        Tr�d? UpdatedTr�d = JsonSerializer.Deserialize<Tr�d>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true // Ignore case when matching JSON properties to C# properties
        });

        // Return the updated post (vote increased)
        CallRequestRefresh();
        return UpdatedTr�d;
    }

    public async Task<Kommentar> UpVotesKommentar(int kommentarID)
    {
        string url = $"{baseAPI}kommentar/{kommentarID}/upvote";

        // Post JSON to API, save the HttpResponseMessage
        HttpResponseMessage msg = await http.PutAsJsonAsync(url, "");

        // Get the JSON string from the response
        string json = msg.Content.ReadAsStringAsync().Result;

        // Deserialize the JSON string to a Post object
        Kommentar? UpdatedKommentar = JsonSerializer.Deserialize<Kommentar>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true // Ignore case when matching JSON properties to C# properties
        });

        // Return the updated post (vote increased)
        CallRequestRefresh();
        return UpdatedKommentar;
    }

    public async Task<Kommentar> DownVotesKommentar(int kommentarID)
    {
        string url = $"{baseAPI}kommentar/{kommentarID}/downvote";

        // Post JSON to API, save the HttpResponseMessage
        HttpResponseMessage msg = await http.PutAsJsonAsync(url, "");

        // Get the JSON string from the response
        string json = msg.Content.ReadAsStringAsync().Result;

        // Deserialize the JSON string to a Post object
        Kommentar? UpdatedKommentar = JsonSerializer.Deserialize<Kommentar>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true // Ignore case when matching JSON properties to C# properties
        });

        // Return the updated post (vote increased)
        CallRequestRefresh();
        return UpdatedKommentar;
    }
}
