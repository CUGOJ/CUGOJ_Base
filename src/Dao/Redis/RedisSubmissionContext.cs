namespace CUGOJ.Base.Dao.Redis;
public class RedisSubmissionContext : ISubmissionContext
{
    public Task<List<SubmissionStruct>> GetSubmissionList(SubmissionListQueryStruct submissionListQueryStruct)
    {
        throw new NotImplementedException();
    }

    public Task<List<SubmissionStruct>> MulGetSubmissionStruct(List<long> submissionIDList, bool isGetDetail)
    {
        throw new NotImplementedException();
    }

    public Task<long> SaveSubmissionStruct(SubmissionStruct submissionStruct)
    {
        throw new NotImplementedException();
    }
}