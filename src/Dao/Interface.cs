namespace CUGOJ.Base.Dao;

public interface IUserContext
{
    Task<List<UserStruct>> MulGetUserStruct(List<long> UserIDList, bool IsGetDetail);
}

public interface IProblemContext
{
    Task<List<ProblemStruct>> MulGetProblemStruct(List<long> ProblemIDList, bool IsGetDetail);
    Task<long> SaveProblemStruct(ProblemStruct problemStruct);
}

public interface IContestContext
{
    Task<List<ContestStruct>> MulGetContestStruct(List<long> ContestIDList, bool IsGetDetail);
    Task<long> SaveContestStruct(ContestStruct contestStruct);
}