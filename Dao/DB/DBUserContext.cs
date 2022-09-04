namespace CUGOJ.Base.Dao.DB;

public class DBUserContext : IDBUserContext
{
    public virtual async Task<List<UserStruct>> MulGetUserStruct(List<long> UserIDList, bool IsGetDetail)
    {
        throw new NotImplementedException();
    }
}