using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace BikeStore.Pages
{
    /// <summary>
    /// Interaction logic for PFilterPanel.xaml
    /// </summary>
    public partial class PFilterPanel : Page, INotifyPropertyChanged, ISearchRequiredByMainOld
    {
        private IResultsRequiredBySearchOld page;                   // results page; will be set by StartWindow                              
        private string schemaName;                               // will get it from StartWindow
        private string tableName;                                // will get it from StartWindow
        private ObservableCollection<string> columnNames;        // will get it from Result page
        private string search_1 = "";
        private string search_2 = "";
        private string search_3 = "";
        private SqlQuery query;
        private int totalRecords = 0;        
        private Dictionary<string, string> columnsMap = new Dictionary<string, string>(); // will be assembled in InitializeColumnsMap()
        public event PropertyChangedEventHandler PropertyChanged;                         // needed to update the lists bound to the comboboxes
        private ObservableCollection<string> cmb_selected { get; set; } = new ObservableCollection<string>(); // holds for each combobox the currently selected item

        /* The following lists are used to ensure that the dropdown lists of the combo boxes contain only the values which are allowed,
         * based on the selections of the other combo boxes.
         * When Cmb_Closed() is fired, the respective list entry in Cmb_selected gets updated, and then PopulateComboBoxes() is called.
         * This updates the respective Cmb_[1...3]_searchable list entries, and on change, the event handler NotifyPropertyChanged()
         * gets fired. This results in a re-binding of the entries to the comboboxes.
         * */
        private ObservableCollection<string> cmb_1_searchable;
        public ObservableCollection<string> Cmb_1_searchable
        {
            get
            {
                return cmb_1_searchable;
            }
            set
            {
                cmb_1_searchable = value;
                NotifyPropertyChanged("Cmb_1_searchable");
            }
        }
        private ObservableCollection<string> cmb_2_searchable;
        public ObservableCollection<string> Cmb_2_searchable
        {
            get
            {
                return cmb_2_searchable;
            }
            set
            {
                cmb_2_searchable = value;
                NotifyPropertyChanged("Cmb_2_searchable");
            }
        }
        private ObservableCollection<string> cmb_3_searchable;
        public ObservableCollection<string> Cmb_3_searchable
        {
            get
            {
                return cmb_3_searchable;
            }
            set
            {
                cmb_3_searchable = value;
                NotifyPropertyChanged("Cmb_3_searchable");
            }
        }

        public PFilterPanel()
        {
            InitializeComponent();            
        }

        /**
         * Clears search strings, lists and resets visual components to get ready for a new result page.
         */
        public void Clear()
        {
            System.Diagnostics.Debug.WriteLine("Clearing components");
            search_1 = "";
            search_2 = "";
            search_3 = "";
            Tb_1.Text = "";
            Tb_2.Text = "";
            Tb_3.Text = "";
            Cb_CombineAnd.IsChecked = false;
            Cb_ExactMatch.IsChecked = false;
            columnsMap.Clear();
            cmb_selected.Clear();
            Bt_UnFilter.Visibility = Visibility.Hidden;
            Txt_ResultsHeadline.Text = "";
        }

        /**
         * Collects all information about the current table and search parameters and passes them to the
         * generic method Query() in SqlQuery, which returns the data table of results.
         * This data table is then passed on to the Results page for display.
         **/
        public void Query()
        {
            bool filter = false;
            if (!string.IsNullOrEmpty(search_1) || !string.IsNullOrEmpty(search_2) || !string.IsNullOrEmpty(search_3))
            {
                filter = true;
            }

            query = new SqlQuery(schemaName, tableName, new List<string> { columnsMap[cmb_selected[0]], columnsMap[cmb_selected[1]], columnsMap[cmb_selected[2]] }, filter, new List<string> { search_1, search_2, search_3 }, Cb_CombineAnd.IsChecked.Value, Cb_ExactMatch.IsChecked.Value);
            DataTable dt = query.Query();
            page.SetDataTable(dt); // This will update the Results page!

            if (filter == false)
            {
                totalRecords = dt.Rows.Count;
            }
            int numRecords = dt.Rows.Count;

            if (filter == true)
            {
                Txt_ResultsHeadline.Text = "Filtered results: ";
                Bt_UnFilter.Visibility = Visibility.Visible;
            }
            else
            {
                Txt_ResultsHeadline.Text = "All results: ";
            }
            Txt_ResultsHeadline.Text += numRecords.ToString();
            if (filter == true)
            {
                Txt_ResultsHeadline.Text += " of " + totalRecords;
            }
        }

        /**
         * Sets a new result page, clears the text fields and updates the combo boxes.
         */
        public void SetResultsPage(IResultsRequiredBySearchOld _page, string _schemaName, string _tableName)
        {
            page = _page;
            schemaName = _schemaName;
            tableName = _tableName;
            page.SetTablePath(schemaName, tableName);    // Set the table path in the Result page, so it knows the display column names
            InitializeColumnsMap();          // Get column display names and perform query to get column names
            InitializeComboBoxes();          // Set the default selected properties of the combo boxes
            PopulateComboBoxes();            // Update the combo boxes accordingly
            Query();                         // Make an SqlQuery to show the full list of entries
        }

        /**
         * Gets the list of column names from the page.
         * Executes a query which retrieves all table columns' real names.
         * Then maps these into the list columns, in the order of their occurrence in the table.
         */
        private void InitializeColumnsMap()
        {
            System.Diagnostics.Debug.WriteLine("Initializing map of columns");
            System.Diagnostics.Debug.WriteLine("  Getting list of column names from Results page");
            columnNames = page.GetColumnNames();
            System.Diagnostics.Debug.WriteLine("    Got "+ columnNames.Count + " names");
            System.Diagnostics.Debug.WriteLine("  Getting list of columns from database");
            query = new SqlQuery(schemaName, tableName);            
            List<string> columns = query.GetTableColumns();
            System.Diagnostics.Debug.WriteLine("    Got " + columns.Count + " names");            
            if (columnNames.Count != columns.Count)
            {
                System.Diagnostics.Debug.WriteLine("Error: number of columns differs");
                return;
            }
            System.Diagnostics.Debug.WriteLine("  Mapping column display names to column names");
            for (int i = 0; i < columns.Count; i++)
            {
                columnsMap[columnNames[i]] = columns[i];
            }
        }

        /**
         * Sets the default selected column names in the comboboxes after a new Results page was loaded. 
         */
        private void InitializeComboBoxes()
        {
            System.Diagnostics.Debug.WriteLine("Initializing comboboxes");
            cmb_selected.Clear();
            for (int i = 0; i < 3; i++)
            {
                cmb_selected.Add(columnNames[i]);
            }
        }

        /**
         * Updates the content of each combo box, so that no already selected entry can be selected for 
         * a second or third time.
         */
        private void PopulateComboBoxes()
        {
            System.Diagnostics.Debug.WriteLine("Populating comboboxes");
            // 1. populate each cmb with all items
            // should update through data binding in XAML

            Cmb_1_searchable = new ObservableCollection<string>(columnNames);
            Cmb_2_searchable = new ObservableCollection<string>(columnNames);
            Cmb_3_searchable = new ObservableCollection<string>(columnNames);

            // 2. each cmb: select item according to list
            Cmb_1.SelectedItem = cmb_selected[0];
            Cmb_2.SelectedItem = cmb_selected[1];
            Cmb_3.SelectedItem = cmb_selected[2];

            // 3. each cmb: remove item from list which is selected in others
            Cmb_1_searchable.Remove(cmb_selected[1]);
            Cmb_1_searchable.Remove(cmb_selected[2]);

            Cmb_2_searchable.Remove(cmb_selected[0]);
            Cmb_2_searchable.Remove(cmb_selected[2]);

            Cmb_3_searchable.Remove(cmb_selected[0]);
            Cmb_3_searchable.Remove(cmb_selected[1]);

            DataContext = this;
        }

        /**
         * Event handler. Fires when one of the following lists changes:
         * - Cmb_1_searchable
         * - Cmb_2_searchable
         * - Cmb_3_searchable
         */
        public void NotifyPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        /**
         * Triggered on click of button "Show all".
         */
        private void Bt_UnFilter_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).Visibility = Visibility.Hidden;
            search_1 = "";
            search_2 = "";
            search_3 = "";
            Query();
        }

        /**
         * Triggered on click of button "Filter".
         */
        private void Bt_Search_Click(object sender, RoutedEventArgs e)
        {
            search_1 = Tb_1.Text;
            search_2 = Tb_2.Text;
            search_3 = Tb_3.Text;
            Query();
        }

        /**
        * Triggered when a combobox closes.
        */
        private void Cmb_Closed(object sender, EventArgs e)
        {
            string cbBoxName = ((ComboBox)sender).Name;
            short no;
            bool isParsable = Int16.TryParse(cbBoxName.Substring(cbBoxName.Length - 1), out no);
            if (isParsable)
            {
                string selectedItem = (string)((ComboBox)sender).SelectedItem;
                cmb_selected[no - 1] = selectedItem;
                PopulateComboBoxes();
            }
        }

        
    }
}
