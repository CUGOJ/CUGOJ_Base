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
        MulGetUserInfoResponse resp = new();
        try
        {
            if (DaoContext.UserContext == null)
            {
                throw new Exception("数据库连接失败");
            }
            bool isGetDetail =  req.__isset.IsGetUserDetail && req.IsGetUserDetail;
            resp.UserList = await DaoContext.UserContext.MulGetUserStruct(req.UserIDList, isGetDetail);
            resp.BaseResp = RPCTools.SuccessBaseResp();
        }
        catch (Exception e)
        {
            Logger.Error($"MulGetUserInfo出错\n{e}");
            resp.BaseResp = RPCTools.ErrorBaseResp(e);
        }
        return resp;
    }
    public virtual async Task<SaveUserInfoResponse> SaveUserInfo(SaveUserInfoRequest req, CancellationToken cancellationToken = default)
    {
        SaveUserInfoResponse resp = new();
        try
        {
            if (DaoContext.UserContext == null)
            {
                throw new Exception("数据库连接失败");
            }
            long userID = await DaoContext.UserContext.SaveUserStruct(req.User);
            resp.UserID = userID;
            resp.BaseResp = RPCTools.SuccessBaseResp();
        }
        catch (Exception e)
        {
            Logger.Error($"保存用户信息出错\n{e}");
            resp.BaseResp = RPCTools.ErrorBaseResp(e);
        }
        return resp;
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
            if (DaoContext.ProblemContext == null)
                throw new Exception("数据库连接失败");
            long problemID = await DaoContext.ProblemContext.SaveProblemStruct(req.Problem);
            resp.ProblemID = problemID;
            resp.BaseResp = RPCTools.SuccessBaseResp();
        }
        catch (Exception e)
        {
            Logger.Error("保存题目信息出错, {0}", e);
            resp.BaseResp = RPCTools.ErrorBaseResp(e);
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

    public virtual async Task<GetProblemListResponse> GetProblemList(GetProblemListRequest req, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    public virtual async Task<SaveContestInfoResponse> SaveContestInfo(SaveContestInfoRequest req, CancellationToken cancellationToken = default)
    {
        SaveContestInfoResponse resp = new();
        try
        {
            if (DaoContext.ContestContext == null)
                throw new Exception("数据库连接失败");
            long contestID = await DaoContext.ContestContext.SaveContestStruct(req.Contest);
            resp.ContestID = contestID;
            resp.BaseResp = RPCTools.SuccessBaseResp();
        }
        catch (Exception e)
        {
            Logger.Error("保存比赛信息出错, {0}", e);
            resp.BaseResp = RPCTools.ErrorBaseResp(e);
        }
        return resp;
    }
    public virtual async Task<MulGetContestInfoResponse> MulGetContestInfo(MulGetContestInfoRequest req, CancellationToken cancellationToken = default)
    {
        MulGetContestInfoResponse resp = new();
        try
        {
            if (DaoContext.ContestContext == null)
            {
                throw new Exception("数据库连接失败");
            }
            bool isGetDetail = req.__isset.IsGetContestContent && req.IsGetContestContent;
            resp.ContestList = await DaoContext.ContestContext.MulGetContestStruct(req.ContestIDList, isGetDetail);
            resp.BaseResp = RPCTools.SuccessBaseResp();
        }
        catch (Exception e)
        {
            Logger.Error($"MulGetContestInfo出错\n{e}");
            resp.BaseResp = RPCTools.ErrorBaseResp(e);
        }
        return resp;
    }
    public virtual async Task<GetContestListResponse> GetContestList(GetContestListRequest req, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    public virtual async Task<SaveSubmissionInfoResponse> SaveSubmissionInfo(SaveSubmissionInfoRequest req, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    public virtual async Task<GetSubmissionListResponse> GetSubmissionList(GetSubmissionListRequest req, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}