using System;
using System.Collections.Generic;

namespace CompCompany1.Models;

public partial class Gpu
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Price { get; set; }

    public string ProducingСountry { get; set; } = null!;
}
