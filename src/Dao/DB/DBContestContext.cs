using CUGOJ.Base.Dao.DB.Models;
using CUGOJ.Base.Conv;
using Microsoft.EntityFrameworkCore;
namespace CUGOJ.Base.Dao.DB;

public class DBContestContext : IContestContext
{
    public virtual async Task<List<ContestStruct>> MulGetContestStruct(List<long> ContestIDList, bool IsGetDetail)
    {
        using var context = DBContext.Context;
        List<ulong> IDList = Conv.CommonConv.LongList2ULongList(ContestIDList);
        List<ContestBase>? contestBases = null;
        List<ContestContent>? contestContents = null;
        List<User>? users = null;
        List<Organization>? organizations = null;
        if (IsGetDetail)
        {
            contestBases = await (from b in context.ContestBases
                                  where IDList.Contains(b.Id)
                                  orderby b.Id
                                  select b).ToListAsync();
            contestContents = await (from b in context.ContestContents
                                     where IDList.Contains(b.ContestId)
                                     orderby b.ContestId
                                     select b).ToListAsync();
        }
        else
        {
            contestBases = await (from b in context.ContestBases
                                  where IDList.Contains(b.Id)
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
            Logger.Error("查找比赛出错, 无法获取比赛基本信息,ContestIDLis: {0}", ContestIDList);
            throw new Exception("查找比赛出错");
        }
        if ((contestContents != null && contestContents.Count != contestBases.Count)
        || (users != null && users.Count != contestBases.Count)
        || (organizations != null && organizations.Count != contestBases.Count))
        {
            Logger.Error("查找比赛出错, 详细信息与基本信息不匹配,ContestIDLis: {0},base: {1},contents, {2},users: {3},organizations: {4}", ContestIDList, contestBases, contestContents, users, organizations);
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
        ContestBase contestBase = Conv.Conv.ContestConv.ContestStruct2BasePo(contestStruct);
        context.ContestBases.Update(contestBase);
        if (contestStruct.Content != null && contestStruct.Content != string.Empty)
        {
            contestStruct.ID = CommonConv.ULong2Long(contestBase.Id);
            ContestContent contestContent = Conv.Conv.ContestConv.ContestStruct2ContentPo(contestStruct);
            context.ContestContents.Update(contestContent);
        }
        await context.SaveChangesAsync();
        return CommonConv.ULong2Long(contestBase.Id);
    }


}