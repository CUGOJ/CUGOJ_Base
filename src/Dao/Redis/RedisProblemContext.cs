using CUGOJ.Base.Dao.DB;
using CUGOJ.CUGOJ_Tools.Redis;
namespace CUGOJ.Base.Dao.Redis;
public class RedisProblemContext : IProblemContext
{
    private DBProblemContext _dbProblemContext;
    public RedisProblemContext()
    {
        _dbProblemContext = TraceFactory.CreateTracableObject<DBProblemContext>(true, true);
    }

    public virtual async Task<List<ProblemStruct>> MulGetProblemStruct(List<long> problemIDList, bool isGetDetail)
    {
        List<ProblemStruct> problemList = new();
        var context = RedisContext.Context;
        if (context != null)
        {
            foreach (long problemID in problemIDList)
            {
                ProblemStruct? problem = null;
                if (!isGetDetail)
                {
                    problem = await context.GetWithCacheKey<ProblemStruct, long>
                    (
                        async (id) =>
                        {
                            var problemStructs = await _dbProblemContext.MulGetProblemStruct(new List<long> { id }, false);
                            return problemStructs == null ? null : problemStructs.FirstOrDefault();
                        },
                        problemID,
                        "problem_" + problemID.ToString()
                    );
                }
                else
                {
                    problem = await context.GetWithCacheKey<ProblemStruct, long>
                    (
                        async (id) =>
                        {
                            var problemStructs = await _dbProblemContext.MulGetProblemStruct(new List<long> { id }, true);
                            return problemStructs == null ? null : problemStructs.FirstOrDefault();
                        },
                        problemID,
                        "problem_detail_" + problemID.ToString()
                    );
                }
                if (problem != null)
                {
                    problemList.Add(problem);
                }
            }
        }
        problemList.Sort((a, b) => a.ID.CompareTo(b.ID));
        return problemList;
    }

    public virtual async Task<long> SaveProblemStruct(ProblemStruct problemStruct)
    {
        return await _dbProblemContext.SaveProblemStruct(problemStruct);
    }

    public virtual async Task<List<ProblemStruct>> GetProblemList(long cursor, long limit)
    {
        long pageNum = cursor / limit;
        var context = RedisContext.Context;
        List<ProblemStruct>? problemList = null;
        if (context != null)
        {
            problemList = await context.GetWithCacheKey<List<ProblemStruct>, PagingQueryStruct>
            (
                async (pagingQueryStruct) =>
                {
                    return await _dbProblemContext.GetProblemList(pagingQueryStruct.Cursor, pagingQueryStruct.Limit);
                },
                new PagingQueryStruct { Cursor = cursor, Limit = limit },
                "problem_list_" + pageNum.ToString(),
                pageNum switch
                {
                    <= 3 => 5,
                    <= 10 => 30,
                    _ => 60
                }
            );
        }
        return problemList ?? new();
    }
}