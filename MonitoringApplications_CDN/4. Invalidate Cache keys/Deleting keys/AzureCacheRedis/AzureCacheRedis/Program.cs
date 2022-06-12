using StackExchange.Redis;

string connectionString = "simplilearnredis.redis.cache.windows.net:6380,password=hsEgOuMS7SYFI24RhdirGKAZaznxGftuqAzCaDDB4So=,ssl=True,abortConnect=False";

ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(connectionString);

//SetCacheData();
//GetCacheData();
DeleteKey("top:3:courses");

void SetCacheData()
{
    IDatabase database=redis.GetDatabase();

    database.StringSet("top:3:courses", "AZ-104,AZ-305,AZ-204");

    Console.WriteLine("Cache data set");
}

void GetCacheData()
{
    IDatabase database = redis.GetDatabase();
    if (database.KeyExists("top:3:courses"))
        Console.WriteLine(database.StringGet("top:3:courses"));
    else
        Console.WriteLine("key does not exist");

}

void DeleteKey(string keyName)
{
    IDatabase database = redis.GetDatabase();
    if (database.KeyExists("top:3:courses"))
    {
        database.KeyDelete(keyName);
        Console.WriteLine("Key deleted");
    }
    else
        Console.WriteLine("key does not exist");
}