namespace CUGOJ.Base.Dao;


public interface IUserContext
{
    Task<List<UserStruct>> MulGetUserStruct(List<long> userIDList, bool isGetDetail);
    Task<long> SaveUserStruct(UserStruct userStruct);
}

public interface IProblemContext
{
    Task<List<ProblemStruct>> MulGetProblemStruct(List<long> problemIDList, bool isGetDetail);
    Task<long> SaveProblemStruct(ProblemStruct problemStruct);
    /// <summary>
    /// 查询某一页的题单
    /// </summary>
    /// <param name="cursor">偏移量</param>
    /// <param name="limit">页大小</param>
    Task<List<ProblemStruct>> GetProblemList(long cursor, long limit);
}

public interface IContestContext
{
    Task<List<ContestStruct>> MulGetContestStruct(List<long> contestIDList, bool isGetDetail);
    Task<long> SaveContestStruct(ContestStruct contestStruct);
    Task<List<ContestStruct>> GetContestList(long cursor, long limit);
}

public interface ISubmissionContext
{
    Task<List<SubmissionStruct>> MulGetSubmissionStruct(List<long> submissionIDList, bool isGetDetail);
    Task<long> SaveSubmissionStruct(SubmissionStruct submissionStruct);
    Task<List<SubmissionStruct>> GetSubmissionList(SubmissionListQueryStruct submissionListQueryStruct);
}

public interface IRegisterContext
{
    Task<List<RegisterStruct>> MulGetRegisterStruct(List<long> registerIDList, bool isGetDetail);
    Task<long> SaveRegisterStruct(RegisterStruct registerStruct);
    Task<List<RegisterStruct>> GetRegisterList(PagingQueryStruct pagingQueryStruct);
}