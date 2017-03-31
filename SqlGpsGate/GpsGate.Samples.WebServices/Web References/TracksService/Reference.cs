﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.296.
// 
#pragma warning disable 1591

namespace GpsGate.Samples.SOAP.TracksService {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="TracksSoap", Namespace="http://gpsgate.com/services/")]
    public partial class Tracks : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback GetTracksByUsersOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetTripsByUserOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetTrackDataChunkOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetFatPointsOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetLifeRecorderByUserOperationCompleted;
        
        private System.Threading.SendOrPostCallback SetLifeTrackRecorderFiltersOperationCompleted;
        
        private System.Threading.SendOrPostCallback DeleteTrackOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public Tracks() {
            this.Url = global::GpsGate.Samples.SOAP.Properties.Settings.Default.GpsGate_Samples_SOAP_TracksService_Tracks;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event GetTracksByUsersCompletedEventHandler GetTracksByUsersCompleted;
        
        /// <remarks/>
        public event GetTripsByUserCompletedEventHandler GetTripsByUserCompleted;
        
        /// <remarks/>
        public event GetTrackDataChunkCompletedEventHandler GetTrackDataChunkCompleted;
        
        /// <remarks/>
        public event GetFatPointsCompletedEventHandler GetFatPointsCompleted;
        
        /// <remarks/>
        public event GetLifeRecorderByUserCompletedEventHandler GetLifeRecorderByUserCompleted;
        
        /// <remarks/>
        public event SetLifeTrackRecorderFiltersCompletedEventHandler SetLifeTrackRecorderFiltersCompleted;
        
        /// <remarks/>
        public event DeleteTrackCompletedEventHandler DeleteTrackCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://gpsgate.com/services/GetTracksByUsers", RequestNamespace="http://gpsgate.com/services/", ResponseNamespace="http://gpsgate.com/services/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode GetTracksByUsers(string strSessionID, int iApplicationID, int[] arrUserIDs, System.DateTime dtStart, System.DateTime dtEnd) {
            object[] results = this.Invoke("GetTracksByUsers", new object[] {
                        strSessionID,
                        iApplicationID,
                        arrUserIDs,
                        dtStart,
                        dtEnd});
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        public void GetTracksByUsersAsync(string strSessionID, int iApplicationID, int[] arrUserIDs, System.DateTime dtStart, System.DateTime dtEnd) {
            this.GetTracksByUsersAsync(strSessionID, iApplicationID, arrUserIDs, dtStart, dtEnd, null);
        }
        
        /// <remarks/>
        public void GetTracksByUsersAsync(string strSessionID, int iApplicationID, int[] arrUserIDs, System.DateTime dtStart, System.DateTime dtEnd, object userState) {
            if ((this.GetTracksByUsersOperationCompleted == null)) {
                this.GetTracksByUsersOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetTracksByUsersOperationCompleted);
            }
            this.InvokeAsync("GetTracksByUsers", new object[] {
                        strSessionID,
                        iApplicationID,
                        arrUserIDs,
                        dtStart,
                        dtEnd}, this.GetTracksByUsersOperationCompleted, userState);
        }
        
        private void OnGetTracksByUsersOperationCompleted(object arg) {
            if ((this.GetTracksByUsersCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetTracksByUsersCompleted(this, new GetTracksByUsersCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://gpsgate.com/services/GetTripsByUser", RequestNamespace="http://gpsgate.com/services/", ResponseNamespace="http://gpsgate.com/services/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode GetTripsByUser(string strSessionID, int iApplicationID, int iUserID, System.DateTime dtStart, System.DateTime dtEnd) {
            object[] results = this.Invoke("GetTripsByUser", new object[] {
                        strSessionID,
                        iApplicationID,
                        iUserID,
                        dtStart,
                        dtEnd});
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        public void GetTripsByUserAsync(string strSessionID, int iApplicationID, int iUserID, System.DateTime dtStart, System.DateTime dtEnd) {
            this.GetTripsByUserAsync(strSessionID, iApplicationID, iUserID, dtStart, dtEnd, null);
        }
        
        /// <remarks/>
        public void GetTripsByUserAsync(string strSessionID, int iApplicationID, int iUserID, System.DateTime dtStart, System.DateTime dtEnd, object userState) {
            if ((this.GetTripsByUserOperationCompleted == null)) {
                this.GetTripsByUserOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetTripsByUserOperationCompleted);
            }
            this.InvokeAsync("GetTripsByUser", new object[] {
                        strSessionID,
                        iApplicationID,
                        iUserID,
                        dtStart,
                        dtEnd}, this.GetTripsByUserOperationCompleted, userState);
        }
        
        private void OnGetTripsByUserOperationCompleted(object arg) {
            if ((this.GetTripsByUserCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetTripsByUserCompleted(this, new GetTripsByUserCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://gpsgate.com/services/GetTrackDataChunk", RequestNamespace="http://gpsgate.com/services/", ResponseNamespace="http://gpsgate.com/services/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode GetTrackDataChunk(string strSessionID, int iApplicationID, int iTrackInfoID, System.DateTime dtStart, System.DateTime dtEnd, int iStartIndex, int iStopIndex) {
            object[] results = this.Invoke("GetTrackDataChunk", new object[] {
                        strSessionID,
                        iApplicationID,
                        iTrackInfoID,
                        dtStart,
                        dtEnd,
                        iStartIndex,
                        iStopIndex});
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        public void GetTrackDataChunkAsync(string strSessionID, int iApplicationID, int iTrackInfoID, System.DateTime dtStart, System.DateTime dtEnd, int iStartIndex, int iStopIndex) {
            this.GetTrackDataChunkAsync(strSessionID, iApplicationID, iTrackInfoID, dtStart, dtEnd, iStartIndex, iStopIndex, null);
        }
        
        /// <remarks/>
        public void GetTrackDataChunkAsync(string strSessionID, int iApplicationID, int iTrackInfoID, System.DateTime dtStart, System.DateTime dtEnd, int iStartIndex, int iStopIndex, object userState) {
            if ((this.GetTrackDataChunkOperationCompleted == null)) {
                this.GetTrackDataChunkOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetTrackDataChunkOperationCompleted);
            }
            this.InvokeAsync("GetTrackDataChunk", new object[] {
                        strSessionID,
                        iApplicationID,
                        iTrackInfoID,
                        dtStart,
                        dtEnd,
                        iStartIndex,
                        iStopIndex}, this.GetTrackDataChunkOperationCompleted, userState);
        }
        
        private void OnGetTrackDataChunkOperationCompleted(object arg) {
            if ((this.GetTrackDataChunkCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetTrackDataChunkCompleted(this, new GetTrackDataChunkCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://gpsgate.com/services/GetFatPoints", RequestNamespace="http://gpsgate.com/services/", ResponseNamespace="http://gpsgate.com/services/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode GetFatPoints(string strSessionID, int iApplicationID, int iTrackInfoID, System.DateTime dtStart, System.DateTime dtEnd) {
            object[] results = this.Invoke("GetFatPoints", new object[] {
                        strSessionID,
                        iApplicationID,
                        iTrackInfoID,
                        dtStart,
                        dtEnd});
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        public void GetFatPointsAsync(string strSessionID, int iApplicationID, int iTrackInfoID, System.DateTime dtStart, System.DateTime dtEnd) {
            this.GetFatPointsAsync(strSessionID, iApplicationID, iTrackInfoID, dtStart, dtEnd, null);
        }
        
        /// <remarks/>
        public void GetFatPointsAsync(string strSessionID, int iApplicationID, int iTrackInfoID, System.DateTime dtStart, System.DateTime dtEnd, object userState) {
            if ((this.GetFatPointsOperationCompleted == null)) {
                this.GetFatPointsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetFatPointsOperationCompleted);
            }
            this.InvokeAsync("GetFatPoints", new object[] {
                        strSessionID,
                        iApplicationID,
                        iTrackInfoID,
                        dtStart,
                        dtEnd}, this.GetFatPointsOperationCompleted, userState);
        }
        
        private void OnGetFatPointsOperationCompleted(object arg) {
            if ((this.GetFatPointsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetFatPointsCompleted(this, new GetFatPointsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://gpsgate.com/services/GetLifeRecorderByUser", RequestNamespace="http://gpsgate.com/services/", ResponseNamespace="http://gpsgate.com/services/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode GetLifeRecorderByUser(string strSessionID, int iApplicationID, int iOwnerID) {
            object[] results = this.Invoke("GetLifeRecorderByUser", new object[] {
                        strSessionID,
                        iApplicationID,
                        iOwnerID});
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        public void GetLifeRecorderByUserAsync(string strSessionID, int iApplicationID, int iOwnerID) {
            this.GetLifeRecorderByUserAsync(strSessionID, iApplicationID, iOwnerID, null);
        }
        
        /// <remarks/>
        public void GetLifeRecorderByUserAsync(string strSessionID, int iApplicationID, int iOwnerID, object userState) {
            if ((this.GetLifeRecorderByUserOperationCompleted == null)) {
                this.GetLifeRecorderByUserOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetLifeRecorderByUserOperationCompleted);
            }
            this.InvokeAsync("GetLifeRecorderByUser", new object[] {
                        strSessionID,
                        iApplicationID,
                        iOwnerID}, this.GetLifeRecorderByUserOperationCompleted, userState);
        }
        
        private void OnGetLifeRecorderByUserOperationCompleted(object arg) {
            if ((this.GetLifeRecorderByUserCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetLifeRecorderByUserCompleted(this, new GetLifeRecorderByUserCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://gpsgate.com/services/SetLifeTrackRecorderFilters", RequestNamespace="http://gpsgate.com/services/", ResponseNamespace="http://gpsgate.com/services/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode SetLifeTrackRecorderFilters(string strSessionID, int iApplicationID, int iOwnerID, double dblTimeFilter, double dblDistanceFilter, double dblRestartInterval, double dblRestartIntervalOffset, double dblDirectionFilter, double dblDirectionThreshold, double dblSpeedFilter, double dblRestartTime, double dblRestartDistance, double dblSmsTimeFilter, bool bMotion) {
            object[] results = this.Invoke("SetLifeTrackRecorderFilters", new object[] {
                        strSessionID,
                        iApplicationID,
                        iOwnerID,
                        dblTimeFilter,
                        dblDistanceFilter,
                        dblRestartInterval,
                        dblRestartIntervalOffset,
                        dblDirectionFilter,
                        dblDirectionThreshold,
                        dblSpeedFilter,
                        dblRestartTime,
                        dblRestartDistance,
                        dblSmsTimeFilter,
                        bMotion});
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        public void SetLifeTrackRecorderFiltersAsync(string strSessionID, int iApplicationID, int iOwnerID, double dblTimeFilter, double dblDistanceFilter, double dblRestartInterval, double dblRestartIntervalOffset, double dblDirectionFilter, double dblDirectionThreshold, double dblSpeedFilter, double dblRestartTime, double dblRestartDistance, double dblSmsTimeFilter, bool bMotion) {
            this.SetLifeTrackRecorderFiltersAsync(strSessionID, iApplicationID, iOwnerID, dblTimeFilter, dblDistanceFilter, dblRestartInterval, dblRestartIntervalOffset, dblDirectionFilter, dblDirectionThreshold, dblSpeedFilter, dblRestartTime, dblRestartDistance, dblSmsTimeFilter, bMotion, null);
        }
        
        /// <remarks/>
        public void SetLifeTrackRecorderFiltersAsync(string strSessionID, int iApplicationID, int iOwnerID, double dblTimeFilter, double dblDistanceFilter, double dblRestartInterval, double dblRestartIntervalOffset, double dblDirectionFilter, double dblDirectionThreshold, double dblSpeedFilter, double dblRestartTime, double dblRestartDistance, double dblSmsTimeFilter, bool bMotion, object userState) {
            if ((this.SetLifeTrackRecorderFiltersOperationCompleted == null)) {
                this.SetLifeTrackRecorderFiltersOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSetLifeTrackRecorderFiltersOperationCompleted);
            }
            this.InvokeAsync("SetLifeTrackRecorderFilters", new object[] {
                        strSessionID,
                        iApplicationID,
                        iOwnerID,
                        dblTimeFilter,
                        dblDistanceFilter,
                        dblRestartInterval,
                        dblRestartIntervalOffset,
                        dblDirectionFilter,
                        dblDirectionThreshold,
                        dblSpeedFilter,
                        dblRestartTime,
                        dblRestartDistance,
                        dblSmsTimeFilter,
                        bMotion}, this.SetLifeTrackRecorderFiltersOperationCompleted, userState);
        }
        
        private void OnSetLifeTrackRecorderFiltersOperationCompleted(object arg) {
            if ((this.SetLifeTrackRecorderFiltersCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SetLifeTrackRecorderFiltersCompleted(this, new SetLifeTrackRecorderFiltersCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://gpsgate.com/services/DeleteTrack", RequestNamespace="http://gpsgate.com/services/", ResponseNamespace="http://gpsgate.com/services/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Xml.XmlNode DeleteTrack(string strSessionID, int iApplicationID, int iTrackInfoID) {
            object[] results = this.Invoke("DeleteTrack", new object[] {
                        strSessionID,
                        iApplicationID,
                        iTrackInfoID});
            return ((System.Xml.XmlNode)(results[0]));
        }
        
        /// <remarks/>
        public void DeleteTrackAsync(string strSessionID, int iApplicationID, int iTrackInfoID) {
            this.DeleteTrackAsync(strSessionID, iApplicationID, iTrackInfoID, null);
        }
        
        /// <remarks/>
        public void DeleteTrackAsync(string strSessionID, int iApplicationID, int iTrackInfoID, object userState) {
            if ((this.DeleteTrackOperationCompleted == null)) {
                this.DeleteTrackOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDeleteTrackOperationCompleted);
            }
            this.InvokeAsync("DeleteTrack", new object[] {
                        strSessionID,
                        iApplicationID,
                        iTrackInfoID}, this.DeleteTrackOperationCompleted, userState);
        }
        
        private void OnDeleteTrackOperationCompleted(object arg) {
            if ((this.DeleteTrackCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DeleteTrackCompleted(this, new DeleteTrackCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void GetTracksByUsersCompletedEventHandler(object sender, GetTracksByUsersCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetTracksByUsersCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetTracksByUsersCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Xml.XmlNode Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Xml.XmlNode)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void GetTripsByUserCompletedEventHandler(object sender, GetTripsByUserCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetTripsByUserCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetTripsByUserCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Xml.XmlNode Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Xml.XmlNode)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void GetTrackDataChunkCompletedEventHandler(object sender, GetTrackDataChunkCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetTrackDataChunkCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetTrackDataChunkCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Xml.XmlNode Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Xml.XmlNode)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void GetFatPointsCompletedEventHandler(object sender, GetFatPointsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetFatPointsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetFatPointsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Xml.XmlNode Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Xml.XmlNode)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void GetLifeRecorderByUserCompletedEventHandler(object sender, GetLifeRecorderByUserCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetLifeRecorderByUserCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetLifeRecorderByUserCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Xml.XmlNode Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Xml.XmlNode)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void SetLifeTrackRecorderFiltersCompletedEventHandler(object sender, SetLifeTrackRecorderFiltersCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SetLifeTrackRecorderFiltersCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SetLifeTrackRecorderFiltersCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Xml.XmlNode Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Xml.XmlNode)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void DeleteTrackCompletedEventHandler(object sender, DeleteTrackCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DeleteTrackCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DeleteTrackCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Xml.XmlNode Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Xml.XmlNode)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591