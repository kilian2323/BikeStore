﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Classes
{
    /**
     * Describes a column which is derived from a JOIN of several SQL tables
     */

    public class ColumnDerived : ColumnBase
    {
        public ColumnDerived(string nit, string nip, string niv, Type dt) : base(nit, nip, niv, dt)
        {
        }
    }
}
