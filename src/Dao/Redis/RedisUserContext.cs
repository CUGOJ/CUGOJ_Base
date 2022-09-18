using CUGOJ.Base.Dao.DB;
using CUGOJ.CUGOJ_Tools.Redis;
namespace CUGOJ.Base.Dao.Redis;
public class RedisUserContext : IUserContext
{
    private DBUserContext _dbUserContext;
    public RedisUserContext()
    {
        _dbUserContext = TraceFactory.CreateTracableObject<DBUserContext>(true, true);
    }

    public virtual async Task<List<UserStruct>> MulGetUserStruct(List<long> userIDList, bool isGetDetail)
    {
        List<UserStruct> userList = new();
        List<long> missIDList = new();
        var context = RedisContext.Context;
        if (context != null)
        {
            foreach (long userID in userIDList)
            {
                UserStruct? userStruct;
                if (!isGetDetail)
                {
                    userStruct = await context.GetWithCacheKey<UserStruct, long>(
                        async (id) =>
                        {
                            var userStructList = await _dbUserContext.MulGetUserStruct(new List<long> { id }, false);
                            return userStructList == null ? null : userStructList.FirstOrDefault();
                        },
                        userID,
                        "user_" + userID.ToString()
                    );
                }
                else
                {
                    userStruct = await context.GetWithCacheKey<UserStruct, long>(
                        async (id) =>
                        {
                            var userStructList = await _dbUserContext.MulGetUserStruct(new List<long> { id }, true);
                            return userStructList == null ? null : userStructList.FirstOrDefault();
                        },
                        userID,
                        "user_detail" + userID.ToString()
                    );
                }
                if (userStruct != null)
                {
                    userList.Add(userStruct);
                }
            }
        }
        userList.Sort((a, b) => a.ID.CompareTo(b.ID));
        return userList;
    }
    public virtual async Task<long> SaveUserStruct(UserStruct userStruct)
    {
        return await _dbUserContext.SaveUserStruct(userStruct);
    }
}