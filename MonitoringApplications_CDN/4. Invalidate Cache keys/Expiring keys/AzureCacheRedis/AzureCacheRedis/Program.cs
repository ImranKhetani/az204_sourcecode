using StackExchange.Redis;

string connectionString = "appcache1000.redis.cache.windows.net:6380,password=FGy8SbBmvXE4jRy0LwMzVAbSrSDKSSA6bAzCaDBRUFo=,ssl=True,abortConnect=False";

ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(connectionString);

//SetCacheData();
//GetCacheData();
//DeleteKey("top:3:courses");
SetKeyExpiry("top:3:courses", new TimeSpan(0, 0, 30));

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

void SetKeyExpiry(string key,TimeSpan expiry)
{
    IDatabase database = redis.GetDatabase();
    database.KeyExpire(key, expiry);
    Console.WriteLine("Set the key expiry to 30 seconds");
}