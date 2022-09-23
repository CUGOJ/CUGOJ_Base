using CUGOJ.Base.Dao.DB.Models;
using Microsoft.EntityFrameworkCore;
namespace CUGOJ.Base.Dao.DB;

public class DBContestContext : IContestContext
{
    public virtual async Task<List<ContestStruct>> GetContestList(PagingQueryStruct pagingQueryStruct)
    {
        using var context = DBContext.Context;
        List<ContestBase> contestList = await (from c in context.ContestBases
                                               orderby c.Id
                                               select c).Skip((int)pagingQueryStruct.Cursor).Take((int)pagingQueryStruct.Limit).ToListAsync();
        if (contestList == null)
        {
            Logger.Error("获取比赛列表出错, cursor: {0}, limit: {1}", pagingQueryStruct.Cursor, pagingQueryStruct.Limit);
            throw new Exception("获取比赛列表出错");
        }
        List<ContestStruct> res = new List<ContestStruct>();
        foreach (var contest in contestList)
        {
            res.Add(Conv.Conv.ContestConv.ContestPo2ContestStruct(contest));
        }
        return res;
    }

    public virtual async Task<List<ContestStruct>> MulGetContestStruct(List<long> contestIDList, bool isGetDetail)
    {
        using var context = DBContext.Context;
        List<ContestBase>? contestBases = null;
        List<ContestContent>? contestContents = null;
        List<User>? users = null;
        List<Organization>? organizations = null;
        if (isGetDetail)
        {
            contestBases = await (from b in context.ContestBases
                                  where contestIDList.Contains(b.Id)
                                  orderby b.Id
                                  select b).ToListAsync();
            contestContents = await (from b in context.ContestContents
                                     where contestIDList.Contains(b.ContestId)
                                     orderby b.ContestId
                                     select b).ToListAsync();
        }
        else
        {
            contestBases = await (from b in context.ContestBases
                                  where contestIDList.Contains(b.Id)
                                  orderby b.Id
                                  select new ContestBase
                                  {
                                      Id = b.Id,
                                      Title = b.Title,
                                      Profile = b.Profile,
                                      Writers = b.Writers,
                                      StartTime = b.StartTime,
                                      EndTime = b.EndTime,
                                      Type = b.Type,
                                      Status = b.Status
                                  }).ToListAsync();
        }
        if (contestBases == null)
        {
            Logger.Error("查找比赛出错, 无法获取比赛基本信息,ContestIDLis: {0}", contestIDList);
            throw new Exception("查找比赛出错");
        }
        if ((contestContents != null && contestContents.Count != contestBases.Count)
        || (users != null && users.Count != contestBases.Count)
        || (organizations != null && organizations.Count != contestBases.Count))
        {
            Logger.Error("查找比赛出错, 详细信息与基本信息不匹配,ContestIDLis: {0},base: {1},contents, {2},users: {3},organizations: {4}", contestIDList, contestBases, contestContents, users, organizations);
            throw new Exception("查找比赛出错");
        }
        List<ContestStruct> contestStructs = new List<ContestStruct>();
        for (int i = 0; i < contestBases.Count; i++)
        {
            ContestStruct contestStruct = Conv.Conv.ContestConv.ContestPo2ContestStruct(contestBases[i], contestContents?[i], organizations?[i], users?[i]);
            contestStructs.Add(contestStruct);
        }
        return contestStructs;
    }

    public virtual async Task<long> SaveContestStruct(ContestStruct contestStruct)
    {
        using var context = DBContext.Context;
        using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            ContestBase contestBase = Conv.Conv.ContestConv.ContestStruct2BasePo(contestStruct);
            context.Entry<ContestBase>(contestBase).Property(p=>p.CreateTime).IsModified = false;
            context.Entry<ContestBase>(contestBase).Property(p=>p.UpdateTime).IsModified = false;
            context.ContestBases.Update(contestBase);
            await context.SaveChangesAsync();
            if (contestStruct.Content != null && contestStruct.Content != string.Empty)
            {
                contestStruct.ID = contestBase.Id;
                context.ContestContents.Update(Conv.Conv.ContestConv.ContestStruct2ContentPo(contestStruct,await (from c in context.ContestContents where c.ContestId==contestBase.Id select c).FirstOrDefaultAsync()));
                await context.SaveChangesAsync();
            }
            await transaction.CommitAsync();
            return contestBase.Id;
        }
        catch(Exception e)
        {
            Logger.Error("SaveContestStruct出错, {0}.", e);
            throw new Exception("SaveContestStruct出错");
        }
    }
}