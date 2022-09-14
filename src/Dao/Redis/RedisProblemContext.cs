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

    public virtual async Task<List<ProblemStruct>> MulGetProblemStruct(List<long> ProblemIDList, bool isGetDetail)
    {
        List<ProblemStruct> problemList = new();
        List<long> missIDList = new();
        foreach (long problemID in ProblemIDList)
        {
            ProblemStruct? problemStruct;
            if (!isGetDetail)
            {
                problemStruct = await RedisContext.Context?.Get<ProblemStruct>("problem_" + problemID.ToString());
            }
            else
            {
                problemStruct = await RedisContext.Context?.Get<ProblemStruct>("problem_detail_" + problemID.ToString());
            }
            if (problemStruct == null)
            {
                missIDList.Add(problemID);
            }
            else
            {
                problemList.Add(problemStruct);
            }
        }
        List<ProblemStruct> missProblemList = await _dbProblemContext.MulGetProblemStruct(missIDList, isGetDetail);
        foreach (ProblemStruct problemStruct in missProblemList)
        {
            if (!isGetDetail)
            {
                RedisContext.Context?.Set("problem_" + problemStruct.ID.ToString(), problemStruct, TimeSpan.FromSeconds(30));
            }
            else
            {
                RedisContext.Context?.Set("problem_detail_" + problemStruct.ID.ToString(), problemStruct, TimeSpan.FromSeconds(5));
            }
        }
        problemList.AddRange(missProblemList);
        problemList.Sort((a, b) => a.ID.CompareTo(b.ID));
        return problemList;
    }

    public virtual async Task<long> SaveProblemStruct(ProblemStruct problemStruct)
    {
        return await _dbProblemContext.SaveProblemStruct(problemStruct);
    }

}