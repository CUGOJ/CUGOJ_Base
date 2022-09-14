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

    public async Task<List<UserStruct>> MulGetUserStruct(List<long> userIDList, bool isGetDetail)
    {
        List<UserStruct> userList = new();
        List<long> missIDList = new();
        foreach (long userID in userIDList)
        {
            UserStruct? userStruct;
            if (!isGetDetail)
            {
                userStruct = RedisContext.Context?.Get<UserStruct>("user_" + userID.ToString());
            }
            else
            {
                userStruct = RedisContext.Context?.Get<UserStruct>("user_detail_" + userID.ToString());
            }
            if (userStruct == null)
            {
                missIDList.Add(userID);
            }
            else
            {
                userList.Add(userStruct);
            }
        }
        List<UserStruct> missUserList = await _dbUserContext.MulGetUserStruct(missIDList, isGetDetail);
        foreach (UserStruct userStruct in missUserList)
        {
            if (!isGetDetail)
            {
                RedisContext.Context?.Set("user_" + userStruct.ID.ToString(), userStruct, TimeSpan.FromSeconds(30));
            }
            else
            {
                RedisContext.Context?.Set("user_detail_" + userStruct.ID.ToString(), userStruct, TimeSpan.FromSeconds(5));
            }
        }
        userList.AddRange(missUserList);
        userList.Sort((a, b) => a.ID.CompareTo(b.ID));
        return userList;
    }
    public async Task<long> SaveUserStruct(UserStruct userStruct)
    {
        return await _dbUserContext.SaveUserStruct(userStruct);
    }
}