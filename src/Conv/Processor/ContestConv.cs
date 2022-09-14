using CUGOJ.Base.Dao.DB.Models;
namespace CUGOJ.Base.Conv.Processor;

public class ContestConv
{
    public virtual ContestStruct ContestPo2ContestStruct(ContestBase contestBase, ContestContent? contestContent = null, Organization? organization = null, User? owner = null)
    {
        ContestStruct contestStruct = new ContestStruct
        {
            ID = contestBase.Id,
            Title = contestBase.Title,
            Status = ((ContestStatusEnum)contestBase.Status),
            CreateTime = CommonConv.DateTime2Unix(contestBase.CreateTime),
            UpdateTime = CommonConv.DateTime2Unix(contestBase.UpdateTime),
            StartTime = CommonConv.DateTime2Unix(contestBase.StartTime),
            EndTime = CommonConv.DateTime2Unix(contestBase.EndTime),
            Type = ((ContestTypeEnum)contestBase.Type),
            Profile = contestBase.Profile,
            Writer = contestBase.Writers,
        };
        if (contestContent != null)
            contestStruct.Content = contestContent.Content;
        if (organization != null)
            contestStruct.Organization = Conv.OrganizationConv.OrganizationPo2Struct(organization);
        else
            contestStruct.Organization = new OrganizationStruct { ID = contestBase.OrganizationId };
        return contestStruct;
    }

    public virtual ContestBase ContestStruct2BasePo(ContestStruct contestStruct)
    {
        return new ContestBase
        {
            Id = contestStruct.ID,
            Title = contestStruct.Title,
            Status = ((int)contestStruct.Status),
            CreateTime = CommonConv.Unix2DateTime(contestStruct.CreateTime),
            UpdateTime = CommonConv.Unix2DateTime(contestStruct.UpdateTime),
            StartTime = CommonConv.Unix2DateTime(contestStruct.StartTime),
            EndTime = CommonConv.Unix2DateTime(contestStruct.EndTime),
            Type = ((int)contestStruct.Type),
            Profile = contestStruct.Profile,
            Writers = contestStruct.Writer,
            OrganizationId = contestStruct.Organization.ID,
            OwnerId = contestStruct.Owner.ID,
        };
    }

    public virtual ContestContent ContestStruct2ContentPo(ContestStruct contestStruct)
    {
        return new ContestContent
        {
            Id = contestStruct.ID,
            Content = contestStruct.Content,
        };
    }
}