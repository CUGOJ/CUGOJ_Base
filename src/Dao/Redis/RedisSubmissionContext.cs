using CUGOJ.Base.Dao.DB;
using CUGOJ.Base.Dao.DB.Models;
using CUGOJ.CUGOJ_Tools.Redis;

namespace CUGOJ.Base.Dao.Redis;
public class RedisSubmissionContext : ISubmissionContext
{
    private DBSubmissionContext _dbSubmissionContext;
    public RedisSubmissionContext()
    {
        _dbSubmissionContext = TraceFactory.CreateTracableObject<DBSubmissionContext>(true, true);
    }
    public async Task<List<SubmissionStruct>> GetSubmissionList(SubmissionListQueryStruct submissionListQueryStruct)
    {
        List<SubmissionStruct>? submissionStructList = null;
        var context = RedisContext.Context;
        if (submissionListQueryStruct.NewSubmissionFirst && submissionListQueryStruct.MaxStoredTimestamp <= 0)
        {
            Logger.Error("查询提交列表出错, 需要查询最新提交时给出的时间戳限制错误, MaxStoredTimestamp: {0}.", submissionListQueryStruct.MaxStoredTimestamp);
            throw new Exception("查询提交列表出错");
        }
        if (context != null)
        {
            if (submissionListQueryStruct.ContestID >= 1)
            {
                if (submissionListQueryStruct.NewSubmissionFirst)
                {
                    submissionStructList= await context.GetWithZSetCache<SubmissionStruct, SubmissionListQueryStruct>
                    (
                        async (submissionListQueryStruct) =>
                        {
                            return await _dbSubmissionContext.GetSubmissionList(submissionListQueryStruct);
                        },
                        (submissionStruct) =>
                        {
                            return submissionStruct.SubmitTime;
                        },
                        submissionListQueryStruct,
                        "contest_" + submissionListQueryStruct.ContestID + "_submission",
                        submissionListQueryStruct.Cursor,
                        submissionListQueryStruct.Limit,
                        submissionListQueryStruct.MaxStoredTimestamp,
                        Conv.CommonConv.DateTime2Unix(DateTime.Now)
                    );
                }
                else
                {
                    submissionStructList= await context.GetWithZSetCache<SubmissionStruct, SubmissionListQueryStruct>
                    (
                        async (submissionListQueryStruct) =>
                        {
                            return await _dbSubmissionContext.GetSubmissionList(submissionListQueryStruct);
                        },
                        (submissionStruct) =>
                        {
                            return submissionStruct.SubmitTime;
                        },
                        submissionListQueryStruct,
                        "problem_" + submissionListQueryStruct.ProblemID + "_submission",
                        submissionListQueryStruct.Cursor,
                        submissionListQueryStruct.Limit
                    );
                }
            }
            else if(submissionListQueryStruct.ProblemID>=1)
            {
                if (submissionListQueryStruct.NewSubmissionFirst)
                {
                    submissionStructList= await context.GetWithZSetCache<SubmissionStruct, SubmissionListQueryStruct>
                    (
                        async (submissionListQueryStruct) =>
                        {
                            return await _dbSubmissionContext.GetSubmissionList(submissionListQueryStruct);
                        },
                        (submissionStruct) =>
                        {
                            return submissionStruct.SubmitTime;
                        },
                        submissionListQueryStruct,
                        "problem_" + submissionListQueryStruct.ProblemID + "_submission",
                        submissionListQueryStruct.Cursor,
                        submissionListQueryStruct.Limit,
                        submissionListQueryStruct.MaxStoredTimestamp,
                        Conv.CommonConv.DateTime2Unix(DateTime.Now)
                    );
                }
                else
                {
                    submissionStructList= await context.GetWithZSetCache<SubmissionStruct, SubmissionListQueryStruct>
                    (
                        async (submissionListQueryStruct) =>
                        {
                            return await _dbSubmissionContext.GetSubmissionList(submissionListQueryStruct);
                        },
                        (submissionStruct) =>
                        {
                            return submissionStruct.SubmitTime;
                        },
                        submissionListQueryStruct,
                        "problem_" + submissionListQueryStruct.ContestID + "_submission",
                        submissionListQueryStruct.Cursor,
                        submissionListQueryStruct.Limit
                    );
                }
            }
            else
            {
                Logger.Error("查询提交列表出错, problemID: {0}, contestID: {1}.", submissionListQueryStruct.ProblemID, submissionListQueryStruct.ContestID);
                throw new Exception("查询提交列表出错");
            }
        }
        return submissionStructList ?? new();
    }

    public Dictionary<long, int> GetSubmissionResult(long userID, List<long> problemID, int ac_staus)
    {
        throw new NotImplementedException();
    }

    public async Task<List<SubmissionStruct>> MulGetSubmissionStruct(List<long> submissionIDList, bool isGetDetail)
    {
        List<SubmissionStruct> submissionStructList = new();
        var context = RedisContext.Context;
        if (context != null)
        {
            foreach (long submissionID in submissionIDList)
            {
                SubmissionStruct? submissionStruct = null;
                if (!isGetDetail)
                {
                    submissionStruct = await context.GetWithCacheKey<SubmissionStruct, long>
                    (
                        async (id) =>
                        {
                            var submissionStructs = await _dbSubmissionContext.MulGetSubmissionStruct(new List<long> { id }, false);
                            return submissionStructs == null ? null : submissionStructs.FirstOrDefault();
                        },
                        submissionID,
                        "submission_" + submissionID.ToString()
                    );
                }
                else
                {
                    submissionStruct = await context.GetWithCacheKey<SubmissionStruct, long>
                    (
                       async (id) =>
                        {
                            var submissionStructs = await _dbSubmissionContext.MulGetSubmissionStruct(new List<long> { id }, true);
                            return submissionStructs == null ? null : submissionStructs.FirstOrDefault();
                        },
                        submissionID,
                        "submission_detail_" + submissionID.ToString()
                    );
                }
                if (submissionStruct != null)
                {
                    submissionStructList.Add(submissionStruct);
                }
            }
        }
        return submissionStructList;
    }

    public async Task<long> SaveSubmissionStruct(SubmissionStruct submissionStruct)
    {
        return await _dbSubmissionContext.SaveSubmissionStruct(submissionStruct);
    }
}