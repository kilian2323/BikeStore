using Common.Classes;
using System;
using System.Collections.Generic;

namespace Common.Classes
{
    public class Table
    {
        public string SchemaName { get; set; }

        public string TableName { get; set; }

        public List<ColumnBase> columns { get; set; } = new List<ColumnBase>();

        public Type ModelDataType { get; set; } // C# class type of the model representing this table

        public Table(string sName, string tName, Type mdt)
        {
            SchemaName = sName;
            TableName = tName;
            ModelDataType = mdt;            
        }

        public void AddColumn(ColumnBase c)
        {
            if (columns.Contains(c))
            {
                return;
            }
            columns.Add(c);
        }
    }
}
