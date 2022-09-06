using CUGOJ.Base.Dao.DB.Models;
namespace CUGOJ.Base.Conv;
public static class UserConv
{
    public static UserStruct UserPo2UserStruct(User user)
    {
        UserStruct res = new()
        {
            ID = CommonConv.ULong2Long(user.UserId),
            Phone = user.Phone,
            Email = user.Email,
            Signature = user.Signature,
            Organization_id = CommonConv.ULong2Long(user.OrganizationId),
            Nickname = user.Nickname,
            Realname = user.Realname,
            Avatar = user.Avatar,
            Type = ((UserTypeEnum)user.UserType),
            Status = ((UserStatusEnum)user.Status),
            CreateTime = CommonConv.DateTime2Long(user.CreateTime),
            UpdateTime = CommonConv.DateTime2Long(user.UpdateTime)
        };
        return res;
    }
}