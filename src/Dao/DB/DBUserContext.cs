using CUGOJ.Base.Dao.DB.Models;
using Microsoft.EntityFrameworkCore;
namespace CUGOJ.Base.Dao.DB;

public class DBUserContext : IDBUserContext
{
    public virtual async Task<List<UserStruct>> MulGetUserStruct(List<long> userIDList, bool isGetDetail)
    {
        List<ulong> IDList = Conv.CommonConv.LongList2ULongList(userIDList);
        List<User>? users = null;
        if (isGetDetail)
        {
            users = await (from u in DBContext.Context?.Users
                           where IDList.Contains(u.Id)
                           orderby u.Id
                           select new User
                           {
                               UserId = u.UserId,
                               Nickname = u.Nickname,
                               Realname = u.Realname,
                               Phone = u.Phone,
                               Email = u.Email,
                               Signature = u.Signature,
                               OrganizationId = u.OrganizationId,
                               Avatar = u.Avatar,
                               UserType = u.UserType,
                               Status = u.Status,
                               CreateTime = u.CreateTime,
                               UpdateTime = u.UpdateTime
                           }).ToListAsync();
        }
        else
        {
            users = await (from u in DBContext.Context?.Users
                           where IDList.Contains(u.Id)
                           orderby u.Id
                           select new User
                           {
                               UserId = u.UserId,
                               Nickname = u.Nickname,
                               OrganizationId = u.OrganizationId
                           }).ToListAsync();
        }
        if (users == null)
        {
            Logger.Error($"查找用户出错，无法获取用户信息。\nUserIDList:{userIDList}");
            throw new Exception("查找用户出错");
        }
        List<UserStruct> res = new();
        foreach (User user in users)
        {
            res.Add(Conv.UserConv.UserPo2UserStruct(user));
        }
        return res;
    }

    public virtual async void MulSaveUserStruct(List<UserStruct> userStructs)
    {
        List<User> users = new();
        foreach (UserStruct userStruct in userStructs)
        {
            users.Add(Conv.UserConv.UserStruct2UserPo(userStruct));
        }
        await DBContext.Context?.Users.AddRangeAsync(users);
        await DBContext.Context?.SaveChangesAsync();
    }
}