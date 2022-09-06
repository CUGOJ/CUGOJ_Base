using CUGOJ.Base.Dao.DB.Models;
using Microsoft.EntityFrameworkCore;
namespace CUGOJ.Base.Dao.DB;

public class DBUserContext : IUserContext
{
    public virtual async Task<List<UserStruct>> MulGetUserStruct(List<long> userIDList, bool isGetDetail)
    {
        using var context=DBContext.Context;
        List<User>? users = null;
        if (isGetDetail)
        {
            users = await (from u in context.Users
                           where userIDList.Contains(u.Id)
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
            users = await (from u in context.Users
                           where userIDList.Contains(u.Id)
                           orderby u.Id
                           select new User
                           {
                               UserId = u.UserId,
                               Nickname = u.Nickname,
                               OrganizationId = u.OrganizationId,
                               Status = u.Status
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
            res.Add(Conv.Conv.UserConv.UserPo2UserStruct(user));
        }
        return res;
    }

    public virtual async Task<long> SaveUserStruct(UserStruct userStruct)
    {
        using var context = DBContext.Context;
        User user = Conv.Conv.UserConv.UserStruct2UserPo(userStruct);
        context.Users.Update(user);
        await context.SaveChangesAsync();
        return user.Id;
    }
}