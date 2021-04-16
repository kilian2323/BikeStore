using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Text;

namespace BikeStore
{
    /**
    * Methods required by the Search panel to interface the Result page.
    */
    public interface IResultsRequiredBySearch
    {


        public void SetTablePath(string schemaName, string tableName);
        public ObservableCollection<string> GetColumnNames();
        public void SetDataTable(DataTable dt);

    }
}
