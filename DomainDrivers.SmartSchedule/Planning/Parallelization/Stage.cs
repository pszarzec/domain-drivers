namespace DomainDrivers.SmartSchedule.Planning.Parallelization;

public record Stage(string StageName, ISet<Stage> Dependencies, ISet<ResourceName> Resources, TimeSpan Duration, int Weight)
{
    public int Weight { get; private set; } = Weight;
    public string Name => StageName;

    public Stage(string name) : this(name, new HashSet<Stage>(), new HashSet<ResourceName>(), TimeSpan.Zero, 1)
    {
    }

    public Stage DependsOn(Stage stage)
    {
        Weight += stage.Weight;
        
        Dependencies.Add(stage);
        return this;
    }

    /// <summary>
    /// Can be used in case of:
    /// - financing is ready for stage
    /// - rule of law that increase priority
    /// </summary>
    /// <returns></returns>
    public Stage IncreasePriority()
    {
        if (Weight > 1)
        {
            Weight -= 1;
        }
        
        return this;
    }

    /// <summary>
    /// Can be used in case of:
    /// - rule of law that decrease priority
    /// - logical rule that move this stage after other stages
    /// </summary>
    /// <returns></returns>
    public Stage DecreasePriority()
    {
        Weight += 1;
        return this;
    }

    public virtual bool Equals(Stage? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return StageName == other.StageName;
    }

    public override int GetHashCode()
    {
        return StageName.GetHashCode();
    }
}

public record ResourceName(string Name);