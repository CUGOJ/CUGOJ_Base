namespace CUGOJ.Base.Dao.DB;

public interface IDBUserContext
{
    Task<List<UserStruct>> MulGetUserStruct(List<long> UserIDList, bool IsGetDetail);
}

public interface IDBProblemContext
{
    Task<List<ProblemStruct>> MulGetProblemStruct(List<long> ProblemIDList, bool IsGetDetail);
}