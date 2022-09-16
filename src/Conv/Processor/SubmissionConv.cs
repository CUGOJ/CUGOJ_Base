using CUGOJ.Base.Dao.DB.Models;
namespace CUGOJ.Base.Conv.Processor;
public class SubmmissionConv
{
    public virtual SubmissionStruct SubmissionPo2SubmissionStruct(SubmissionBase submissionBase, SubmissionContent? submissionContent = null, User? user = null, Team? team = null, ProblemBase? problemBase = null)
    {
        return new SubmissionStruct
        {
            ID = submissionBase.Id,
            User = new UserStruct { ID = submissionBase.SubmitterId },
            Team = new TeamStruct { ID = submissionBase.SubmitterId },
            Problem = new ProblemStruct { ID = submissionBase.ProblemId ?? -1 },
            Contest = new ContestStruct { ID = submissionBase.ContestId ?? -1 },
            SubmitTime = CommonConv.DateTime2Unix(submissionBase.SubmitTime),
            Status = submissionBase.Status,
            Type = submissionBase.Type,
            Properties = submissionBase.Properties,
            Content = submissionContent == null ? null : submissionContent.Content,
            CreateTime = CommonConv.DateTime2Unix(submissionBase.CreateTime),
            UpdateTime = CommonConv.DateTime2Unix(submissionBase.UpdateTime),
        };
    }

    public virtual SubmissionBase SubmissionStruct2SubmissionPo(SubmissionStruct submissionStruct)
    {
        return new SubmissionBase
        {
            SubmitTime = CommonConv.Unix2DateTime(submissionStruct.SubmitTime),
            // 判提交者类型（补）
            SubmitterId = submissionStruct.User.ID,
            SubmitterType = submissionStruct.Type,//---
            Status = submissionStruct.Status,
            Type = submissionStruct.Type,
            ContestId = submissionStruct.Contest.ID,
            ProblemId = submissionStruct.Problem.ID,
            UpdateTime = CommonConv.Unix2DateTime(submissionStruct.UpdateTime),
            CreateTime = CommonConv.Unix2DateTime(submissionStruct.CreateTime),
            Properties = submissionStruct.Properties
        };
    }

    public virtual SubmissionContent SubmissionStruct2ContentPo(SubmissionStruct submissionStruct)
    {
        return new SubmissionContent
        {
            SubmissionId = submissionStruct.ID,
            Content = submissionStruct.Content
        };
    }
}