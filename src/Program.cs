// See https://aka.ms/new-console-template for more information
try
{
    var parsedArgs = CUGOJ.CUGOJ_Tools.Tools.ParamsTool.ParseArgs(args, CUGOJ.RPC.Gen.Base.ServiceTypeEnum.Base);
    string connectionString = "";
    if (parsedArgs.ContainsKey("connectionString"))
        connectionString = parsedArgs["connectionString"];
    if (parsedArgs.ContainsKey("debug"))
    {
        Context.Debug = true;
        Context.ServiceBaseInfo = new()
        {
            LogAddress = string.Empty,
            TraceAddress = string.Empty
        };
        if (parsedArgs.ContainsKey("mysql"))
        {
            Context.ServiceBaseInfo.MysqlAddress = parsedArgs["mysql"];
        }
        if (parsedArgs.ContainsKey("redis"))
        {
            Context.ServiceBaseInfo.RedisAddress = parsedArgs["redis"];
        }
        if (parsedArgs.ContainsKey("rabbit"))
        {
            Context.ServiceBaseInfo.RabbitMQAddress = parsedArgs["rabbit"];
        }
        if (parsedArgs.ContainsKey("neo4j"))
        {
            Context.ServiceBaseInfo.Neo4jAddress = parsedArgs["neo4j"];
        }
    }

    await CUGOJ.CUGOJ_Tools.RPC.RPCService.StartBaseService<CUGOJ.BaseService.BaseServiceHandler>(connectionString, () =>
    {
        CUGOJ.Base.Dao.DaoContext.InitDAO();
    });
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}