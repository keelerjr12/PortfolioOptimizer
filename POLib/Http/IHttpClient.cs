using System.Threading.Tasks;

namespace POLib.Http
{
    public interface IHttpClient
    {
        Task<string> ReadAsync(string url);
    }
}
