using CUGOJ.CUGOJ_Tools.Trace;
using CUGOJ.Base.Dao.DB.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
namespace CUGOJ.Base.Dao.DB;

internal static class DBContext
{
    private static object _initLock = new();
    private static int _initCount = new();

    private static IDbContextFactory<Context.CUGOJContext>? _factory;
    public static Context.CUGOJContext Context
    {
        get
        {
            if (_factory == null)
            {
                throw new Exception("未能成功连接到DB, 请检查DB配置");
            }
            return _factory.CreateDbContext();
        }
    }
    public static void InitDB()
    {
        if (_initCount != 0) return;
        lock (_initLock)
        {
            if (_initCount != 0) return;
            _initCount++;
            if (CUGOJ.CUGOJ_Tools.Context.Context.ServiceBaseInfo.MysqlAddress != string.Empty &&
            CUGOJ.CUGOJ_Tools.Context.Context.ServiceBaseInfo.MysqlAddress != "null")
            {
                var options = new DbContextOptionsBuilder<CUGOJContext>()
                .UseMySql(CUGOJ.CUGOJ_Tools.Context.Context.ServiceBaseInfo.MysqlAddress, Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.26-mysql"))
                .Options;
                _factory = new PooledDbContextFactory<CUGOJContext>(options);
            }
        }
    }


}