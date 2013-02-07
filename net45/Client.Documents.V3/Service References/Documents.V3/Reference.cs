﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18010
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gecko.NCore.Client.Documents.V3 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="EphorteIdentity", Namespace="http://www.gecko.no/ephorte/services/documents/v3")]
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
    [System.Runtime.Serialization.DataContractAttribute(Name="DocumentCriteria", Namespace="http://www.gecko.no/ephorte/services/documents/v3")]
    [System.SerializableAttribute()]
    public partial class DocumentCriteria : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int DocumentIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Gecko.NCore.Client.Documents.V3.EphorteIdentity EphorteIdentityField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string VariantField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int VersionField;
        
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
        public int DocumentId {
            get {
                return this.DocumentIdField;
            }
            set {
                if ((this.DocumentIdField.Equals(value) != true)) {
                    this.DocumentIdField = value;
                    this.RaisePropertyChanged("DocumentId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Gecko.NCore.Client.Documents.V3.EphorteIdentity EphorteIdentity {
            get {
                return this.EphorteIdentityField;
            }
            set {
                if ((object.ReferenceEquals(this.EphorteIdentityField, value) != true)) {
                    this.EphorteIdentityField = value;
                    this.RaisePropertyChanged("EphorteIdentity");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Variant {
            get {
                return this.VariantField;
            }
            set {
                if ((object.ReferenceEquals(this.VariantField, value) != true)) {
                    this.VariantField = value;
                    this.RaisePropertyChanged("Variant");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Version {
            get {
                return this.VersionField;
            }
            set {
                if ((this.VersionField.Equals(value) != true)) {
                    this.VersionField = value;
                    this.RaisePropertyChanged("Version");
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
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.gecko.no/ephorte/services/documents/v3", ConfigurationName="Documents.V3.DocumentService")]
    public interface DocumentService {
        
        [System.ServiceModel.OperationContractAttribute(Action="UploadFile", ReplyAction="http://www.gecko.no/ephorte/services/documents/v3/DocumentService/UploadFileRespo" +
            "nse")]
        Gecko.NCore.Client.Documents.V3.UploadReturnMessage UploadFile(Gecko.NCore.Client.Documents.V3.UploadMessage request);
        
        [System.ServiceModel.OperationContractAttribute(Action="UploadFile", ReplyAction="http://www.gecko.no/ephorte/services/documents/v3/DocumentService/UploadFileRespo" +
            "nse")]
        System.Threading.Tasks.Task<Gecko.NCore.Client.Documents.V3.UploadReturnMessage> UploadFileAsync(Gecko.NCore.Client.Documents.V3.UploadMessage request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.gecko.no/ephorte/services/documents/v3/DocumentService/Checkin", ReplyAction="http://www.gecko.no/ephorte/services/documents/v3/DocumentService/CheckinResponse" +
            "")]
        Gecko.NCore.Client.Documents.V3.CheckinResponse Checkin(Gecko.NCore.Client.Documents.V3.CheckinMessage request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.gecko.no/ephorte/services/documents/v3/DocumentService/Checkin", ReplyAction="http://www.gecko.no/ephorte/services/documents/v3/DocumentService/CheckinResponse" +
            "")]
        System.Threading.Tasks.Task<Gecko.NCore.Client.Documents.V3.CheckinResponse> CheckinAsync(Gecko.NCore.Client.Documents.V3.CheckinMessage request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.gecko.no/ephorte/services/documents/v3/DocumentService/CancelCheckout", ReplyAction="http://www.gecko.no/ephorte/services/documents/v3/DocumentService/CancelCheckoutR" +
            "esponse")]
        Gecko.NCore.Client.Documents.V3.CancelCheckoutResponse CancelCheckout(Gecko.NCore.Client.Documents.V3.CancelCheckoutRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.gecko.no/ephorte/services/documents/v3/DocumentService/CancelCheckout", ReplyAction="http://www.gecko.no/ephorte/services/documents/v3/DocumentService/CancelCheckoutR" +
            "esponse")]
        System.Threading.Tasks.Task<Gecko.NCore.Client.Documents.V3.CancelCheckoutResponse> CancelCheckoutAsync(Gecko.NCore.Client.Documents.V3.CancelCheckoutRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.gecko.no/ephorte/services/documents/v3/DocumentService/Checkout", ReplyAction="http://www.gecko.no/ephorte/services/documents/v3/DocumentService/CheckoutRespons" +
            "e")]
        Gecko.NCore.Client.Documents.V3.CheckoutResponseMessage Checkout(Gecko.NCore.Client.Documents.V3.CheckoutRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.gecko.no/ephorte/services/documents/v3/DocumentService/Checkout", ReplyAction="http://www.gecko.no/ephorte/services/documents/v3/DocumentService/CheckoutRespons" +
            "e")]
        System.Threading.Tasks.Task<Gecko.NCore.Client.Documents.V3.CheckoutResponseMessage> CheckoutAsync(Gecko.NCore.Client.Documents.V3.CheckoutRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.gecko.no/ephorte/services/documents/v3/DocumentService/GetDocumentCont" +
            "entBase", ReplyAction="http://www.gecko.no/ephorte/services/documents/v3/DocumentService/GetDocumentCont" +
            "entBaseResponse")]
        Gecko.NCore.Client.Documents.V3.DocumentReturnMessage GetDocumentContentBase(Gecko.NCore.Client.Documents.V3.GetDocumentContentMessage request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.gecko.no/ephorte/services/documents/v3/DocumentService/GetDocumentCont" +
            "entBase", ReplyAction="http://www.gecko.no/ephorte/services/documents/v3/DocumentService/GetDocumentCont" +
            "entBaseResponse")]
        System.Threading.Tasks.Task<Gecko.NCore.Client.Documents.V3.DocumentReturnMessage> GetDocumentContentBaseAsync(Gecko.NCore.Client.Documents.V3.GetDocumentContentMessage request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.gecko.no/ephorte/services/documents/v3/DocumentService/GetDocumentCont" +
            "entByJournalPostId", ReplyAction="http://www.gecko.no/ephorte/services/documents/v3/DocumentService/GetDocumentCont" +
            "entByJournalPostIdResponse")]
        Gecko.NCore.Client.Documents.V3.DocumentReturnMessage GetDocumentContentByJournalPostId(Gecko.NCore.Client.Documents.V3.GetJournalpostDocumentContentMessage request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.gecko.no/ephorte/services/documents/v3/DocumentService/GetDocumentCont" +
            "entByJournalPostId", ReplyAction="http://www.gecko.no/ephorte/services/documents/v3/DocumentService/GetDocumentCont" +
            "entByJournalPostIdResponse")]
        System.Threading.Tasks.Task<Gecko.NCore.Client.Documents.V3.DocumentReturnMessage> GetDocumentContentByJournalPostIdAsync(Gecko.NCore.Client.Documents.V3.GetJournalpostDocumentContentMessage request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.gecko.no/ephorte/services/documents/v3/DocumentService/GetDocumentCont" +
            "entByMoId", ReplyAction="http://www.gecko.no/ephorte/services/documents/v3/DocumentService/GetDocumentCont" +
            "entByMoIdResponse")]
        Gecko.NCore.Client.Documents.V3.DocumentReturnMessage GetDocumentContentByMoId(Gecko.NCore.Client.Documents.V3.GetMoeteDocumentContentMessage request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.gecko.no/ephorte/services/documents/v3/DocumentService/GetDocumentCont" +
            "entByMoId", ReplyAction="http://www.gecko.no/ephorte/services/documents/v3/DocumentService/GetDocumentCont" +
            "entByMoIdResponse")]
        System.Threading.Tasks.Task<Gecko.NCore.Client.Documents.V3.DocumentReturnMessage> GetDocumentContentByMoIdAsync(Gecko.NCore.Client.Documents.V3.GetMoeteDocumentContentMessage request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.gecko.no/ephorte/services/documents/v3/DocumentService/GetDocumentCont" +
            "entByUbId", ReplyAction="http://www.gecko.no/ephorte/services/documents/v3/DocumentService/GetDocumentCont" +
            "entByUbIdResponse")]
        Gecko.NCore.Client.Documents.V3.DocumentReturnMessage GetDocumentContentByUbId(Gecko.NCore.Client.Documents.V3.GetUtvalgDocumentContentMessage request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.gecko.no/ephorte/services/documents/v3/DocumentService/GetDocumentCont" +
            "entByUbId", ReplyAction="http://www.gecko.no/ephorte/services/documents/v3/DocumentService/GetDocumentCont" +
            "entByUbIdResponse")]
        System.Threading.Tasks.Task<Gecko.NCore.Client.Documents.V3.DocumentReturnMessage> GetDocumentContentByUbIdAsync(Gecko.NCore.Client.Documents.V3.GetUtvalgDocumentContentMessage request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="UploadMessage", WrapperNamespace="http://www.gecko.no/ephorte/services/documents/v3", IsWrapped=true)]
    public partial class UploadMessage {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.gecko.no/ephorte/services/documents/v3")]
        public Gecko.NCore.Client.Documents.V3.EphorteIdentity EphorteIdentity;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.gecko.no/ephorte/services/documents/v3")]
        public string FileName;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.gecko.no/ephorte/services/documents/v3")]
        public string StorageIdentifier;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.gecko.no/ephorte/services/documents/v3", Order=0)]
        public System.IO.Stream Content;
        
        public UploadMessage() {
        }
        
        public UploadMessage(Gecko.NCore.Client.Documents.V3.EphorteIdentity EphorteIdentity, string FileName, string StorageIdentifier, System.IO.Stream Content) {
            this.EphorteIdentity = EphorteIdentity;
            this.FileName = FileName;
            this.StorageIdentifier = StorageIdentifier;
            this.Content = Content;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="UploadReturnMessage", WrapperNamespace="http://www.gecko.no/ephorte/services/documents/v3", IsWrapped=true)]
    public partial class UploadReturnMessage {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.gecko.no/ephorte/services/documents/v3")]
        public string FileName;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.gecko.no/ephorte/services/documents/v3")]
        public string Identifier;
        
        public UploadReturnMessage() {
        }
        
        public UploadReturnMessage(string FileName, string Identifier) {
            this.FileName = FileName;
            this.Identifier = Identifier;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="CheckinMessage", WrapperNamespace="http://www.gecko.no/ephorte/services/documents/v3", IsWrapped=true)]
    public partial class CheckinMessage {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.gecko.no/ephorte/services/documents/v3")]
        public Gecko.NCore.Client.Documents.V3.DocumentCriteria DocumentCriteria;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.gecko.no/ephorte/services/documents/v3")]
        public System.Guid Guid;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.gecko.no/ephorte/services/documents/v3")]
        public string Path;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.gecko.no/ephorte/services/documents/v3", Order=0)]
        public System.IO.Stream Content;
        
        public CheckinMessage() {
        }
        
        public CheckinMessage(Gecko.NCore.Client.Documents.V3.DocumentCriteria DocumentCriteria, System.Guid Guid, string Path, System.IO.Stream Content) {
            this.DocumentCriteria = DocumentCriteria;
            this.Guid = Guid;
            this.Path = Path;
            this.Content = Content;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class CheckinResponse {
        
        public CheckinResponse() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="CancelCheckoutRequest", WrapperNamespace="http://www.gecko.no/ephorte/services/documents/v3", IsWrapped=true)]
    public partial class CancelCheckoutRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.gecko.no/ephorte/services/documents/v3")]
        public int CommitteeHandlingDocumentId;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.gecko.no/ephorte/services/documents/v3")]
        public int DocumentId;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.gecko.no/ephorte/services/documents/v3")]
        public Gecko.NCore.Client.Documents.V3.EphorteIdentity Identity;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.gecko.no/ephorte/services/documents/v3")]
        public int JournalpostId;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.gecko.no/ephorte/services/documents/v3")]
        public int MeetingDocumentId;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.gecko.no/ephorte/services/documents/v3")]
        public string Variant;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.gecko.no/ephorte/services/documents/v3")]
        public int Version;
        
        public CancelCheckoutRequest() {
        }
        
        public CancelCheckoutRequest(int CommitteeHandlingDocumentId, int DocumentId, Gecko.NCore.Client.Documents.V3.EphorteIdentity Identity, int JournalpostId, int MeetingDocumentId, string Variant, int Version) {
            this.CommitteeHandlingDocumentId = CommitteeHandlingDocumentId;
            this.DocumentId = DocumentId;
            this.Identity = Identity;
            this.JournalpostId = JournalpostId;
            this.MeetingDocumentId = MeetingDocumentId;
            this.Variant = Variant;
            this.Version = Version;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class CancelCheckoutResponse {
        
        public CancelCheckoutResponse() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="CheckoutRequest", WrapperNamespace="http://www.gecko.no/ephorte/services/documents/v3", IsWrapped=true)]
    public partial class CheckoutRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.gecko.no/ephorte/services/documents/v3")]
        public int DocumentId;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.gecko.no/ephorte/services/documents/v3")]
        public Gecko.NCore.Client.Documents.V3.EphorteIdentity Identity;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.gecko.no/ephorte/services/documents/v3")]
        public string Variant;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.gecko.no/ephorte/services/documents/v3")]
        public int Version;
        
        public CheckoutRequest() {
        }
        
        public CheckoutRequest(int DocumentId, Gecko.NCore.Client.Documents.V3.EphorteIdentity Identity, string Variant, int Version) {
            this.DocumentId = DocumentId;
            this.Identity = Identity;
            this.Variant = Variant;
            this.Version = Version;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="CheckoutResponseMessage", WrapperNamespace="http://www.gecko.no/ephorte/services/documents/v3", IsWrapped=true)]
    public partial class CheckoutResponseMessage {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.gecko.no/ephorte/services/documents/v3", Order=0)]
        public System.IO.Stream Content;
        
        public CheckoutResponseMessage() {
        }
        
        public CheckoutResponseMessage(System.IO.Stream Content) {
            this.Content = Content;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetDocumentContentMessage", WrapperNamespace="http://www.gecko.no/ephorte/services/documents/v3", IsWrapped=true)]
    public partial class GetDocumentContentMessage {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.gecko.no/ephorte/services/documents/v3")]
        public int DocumentId;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.gecko.no/ephorte/services/documents/v3")]
        public Gecko.NCore.Client.Documents.V3.EphorteIdentity Identity;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.gecko.no/ephorte/services/documents/v3")]
        public string Variant;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.gecko.no/ephorte/services/documents/v3")]
        public int Version;
        
        public GetDocumentContentMessage() {
        }
        
        public GetDocumentContentMessage(int DocumentId, Gecko.NCore.Client.Documents.V3.EphorteIdentity Identity, string Variant, int Version) {
            this.DocumentId = DocumentId;
            this.Identity = Identity;
            this.Variant = Variant;
            this.Version = Version;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="DocumentReturnMessage", WrapperNamespace="http://www.gecko.no/ephorte/services/documents/v3", IsWrapped=true)]
    public partial class DocumentReturnMessage {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.gecko.no/ephorte/services/documents/v3")]
        public string FileName;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.gecko.no/ephorte/services/documents/v3", Order=0)]
        public System.IO.Stream Content;
        
        public DocumentReturnMessage() {
        }
        
        public DocumentReturnMessage(string FileName, System.IO.Stream Content) {
            this.FileName = FileName;
            this.Content = Content;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetJournalpostDocumentContentMessage", WrapperNamespace="http://www.gecko.no/ephorte/services/documents/v3", IsWrapped=true)]
    public partial class GetJournalpostDocumentContentMessage {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.gecko.no/ephorte/services/documents/v3")]
        public Gecko.NCore.Client.Documents.V3.EphorteIdentity Identity;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.gecko.no/ephorte/services/documents/v3")]
        public int JournalpostId;
        
        public GetJournalpostDocumentContentMessage() {
        }
        
        public GetJournalpostDocumentContentMessage(Gecko.NCore.Client.Documents.V3.EphorteIdentity Identity, int JournalpostId) {
            this.Identity = Identity;
            this.JournalpostId = JournalpostId;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetMoeteDocumentContentMessage", WrapperNamespace="http://www.gecko.no/ephorte/services/documents/v3", IsWrapped=true)]
    public partial class GetMoeteDocumentContentMessage {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.gecko.no/ephorte/services/documents/v3")]
        public string DocumentType;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.gecko.no/ephorte/services/documents/v3")]
        public Gecko.NCore.Client.Documents.V3.EphorteIdentity Identity;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.gecko.no/ephorte/services/documents/v3")]
        public int MoId;
        
        public GetMoeteDocumentContentMessage() {
        }
        
        public GetMoeteDocumentContentMessage(string DocumentType, Gecko.NCore.Client.Documents.V3.EphorteIdentity Identity, int MoId) {
            this.DocumentType = DocumentType;
            this.Identity = Identity;
            this.MoId = MoId;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="GetUtvalgDocumentContentMessage", WrapperNamespace="http://www.gecko.no/ephorte/services/documents/v3", IsWrapped=true)]
    public partial class GetUtvalgDocumentContentMessage {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.gecko.no/ephorte/services/documents/v3")]
        public Gecko.NCore.Client.Documents.V3.EphorteIdentity Identity;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.gecko.no/ephorte/services/documents/v3")]
        public string SakType;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.gecko.no/ephorte/services/documents/v3")]
        public int UbId;
        
        public GetUtvalgDocumentContentMessage() {
        }
        
        public GetUtvalgDocumentContentMessage(Gecko.NCore.Client.Documents.V3.EphorteIdentity Identity, string SakType, int UbId) {
            this.Identity = Identity;
            this.SakType = SakType;
            this.UbId = UbId;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface DocumentServiceChannel : Gecko.NCore.Client.Documents.V3.DocumentService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DocumentServiceClient : System.ServiceModel.ClientBase<Gecko.NCore.Client.Documents.V3.DocumentService>, Gecko.NCore.Client.Documents.V3.DocumentService {
        
        public DocumentServiceClient() {
        }
        
        public DocumentServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DocumentServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DocumentServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DocumentServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Gecko.NCore.Client.Documents.V3.UploadReturnMessage UploadFile(Gecko.NCore.Client.Documents.V3.UploadMessage request) {
            return base.Channel.UploadFile(request);
        }
        
        public System.Threading.Tasks.Task<Gecko.NCore.Client.Documents.V3.UploadReturnMessage> UploadFileAsync(Gecko.NCore.Client.Documents.V3.UploadMessage request) {
            return base.Channel.UploadFileAsync(request);
        }
        
        public Gecko.NCore.Client.Documents.V3.CheckinResponse Checkin(Gecko.NCore.Client.Documents.V3.CheckinMessage request) {
            return base.Channel.Checkin(request);
        }
        
        public System.Threading.Tasks.Task<Gecko.NCore.Client.Documents.V3.CheckinResponse> CheckinAsync(Gecko.NCore.Client.Documents.V3.CheckinMessage request) {
            return base.Channel.CheckinAsync(request);
        }
        
        public Gecko.NCore.Client.Documents.V3.CancelCheckoutResponse CancelCheckout(Gecko.NCore.Client.Documents.V3.CancelCheckoutRequest request) {
            return base.Channel.CancelCheckout(request);
        }
        
        public System.Threading.Tasks.Task<Gecko.NCore.Client.Documents.V3.CancelCheckoutResponse> CancelCheckoutAsync(Gecko.NCore.Client.Documents.V3.CancelCheckoutRequest request) {
            return base.Channel.CancelCheckoutAsync(request);
        }
        
        public Gecko.NCore.Client.Documents.V3.CheckoutResponseMessage Checkout(Gecko.NCore.Client.Documents.V3.CheckoutRequest request) {
            return base.Channel.Checkout(request);
        }
        
        public System.Threading.Tasks.Task<Gecko.NCore.Client.Documents.V3.CheckoutResponseMessage> CheckoutAsync(Gecko.NCore.Client.Documents.V3.CheckoutRequest request) {
            return base.Channel.CheckoutAsync(request);
        }
        
        public Gecko.NCore.Client.Documents.V3.DocumentReturnMessage GetDocumentContentBase(Gecko.NCore.Client.Documents.V3.GetDocumentContentMessage request) {
            return base.Channel.GetDocumentContentBase(request);
        }
        
        public System.Threading.Tasks.Task<Gecko.NCore.Client.Documents.V3.DocumentReturnMessage> GetDocumentContentBaseAsync(Gecko.NCore.Client.Documents.V3.GetDocumentContentMessage request) {
            return base.Channel.GetDocumentContentBaseAsync(request);
        }
        
        public Gecko.NCore.Client.Documents.V3.DocumentReturnMessage GetDocumentContentByJournalPostId(Gecko.NCore.Client.Documents.V3.GetJournalpostDocumentContentMessage request) {
            return base.Channel.GetDocumentContentByJournalPostId(request);
        }
        
        public System.Threading.Tasks.Task<Gecko.NCore.Client.Documents.V3.DocumentReturnMessage> GetDocumentContentByJournalPostIdAsync(Gecko.NCore.Client.Documents.V3.GetJournalpostDocumentContentMessage request) {
            return base.Channel.GetDocumentContentByJournalPostIdAsync(request);
        }
        
        public Gecko.NCore.Client.Documents.V3.DocumentReturnMessage GetDocumentContentByMoId(Gecko.NCore.Client.Documents.V3.GetMoeteDocumentContentMessage request) {
            return base.Channel.GetDocumentContentByMoId(request);
        }
        
        public System.Threading.Tasks.Task<Gecko.NCore.Client.Documents.V3.DocumentReturnMessage> GetDocumentContentByMoIdAsync(Gecko.NCore.Client.Documents.V3.GetMoeteDocumentContentMessage request) {
            return base.Channel.GetDocumentContentByMoIdAsync(request);
        }
        
        public Gecko.NCore.Client.Documents.V3.DocumentReturnMessage GetDocumentContentByUbId(Gecko.NCore.Client.Documents.V3.GetUtvalgDocumentContentMessage request) {
            return base.Channel.GetDocumentContentByUbId(request);
        }
        
        public System.Threading.Tasks.Task<Gecko.NCore.Client.Documents.V3.DocumentReturnMessage> GetDocumentContentByUbIdAsync(Gecko.NCore.Client.Documents.V3.GetUtvalgDocumentContentMessage request) {
            return base.Channel.GetDocumentContentByUbIdAsync(request);
        }
    }
}
