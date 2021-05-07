using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Core.Classes
{
    /* Contains all parameters required to perform a query */
    public class Search
    {
        public List<Tuple<string, string>> ColumnsAndSearchStrings = new List<Tuple<string, string>>();
        public bool combineAnd;
        public bool exactMatch;
        public string tableAlias;
        public bool filter;

        public Search(string _tableAlias, string colName1, string colName2, string colName3, string search1, string search2, string search3, bool _combineAnd, bool _exactMatch)
        {
            Tuple<string, string> tuple1 = new Tuple<string, string>(colName1, search1);
            Tuple<string, string> tuple2 = new Tuple<string, string>(colName2, search2);
            Tuple<string, string> tuple3 = new Tuple<string, string>(colName3, search3);
            ColumnsAndSearchStrings.Add(tuple1);
            ColumnsAndSearchStrings.Add(tuple2);
            ColumnsAndSearchStrings.Add(tuple3);
            tableAlias = _tableAlias;
            combineAnd = _combineAnd;
            exactMatch = _exactMatch;
            if (string.IsNullOrEmpty(search1) && string.IsNullOrEmpty(search2) && string.IsNullOrEmpty(search3))
            {                
                filter = false;
            }
            else
            {
                Debug.WriteLine("search1 empty: " + string.IsNullOrEmpty(search1));
                Debug.WriteLine("search2 empty: " + string.IsNullOrEmpty(search2));
                Debug.WriteLine("search3 empty: " + string.IsNullOrEmpty(search3));
                Debug.WriteLine("col1: " + colName1);
                Debug.WriteLine("col2: " + colName2);
                Debug.WriteLine("col3: " + colName3);
                filter = true;
            }
        }
    }
}
