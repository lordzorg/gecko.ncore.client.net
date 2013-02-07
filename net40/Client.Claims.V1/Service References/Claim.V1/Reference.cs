﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18010
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gecko.NCore.Client.Claim.V1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="EphorteIdentity", Namespace="http://www.gecko.no/ephorte/services/claim/v1")]
    [System.SerializableAttribute()]
    public partial class EphorteIdentity : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DatabaseField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PasswordField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string RoleField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ExternalSystemNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Database {
            get {
                return this.DatabaseField;
            }
            set {
                if ((object.ReferenceEquals(this.DatabaseField, value) != true)) {
                    this.DatabaseField = value;
                    this.RaisePropertyChanged("Database");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Password {
            get {
                return this.PasswordField;
            }
            set {
                if ((object.ReferenceEquals(this.PasswordField, value) != true)) {
                    this.PasswordField = value;
                    this.RaisePropertyChanged("Password");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Role {
            get {
                return this.RoleField;
            }
            set {
                if ((object.ReferenceEquals(this.RoleField, value) != true)) {
                    this.RoleField = value;
                    this.RaisePropertyChanged("Role");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserName {
            get {
                return this.UserNameField;
            }
            set {
                if ((object.ReferenceEquals(this.UserNameField, value) != true)) {
                    this.UserNameField = value;
                    this.RaisePropertyChanged("UserName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public string ExternalSystemName {
            get {
                return this.ExternalSystemNameField;
            }
            set {
                if ((object.ReferenceEquals(this.ExternalSystemNameField, value) != true)) {
                    this.ExternalSystemNameField = value;
                    this.RaisePropertyChanged("ExternalSystemName");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="Identifier", Namespace="http://www.gecko.no/ephorte/services/claim/v1")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(object[]))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Gecko.NCore.Client.Claim.V1.EphorteIdentity))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Gecko.NCore.Client.Claim.V1.Identifier[]))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Gecko.NCore.Client.Claim.V1.DataObjectClaims[]))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Gecko.NCore.Client.Claim.V1.DataObjectClaims))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Gecko.NCore.Client.Claim.V1.EphorteClaim[]))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Gecko.NCore.Client.Claim.V1.EphorteClaim))]
    public partial class Identifier : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private object[] KeysField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ObjectTypeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public object[] Keys {
            get {
                return this.KeysField;
            }
            set {
                if ((object.ReferenceEquals(this.KeysField, value) != true)) {
                    this.KeysField = value;
                    this.RaisePropertyChanged("Keys");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ObjectType {
            get {
                return this.ObjectTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.ObjectTypeField, value) != true)) {
                    this.ObjectTypeField = value;
                    this.RaisePropertyChanged("ObjectType");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="DataObjectClaims", Namespace="http://www.gecko.no/ephorte/services/claim/v1")]
    [System.SerializableAttribute()]
    public partial class DataObjectClaims : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Gecko.NCore.Client.Claim.V1.EphorteClaim[] ClaimsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Gecko.NCore.Client.Claim.V1.Identifier IdentifierField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Gecko.NCore.Client.Claim.V1.EphorteClaim[] Claims {
            get {
                return this.ClaimsField;
            }
            set {
                if ((object.ReferenceEquals(this.ClaimsField, value) != true)) {
                    this.ClaimsField = value;
                    this.RaisePropertyChanged("Claims");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Gecko.NCore.Client.Claim.V1.Identifier Identifier {
            get {
                return this.IdentifierField;
            }
            set {
                if ((object.ReferenceEquals(this.IdentifierField, value) != true)) {
                    this.IdentifierField = value;
                    this.RaisePropertyChanged("Identifier");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="EphorteClaim", Namespace="http://www.gecko.no/ephorte/services/claim/v1")]
    [System.SerializableAttribute()]
    public partial class EphorteClaim : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ValueField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Value {
            get {
                return this.ValueField;
            }
            set {
                if ((object.ReferenceEquals(this.ValueField, value) != true)) {
                    this.ValueField = value;
                    this.RaisePropertyChanged("Value");
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
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.gecko.no/ephorte/services/claim/v1", ConfigurationName="Claim.V1.ClaimService")]
    public interface ClaimService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.gecko.no/ephorte/services/claim/v1/ClaimService/GetDataObjectClaims", ReplyAction="http://www.gecko.no/ephorte/services/claim/v1/ClaimService/GetDataObjectClaimsRes" +
            "ponse")]
        Gecko.NCore.Client.Claim.V1.DataObjectClaims[] GetDataObjectClaims(Gecko.NCore.Client.Claim.V1.EphorteIdentity identity, Gecko.NCore.Client.Claim.V1.Identifier[] identifiers);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.gecko.no/ephorte/services/claim/v1/ClaimService/GetUserClaims", ReplyAction="http://www.gecko.no/ephorte/services/claim/v1/ClaimService/GetUserClaimsResponse")]
        Gecko.NCore.Client.Claim.V1.EphorteClaim[] GetUserClaims(Gecko.NCore.Client.Claim.V1.EphorteIdentity identity, int userId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ClaimServiceChannel : Gecko.NCore.Client.Claim.V1.ClaimService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ClaimServiceClient : System.ServiceModel.ClientBase<Gecko.NCore.Client.Claim.V1.ClaimService>, Gecko.NCore.Client.Claim.V1.ClaimService {
        
        public ClaimServiceClient() {
        }
        
        public ClaimServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ClaimServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ClaimServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ClaimServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Gecko.NCore.Client.Claim.V1.DataObjectClaims[] GetDataObjectClaims(Gecko.NCore.Client.Claim.V1.EphorteIdentity identity, Gecko.NCore.Client.Claim.V1.Identifier[] identifiers) {
            return base.Channel.GetDataObjectClaims(identity, identifiers);
        }
        
        public Gecko.NCore.Client.Claim.V1.EphorteClaim[] GetUserClaims(Gecko.NCore.Client.Claim.V1.EphorteIdentity identity, int userId) {
            return base.Channel.GetUserClaims(identity, userId);
        }
    }
}
