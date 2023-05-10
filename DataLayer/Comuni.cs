using System;
using System.Collections.Generic;

namespace DataLayer;

public partial class Comuni
{
    public string? Comune { get; set; }

    public string? Regione { get; set; }

    public string? Provincia { get; set; }

    public string? Sigla { get; set; }

    public string Code { get; set; } = null!;
}
