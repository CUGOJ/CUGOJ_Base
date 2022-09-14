using CUGOJ.Base.Dao.DB.Models;
using Microsoft.EntityFrameworkCore;
namespace CUGOJ.Base.Dao.DB;

public class DBProblemContext : IProblemContext
{
    public virtual async Task<List<ProblemStruct>> MulGetProblemStruct(List<long> problemIDList, bool isGetDetail)
    {
        using var context = DBContext.Context;
        List<ProblemBase>? problemBases = null;
        List<ProblemContent>? problemContents = null;
        List<User>? users = null;
        List<ProblemSource>? sources = null;
        if (isGetDetail)
        {
            problemBases = await (from b in context.ProblemBases
                                  where problemIDList.Contains(b.Id)
                                  orderby b.Id
                                  select b).ToListAsync();
            problemContents = await (from b in context.ProblemContents
                                     where problemIDList.Contains(b.ProblemId)
                                     orderby b.ProblemId
                                     select b).ToListAsync();
        }
        else
        {
            problemBases = await (from b in context.ProblemBases
                                  where problemIDList.Contains(b.Id)
                                  orderby b.Id
                                  select new ProblemBase
                                  {
                                      Id = b.Id,
                                      Title = b.Title,
                                      ShowId = b.ShowId,
                                      SubmissionCount = b.SubmissionCount,
                                      AcceptedCount = b.AcceptedCount,
                                      Type = b.Type,
                                      Status = b.Status
                                  }).ToListAsync();
        }
        if (problemBases == null)
        {
            Logger.Error("查找题目出错, 无法获取题目基本信息,ProblemIDList: {0}", problemIDList);
            throw new Exception("查找题目出错");
        }
        if ((problemContents != null && problemContents.Count != problemBases.Count)
        || users != null && users.Count != problemBases.Count
        || sources != null && sources.Count != problemBases.Count)
        {
            Logger.Error("查找题目出错, 详细信息与基本信息不匹配,ProblemIDLis: {0},base: {1},contents, {2},users: {3},sources: {4}", problemIDList, problemBases, problemContents, users, sources);
            throw new Exception("查找题目出错");
        }
        List<ProblemStruct> res = new List<ProblemStruct>();
        for (int i = 0; i < problemBases.Count; i++)
        {
            res.Add(Conv.Conv.ProblemConv.ProblemPo2ProblemStruct(problemBases[i],
            problemContents != null ? problemContents[i] : null,
            users != null ? users[i] : null,
            sources != null ? sources[i] : null));
        }
        return res;
    }

    public virtual async Task<long> SaveProblemStruct(ProblemStruct problemStruct)
    {
        using var context = DBContext.Context;
        var problemBase = Conv.Conv.ProblemConv.ProblemStruct2BasePo(problemStruct);
        context.Update(problemBase);

        if (problemStruct.Content != null && problemStruct.Content != string.Empty)
        {
            problemStruct.ID = problemBase.Id;
            ProblemContent problemContent = Conv.Conv.ProblemConv.ProblemStruct2ContentPo(problemStruct);
            context.Update(problemContent);
        }
        await context.SaveChangesAsync();
        return problemBase.Id;
    }

    public virtual async Task<List<ProblemStruct>> GetProblemList(long cursor, long limit)
    {
        using var context = DBContext.Context;
        List<ProblemBase> problemBases = await (from b in context.ProblemBases
                                                orderby b.Id
                                                select b
                                                 ).Skip(((int)cursor)).Take((int)limit).ToListAsync();
        if (problemBases == null)
        {
            Logger.Error("获取题目列表出错, 无法获取题目基本信息, cursor: {0}, limit: {0}", cursor, limit);
            throw new Exception("获取题目列表出错");
        }

        List<ProblemStruct> res = new List<ProblemStruct>();
        foreach (var problemBase in problemBases)
        {
            res.Add(Conv.Conv.ProblemConv.ProblemPo2ProblemStruct(problemBase));
        }
        return res;
    }
}