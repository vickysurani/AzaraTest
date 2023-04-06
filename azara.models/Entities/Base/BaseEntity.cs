﻿namespace azara.models.Entities.Base;

public class BaseEntity : BaseIdEntity
{
    [Required]
    public bool Active { get; set; }

    [Required]
    public bool Deleted { get; set; }

    [Required]
    public DateTime Created { get; set; }

    [Required]
    public DateTime Modified { get; set; }

    public BaseEntity()
    {
        Active = true;
        Deleted = false;
        Created = DateTime.UtcNow;
        Modified = DateTime.UtcNow;
    }
}