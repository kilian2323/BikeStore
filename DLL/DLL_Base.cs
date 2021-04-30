using Core.Classes;
using Core.Definitions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace DLL
{
    public class DLL_Base
    {
        public string connectionString = @"Data Source=.;Initial Catalog=BikeStores;Integrated Security=True;";
        public SqlConnection cnn;

        public string GetFilterString(Search search, string tableReference)
        {
            bool combineAnd = search.combineAnd;
            bool exactMatch = search.exactMatch;
            List<string> searchStrings = new List<string>();
            List<string> searchColumns = new List<string>();
            foreach(Tuple<string,string> t in search.ColumnsAndSearchStrings)
            {
                foreach (ColumnBase cb in Tables.GetTableFromAlias(search.tableAlias).columns)
                {
                    if (t.Item1.Equals(cb.NameInViews))
                    {
                        searchColumns.Add(cb.NameInTable);
                        break;
                    }
                }
                searchStrings.Add(t.Item2);
            }
            if (searchColumns.Count < 3)
            {
                Debug.WriteLine("Some of the search columns could not be found in the table");
                return "";
            }
            string filterString = " WHERE ";
            string filterCombine = " OR ";
            string match = " LIKE ";
            if (combineAnd)
            {
                filterCombine = " AND ";
            }
            if (exactMatch)
            {
                match = " = ";
            }

            if (!string.IsNullOrEmpty(searchStrings[0]))
            {
                filterString += (tableReference + ".");
                filterString += ("[" + searchColumns[0] + "]");                
                filterString += match;
                filterString += "'";
                if (!exactMatch)
                {
                    filterString += "%";
                }
                filterString += searchStrings[0];
                if (!exactMatch)
                {
                    filterString += "%";
                }
                filterString += "'";
            }

            if (!string.IsNullOrEmpty(searchStrings[1]))
            {
                if (!string.IsNullOrEmpty(searchStrings[0]))
                {
                    filterString += filterCombine;
                }
                filterString += (tableReference + ".");
                filterString += ("[" + searchColumns[1] + "]");
                filterString += match;
                filterString += "'";
                if (!exactMatch)
                {
                    filterString += "%";
                }
                filterString += searchStrings[1];
                if (!exactMatch)
                {
                    filterString += "%";
                }
                filterString += "'";
            }

            if (!string.IsNullOrEmpty(searchStrings[2]))
            {
                if (!string.IsNullOrEmpty(searchStrings[0]) || !string.IsNullOrEmpty(searchStrings[1]))
                {
                    filterString += filterCombine;
                }
                filterString += (tableReference + ".");
                filterString += ("[" + searchColumns[2] + "]");
                filterString += match;
                filterString += "'";
                if (!exactMatch)
                {
                    filterString += "%";
                }
                filterString += searchStrings[2];
                if (!exactMatch)
                {
                    filterString += "%";
                }
                filterString += "'";
            }
            return filterString;
        }        
    }
}
