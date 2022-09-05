using CUGOJ.Base.Dao.DB.Models;
namespace CUGOJ.Base.Conv.Processor;
public class ProblemConv
{
    public virtual ProblemStruct ProblemPo2ProblemStruct(ProblemBase problemBase, ProblemContent? problemContent = null, User? writer = null, ProblemSource? problemSource = null)
    {
        ProblemStruct res = new ProblemStruct()
        {
            ID = CommonConv.ULong2Long(problemBase.Id),
            Title = problemBase.Title,
            Status = ((ProblemStatusEnum)problemBase.Status),
            CreateTime = CommonConv.DateTime2Unix(problemBase.CreateTime),
            UpdateTime = CommonConv.DateTime2Unix(problemBase.UpdateTime),
            Type = ((ProblemTypeEnum)problemBase.Type),
            Version = CommonConv.ULong2Long(problemBase.Version),
            AcceptedCount = CommonConv.ULong2Long(problemBase.AcceptedCount),
            SubmissionCount = CommonConv.ULong2Long(problemBase.SubmissionCount),
            Source = new()
            {
                ID = CommonConv.ULong2Long(problemBase.SourceId)
            },
            ShowID = problemBase.ShowId,
            Writer = new()
            {
                ID = CommonConv.ULong2Long(problemBase.WriterId)
            },
            Properties = problemBase.Properties
        };
        if (problemContent != null)
            res.Content = problemContent.Content;
        return res;
    }

    public virtual ProblemBase ProblemStruct2BasePo(ProblemStruct problemStruct)
    {
        return new ProblemBase
        {
            Id = CommonConv.Long2ULong(problemStruct.ID),
            Title = problemStruct.Title,
            Status = ((int)problemStruct.Status),
            CreateTime = CommonConv.Unix2DateTime(problemStruct.CreateTime),
            UpdateTime = CommonConv.Unix2DateTime(problemStruct.UpdateTime),
            Type = ((int)problemStruct.Type),
            Version = CommonConv.Long2ULong(problemStruct.Version),
            AcceptedCount = CommonConv.Long2ULong(problemStruct.AcceptedCount),
            SubmissionCount = CommonConv.Long2ULong(problemStruct.SubmissionCount),
            SourceId = CommonConv.Long2ULong(problemStruct.Source.ID),
            ShowId = problemStruct.ShowID,
            WriterId = CommonConv.Long2ULong(problemStruct.Writer.ID),
            Properties = problemStruct.Properties
        };
    }

    public virtual ProblemContent ProblemStruct2ContentPo(ProblemStruct problemStruct)
    {
        return new ProblemContent
        {
            ProblemId = CommonConv.Long2ULong(problemStruct.ID),
            Content = problemStruct.Content
        };
    }
}
