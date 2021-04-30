using BLL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using UI.Interfaces;
using Core.Classes;
using System.Diagnostics;

namespace UI.Pages
{
    /// <summary>
    /// Interaction logic for PSearch.xaml
    /// </summary>
    public partial class PSearch : Page, INotifyPropertyChanged
    {
        private List<string> colDisplayNames = new List<string>();
        private List<string> colSelected = new List<string>(3);
        private string tableAlias;

        private int totalRecords = 0;
        public event PropertyChangedEventHandler PropertyChanged;

        public Search thisSearch;
        private IResultsRequiredBySearch resultPage;

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

        public PSearch(IResultsRequiredBySearch _resultPage)
        {
            InitializeComponent();
            SetResultPage(_resultPage);
            InitializeColDisplayNames();
            DoSearch(); // initial search with empty search strings (retrieves all results)
        }

        /**
         * Clears search strings, lists and resets visual components to get ready for a new result page.
         */
        public void Clear()
        {
            System.Diagnostics.Debug.WriteLine("Clearing components");
            Tb_1.Text = "";
            Tb_2.Text = "";
            Tb_3.Text = "";
            Cb_CombineAnd.IsChecked = false;
            Cb_ExactMatch.IsChecked = false;
            colDisplayNames.Clear();
            colSelected.Clear();
            tableAlias = "";
            Bt_UnFilter.Visibility = Visibility.Hidden;
            Txt_ResultsHeadline.Text = "";
            thisSearch = null;
        }

        public void SetResultPage(IResultsRequiredBySearch _resultPage)
        {
            resultPage = _resultPage;
            tableAlias = _resultPage.GetTableAlias();            
        }


        public void DoSearch()
        {
            thisSearch = new Search(tableAlias, colSelected[0], colSelected[1], colSelected[2], Tb_1.Text, Tb_2.Text, Tb_3.Text, Cb_CombineAnd.IsChecked.Value, Cb_ExactMatch.IsChecked.Value);
            resultPage.DoSearch(thisSearch);
        }

        

        private void InitializeColDisplayNames()
        {
            BLL_Base ba = new BLL_Base();
            colDisplayNames = ba.GetTableColumns(tableAlias);
            if (colDisplayNames == null)
            {
                return;
            }
            SelectDefaultComboBoxEntries();
            PopulateComboBoxes();
        }

        private void SelectDefaultComboBoxEntries()
        {
            for (int c = 0; c < Math.Min(3,colDisplayNames.Count); c++)
            {
                colSelected.Add(colDisplayNames[c]);
            }
        }

        /* To be called whenever a combobox closes */
        
        private void PopulateComboBoxes()
        {
            System.Diagnostics.Debug.WriteLine("Populating comboboxes");
            // 1. populate each cmb with all items
            // should update through data binding in XAML

            Cmb_1_searchable = new ObservableCollection<string>(colDisplayNames);
            Cmb_2_searchable = new ObservableCollection<string>(colDisplayNames);
            Cmb_3_searchable = new ObservableCollection<string>(colDisplayNames);

            // 2. each cmb: select item according to list
            Cmb_1.SelectedItem = colSelected[0];
            Cmb_2.SelectedItem = colSelected[1];
            Cmb_3.SelectedItem = colSelected[2];

            // 3. each cmb: remove item from list which is selected in others
            Cmb_1_searchable.Remove(colSelected[1]);
            Cmb_1_searchable.Remove(colSelected[2]);

            Cmb_2_searchable.Remove(colSelected[0]);
            Cmb_2_searchable.Remove(colSelected[2]);

            Cmb_3_searchable.Remove(colSelected[0]);
            Cmb_3_searchable.Remove(colSelected[1]);

            DataContext = this;
        }

        private void DisplayResults(DataTable dt)
        {

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
            colSelected.Clear();
            DoSearch();
        }

        /**
         * Triggered on click of button "Filter".
         */
        private void Bt_Search_Click(object sender, RoutedEventArgs e)
        {
            DoSearch();
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
                colSelected[no - 1] = selectedItem;
                PopulateComboBoxes();
            }
        }
    }
}
