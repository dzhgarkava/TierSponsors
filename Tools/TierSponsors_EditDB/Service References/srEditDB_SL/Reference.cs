﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This code was auto-generated by Microsoft.Silverlight.ServiceReference, version 5.0.61118.0
// 
namespace TierSponsors_EditDB.srEditDB_SL {
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ServiceEditDB_SL.OrganisationsCollection", Namespace="http://schemas.datacontract.org/2004/07/TierSponsors_EditDB.Web")]
    public partial class ServiceEditDB_SLOrganisationsCollection : object, System.ComponentModel.INotifyPropertyChanged {
        
        private System.Collections.ObjectModel.ObservableCollection<TierSponsors_EditDB.srEditDB_SL.ServiceEditDB_SLOrganisation> OrgListField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.ObjectModel.ObservableCollection<TierSponsors_EditDB.srEditDB_SL.ServiceEditDB_SLOrganisation> OrgList {
            get {
                return this.OrgListField;
            }
            set {
                if ((object.ReferenceEquals(this.OrgListField, value) != true)) {
                    this.OrgListField = value;
                    this.RaisePropertyChanged("OrgList");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ServiceEditDB_SL.Organisation", Namespace="http://schemas.datacontract.org/2004/07/TierSponsors_EditDB.Web")]
    public partial class ServiceEditDB_SLOrganisation : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string CityField;
        
        private string CountyField;
        
        private string IDField;
        
        private string NameField;
        
        private string NameCityCountyField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string City {
            get {
                return this.CityField;
            }
            set {
                if ((object.ReferenceEquals(this.CityField, value) != true)) {
                    this.CityField = value;
                    this.RaisePropertyChanged("City");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string County {
            get {
                return this.CountyField;
            }
            set {
                if ((object.ReferenceEquals(this.CountyField, value) != true)) {
                    this.CountyField = value;
                    this.RaisePropertyChanged("County");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ID {
            get {
                return this.IDField;
            }
            set {
                if ((object.ReferenceEquals(this.IDField, value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string NameCityCounty {
            get {
                return this.NameCityCountyField;
            }
            set {
                if ((object.ReferenceEquals(this.NameCityCountyField, value) != true)) {
                    this.NameCityCountyField = value;
                    this.RaisePropertyChanged("NameCityCounty");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="", ConfigurationName="srEditDB_SL.ServiceEditDB_SL")]
    public interface ServiceEditDB_SL {
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="urn:ServiceEditDB_SL/GetOrganisations", ReplyAction="urn:ServiceEditDB_SL/GetOrganisationsResponse")]
        System.IAsyncResult BeginGetOrganisations(bool Checked, System.AsyncCallback callback, object asyncState);
        
        TierSponsors_EditDB.srEditDB_SL.ServiceEditDB_SLOrganisationsCollection EndGetOrganisations(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="urn:ServiceEditDB_SL/GetOrganisationByID", ReplyAction="urn:ServiceEditDB_SL/GetOrganisationByIDResponse")]
        System.IAsyncResult BeginGetOrganisationByID(string ID, System.AsyncCallback callback, object asyncState);
        
        TierSponsors_EditDB.srEditDB_SL.ServiceEditDB_SLOrganisation EndGetOrganisationByID(System.IAsyncResult result);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="urn:ServiceEditDB_SL/SetOrganisationByID", ReplyAction="urn:ServiceEditDB_SL/SetOrganisationByIDResponse")]
        System.IAsyncResult BeginSetOrganisationByID(string ID, string Name, string City, string County, string NameCityCounty, string Checked, System.AsyncCallback callback, object asyncState);
        
        void EndSetOrganisationByID(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ServiceEditDB_SLChannel : TierSponsors_EditDB.srEditDB_SL.ServiceEditDB_SL, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GetOrganisationsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetOrganisationsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public TierSponsors_EditDB.srEditDB_SL.ServiceEditDB_SLOrganisationsCollection Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((TierSponsors_EditDB.srEditDB_SL.ServiceEditDB_SLOrganisationsCollection)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class GetOrganisationByIDCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public GetOrganisationByIDCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public TierSponsors_EditDB.srEditDB_SL.ServiceEditDB_SLOrganisation Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((TierSponsors_EditDB.srEditDB_SL.ServiceEditDB_SLOrganisation)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceEditDB_SLClient : System.ServiceModel.ClientBase<TierSponsors_EditDB.srEditDB_SL.ServiceEditDB_SL>, TierSponsors_EditDB.srEditDB_SL.ServiceEditDB_SL {
        
        private BeginOperationDelegate onBeginGetOrganisationsDelegate;
        
        private EndOperationDelegate onEndGetOrganisationsDelegate;
        
        private System.Threading.SendOrPostCallback onGetOrganisationsCompletedDelegate;
        
        private BeginOperationDelegate onBeginGetOrganisationByIDDelegate;
        
        private EndOperationDelegate onEndGetOrganisationByIDDelegate;
        
        private System.Threading.SendOrPostCallback onGetOrganisationByIDCompletedDelegate;
        
        private BeginOperationDelegate onBeginSetOrganisationByIDDelegate;
        
        private EndOperationDelegate onEndSetOrganisationByIDDelegate;
        
        private System.Threading.SendOrPostCallback onSetOrganisationByIDCompletedDelegate;
        
        private BeginOperationDelegate onBeginOpenDelegate;
        
        private EndOperationDelegate onEndOpenDelegate;
        
        private System.Threading.SendOrPostCallback onOpenCompletedDelegate;
        
        private BeginOperationDelegate onBeginCloseDelegate;
        
        private EndOperationDelegate onEndCloseDelegate;
        
        private System.Threading.SendOrPostCallback onCloseCompletedDelegate;
        
        public ServiceEditDB_SLClient() {
        }
        
        public ServiceEditDB_SLClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceEditDB_SLClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceEditDB_SLClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceEditDB_SLClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public System.Net.CookieContainer CookieContainer {
            get {
                System.ServiceModel.Channels.IHttpCookieContainerManager httpCookieContainerManager = this.InnerChannel.GetProperty<System.ServiceModel.Channels.IHttpCookieContainerManager>();
                if ((httpCookieContainerManager != null)) {
                    return httpCookieContainerManager.CookieContainer;
                }
                else {
                    return null;
                }
            }
            set {
                System.ServiceModel.Channels.IHttpCookieContainerManager httpCookieContainerManager = this.InnerChannel.GetProperty<System.ServiceModel.Channels.IHttpCookieContainerManager>();
                if ((httpCookieContainerManager != null)) {
                    httpCookieContainerManager.CookieContainer = value;
                }
                else {
                    throw new System.InvalidOperationException("Unable to set the CookieContainer. Please make sure the binding contains an HttpC" +
                            "ookieContainerBindingElement.");
                }
            }
        }
        
        public event System.EventHandler<GetOrganisationsCompletedEventArgs> GetOrganisationsCompleted;
        
        public event System.EventHandler<GetOrganisationByIDCompletedEventArgs> GetOrganisationByIDCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> SetOrganisationByIDCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> OpenCompleted;
        
        public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> CloseCompleted;
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult TierSponsors_EditDB.srEditDB_SL.ServiceEditDB_SL.BeginGetOrganisations(bool Checked, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetOrganisations(Checked, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        TierSponsors_EditDB.srEditDB_SL.ServiceEditDB_SLOrganisationsCollection TierSponsors_EditDB.srEditDB_SL.ServiceEditDB_SL.EndGetOrganisations(System.IAsyncResult result) {
            return base.Channel.EndGetOrganisations(result);
        }
        
        private System.IAsyncResult OnBeginGetOrganisations(object[] inValues, System.AsyncCallback callback, object asyncState) {
            bool Checked = ((bool)(inValues[0]));
            return ((TierSponsors_EditDB.srEditDB_SL.ServiceEditDB_SL)(this)).BeginGetOrganisations(Checked, callback, asyncState);
        }
        
        private object[] OnEndGetOrganisations(System.IAsyncResult result) {
            TierSponsors_EditDB.srEditDB_SL.ServiceEditDB_SLOrganisationsCollection retVal = ((TierSponsors_EditDB.srEditDB_SL.ServiceEditDB_SL)(this)).EndGetOrganisations(result);
            return new object[] {
                    retVal};
        }
        
        private void OnGetOrganisationsCompleted(object state) {
            if ((this.GetOrganisationsCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetOrganisationsCompleted(this, new GetOrganisationsCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetOrganisationsAsync(bool Checked) {
            this.GetOrganisationsAsync(Checked, null);
        }
        
        public void GetOrganisationsAsync(bool Checked, object userState) {
            if ((this.onBeginGetOrganisationsDelegate == null)) {
                this.onBeginGetOrganisationsDelegate = new BeginOperationDelegate(this.OnBeginGetOrganisations);
            }
            if ((this.onEndGetOrganisationsDelegate == null)) {
                this.onEndGetOrganisationsDelegate = new EndOperationDelegate(this.OnEndGetOrganisations);
            }
            if ((this.onGetOrganisationsCompletedDelegate == null)) {
                this.onGetOrganisationsCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetOrganisationsCompleted);
            }
            base.InvokeAsync(this.onBeginGetOrganisationsDelegate, new object[] {
                        Checked}, this.onEndGetOrganisationsDelegate, this.onGetOrganisationsCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult TierSponsors_EditDB.srEditDB_SL.ServiceEditDB_SL.BeginGetOrganisationByID(string ID, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginGetOrganisationByID(ID, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        TierSponsors_EditDB.srEditDB_SL.ServiceEditDB_SLOrganisation TierSponsors_EditDB.srEditDB_SL.ServiceEditDB_SL.EndGetOrganisationByID(System.IAsyncResult result) {
            return base.Channel.EndGetOrganisationByID(result);
        }
        
        private System.IAsyncResult OnBeginGetOrganisationByID(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string ID = ((string)(inValues[0]));
            return ((TierSponsors_EditDB.srEditDB_SL.ServiceEditDB_SL)(this)).BeginGetOrganisationByID(ID, callback, asyncState);
        }
        
        private object[] OnEndGetOrganisationByID(System.IAsyncResult result) {
            TierSponsors_EditDB.srEditDB_SL.ServiceEditDB_SLOrganisation retVal = ((TierSponsors_EditDB.srEditDB_SL.ServiceEditDB_SL)(this)).EndGetOrganisationByID(result);
            return new object[] {
                    retVal};
        }
        
        private void OnGetOrganisationByIDCompleted(object state) {
            if ((this.GetOrganisationByIDCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.GetOrganisationByIDCompleted(this, new GetOrganisationByIDCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void GetOrganisationByIDAsync(string ID) {
            this.GetOrganisationByIDAsync(ID, null);
        }
        
        public void GetOrganisationByIDAsync(string ID, object userState) {
            if ((this.onBeginGetOrganisationByIDDelegate == null)) {
                this.onBeginGetOrganisationByIDDelegate = new BeginOperationDelegate(this.OnBeginGetOrganisationByID);
            }
            if ((this.onEndGetOrganisationByIDDelegate == null)) {
                this.onEndGetOrganisationByIDDelegate = new EndOperationDelegate(this.OnEndGetOrganisationByID);
            }
            if ((this.onGetOrganisationByIDCompletedDelegate == null)) {
                this.onGetOrganisationByIDCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetOrganisationByIDCompleted);
            }
            base.InvokeAsync(this.onBeginGetOrganisationByIDDelegate, new object[] {
                        ID}, this.onEndGetOrganisationByIDDelegate, this.onGetOrganisationByIDCompletedDelegate, userState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.IAsyncResult TierSponsors_EditDB.srEditDB_SL.ServiceEditDB_SL.BeginSetOrganisationByID(string ID, string Name, string City, string County, string NameCityCounty, string Checked, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginSetOrganisationByID(ID, Name, City, County, NameCityCounty, Checked, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        void TierSponsors_EditDB.srEditDB_SL.ServiceEditDB_SL.EndSetOrganisationByID(System.IAsyncResult result) {
            base.Channel.EndSetOrganisationByID(result);
        }
        
        private System.IAsyncResult OnBeginSetOrganisationByID(object[] inValues, System.AsyncCallback callback, object asyncState) {
            string ID = ((string)(inValues[0]));
            string Name = ((string)(inValues[1]));
            string City = ((string)(inValues[2]));
            string County = ((string)(inValues[3]));
            string NameCityCounty = ((string)(inValues[4]));
            string Checked = ((string)(inValues[5]));
            return ((TierSponsors_EditDB.srEditDB_SL.ServiceEditDB_SL)(this)).BeginSetOrganisationByID(ID, Name, City, County, NameCityCounty, Checked, callback, asyncState);
        }
        
        private object[] OnEndSetOrganisationByID(System.IAsyncResult result) {
            ((TierSponsors_EditDB.srEditDB_SL.ServiceEditDB_SL)(this)).EndSetOrganisationByID(result);
            return null;
        }
        
        private void OnSetOrganisationByIDCompleted(object state) {
            if ((this.SetOrganisationByIDCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.SetOrganisationByIDCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void SetOrganisationByIDAsync(string ID, string Name, string City, string County, string NameCityCounty, string Checked) {
            this.SetOrganisationByIDAsync(ID, Name, City, County, NameCityCounty, Checked, null);
        }
        
        public void SetOrganisationByIDAsync(string ID, string Name, string City, string County, string NameCityCounty, string Checked, object userState) {
            if ((this.onBeginSetOrganisationByIDDelegate == null)) {
                this.onBeginSetOrganisationByIDDelegate = new BeginOperationDelegate(this.OnBeginSetOrganisationByID);
            }
            if ((this.onEndSetOrganisationByIDDelegate == null)) {
                this.onEndSetOrganisationByIDDelegate = new EndOperationDelegate(this.OnEndSetOrganisationByID);
            }
            if ((this.onSetOrganisationByIDCompletedDelegate == null)) {
                this.onSetOrganisationByIDCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnSetOrganisationByIDCompleted);
            }
            base.InvokeAsync(this.onBeginSetOrganisationByIDDelegate, new object[] {
                        ID,
                        Name,
                        City,
                        County,
                        NameCityCounty,
                        Checked}, this.onEndSetOrganisationByIDDelegate, this.onSetOrganisationByIDCompletedDelegate, userState);
        }
        
        private System.IAsyncResult OnBeginOpen(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(callback, asyncState);
        }
        
        private object[] OnEndOpen(System.IAsyncResult result) {
            ((System.ServiceModel.ICommunicationObject)(this)).EndOpen(result);
            return null;
        }
        
        private void OnOpenCompleted(object state) {
            if ((this.OpenCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.OpenCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void OpenAsync() {
            this.OpenAsync(null);
        }
        
        public void OpenAsync(object userState) {
            if ((this.onBeginOpenDelegate == null)) {
                this.onBeginOpenDelegate = new BeginOperationDelegate(this.OnBeginOpen);
            }
            if ((this.onEndOpenDelegate == null)) {
                this.onEndOpenDelegate = new EndOperationDelegate(this.OnEndOpen);
            }
            if ((this.onOpenCompletedDelegate == null)) {
                this.onOpenCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnOpenCompleted);
            }
            base.InvokeAsync(this.onBeginOpenDelegate, null, this.onEndOpenDelegate, this.onOpenCompletedDelegate, userState);
        }
        
        private System.IAsyncResult OnBeginClose(object[] inValues, System.AsyncCallback callback, object asyncState) {
            return ((System.ServiceModel.ICommunicationObject)(this)).BeginClose(callback, asyncState);
        }
        
        private object[] OnEndClose(System.IAsyncResult result) {
            ((System.ServiceModel.ICommunicationObject)(this)).EndClose(result);
            return null;
        }
        
        private void OnCloseCompleted(object state) {
            if ((this.CloseCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.CloseCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void CloseAsync() {
            this.CloseAsync(null);
        }
        
        public void CloseAsync(object userState) {
            if ((this.onBeginCloseDelegate == null)) {
                this.onBeginCloseDelegate = new BeginOperationDelegate(this.OnBeginClose);
            }
            if ((this.onEndCloseDelegate == null)) {
                this.onEndCloseDelegate = new EndOperationDelegate(this.OnEndClose);
            }
            if ((this.onCloseCompletedDelegate == null)) {
                this.onCloseCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnCloseCompleted);
            }
            base.InvokeAsync(this.onBeginCloseDelegate, null, this.onEndCloseDelegate, this.onCloseCompletedDelegate, userState);
        }
        
        protected override TierSponsors_EditDB.srEditDB_SL.ServiceEditDB_SL CreateChannel() {
            return new ServiceEditDB_SLClientChannel(this);
        }
        
        private class ServiceEditDB_SLClientChannel : ChannelBase<TierSponsors_EditDB.srEditDB_SL.ServiceEditDB_SL>, TierSponsors_EditDB.srEditDB_SL.ServiceEditDB_SL {
            
            public ServiceEditDB_SLClientChannel(System.ServiceModel.ClientBase<TierSponsors_EditDB.srEditDB_SL.ServiceEditDB_SL> client) : 
                    base(client) {
            }
            
            public System.IAsyncResult BeginGetOrganisations(bool Checked, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[1];
                _args[0] = Checked;
                System.IAsyncResult _result = base.BeginInvoke("GetOrganisations", _args, callback, asyncState);
                return _result;
            }
            
            public TierSponsors_EditDB.srEditDB_SL.ServiceEditDB_SLOrganisationsCollection EndGetOrganisations(System.IAsyncResult result) {
                object[] _args = new object[0];
                TierSponsors_EditDB.srEditDB_SL.ServiceEditDB_SLOrganisationsCollection _result = ((TierSponsors_EditDB.srEditDB_SL.ServiceEditDB_SLOrganisationsCollection)(base.EndInvoke("GetOrganisations", _args, result)));
                return _result;
            }
            
            public System.IAsyncResult BeginGetOrganisationByID(string ID, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[1];
                _args[0] = ID;
                System.IAsyncResult _result = base.BeginInvoke("GetOrganisationByID", _args, callback, asyncState);
                return _result;
            }
            
            public TierSponsors_EditDB.srEditDB_SL.ServiceEditDB_SLOrganisation EndGetOrganisationByID(System.IAsyncResult result) {
                object[] _args = new object[0];
                TierSponsors_EditDB.srEditDB_SL.ServiceEditDB_SLOrganisation _result = ((TierSponsors_EditDB.srEditDB_SL.ServiceEditDB_SLOrganisation)(base.EndInvoke("GetOrganisationByID", _args, result)));
                return _result;
            }
            
            public System.IAsyncResult BeginSetOrganisationByID(string ID, string Name, string City, string County, string NameCityCounty, string Checked, System.AsyncCallback callback, object asyncState) {
                object[] _args = new object[6];
                _args[0] = ID;
                _args[1] = Name;
                _args[2] = City;
                _args[3] = County;
                _args[4] = NameCityCounty;
                _args[5] = Checked;
                System.IAsyncResult _result = base.BeginInvoke("SetOrganisationByID", _args, callback, asyncState);
                return _result;
            }
            
            public void EndSetOrganisationByID(System.IAsyncResult result) {
                object[] _args = new object[0];
                base.EndInvoke("SetOrganisationByID", _args, result);
            }
        }
    }
}
