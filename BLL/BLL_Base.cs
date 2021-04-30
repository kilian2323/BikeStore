using Core.Classes;
using Core.Models;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace BLL
{
    public class BLL_Base
    {
        /* To be called from GUI level */
        public List<string> GetTableColumns(string tableAlias)
        {
            List<string> searchableColumns = new List<string>();
            Table t = Core.Definitions.Tables.GetTableFromAlias(tableAlias);
            List<ColumnBase> allColumns = t.columns;
            Debug.WriteLine("Table has " + allColumns.Count + " columns");
            foreach (ColumnBase b in allColumns)
            {                
                if (b.IsRetrievable && b.IsVisible)
                {
                    Debug.WriteLine("Adding displayable column "+ b.NameInViews);
                    searchableColumns.Add(b.NameInViews);
                }
            }
            return searchableColumns;
        }
    }
}
