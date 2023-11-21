using System;
using System.Collections.Generic;

namespace CompCompany1.Models;

public partial class Cpu
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Price { get; set; }

    public string ProducingCountry { get; set; } = null!;
}
