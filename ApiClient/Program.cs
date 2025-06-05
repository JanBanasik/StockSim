using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes; // For pretty printing JSON
using System.Text.Json.Serialization; // For JsonIgnore

using StockSim.Shared;
using StockSim.Shared.ApiModels; // Use the new DTOs

const string baseUrl = "http://localhost:5247";

// admin key
// const string apiKey = "9c1403e180a141c4aa40c4650987aff4"; 

// testtest user key
const string apiKey = "71593539f6a148e790a73495ca30dedf";

using var client = new HttpClient();
client.BaseAddress = new Uri(baseUrl);
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
client.DefaultRequestHeaders.Add("X-Api-Key", apiKey);


var jsonOptions = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true,
    WriteIndented = true,
    Converters = { new JsonStringEnumConverter() }
};

Console.WriteLine("StockSim API Client (Postman-like)");
Console.WriteLine("----------------------------------");
Console.WriteLine($"Base URL: {baseUrl}");
Console.WriteLine($"Using API Key: {apiKey}");
Console.WriteLine("Enter commands (e.g., GET /api/companies, POST /api/trade/buy):");
Console.WriteLine("Type 'exit' to quit.");
Console.WriteLine("For POST/PUT, enter JSON body on subsequent lines, end with an empty line.");
Console.WriteLine("----------------------------------");


while (true)
{
    Console.Write("> ");
    var input = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(input))
    {
        continue;
    }

    if (input.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
    {
        break;
    }

    var parts = input.Trim().Split(new[] { ' ' }, 2);
    if (parts.Length != 2)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Invalid command format. Use: METHOD /path");
        Console.ResetColor();
        continue;
    }

    var method = parts[0].Trim().ToUpper();
    var path = parts[1].Trim();

     if (!path.StartsWith("/") && !path.StartsWith("http", StringComparison.OrdinalIgnoreCase))
     {
          Console.ForegroundColor = ConsoleColor.Yellow;
          Console.WriteLine("Invalid path format. Must start with '/' or 'http'.");
          Console.ResetColor();
          continue;
     }


     if (path.StartsWith("/"))
     {
         path = $"{baseUrl}{path}";
     }


    HttpResponseMessage? response = null;
    string? requestBody = null;

    try
    {
        var httpRequestMessage = new HttpRequestMessage(new HttpMethod(method), path);


         httpRequestMessage.Headers.Add("X-Api-Key", apiKey);


        if (method == "POST" || method == "PUT" || method == "PATCH")
        {
            Console.WriteLine("Enter JSON request body (end with an empty line):");
            var bodyBuilder = new StringBuilder();
            string? line;
            while (!string.IsNullOrEmpty(line = Console.ReadLine()))
            {
                bodyBuilder.AppendLine(line);
            }
            requestBody = bodyBuilder.ToString().Trim();

            if (!string.IsNullOrEmpty(requestBody))
            {
                httpRequestMessage.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
            }
        }


        Console.WriteLine($"\nSending {method} to {path}...");
        if (!string.IsNullOrEmpty(requestBody))
        {
             Console.WriteLine("Request Body:\n" + requestBody);
        }

        response = await client.SendAsync(httpRequestMessage);

        Console.WriteLine($"\nResponse Status: {response.StatusCode}");

        var responseBody = await response.Content.ReadAsStringAsync();

        if (!string.IsNullOrEmpty(responseBody))
        {
            Console.WriteLine("\nResponse Body:");
            try
            {
                var jsonNode = JsonNode.Parse(responseBody);
                Console.WriteLine(jsonNode?.ToJsonString(jsonOptions));
            }
            catch (JsonException)
            {

                Console.WriteLine(responseBody);
            }
        }
        else
        {
            Console.WriteLine("\nResponse Body: (Empty)");
        }

 	}
    catch (HttpRequestException e)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\nHTTP Request Error: {e.Message}");
        if (e.StatusCode.HasValue)
        {
             Console.WriteLine($"Status Code: {e.StatusCode}");
        }
        if (response != null)
        {
            var errorBody = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(errorBody))
            {
                 Console.WriteLine("Error Response Body:\n" + errorBody);
            }
        }

        Console.ResetColor();
    }
    catch (JsonException e)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\nJSON Deserialization/Parsing Error: {e.Message}");
         Console.WriteLine($"Raw Response Body (if available): {response?.Content.ReadAsStringAsync().Result}"); // Use .Result cautiously in async context
        Console.ResetColor();
    }
    catch (Exception e)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\nAn unexpected error occurred: {e.Message}");
        Console.ResetColor();
    }

    Console.WriteLine("\n----------------------------------");
}

Console.WriteLine("Exiting client.");