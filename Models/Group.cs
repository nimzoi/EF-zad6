using System;
using System.Collections.Generic;

namespace EfExample.Models;

public partial class Group
{
    public int IdGroup { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<StudentGroup> StudentGroups { get; set; } = new List<StudentGroup>();
}
