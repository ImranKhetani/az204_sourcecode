using Azure.Core;
using Azure.Identity;
using Azure.Storage.Blobs;



string blobURI = "https://simplilearnsaci02.blob.core.windows.net/data/SampleDoc.txt";
string filePath = "D:\\SampleDoc.txt";

TokenCredential tokenCredential = new DefaultAzureCredential();

BlobClient blobClient = new BlobClient(new Uri(blobURI), tokenCredential);

await blobClient.DownloadToAsync(filePath);

Console.WriteLine("The blob is downloaded");

