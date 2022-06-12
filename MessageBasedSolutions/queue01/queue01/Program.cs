using Azure;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using System;
using System.Threading.Tasks;

namespace queue01
{
    class Program
    {
        static async Task Main(string[] args)
        {
            string connectionString = "DefaultEndpointsProtocol=https;AccountName=simplilearnsaci02;AccountKey=5vjyfYPOdveWagavte//skj35go/OEXN6Ki2dD4f/AOGndZkX+FNd/uzRLwCN5dc64D39j95Awsy+AStQVy1qA==;EndpointSuffix=core.windows.net";
            string queueName = "messages";

            QueueClient queueClient = new QueueClient(connectionString, queueName);

            string FirstMessage = "This is a first Message";
            string SecondMessage = "This is a Second Message";

            Console.WriteLine("\nAdding messages to the queue...");

            // Send several messages to the queue
            await queueClient.SendMessageAsync(FirstMessage);
            //await queueClient.SendMessageAsync(SecondMessage);
            // Add message with never retire ttl
            await queueClient.SendMessageAsync(SecondMessage, null, TimeSpan.FromSeconds(-1));

            Console.WriteLine("\nPeek at the messages in the queue...");

            // Peek at messages in the queue
            PeekedMessage[] peekedMessages = await queueClient.PeekMessagesAsync(maxMessages: 10);

            foreach (PeekedMessage peekedMessage in peekedMessages)
            {
                // Display the message
                Console.WriteLine($"Message: {peekedMessage.MessageText}");
            }

            Console.WriteLine("\nReceiving messages from the queue...");

            // Get messages from the queue
            QueueMessage[] messages = await queueClient.ReceiveMessagesAsync(maxMessages: 10);

            Console.WriteLine("\nPress Enter key to 'process' messages and delete them from the queue...");
            Console.ReadLine();

            // Process and delete messages from the queue
            foreach (QueueMessage message in messages)
            {
                // "Process" the message
                Console.WriteLine($"Message: {message.MessageText}");

                // Let the service know we're finished with
                // the message and it can be safely deleted.
                await queueClient.DeleteMessageAsync(message.MessageId, message.PopReceipt);
            }
        }
    }
}
