﻿#pragma checksum "D:\git\TierSponsors\Tools\TierSponsors_EditDB\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D00132F2DA0A44C850A1991C22D5AC6D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18047
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace TierSponsors_EditDB {
    
    
    public partial class MainPage : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.ListBox lbxOrganisations;
        
        internal System.Windows.Controls.TextBox txtID;
        
        internal System.Windows.Controls.TextBox txtNameCityCounty;
        
        internal System.Windows.Controls.TextBox txtCity;
        
        internal System.Windows.Controls.TextBox txtCounty;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/TierSponsors_EditDB;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.lbxOrganisations = ((System.Windows.Controls.ListBox)(this.FindName("lbxOrganisations")));
            this.txtID = ((System.Windows.Controls.TextBox)(this.FindName("txtID")));
            this.txtNameCityCounty = ((System.Windows.Controls.TextBox)(this.FindName("txtNameCityCounty")));
            this.txtCity = ((System.Windows.Controls.TextBox)(this.FindName("txtCity")));
            this.txtCounty = ((System.Windows.Controls.TextBox)(this.FindName("txtCounty")));
        }
    }
}

