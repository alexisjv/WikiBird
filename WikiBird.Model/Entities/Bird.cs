using System;
using System.Collections.Generic;

namespace WikiBird.Model.Entities;

public partial class Bird
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Image { get; set; } = null!;
}
