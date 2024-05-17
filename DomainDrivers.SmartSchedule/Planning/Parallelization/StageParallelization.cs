namespace DomainDrivers.SmartSchedule.Planning.Parallelization;

public class StageParallelization
{
    public ParallelStagesList Of(ISet<Stage> stages)
    {
        var parallelStages = ParallelStagesList.Empty();
        var groupedByWeights = stages.GroupBy(s => s.Weight).ToList();

        if (groupedByWeights.All(x => x.Key != 1))
        {
            return parallelStages;
        }
        
        return groupedByWeights.Aggregate(parallelStages, (result, group) =>
            result.Add(CreateParallelStages(group)));
    }

    private static ParallelStages CreateParallelStages(IGrouping<int, Stage> group) =>
        new(group.Select(x => x).ToHashSet());
}