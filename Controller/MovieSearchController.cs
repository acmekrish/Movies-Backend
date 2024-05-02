using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class MovieSearchController : ControllerBase
{
    private readonly string _apiKey = "3fa6b1bc"; 

    [HttpGet]
    public async Task<IActionResult> Search(string title)
    {
        string url = $"http://www.omdbapi.com/?s={title}&apikey={_apiKey}";
        using var httpClient = new HttpClient();
        HttpResponseMessage response = await httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            var movieData = JsonConvert.DeserializeObject<MovieSearchResult>(content);
            return Ok(movieData.Search);
        }
        else
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error fetching movie data");
        }
    }
}
