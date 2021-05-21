using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Classes
{
    /**
     * Base class for all column classes
     */

    public class ColumnBase
    {

        public string NameInTable { get; set; }

        public string NameInProperties { get; set; }

        public string NameInViews { get; set; }

        public Type TableDataType { get; set; } // C# primitive data type of the column content in the SQL table

        public bool IsVisible { get; set; }

        public bool IsRetrievable { get; set; }  // whether it is allowed to retrieve this column from the table

        public int ViewWidth { get; set; }

        public int MaxChar { get; set; } = 0; // only used if data type is string

        public ColumnBase(string nit, string nip, string niv, Type dt)
        {
            NameInTable = nit;
            NameInProperties = nip;
            NameInViews = niv;
            TableDataType = dt;
        }
    }
}
