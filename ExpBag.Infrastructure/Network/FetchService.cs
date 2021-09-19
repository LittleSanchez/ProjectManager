using ExpBag.Application.Interfaces;
using ExpBag.Domain.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ExpBag.Infrastructure.Network
{
    public class FetchService : IFetchService
    {
        public async Task DownloadFile(string url, string destination)
        {
            var webClient = new WebClient();
            if (!Directory.Exists(Path.GetDirectoryName(destination)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(destination));
            }
            webClient.DownloadFileAsync(new Uri(url), destination);
        }

        public async Task<Return?> Fetch<Body, Return>(string url, string method = "GET", Body? body = null, Action<HttpWebRequest> requestOptions = null) where Body : class where Return: class
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = method;
                requestOptions?.Invoke(httpWebRequest);
                if (body != null)
                {
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        string json = JsonConvert.SerializeObject(body);
                        await streamWriter.WriteAsync(json);
                    }
                }
                var httpResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
                if (httpResponse.StatusCode != HttpStatusCode.OK)
                {
                    throw new WebException($"Error code: {httpResponse.StatusCode}, message: {httpResponse.StatusDescription}");
                }
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    string json = await streamReader.ReadToEndAsync();
                    var responseData = JsonConvert.DeserializeObject<Return>(json);
                    return responseData;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Return> FetchFileAsync<Return>(string url, FormFile file, Action<HttpWebRequest> requestOptions = null) where Return : class
        {
            HttpClient httpClient = new HttpClient();
            MultipartFormDataContent form = new MultipartFormDataContent();

            var fileBytes = await File.ReadAllBytesAsync(file.FilePath);

            form.Add(new ByteArrayContent(fileBytes, 0, fileBytes.Length), file.FormElementName, Path.GetFileName(file.FilePath));
            HttpResponseMessage response = await httpClient.PostAsync(url, form);

            response.EnsureSuccessStatusCode();
            httpClient.Dispose();
            var json = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<Return>(json);
            return responseData;
        }

        public async Task<Return> FetchFilesAsync<Return>(string url, IEnumerable<FormFile> files, Action<HttpRequestHeaders> requestOptions = null) where Return : class
        {
            HttpClient httpClient = new HttpClient();
            requestOptions(httpClient.DefaultRequestHeaders);
            MultipartFormDataContent form = new MultipartFormDataContent();

            foreach(var file in files)
            {
                var fileBytes = await File.ReadAllBytesAsync(file.FilePath);

                form.Add(new ByteArrayContent(fileBytes, 0, fileBytes.Length), file.FormElementName, Path.GetFileName(file.FilePath));
            }
            HttpResponseMessage response = await httpClient.PostAsync(url, form);

            response.EnsureSuccessStatusCode();
            httpClient.Dispose();
            var json = await response.Content.ReadAsStringAsync();
            var responseData = JsonConvert.DeserializeObject<Return>(json);
            return responseData;
        }
    }
}

