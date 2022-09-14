
using CUGOJ.Base.Dao.DB;
using CUGOJ.CUGOJ_Tools.Redis;

namespace CUGOJ.Base.Dao.Redis;
public class RedisContestContext : IContestContext
{
    private DBContestContext _dbContestContext;
    public RedisContestContext()
    {
        _dbContestContext = TraceFactory.CreateTracableObject<DBContestContext>(true, true);
    }
    public async Task<List<ContestStruct>> GetContestList(long cursor, long limit)
    {
        long pageNum = cursor / limit;
        var context = RedisContext.Context;
        List<ContestStruct>? contestList = null;
        if (context != null)
        {
            contestList = await context.GetWithCache<List<ContestStruct>, PagingQueryStruct>(
                async (pagingQueryStruct) =>
                {
                    return await _dbContestContext.GetContestList(pagingQueryStruct.Cursor, pagingQueryStruct.Limit);
                },
                new PagingQueryStruct { Cursor = cursor, Limit = limit },
                "contest_list_" + pageNum.ToString(),
                pageNum switch
                {
                    <= 3 => 5,
                    <= 10 => 30,
                    _ => 60
                }
            );
        }
        return contestList ?? new();
    }

    public async Task<List<ContestStruct>> MulGetContestStruct(List<long> contestIDList, bool isGetDetail)
    {
        List<ContestStruct>? contestList = new();
        var context = RedisContext.Context;
        if (context != null)
        {
            foreach (long contestID in contestIDList)
            {
                ContestStruct? contest = null;
                if (!isGetDetail)
                {
                    contest = await context.GetWithCache<ContestStruct, long>(
                        async (id) =>
                        {
                            var contestStructs = await _dbContestContext.MulGetContestStruct(new List<long> { id }, false);
                            return contestStructs == null ? null : contestStructs[0];
                        },
                        contestID,
                        "contest_" + contestID.ToString()
                    );
                }
                else
                {
                    contest = await context.GetWithCache<ContestStruct, long>(
                        async (id) =>
                        {
                            var res = await _dbContestContext.MulGetContestStruct(new List<long> { id }, true);
                            return res == null ? null : res[0];
                        },
                        contestID,
                        "contest_detail_" + contestID.ToString()
                    );
                }
                if (contest != null)
                {
                    contestList.Add(contest);
                }
            }
        }
        contestList.Sort((a, b) => a.ID.CompareTo(b.ID));
        return contestList;
    }

    public async Task<long> SaveContestStruct(ContestStruct contestStruct)
    {
        return await _dbContestContext.SaveContestStruct(contestStruct);
    }

}