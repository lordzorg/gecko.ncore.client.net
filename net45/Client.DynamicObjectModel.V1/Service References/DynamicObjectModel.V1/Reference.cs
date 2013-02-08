﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18010
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gecko.NCore.Client.DynamicObjectModel.V1.DynamicObjectModel.V1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="EphorteIdentity", Namespace="http://www.gecko.no/ephorte/services/dynamicobjectmodel/v1")]
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
    [System.Runtime.Serialization.DataContractAttribute(Name="Task", Namespace="http://www.gecko.no/ephorte/services/dynamicobjectmodel/v1")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Gecko.NCore.Client.DynamicObjectModel.V1.DynamicObjectModel.V1.QueryCountTask))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Gecko.NCore.Client.DynamicObjectModel.V1.DynamicObjectModel.V1.QueryTask))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Gecko.NCore.Client.DynamicObjectModel.V1.DynamicObjectModel.V1.DeleteTask))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Gecko.NCore.Client.DynamicObjectModel.V1.DynamicObjectModel.V1.UpdateTask))]
    public partial class Task : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ObjectNameField;
        
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
        public string ObjectName {
            get {
                return this.ObjectNameField;
            }
            set {
                if ((object.ReferenceEquals(this.ObjectNameField, value) != true)) {
                    this.ObjectNameField = value;
                    this.RaisePropertyChanged("ObjectName");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="QueryCountTask", Namespace="http://www.gecko.no/ephorte/services/dynamicobjectmodel/v1")]
    [System.SerializableAttribute()]
    public partial class QueryCountTask : Gecko.NCore.Client.DynamicObjectModel.V1.DynamicObjectModel.V1.Task {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CriteriaField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Criteria {
            get {
                return this.CriteriaField;
            }
            set {
                if ((object.ReferenceEquals(this.CriteriaField, value) != true)) {
                    this.CriteriaField = value;
                    this.RaisePropertyChanged("Criteria");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="QueryTask", Namespace="http://www.gecko.no/ephorte/services/dynamicobjectmodel/v1")]
    [System.SerializableAttribute()]
    public partial class QueryTask : Gecko.NCore.Client.DynamicObjectModel.V1.DynamicObjectModel.V1.Task {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CriteriaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string[] OutputFieldsField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Criteria {
            get {
                return this.CriteriaField;
            }
            set {
                if ((object.ReferenceEquals(this.CriteriaField, value) != true)) {
                    this.CriteriaField = value;
                    this.RaisePropertyChanged("Criteria");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string[] OutputFields {
            get {
                return this.OutputFieldsField;
            }
            set {
                if ((object.ReferenceEquals(this.OutputFieldsField, value) != true)) {
                    this.OutputFieldsField = value;
                    this.RaisePropertyChanged("OutputFields");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DeleteTask", Namespace="http://www.gecko.no/ephorte/services/dynamicobjectmodel/v1")]
    [System.SerializableAttribute()]
    public partial class DeleteTask : Gecko.NCore.Client.DynamicObjectModel.V1.DynamicObjectModel.V1.Task {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CriteriaField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Criteria {
            get {
                return this.CriteriaField;
            }
            set {
                if ((object.ReferenceEquals(this.CriteriaField, value) != true)) {
                    this.CriteriaField = value;
                    this.RaisePropertyChanged("Criteria");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UpdateTask", Namespace="http://www.gecko.no/ephorte/services/dynamicobjectmodel/v1")]
    [System.SerializableAttribute()]
    public partial class UpdateTask : Gecko.NCore.Client.DynamicObjectModel.V1.DynamicObjectModel.V1.Task {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CriteriaField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Gecko.NCore.Client.DynamicObjectModel.V1.DynamicObjectModel.V1.DataObjectInput FieldsField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Criteria {
            get {
                return this.CriteriaField;
            }
            set {
                if ((object.ReferenceEquals(this.CriteriaField, value) != true)) {
                    this.CriteriaField = value;
                    this.RaisePropertyChanged("Criteria");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Gecko.NCore.Client.DynamicObjectModel.V1.DynamicObjectModel.V1.DataObjectInput Fields {
            get {
                return this.FieldsField;
            }
            set {
                if ((object.ReferenceEquals(this.FieldsField, value) != true)) {
                    this.FieldsField = value;
                    this.RaisePropertyChanged("Fields");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="DataObjectInput", Namespace="http://www.gecko.no/ephorte/services/dynamicobjectmodel/v1", ItemName="Field", KeyName="FieldName", ValueName="FieldValue")]
    [System.SerializableAttribute()]
    public class DataObjectInput : System.Collections.Generic.Dictionary<string, object> {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TaskResult", Namespace="http://www.gecko.no/ephorte/services/dynamicobjectmodel/v1")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Gecko.NCore.Client.DynamicObjectModel.V1.DynamicObjectModel.V1.DeleteTaskResult))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Gecko.NCore.Client.DynamicObjectModel.V1.DynamicObjectModel.V1.QueryCountTaskResult))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Gecko.NCore.Client.DynamicObjectModel.V1.DynamicObjectModel.V1.QueryTaskResult))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Gecko.NCore.Client.DynamicObjectModel.V1.DynamicObjectModel.V1.UpdateTaskResult))]
    public partial class TaskResult : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
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
    [System.Runtime.Serialization.DataContractAttribute(Name="DeleteTaskResult", Namespace="http://www.gecko.no/ephorte/services/dynamicobjectmodel/v1")]
    [System.SerializableAttribute()]
    public partial class DeleteTaskResult : Gecko.NCore.Client.DynamicObjectModel.V1.DynamicObjectModel.V1.TaskResult {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CountField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Count {
            get {
                return this.CountField;
            }
            set {
                if ((this.CountField.Equals(value) != true)) {
                    this.CountField = value;
                    this.RaisePropertyChanged("Count");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="QueryCountTaskResult", Namespace="http://www.gecko.no/ephorte/services/dynamicobjectmodel/v1")]
    [System.SerializableAttribute()]
    public partial class QueryCountTaskResult : Gecko.NCore.Client.DynamicObjectModel.V1.DynamicObjectModel.V1.TaskResult {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CountField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Count {
            get {
                return this.CountField;
            }
            set {
                if ((this.CountField.Equals(value) != true)) {
                    this.CountField = value;
                    this.RaisePropertyChanged("Count");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="QueryTaskResult", Namespace="http://www.gecko.no/ephorte/services/dynamicobjectmodel/v1")]
    [System.SerializableAttribute()]
    public partial class QueryTaskResult : Gecko.NCore.Client.DynamicObjectModel.V1.DynamicObjectModel.V1.TaskResult {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Gecko.NCore.Client.DynamicObjectModel.V1.DynamicObjectModel.V1.DataObjectOutput[] ResultSetField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Gecko.NCore.Client.DynamicObjectModel.V1.DynamicObjectModel.V1.DataObjectOutput[] ResultSet {
            get {
                return this.ResultSetField;
            }
            set {
                if ((object.ReferenceEquals(this.ResultSetField, value) != true)) {
                    this.ResultSetField = value;
                    this.RaisePropertyChanged("ResultSet");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UpdateTaskResult", Namespace="http://www.gecko.no/ephorte/services/dynamicobjectmodel/v1")]
    [System.SerializableAttribute()]
    public partial class UpdateTaskResult : Gecko.NCore.Client.DynamicObjectModel.V1.DynamicObjectModel.V1.TaskResult {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CountField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Count {
            get {
                return this.CountField;
            }
            set {
                if ((this.CountField.Equals(value) != true)) {
                    this.CountField = value;
                    this.RaisePropertyChanged("Count");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="DataObjectOutput", Namespace="http://www.gecko.no/ephorte/services/dynamicobjectmodel/v1", ItemName="Field", KeyName="FieldName", ValueName="FieldValue")]
    [System.SerializableAttribute()]
    public class DataObjectOutput : System.Collections.Generic.Dictionary<string, object> {
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.gecko.no/ephorte/services/dynamicobjectmodel/v1", ConfigurationName="DynamicObjectModel.V1.DynamicObjectModel")]
    public interface DynamicObjectModel {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.gecko.no/ephorte/services/dynamicobjectmodel/v1/DynamicObjectModel/Exe" +
            "cuteTask", ReplyAction="http://www.gecko.no/ephorte/services/dynamicobjectmodel/v1/DynamicObjectModel/Exe" +
            "cuteTaskResponse")]
        Gecko.NCore.Client.DynamicObjectModel.V1.DynamicObjectModel.V1.TaskResult[] ExecuteTask(Gecko.NCore.Client.DynamicObjectModel.V1.DynamicObjectModel.V1.EphorteIdentity identity, Gecko.NCore.Client.DynamicObjectModel.V1.DynamicObjectModel.V1.Task[] tasks);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.gecko.no/ephorte/services/dynamicobjectmodel/v1/DynamicObjectModel/Exe" +
            "cuteTask", ReplyAction="http://www.gecko.no/ephorte/services/dynamicobjectmodel/v1/DynamicObjectModel/Exe" +
            "cuteTaskResponse")]
        System.Threading.Tasks.Task<Gecko.NCore.Client.DynamicObjectModel.V1.DynamicObjectModel.V1.TaskResult[]> ExecuteTaskAsync(Gecko.NCore.Client.DynamicObjectModel.V1.DynamicObjectModel.V1.EphorteIdentity identity, Gecko.NCore.Client.DynamicObjectModel.V1.DynamicObjectModel.V1.Task[] tasks);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface DynamicObjectModelChannel : Gecko.NCore.Client.DynamicObjectModel.V1.DynamicObjectModel.V1.DynamicObjectModel, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DynamicObjectModelClient : System.ServiceModel.ClientBase<Gecko.NCore.Client.DynamicObjectModel.V1.DynamicObjectModel.V1.DynamicObjectModel>, Gecko.NCore.Client.DynamicObjectModel.V1.DynamicObjectModel.V1.DynamicObjectModel {
        
        public DynamicObjectModelClient() {
        }
        
        public DynamicObjectModelClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DynamicObjectModelClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DynamicObjectModelClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DynamicObjectModelClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Gecko.NCore.Client.DynamicObjectModel.V1.DynamicObjectModel.V1.TaskResult[] ExecuteTask(Gecko.NCore.Client.DynamicObjectModel.V1.DynamicObjectModel.V1.EphorteIdentity identity, Gecko.NCore.Client.DynamicObjectModel.V1.DynamicObjectModel.V1.Task[] tasks) {
            return base.Channel.ExecuteTask(identity, tasks);
        }
        
        public System.Threading.Tasks.Task<Gecko.NCore.Client.DynamicObjectModel.V1.DynamicObjectModel.V1.TaskResult[]> ExecuteTaskAsync(Gecko.NCore.Client.DynamicObjectModel.V1.DynamicObjectModel.V1.EphorteIdentity identity, Gecko.NCore.Client.DynamicObjectModel.V1.DynamicObjectModel.V1.Task[] tasks) {
            return base.Channel.ExecuteTaskAsync(identity, tasks);
        }
    }
}
