using Core.Classes;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;

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
            Type t = TableTypes.GetTypeFromAlias(search.tableAlias);
            Debug.WriteLine("Type is " + t.ToString());
            MemberInfo[] members = t.GetMembers();            
            foreach (Tuple<string,string> tp in search.ColumnsAndSearchStrings)
            {
                foreach (MemberInfo member in members)
                {
                    var memberAttributeDBName = member.GetCustomAttribute<ColumnDBNameAttribute>();
                    var memberAttributeName = member.GetCustomAttribute<ColumnViewNameAttribute>();
                    if (memberAttributeDBName != null && memberAttributeName != null)
                    {
                        if (tp.Item1.Equals(memberAttributeName.Name))
                        {
                            Debug.WriteLine("Member is " + member.ToString());
                            Debug.WriteLine("Adding attribute " + memberAttributeDBName.Name+ " as " + memberAttributeDBName.Name);
                            searchColumns.Add(memberAttributeDBName.Name);
                        }
                    }
                }
                searchStrings.Add(tp.Item2);                
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
