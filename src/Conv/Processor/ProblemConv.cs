using CUGOJ.Base.Dao.DB.Models;
namespace CUGOJ.Base.Conv.Processor;
public class ProblemConv
{
    public virtual ProblemStruct ProblemPo2ProblemStruct(ProblemBase problemBase, ProblemContent? problemContent = null, User? writer = null, ProblemSource? problemSource = null)
    {
        ProblemStruct res = new ProblemStruct()
        {
            ID = problemBase.Id,
            Title = problemBase.Title,
            Status = ((ProblemStatusEnum)problemBase.Status),
            CreateTime = CommonConv.DateTime2Unix(problemBase.CreateTime),
            UpdateTime = CommonConv.DateTime2Unix(problemBase.UpdateTime),
            Type = ((ProblemTypeEnum)problemBase.Type),
            Version = problemBase.Version ?? 0,
            AcceptedCount = problemBase.AcceptedCount ?? 0,
            SubmissionCount = problemBase.SubmissionCount ?? 0,
            Source = new()
            {
                ID = problemBase.SourceId
            },
            ShowID = problemBase.ShowId,
            Writer = new()
            {
                ID = problemBase.WriterId
            },
            Properties = problemBase.Properties
        };
        if (problemContent != null)
        {
            res.Content = problemContent.Content;
        }
        return res;
    }

    public virtual ProblemBase ProblemStruct2BasePo(ProblemStruct problemStruct)
    {
        return new ProblemBase
        {
            Id = problemStruct.ID,
            Title = problemStruct.Title,
            Status = (int)problemStruct.Status,
            CreateTime = CommonConv.Unix2DateTime(problemStruct.CreateTime),
            UpdateTime = CommonConv.Unix2DateTime(problemStruct.UpdateTime),
            Type = (int)problemStruct.Type,
            Version = problemStruct.Version,
            AcceptedCount = problemStruct.AcceptedCount,
            SubmissionCount = problemStruct.SubmissionCount,
            SourceId = problemStruct.Source.ID,
            ShowId = problemStruct.ShowID,
            WriterId = problemStruct.Writer.ID,
            Properties = problemStruct.Properties
        };
    }

    public virtual ProblemContent ProblemStruct2ContentPo(ProblemStruct problemStruct, ProblemContent? oldContent = null)
    {
        if (oldContent != null)
        {
            oldContent.Content = problemStruct.Content;
            oldContent.ProblemId = problemStruct.ID;
            return oldContent;
        }
        else
        {
            return new ProblemContent
            {
                ProblemId = problemStruct.ID,
                Content = problemStruct.Content
            };
        }
    }
}
