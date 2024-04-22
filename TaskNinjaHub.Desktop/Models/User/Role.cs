namespace TaskNinjaHub.Desktop.Models.User;

public class Role
{
    public virtual Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name for this role.
    /// </summary>
    public virtual string? Name { get; set; }

    /// <summary>
    /// Gets or sets the normalized name for this role.
    /// </summary>
    public virtual string? NormalizedName { get; set; }

    /// <summary>
    /// A random value that should change whenever a role is persisted to the store
    /// </summary>
    public virtual string? ConcurrencyStamp { get; set; }
}