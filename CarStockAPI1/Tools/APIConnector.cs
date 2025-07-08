using APIConsumer.Interfaces;
using CarStockAPI1;
using RestSharp;
using System.Net;

namespace APIConsumer.Tools;

public class APIConnector : IAPIConnector
{
    private readonly RestClient _client;
    public APIConnector()
    {
        _client = new RestClient(Program._configuration["DataLayer:URL"]);
    }

    public async Task<T> GetAsync<T>(string method)
    {
        return await Connect<T>(Method.Get, method);
    }

    public async Task<T> PostAsync<T>(string method, object objectToPost)
    {
        return await Connect<T>(Method.Post, method, objectToPost);
    }

    public async Task<bool> DeleteAsync(string method, string idToRemove)
    {
        return await Connect<bool>(Method.Delete, method);
    }

    public async Task<bool> PatchAsync<T>(string method, object objectToPost)
    {
        return await Connect<bool>(Method.Patch, method, objectToPost);
    }

    public async Task<T> PutAsync<T>(string method, object objectToPost)
    {
        return await Connect<T>(Method.Put, method, objectToPost);
    }

    public async Task<T> Connect<T>(Method method, string urlMethod, object objectToPost = null)
    {
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
        var request = new RestRequest(urlMethod)
        {
            Method = method
        };

        try
        {
            RestResponse<T> response;

            switch (method)
            {
                case Method.Get:
                    response = await _client.ExecuteGetAsync<T>(request);
                    break;
                case Method.Post:
                    if (objectToPost != null)
                        request.AddJsonBody(objectToPost);
                    response = await _client.ExecutePostAsync<T>(request);
                    break;
                case Method.Patch:
                    if (objectToPost != null)
                        request.AddJsonBody(objectToPost);
                    response = await _client.ExecutePatchAsync<T>(request);
                    break;
                case Method.Put:
                    if (objectToPost != null)
                        request.AddJsonBody(objectToPost);
                    response = await _client.ExecutePutAsync<T>(request);
                    break;
                case Method.Delete:
                    response = await _client.ExecuteDeleteAsync<T>(request);
                    break;
                default:
                    throw new ArgumentException("Invalid HTTP method");
            }

            if (response.IsSuccessful)
                return response.Data;
            else
                throw new ApplicationException($"HTTP request failed: {response.StatusCode} - {response.ErrorMessage} - {response.Content}");
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to execute HTTP request", ex);
        }
    }
}
