namespace CUGOJ.Base.Dao;
public struct PagingQueryStruct
{
    public long Cursor { get; set; }
    public long Limit { get; set; }
}

/// <summary>
/// NewSubmissionFirst==true，根据MaxStoredTimestamp查。
/// </summary>
public struct SubmissionListQueryStruct
{
    public long ProblemID { get; set; }
    public long Cursor { get; set; }
    public long Limit { get; set; }
    public long MaxStoredTimestamp { get; set; }
    public bool NewSubmissionFirst { get; set; }
    public long ContestID { get; set; }
}