using CUGOJ.Base.Dao.Redis;
using CUGOJ.Base.Dao.DB;
namespace CUGOJ.Base.Dao
{
    public static class DaoContext
    {
        private static object _initLock = new();
        private static int _initCount = new();
        private static IUserContext? _userContext = null;
        public static IUserContext? UserContext { get => _userContext; }

        private static IProblemContext? _problemContext = null;
        public static IProblemContext? ProblemContext { get => _problemContext; }
        public static void InitDAO()
        {
            if (_initCount != 0) return;
            lock (_initLock)
            {
                if (_initCount != 0) return;
                _initCount++;
                CUGOJ.Base.Dao.DB.DBContext.InitDB();
                CUGOJ.CUGOJ_Tools.Redis.RedisContext.InitRedis();

                _problemContext = TraceFactory.CreateTracableObject<RedisProblemContext>(true, true);
                _userContext = TraceFactory.CreateTracableObject<DBUserContext>(true, true);
            }
        }
    }
}