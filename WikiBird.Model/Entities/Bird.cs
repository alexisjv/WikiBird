using System;
using System.Collections.Generic;

namespace WikiBird.Model.Entities;

public partial class Bird
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }
}
