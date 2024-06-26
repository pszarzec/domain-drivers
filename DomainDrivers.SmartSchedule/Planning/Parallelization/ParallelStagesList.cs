﻿using DomainDrivers.SmartSchedule.Shared;

namespace DomainDrivers.SmartSchedule.Planning.Parallelization;

public record ParallelStagesList(IList<ParallelStages> All)
{
    public static ParallelStagesList Empty()
    {
        return new ParallelStagesList(new List<ParallelStages>());
    }

    public string Print()
    {
        return string.Join(" | ", All.Select(parallelStages => parallelStages.Print()));
    }

    public ParallelStagesList Add(ParallelStages newParallelStages)
    {
        var result = new List<ParallelStages>(All) { newParallelStages };
        return new ParallelStagesList(result);
    }

    public virtual bool Equals(ParallelStagesList? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return All.SequenceEqual(other.All);
    }

    public override int GetHashCode()
    {
        return All.CalculateHashCode();
    }
}