using CUGOJ.Base.Dao.DB.Models;
namespace CUGOJ.Base.Conv;

public static class ProblemConv
{
    public static ProblemStruct ProblemPo2ProblemStruct(ProblemBase problemBase, ProblemContent? problemContent = null, User? writer = null, ProblemSource? problemSource = null)
    {
        ProblemStruct res = new ProblemStruct()
        {
            ID = CommonConv.ULong2Long(problemBase.Id),
            Title = problemBase.Title,
            Status = ((ProblemStatusEnum)problemBase.Status),
            CreateTime = CommonConv.DateTime2Long(problemBase.CreateTime),
            UpdateTime = CommonConv.DateTime2Long(problemBase.UpdateTime),
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
}
