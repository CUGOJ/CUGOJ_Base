using CUGOJ.RPC.Gen.Services.Base;
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
        throw new NotImplementedException();
    }

    public virtual async Task<MulGetProblemInfoResponse> MulGetProblemInfo(MulGetProblemInfoRequest req, CancellationToken cancellationToken = default)
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