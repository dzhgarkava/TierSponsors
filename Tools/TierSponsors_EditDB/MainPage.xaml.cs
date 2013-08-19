using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using TierSponsors_EditDB.srEditDB_SL;


namespace TierSponsors_EditDB
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();

            ServiceEditDB_SLClient client = new ServiceEditDB_SLClient();
            client.GetOrganisationsCompleted += client_GetOrganisationsCompleted;
            client.GetOrganisationsAsync(true);
        }

        void client_GetOrganisationsCompleted(object sender, GetOrganisationsCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                foreach (var org in e.Result.OrgList)
                {
                    ListBoxItem item = new ListBoxItem();
                    item.MouseLeftButtonDown += item_MouseLeftButtonDown;
                    lbxOrganisations.Items.Add(org.ID);
                }
            }
        }

        void item_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ServiceEditDB_SLClient client = new ServiceEditDB_SLClient();
            client.GetOrganisationByIDCompleted += client_GetOrganisationByIDCompleted;
            client.GetOrganisationByIDAsync(sender.ToString());
        }

        void client_GetOrganisationByIDCompleted(object sender, GetOrganisationByIDCompletedEventArgs e)
        {
            txtID.Text = e.Result.ID;
            txtNameCityCounty.Text = e.Result.NameCityCounty;
            txtCity.Text = e.Result.City;
            txtCounty.Text = e.Result.County;
        }

        private void txtID_OnTextChanged(object sender, TextChangedEventArgs e)
        {
           // btnSave.IsEnabled = !String.IsNullOrEmpty(txtID.Text);
        }
    }
}
