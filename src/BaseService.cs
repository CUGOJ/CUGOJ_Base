using CUGOJ.RPC.Gen.Services.Base;
using CUGOJ.Base.Dao;
namespace CUGOJ.BaseService;

public class BaseServiceHandler : CUGOJ.RPC.Gen.Services.Base.BaseService.IAsync
{
    public virtual Task<PingResponse> Ping(PingRequest req, CancellationToken token)
    {
        PingResponse resp = new PingResponse(CommonTools.UnixMili());
        resp.BaseResp = RPCTools.SuccessBaseResp();
        return Task.FromResult(resp);
    }

    public virtual async Task<MulGetUserInfoResponse> MulGetUserInfo(MulGetUserInfoRequest req, CancellationToken token)
    {
        throw new NotImplementedException();
    }
    public virtual async Task<SaveUserInfoResponse> SaveUserInfo(SaveUserInfoRequest req, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    public virtual async Task<LoginResponse> Login(LoginRequest req, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    public virtual async Task<SaveProblemInfoResponse> SaveProblemInfo(SaveProblemInfoRequest req, CancellationToken cancellationToken = default)
    {
        SaveProblemInfoResponse resp = new SaveProblemInfoResponse();
        try
        {
            if (CUGOJ.Base.Dao.DaoContext.ProblemContext == null)
                throw new Exception("数据库连接失败");
            long problemID = await DaoContext.ProblemContext.SaveProblemStruct(req.Problem);
            resp.ProblemID = problemID;
            resp.BaseResp = RPCTools.SuccessBaseResp();
        }
        catch (Exception e)
        {
            Logger.Error("保存题目信息出错, {0}", e);
            resp.BaseResp = RPCTools.ErrorBaseResp();
        }
        return resp;
    }

    public virtual async Task<MulGetProblemInfoResponse> MulGetProblemInfo(MulGetProblemInfoRequest req, CancellationToken cancellationToken = default)
    {
        MulGetProblemInfoResponse resp = new MulGetProblemInfoResponse();
        try
        {
            if (DaoContext.ProblemContext == null)
                throw new Exception("数据库连接失败");
            bool isGetDetail = req.__isset.IsGetProblemContent && req.IsGetProblemContent;
            resp.ProblemList = await DaoContext.ProblemContext.MulGetProblemStruct(req.ProblemIDList, isGetDetail);
            resp.BaseResp = RPCTools.SuccessBaseResp();
        }
        catch (Exception e)
        {
            Logger.Error("MulGetProblemInfo出错, {0}", e);
            resp.BaseResp = RPCTools.ErrorBaseResp(e);
        }
        return resp;
    }

    public virtual async Task<GetContestProblemResponse> GetContestProblem(GetContestProblemRequest req, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    public virtual async Task<GetProblemListResponse> GetProblemList(GetProblemListRequest req, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    public virtual async Task<SaveContestInfoResponse> SaveContestInfo(SaveContestInfoRequest req, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    public virtual async Task<MulGetContestInfoResponse> MulGetContestInfo(MulGetContestInfoRequest req, CancellationToken cancellationToken = default)

    {
        throw new NotImplementedException();
    }
    public virtual async Task<GetContestListResponse> GetContestList(GetContestListRequest req, CancellationToken cancellationToken = default)

    {
        throw new NotImplementedException();
    }
}