using CUGOJ.Base.Conv.Processor;
namespace CUGOJ.Base.Conv;

public static class Conv
{
    private static ProblemConv _problemConv = TraceFactory.CreateTracableObject<ProblemConv>(false, false);
    public static ProblemConv ProblemConv { get => _problemConv; }
    private static ContestConv _contestConv = TraceFactory.CreateTracableObject<ContestConv>(false, false);
    public static ContestConv ContestConv { get => _contestConv; }
    private static OrganizationConv _organizationConv = TraceFactory.CreateTracableObject<OrganizationConv>(false, false);
    public static OrganizationConv OrganizationConv { get => _organizationConv; }

}