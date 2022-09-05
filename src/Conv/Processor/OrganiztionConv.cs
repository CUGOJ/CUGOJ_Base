using CUGOJ.Base.Dao.DB.Models;
namespace CUGOJ.Base.Conv.Processor;
public class OrganizationConv
{
    public virtual OrganizationStruct OrganizationPo2Struct(Organization organization, Organization? parent = null, User? owner = null)
    {
        OrganizationStruct organizationStruct = new OrganizationStruct
        {
            ID = CommonConv.ULong2Long(organization.Id),
            Name = organization.Name,
            Description = organization.Description,
            Avatar = organization.Avatar,
            Status = ((OrganizationStatusEnum)organization.Status),
            CreateTime = CommonConv.DateTime2Unix(organization.CreateTime),
            UpdateTime = CommonConv.DateTime2Unix(organization.UpdateTime),
        };
        if (parent != null)
        {
            organizationStruct.Parent = OrganizationPo2Struct(parent);
        }
        else
            organizationStruct.Parent = new OrganizationStruct { ID = CommonConv.ULong2Long(organization.ParentId) };
        // if (owner != null)
        // {
        //     organizationStruct.Owner = UserConv.UserPo2Struct(owner);
        // }
        // else
        //     organizationStruct.Owner = new UserStruct { ID = CommonConv.ULong2Long(organization.Owner) };
        return organizationStruct;
    }
}