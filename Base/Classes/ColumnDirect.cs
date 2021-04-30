using System;

namespace Core.Classes
{
    /**
     * Describes a column which directly corresponds to an SQL table column
     */

    public class ColumnDirect : ColumnBase
    {
        public ColumnDirect(string nit, string nip, string niv, Type dt) : base(nit, nip, niv, dt)
        {
        }
    }
}
