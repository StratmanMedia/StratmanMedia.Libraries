namespace StratmanMedia.GuardClauses
{
    public class Guard : IGuardClause
    {
        public static IGuardClause Against { get; } = new Guard();

        private Guard() { }
    }
}