using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.Application.Interfaces
{

    public class FormFile
    {
        public string FormElementName { get; set; }
        public string FilePath { get; set; }      
    }

    public interface IFetchService
    {
        Task<Return?> Fetch<Body, Return>(string url, string method = "GET", Body? body = null, Action<HttpWebRequest> requestOptions = null) where Body : class where Return : class;
        Task<Return> FetchFileAsync<Return>(string url, FormFile file, Action<HttpWebRequest> requestOptions = null) where Return : class;
        Task<Return> FetchFilesAsync<Return>(string url, IEnumerable<FormFile> files, Action<HttpRequestHeaders> requestOptions = null) where Return : class;
        Task DownloadFile(string url, string destination);
    }
}
