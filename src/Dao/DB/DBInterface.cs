namespace CUGOJ.Base.Dao.DB;

public interface IDBUserContext
{
    Task<List<UserStruct>> MulGetUserStruct(List<long> userIDList, bool isGetDetail);

    void MulSaveUserStruct(List<UserStruct> userStructList);
}

public interface IDBProblemContext
{
    Task<List<ProblemStruct>> MulGetProblemStruct(List<long> problemIDList, bool isGetDetail);
}