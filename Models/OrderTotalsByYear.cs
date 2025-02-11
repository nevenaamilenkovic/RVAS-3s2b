﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tsql3s2b.Models;

[Keyless]
public partial class OrderTotalsByYear
{
    [Column("orderyear")]
    public int? Orderyear { get; set; }

    [Column("qty")]
    public int? Qty { get; set; }
}
