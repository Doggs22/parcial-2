namespace APIConsumer.Interfaces;

public interface IAPIConnector
{
    Task<T> GetAsync<T>(string method);
    Task<T> PostAsync<T>(string method, object objectToPost);
    Task<bool> DeleteAsync(string method, string idToRemove);
    Task<bool> PatchAsync<T>(string method, object objectToPost);
    Task<T> PutAsync<T>(string method, object objectToPost);
}
