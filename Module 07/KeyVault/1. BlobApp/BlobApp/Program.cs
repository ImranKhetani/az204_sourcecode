using Azure.Identity;
using Azure.Storage.Blobs;


string tenantId = "953be4b9-369a-4e5e-aa72-f0a88392b30e";
string clientId = "c83e38bc-47d5-4513-81d0-234abd3c8fd7";
string clientSecret = "MKq8Q~3LlWDLdF9T-zqL6IVvlc2-2KDLu7hUka6D";


string blobURI = "https://simplilearnsaci2.blob.core.windows.net/data/SampleDoc.txt";
string filePath = "C:\\users\\Imran\\Downloads\\SampleDoc.txt";

 ClientSecretCredential clientCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);

BlobClient blobClient = new BlobClient(new Uri(blobURI),clientCredential);

await blobClient.DownloadToAsync(filePath);

Console.WriteLine("The blob is downloaded");

