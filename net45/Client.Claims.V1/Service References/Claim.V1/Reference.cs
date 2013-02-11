﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18033
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gecko.NCore.Client.Claim.V1 {
    using System.Runtime.Serialization;
    using System;
    
    
    /// <summary>
    /// Defines the identity of the user executing the request.
    /// </summary>
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
        
        /// <summary>
        /// Gets or sets the database.
        /// </summary>
        /// <value>The database.</value>
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
        
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
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
        
        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        /// <value>The role.</value>
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
        
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>The username.</value>
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
        
        /// <summary>
        /// Gets or sets the name of the external system.
        /// </summary>
        /// <value>The name of the external system.</value>
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
    
    /// <summary>
    /// </summary>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Identifier", Namespace="http://www.gecko.no/ephorte/services/claim/v1")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Gecko.NCore.Client.Claim.V1.EphorteIdentity))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Gecko.NCore.Client.Claim.V1.Identifier[]))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Gecko.NCore.Client.Claim.V1.DataObjectClaims[]))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Gecko.NCore.Client.Claim.V1.DataObjectClaims))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Gecko.NCore.Client.Claim.V1.EphorteClaim[]))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Gecko.NCore.Client.Claim.V1.EphorteClaim))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(object[]))]
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
        
        /// <summary>
        /// Gets or sets the keys.
        /// </summary>
        /// <value>The keys.</value>
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
        
        /// <summary>
        /// Gets or sets the type of the object.
        /// </summary>
        /// <value>The type of the object.</value>
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
    
    /// <summary>
    /// </summary>
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
        
        /// <summary>
        /// </summary>
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
        
        /// <summary>
        /// </summary>
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
    
    /// <summary>
    /// </summary>
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
        
        /// <summary>
        /// </summary>
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
    
    /// <summary>
    /// Defines the Claim service contract
    /// 
    /// The claim services provides an interface to look up claims for
    /// specific users or data objects (Sak, Journalpost etc..)
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.gecko.no/ephorte/services/claim/v1", ConfigurationName="Claim.V1.ClaimService")]
    public interface ClaimService {
        
        /// <summary>
        /// Returns the <see cref="T:EphorteClaim" />s for each data object referenced by the supplied identifiers
        /// </summary>
        /// <param name="identity">Login identity</param>
        /// <param name="identifiers">Data object identifiers that reference specific data objects (Sak, Journalpost etc..)</param>
        /// <returns>Claims for each of the supplied identifiers</returns>
        [System.ServiceModel.OperationContractAttribute(Action="http://www.gecko.no/ephorte/services/claim/v1/ClaimService/GetDataObjectClaims", ReplyAction="http://www.gecko.no/ephorte/services/claim/v1/ClaimService/GetDataObjectClaimsRes" +
            "ponse")]
        Gecko.NCore.Client.Claim.V1.DataObjectClaims[] GetDataObjectClaims(Gecko.NCore.Client.Claim.V1.EphorteIdentity identity, Gecko.NCore.Client.Claim.V1.Identifier[] identifiers);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.gecko.no/ephorte/services/claim/v1/ClaimService/GetDataObjectClaims", ReplyAction="http://www.gecko.no/ephorte/services/claim/v1/ClaimService/GetDataObjectClaimsRes" +
            "ponse")]
        System.Threading.Tasks.Task<Gecko.NCore.Client.Claim.V1.DataObjectClaims[]> GetDataObjectClaimsAsync(Gecko.NCore.Client.Claim.V1.EphorteIdentity identity, Gecko.NCore.Client.Claim.V1.Identifier[] identifiers);
        
        /// <summary>
        /// Returns the <see cref="T:EphorteClaim" />s a specific user is authorizied to.
        /// </summary>
        /// <param name="identity">Login identity</param>
        /// <param name="userId">The user id</param>
        /// <returns>The users claims</returns>
        [System.ServiceModel.OperationContractAttribute(Action="http://www.gecko.no/ephorte/services/claim/v1/ClaimService/GetUserClaims", ReplyAction="http://www.gecko.no/ephorte/services/claim/v1/ClaimService/GetUserClaimsResponse")]
        Gecko.NCore.Client.Claim.V1.EphorteClaim[] GetUserClaims(Gecko.NCore.Client.Claim.V1.EphorteIdentity identity, int userId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.gecko.no/ephorte/services/claim/v1/ClaimService/GetUserClaims", ReplyAction="http://www.gecko.no/ephorte/services/claim/v1/ClaimService/GetUserClaimsResponse")]
        System.Threading.Tasks.Task<Gecko.NCore.Client.Claim.V1.EphorteClaim[]> GetUserClaimsAsync(Gecko.NCore.Client.Claim.V1.EphorteIdentity identity, int userId);
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
        
        public System.Threading.Tasks.Task<Gecko.NCore.Client.Claim.V1.DataObjectClaims[]> GetDataObjectClaimsAsync(Gecko.NCore.Client.Claim.V1.EphorteIdentity identity, Gecko.NCore.Client.Claim.V1.Identifier[] identifiers) {
            return base.Channel.GetDataObjectClaimsAsync(identity, identifiers);
        }
        
        public Gecko.NCore.Client.Claim.V1.EphorteClaim[] GetUserClaims(Gecko.NCore.Client.Claim.V1.EphorteIdentity identity, int userId) {
            return base.Channel.GetUserClaims(identity, userId);
        }
        
        public System.Threading.Tasks.Task<Gecko.NCore.Client.Claim.V1.EphorteClaim[]> GetUserClaimsAsync(Gecko.NCore.Client.Claim.V1.EphorteIdentity identity, int userId) {
            return base.Channel.GetUserClaimsAsync(identity, userId);
        }
    }
}
