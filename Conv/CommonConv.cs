namespace CUGOJ.Base.Conv;

public static class CommonConv
{
    public static long DateTime2Long(DateTime time)
    {
        return ((long)(time - DateTime.UnixEpoch).TotalSeconds);
    }

    public static long ULong2Long(ulong? value)
    {
        if (value == null) return 0;
        return ((long)value);
    }

    public static List<ulong> LongList2ULongList(List<long> value)
    {
        List<ulong> res = new List<ulong>();
        foreach (long i in value)
        {
            res.Add((ulong)i);
        }
        return res;
    }
}