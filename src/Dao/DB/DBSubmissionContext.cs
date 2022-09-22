using CUGOJ.Base.Dao.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace CUGOJ.Base.Dao.DB;
public class DBSubmissionContext : ISubmissionContext
{
    public virtual async Task<List<SubmissionStruct>> GetSubmissionList(SubmissionListQueryStruct submissionListQueryStruct)
    {
        List<SubmissionBase>? submissionBaseList = null;
        using var context = DBContext.Context;
        if (submissionListQueryStruct.NewSubmissionFirst && submissionListQueryStruct.MaxStoredTimestamp <= 0)
        {
            Logger.Error("查询提交列表出错, 需要查询最新提交时给出的时间戳限制错误, MaxStoredTimestamp: {0}.", submissionListQueryStruct.MaxStoredTimestamp);
            throw new Exception("查询提交列表出错");
        }
        if (submissionListQueryStruct.ContestID >= 1)
        {
            if (submissionListQueryStruct.NewSubmissionFirst)
            {
                submissionBaseList = await (from s in context.SubmissionBases
                                            where s.ContestId == submissionListQueryStruct.ContestID
                                            && Conv.CommonConv.DateTime2Unix(s.SubmitTime) >= submissionListQueryStruct.MaxStoredTimestamp
                                            select s).Take((int)submissionListQueryStruct.Limit).ToListAsync();
            }
            else
            {
                submissionBaseList = await (from s in context.SubmissionBases
                                            where s.ContestId == submissionListQueryStruct.ContestID
                                            select s)
                                            .Skip((int)submissionListQueryStruct.Cursor)
                                            .Take((int)submissionListQueryStruct.Limit).ToListAsync();
            }
        }
        else if (submissionListQueryStruct.ProblemID >= 1)
        {
            if (submissionListQueryStruct.NewSubmissionFirst)
            {
                submissionBaseList = await (from s in context.SubmissionBases
                                            where s.ProblemId == submissionListQueryStruct.ProblemID
                                            && Conv.CommonConv.DateTime2Unix(s.SubmitTime) >= submissionListQueryStruct.MaxStoredTimestamp
                                            select s)
                                            .Take((int)submissionListQueryStruct.Limit).ToListAsync();
            }
            else
            {
                submissionBaseList = await (from s in context.SubmissionBases
                                            where s.ProblemId == submissionListQueryStruct.ProblemID
                                            select s)
                                            .Skip((int)submissionListQueryStruct.Cursor)
                                            .Take((int)submissionListQueryStruct.Limit).ToListAsync();
            }
        }
        else
        {
            Logger.Error("查询提交列表出错, problemID: {0}, contestID: {1}.", submissionListQueryStruct.ProblemID, submissionListQueryStruct.ContestID);
            throw new Exception("查询提交列表出错");
        }
        List<SubmissionStruct> res = new();
        foreach (SubmissionBase submissionBase in submissionBaseList)
        {
            res.Add(Conv.Conv.SubmmissionConv.SubmissionPo2SubmissionStruct(submissionBase));
        }
        return res;
    }

    public Dictionary<long, int> GetSubmissionResult(long userID, List<long> problemID, int ac_staus)
    {
        throw new NotImplementedException();
    }

    public virtual async Task<List<SubmissionStruct>> MulGetSubmissionStruct(List<long> submissionIDList, bool isGetDetail)
    {
        using var context = DBContext.Context;
        List<SubmissionBase>? submissionBases = null;
        List<SubmissionContent>? submissionContents = null;
        if (isGetDetail)
        {
            submissionBases = await (from s in context.SubmissionBases
                                     where submissionIDList.Contains(s.Id)
                                     orderby s.Id
                                     select s).ToListAsync();
            submissionContents = await (from s in context.SubmissionContents
                                        where submissionIDList.Contains(s.SubmissionId)
                                        orderby s.SubmissionId
                                        select s).ToListAsync();
        }
        else
        {
            submissionBases = await (from s in context.SubmissionBases
                                     where submissionIDList.Contains(s.Id)
                                     orderby s.Id
                                     select new SubmissionBase
                                     {
                                         Id = s.Id,
                                         ProblemId = s.ProblemId,
                                         SubmitTime = s.SubmitTime,
                                         Properties = s.Properties,
                                         Status = s.Status
                                     }).ToListAsync();
        }
        if (submissionBases == null)
        {
            Logger.Error("查找提交记录出错, 无法获取提交记录基本信息, SubmissionIDList: {0}.", submissionIDList);
            throw new Exception("查找提交记录出错");
        }
        if (submissionContents != null && submissionBases.Count != submissionContents.Count)
        {
            Logger.Error("查找提交记录出错, 详细信息与基本信息不匹配, SubmissionIDList: {0}, base: {1}, contents: {1}.", submissionIDList, submissionBases, submissionContents);
            throw new Exception("查找提交记录出错");
        }
        List<SubmissionStruct> res = new();
        for (int i = 0; i < submissionBases.Count; i++)
        {
            res.Add(Conv.Conv.SubmmissionConv.SubmissionPo2SubmissionStruct(submissionBases[i], submissionContents == null ? null : submissionContents[i]));
        }
        return res;
    }

    public virtual async Task<long> SaveSubmissionStruct(SubmissionStruct submissionStruct)
    {
        using var context = DBContext.Context;
        SubmissionBase submissionBase = Conv.Conv.SubmmissionConv.SubmissionStruct2SubmissionPo(submissionStruct);
        context.Update(submissionBase);
        if (submissionStruct.Content != null && submissionStruct.Content != string.Empty)
        {
            submissionStruct.ID = submissionBase.Id;
            SubmissionContent submissionContent = Conv.Conv.SubmmissionConv.SubmissionStruct2ContentPo(submissionStruct);
            context.Update(submissionContent);
        }
        await context.SaveChangesAsync();
        return submissionBase.Id;
    }
}