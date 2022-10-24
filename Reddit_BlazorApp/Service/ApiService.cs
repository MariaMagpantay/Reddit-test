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

    public ApiService(HttpClient http, IConfiguration configuration)
    {
        this.http = http;
        this.configuration = configuration;
        this.baseAPI = configuration["base_api"];
    }

    //GET metoder
    public async Task<Tråd[]> GetTråde()
    {
        string url = $"{baseAPI}tråde";
        return await http.GetFromJsonAsync<Tråd[]>(url);
    }

    public async Task<Tråd> GetTråd(int trådID)
    {
        string url = $"{baseAPI}tråd/{trådID}";
        return await http.GetFromJsonAsync<Tråd>(url);
    }

    //POST metoder
    public async Task<Kommentar> CreateKommentar(int trådID, int brugerID, string tekst)
    {
        string url = $"{baseAPI}tråd/{trådID}";
     
        // Post JSON to API, save the HttpResponseMessage
        HttpResponseMessage msg = await http.PostAsJsonAsync(url, new { tekst, brugerID });

        // Get the JSON string from the response
        string json = msg.Content.ReadAsStringAsync().Result;

        // Deserialize the JSON string to a Comment object
        Kommentar? NyKommentar = JsonSerializer.Deserialize<Kommentar>(json, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true // Ignore case when matching JSON properties to C# properties 
        });

        // Return the new comment 
        return NyKommentar;
    }

    //PUT metoder
    public async Task<Tråd> UpVotesTråd(int trådID)
    {
        string url = $"{baseAPI}tråd/{trådID}/upvote";

        // Post JSON to API, save the HttpResponseMessage
        HttpResponseMessage msg = await http.PutAsJsonAsync(url, "");

        // Get the JSON string from the response
        string json = msg.Content.ReadAsStringAsync().Result;

        // Deserialize the JSON string to a Post object
        Tråd? UpdatedTråd = JsonSerializer.Deserialize<Tråd>(json, new JsonSerializerOptions {
            PropertyNameCaseInsensitive = true // Ignore case when matching JSON properties to C# properties
        });

        // Return the updated post (vote increased)
        return UpdatedTråd;
    }

    public async Task<Tråd> DownVotesTråd(int trådID)
    {
        string url = $"{baseAPI}tråd/{trådID}/downvote";

        // Post JSON to API, save the HttpResponseMessage
        HttpResponseMessage msg = await http.PutAsJsonAsync(url, "");

        // Get the JSON string from the response
        string json = msg.Content.ReadAsStringAsync().Result;

        // Deserialize the JSON string to a Post object
        Tråd? UpdatedTråd = JsonSerializer.Deserialize<Tråd>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true // Ignore case when matching JSON properties to C# properties
        });

        // Return the updated post (vote increased)
        return UpdatedTråd;
    }
}
