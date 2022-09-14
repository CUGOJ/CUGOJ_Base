using CUGOJ.Base.Dao.DB.Models;
namespace CUGOJ.Base.Conv.Processor;
public class UserConv
{
    public virtual UserStruct UserPo2UserStruct(User user)
    {
        return new UserStruct()
        {
            ID = user.UserId,
            Phone = user.Phone,
            Email = user.Email,
            Signature = user.Signature,
            Organization_id = user.OrganizationId,
            Nickname = user.Nickname,
            Realname = user.Realname,
            Avatar = user.Avatar,
            Type = (UserTypeEnum)user.UserType,
            Status = (UserStatusEnum)user.Status,
            CreateTime = CommonConv.DateTime2Unix(user.CreateTime),
            UpdateTime = CommonConv.DateTime2Unix(user.UpdateTime)
        };
    }

    public virtual UserLoginInfoStruct UserPo2UserLoginInfoStruct(User user)
    {
        throw new NotImplementedException();
    }

    public virtual User UserStruct2UserPo(UserStruct userStruct, UserLoginInfoStruct? userLoginInfo = null)
    {
        var user = new User()
        {
            UserId = userStruct.ID,
            Phone = userStruct.Phone,
            Email = userStruct.Email,
            Signature = userStruct.Signature,
            OrganizationId = userStruct.Organization_id,
            Nickname = userStruct.Nickname,
            Realname = userStruct.Realname,
            Avatar = userStruct.Avatar,
            UserType = (int)userStruct.Type,
            Status = (int)userStruct.Status,
            CreateTime = CommonConv.Unix2DateTime(userStruct.CreateTime),
            UpdateTime = CommonConv.Unix2DateTime(userStruct.UpdateTime),
        };
        if (userLoginInfo != null)
        {
            user.Username = userLoginInfo.Username;
            user.Password = userLoginInfo.Password;
            user.Salt = userLoginInfo.Salt;
        }
        return user;
    }
}