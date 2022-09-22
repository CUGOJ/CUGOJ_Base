namespace CUGOJ.Base.Conv;

public static class CommonConv
{
    public static long DateTime2Unix(DateTime? time)
    {
        return time == null ? 0 : ((long)(time.Value - DateTime.UnixEpoch).TotalSeconds);
    }

    public static DateTime? Unix2DateTime(long time)
    {
        return time <= 0 ? null : DateTime.UnixEpoch.AddSeconds(time);
    }

    public static long ULong2Long(ulong? value)
    {
        if (value == null) return 0;
        return ((long)value);
    }

    public static ulong Long2ULong(long? value)
    {
        if (value == null) return 0;
        return ((ulong)value);
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