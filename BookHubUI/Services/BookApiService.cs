using System.Net.Http.Json;
//using BookingClients.DTOs;
using BookHubAPI.DTOs;

namespace BookHubUI.Services;

public class BookApiService
{
    private readonly HttpClient _http;

    public BookApiService(HttpClient http) => _http = http;


    public async Task<List<BookDTO>> GetBooksAsync()

        => await _http.GetFromJsonAsync<List<BookDTO>>("api/books") ?? [];

}
