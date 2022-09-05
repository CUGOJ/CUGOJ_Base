using CUGOJ.CUGOJ_Tools.Trace;
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
    private static IDBUserContext? _userContext = null;
    public static IDBUserContext? UserContext { get => _userContext; }

    private static IDBProblemContext? _problemContext = null;
    public static IDBProblemContext? ProblemContext { get => _problemContext; }
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
                _factory = new PooledDbContextFactory<Context.CUGOJContext>(new DbContextOptionsBuilder<Context.CUGOJContext>()
                .UseMySql(CUGOJ.CUGOJ_Tools.Context.Context.ServiceBaseInfo.MysqlAddress, Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql"))
                .Options);
                if (_factory != null)
                {
                    _problemContext = TraceFactory.CreateTracableObject<DBProblemContext>(true, true);
                    _userContext = TraceFactory.CreateTracableObject<DBUserContext>(true, true);
                }
            }
        }
    }


}