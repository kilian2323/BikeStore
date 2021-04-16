using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BikeStore
{
    class SqlQuery
    {
        private string connectionString = @"Data Source=.;Initial Catalog=BikeStores;Integrated Security=True;";
        private SqlCommand command;
        private SqlConnection cnn;

        private string search_1 = "";
        private string search_2 = "";
        private string search_3 = "";
        private string search_1_column = "";
        private string search_2_column = "";
        private string search_3_column = "";
        private List<string> columnNames = new List<string>();

        private string schemaName;
        private string tableName;
        private bool filter;
        private bool combineAnd;
        private bool exactMatch;

        public SqlQuery(string _schemaName, string _tableName, List<string> _columnNames, bool _filter, List<string> _searchFor, bool _combineAnd, bool _exactMatch)
        {
            try
            {
                if (_searchFor != null)
                {
                    search_1 = _searchFor[0];
                    search_2 = _searchFor[1];
                    search_3 = _searchFor[2];
                }
                if (_columnNames != null)
                {
                    search_1_column = _columnNames[0];
                    search_2_column = _columnNames[1];
                    search_3_column = _columnNames[2];
                }
                schemaName = _schemaName;
                tableName = _tableName;
                filter = _filter;
                combineAnd = _combineAnd;
                exactMatch = _exactMatch;
            }
            catch (NullReferenceException e)
            {

            }
        }

        public SqlQuery(string _schemaName, string _tableName) :
            this(_schemaName, _tableName, null, false, null, false, false) {}
        

        public List<string> GetTableColumns()
        {
            
            string columnQuery = @$"SELECT OBJECT_SCHEMA_NAME (c.object_id) SchemaName,
                                    o.Name AS Table_Name, 
                                    c.Name AS Field_Name,
                                    t.Name AS Data_Type,
                                    t.max_length AS Length_Size,
                                    t.precision AS Precision
                            FROM sys.columns c 
                                 INNER JOIN sys.objects o ON o.object_id = c.object_id
                                 LEFT JOIN  sys.types t on t.user_type_id  = c.user_type_id   
                            WHERE o.type = 'U'
                            AND o.Name = '{tableName}'
                            AND OBJECT_SCHEMA_NAME (c.object_id) = '{schemaName}';";

            //System.Diagnostics.Debug.Print(columnQuery);
            cnn = new SqlConnection(connectionString);

            using (cnn)
            {
                cnn.Open();

                command = new SqlCommand(columnQuery, cnn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string colName = (string)reader[2];
                    columnNames.Add(colName);
                    //System.Diagnostics.Debug.Print(colName);
                }

                cnn.Close();
            }
            return columnNames;
        }

        public DataTable Query()
        {
            // Get data in all columns from the table

            string baseQuery = $"SELECT * FROM [{schemaName}].[{tableName}] ";
            if (filter)
            {
                string filterQuery = "WHERE ";
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

                if (!string.IsNullOrEmpty(search_1))
                {
                    filterQuery += "[";
                    filterQuery += search_1_column;
                    filterQuery += "] ";
                    filterQuery += match;
                    filterQuery += "'";
                    if (!exactMatch)
                    {
                        filterQuery += "%";
                    }
                    filterQuery += search_1;
                    if (!exactMatch)
                    {
                        filterQuery += "%";
                    }
                    filterQuery += "'";
                }

                if (!string.IsNullOrEmpty(search_2))
                {
                    if (!string.IsNullOrEmpty(search_1))
                    {
                        filterQuery += filterCombine;
                    }
                    filterQuery += "[";
                    filterQuery += search_2_column;
                    filterQuery += "] ";
                    filterQuery += match;
                    filterQuery += "'";
                    if (!exactMatch)
                    {
                        filterQuery += "%";
                    }
                    filterQuery += search_2;
                    if (!exactMatch)
                    {
                        filterQuery += "%";
                    }
                    filterQuery += "'";
                }

                if (!string.IsNullOrEmpty(search_3))
                {
                    if (!string.IsNullOrEmpty(search_1) || !string.IsNullOrEmpty(search_2))
                    {
                        filterQuery += filterCombine;
                    }
                    filterQuery += "[";
                    filterQuery += search_3_column;
                    filterQuery += "] ";
                    filterQuery += match;
                    filterQuery += "'";
                    if (!exactMatch)
                    {
                        filterQuery += "%";
                    }
                    filterQuery += search_3;
                    if (!exactMatch)
                    {
                        filterQuery += "%";
                    }
                    filterQuery += "'";
                }
                baseQuery += filterQuery;
            }
            baseQuery += ";";
            System.Diagnostics.Debug.Print(baseQuery);

            System.Data.DataTable dt;
            cnn = new SqlConnection(connectionString);
            using (cnn)
            {
                cnn.Open();

                command = new SqlCommand(baseQuery, cnn);
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    dt = new DataTable();
                    adapter.Fill(dt);                    
                }

                cnn.Close();
            }

            return dt;
        }
    }
}
