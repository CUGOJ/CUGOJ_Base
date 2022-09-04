using CUGOJ.CUGOJ_Tools.Trace;
namespace CUGOJ.Base.Dao.DB;

internal static class DBContext
{
    private static object _initLock = new();
    private static int _initCount = new();

    private static Context.CUGOJContext? _context = null;
    public static Context.CUGOJContext? Context
    {
        get => _context;
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
                _context = new Context.CUGOJContext();
                if (_context != null)
                {
                    _problemContext = TraceFactory.CreateTracableObject<DBProblemContext>(true, true);
                    _userContext = TraceFactory.CreateTracableObject<DBUserContext>(true, true);
                }
            }
        }
    }


}