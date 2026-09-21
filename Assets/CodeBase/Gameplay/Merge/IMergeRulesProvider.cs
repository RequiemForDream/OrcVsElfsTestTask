using CodeBase.Gameplay.Allies;

namespace CodeBase.Gameplay.Merge
{
    public interface IMergeRulesProvider
    {
        bool HasNextTier(AllyType type);
        AllyType GetNextTier(AllyType type);
    }
}