using CUGOJ.Base.Dao.DB.Models;
using Microsoft.EntityFrameworkCore;
namespace CUGOJ.Base.Dao.DB;

public class DBProblemContext : IDBProblemContext
{
    public virtual async Task<List<ProblemStruct>> MulGetProblemStruct(List<long> ProblemIDList, bool IsGetDetail)
    {
        List<ulong> IDList = Conv.CommonConv.LongList2ULongList(ProblemIDList);
        List<ProblemBase>? problemBases = null;
        List<ProblemContent>? problemContents = null;
        List<User>? users = null;
        List<ProblemSource>? sources = null;
        if (IsGetDetail)
        {
            problemBases = await (from b in DBContext.Context?.ProblemBases
                                  where IDList.Contains(b.Id)
                                  orderby b.Id
                                  select b).ToListAsync();
            problemContents = await (from b in DBContext.Context?.ProblemContents
                                     where IDList.Contains(b.ProblemId)
                                     orderby b.Id
                                     select b).ToListAsync();
        }
        else
        {
            problemBases = await (from b in DBContext.Context?.ProblemBases
                                  where IDList.Contains(b.Id)
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
            Logger.Error("查找题目出错, 无法获取题目基本信息,ProblemIDLis: {0}", ProblemIDList);
            throw new Exception("查找题目出错");
        }
        if ((problemContents != null && problemContents.Count != problemBases.Count)
        || users != null && users.Count != problemBases.Count
        || sources != null && sources.Count != problemBases.Count)
        {
            Logger.Error("查找题目出错, 详细信息与基本信息不匹配,ProblemIDLis: {0},base: {1},contents, {2},users: {3},sources: {4}", ProblemIDList, problemBases, problemContents, users, sources);
            throw new Exception("查找题目出错");
        }
        List<ProblemStruct> res = new List<ProblemStruct>();
        for (int i = 0; i < problemBases.Count; i++)
        {
            res.Add(Conv.ProblemConv.ProblemPo2ProblemStruct(problemBases[i],
            problemContents != null ? problemContents[i] : null,
            users != null ? users[i] : null,
            sources != null ? sources[i] : null));
        }
        return res;
    }
}