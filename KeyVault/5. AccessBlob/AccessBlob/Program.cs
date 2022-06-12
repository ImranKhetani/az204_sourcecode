
using Newtonsoft.Json;

using System.Collections.Generic;
using System.Net.Http.Headers;

string tokenUri = "http://169.254.169.254/metadata/identity/oauth2/token?api-version=2018-02-01&resource=https://storage.azure.com";
HttpClient httpClient = new HttpClient();
httpClient.DefaultRequestHeaders.Add("Metadata", "true");

HttpResponseMessage response = await httpClient.GetAsync(tokenUri);
string content = await response.Content.ReadAsStringAsync();

//Console.WriteLine(content);

Dictionary<string,string> values=JsonConvert.DeserializeObject<Dictionary<string,string>>(content);
foreach(KeyValuePair<string,string> pair in values)
{
    Console.WriteLine("Key is " + pair.Key);
    Console.WriteLine("Value is " + pair.Value);
}


Console.WriteLine(values["access_token"]);
// We can now access the blob
string applicationUri = "https://simplilearnsaci2.blob.core.windows.net/data/SampleDoc.txt";
HttpClient httpClientStorage = new HttpClient();
httpClientStorage.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", values["access_token"]);
httpClientStorage.DefaultRequestHeaders.Add("x-ms-version", "2017-11-09");

HttpResponseMessage blobResponse = await httpClientStorage.GetAsync(applicationUri);
string blobContent = await blobResponse.Content.ReadAsStringAsync();

Console.WriteLine(blobContent);
Console.Read();