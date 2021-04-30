﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OAuth2ExampleApp.TwinfieldProcessXml {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.twinfield.com/", ConfigurationName="TwinfieldProcessXml.ProcessXmlSoap")]
    public interface ProcessXmlSoap {
        
        // CODEGEN: Generating message contract since message ProcessXmlStringRequest has headers
        [System.ServiceModel.OperationContractAttribute(Action="http://www.twinfield.com/ProcessXmlString", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlStringResponse ProcessXmlString(OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlStringRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.twinfield.com/ProcessXmlString", ReplyAction="*")]
        System.Threading.Tasks.Task<OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlStringResponse> ProcessXmlStringAsync(OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlStringRequest request);
        
        // CODEGEN: Generating message contract since message ProcessXmlDocumentRequest has headers
        [System.ServiceModel.OperationContractAttribute(Action="http://www.twinfield.com/ProcessXmlDocument", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlDocumentResponse ProcessXmlDocument(OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlDocumentRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.twinfield.com/ProcessXmlDocument", ReplyAction="*")]
        System.Threading.Tasks.Task<OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlDocumentResponse> ProcessXmlDocumentAsync(OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlDocumentRequest request);
        
        // CODEGEN: Generating message contract since message ProcessXmlCompressedRequest has headers
        [System.ServiceModel.OperationContractAttribute(Action="http://www.twinfield.com/ProcessXmlCompressed", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlCompressedResponse ProcessXmlCompressed(OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlCompressedRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.twinfield.com/ProcessXmlCompressed", ReplyAction="*")]
        System.Threading.Tasks.Task<OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlCompressedResponse> ProcessXmlCompressedAsync(OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlCompressedRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.twinfield.com/")]
    public partial class Header : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string sessionIDField;
        
        private string accessTokenField;
        
        private string companyCodeField;
        
        private System.Nullable<System.Guid> companyIdField;
        
        private System.Xml.XmlAttribute[] anyAttrField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string SessionID {
            get {
                return this.sessionIDField;
            }
            set {
                this.sessionIDField = value;
                this.RaisePropertyChanged("SessionID");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string AccessToken {
            get {
                return this.accessTokenField;
            }
            set {
                this.accessTokenField = value;
                this.RaisePropertyChanged("AccessToken");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string CompanyCode {
            get {
                return this.companyCodeField;
            }
            set {
                this.companyCodeField = value;
                this.RaisePropertyChanged("CompanyCode");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(IsNullable=true, Order=3)]
        public System.Nullable<System.Guid> CompanyId {
            get {
                return this.companyIdField;
            }
            set {
                this.companyIdField = value;
                this.RaisePropertyChanged("CompanyId");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAnyAttributeAttribute()]
        public System.Xml.XmlAttribute[] AnyAttr {
            get {
                return this.anyAttrField;
            }
            set {
                this.anyAttrField = value;
                this.RaisePropertyChanged("AnyAttr");
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ProcessXmlString", WrapperNamespace="http://www.twinfield.com/", IsWrapped=true)]
    public partial class ProcessXmlStringRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.twinfield.com/")]
        public OAuth2ExampleApp.TwinfieldProcessXml.Header Header;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.twinfield.com/", Order=0)]
        public string xmlRequest;
        
        public ProcessXmlStringRequest() {
        }
        
        public ProcessXmlStringRequest(OAuth2ExampleApp.TwinfieldProcessXml.Header Header, string xmlRequest) {
            this.Header = Header;
            this.xmlRequest = xmlRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ProcessXmlStringResponse", WrapperNamespace="http://www.twinfield.com/", IsWrapped=true)]
    public partial class ProcessXmlStringResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.twinfield.com/", Order=0)]
        public string ProcessXmlStringResult;
        
        public ProcessXmlStringResponse() {
        }
        
        public ProcessXmlStringResponse(string ProcessXmlStringResult) {
            this.ProcessXmlStringResult = ProcessXmlStringResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ProcessXmlDocument", WrapperNamespace="http://www.twinfield.com/", IsWrapped=true)]
    public partial class ProcessXmlDocumentRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.twinfield.com/")]
        public OAuth2ExampleApp.TwinfieldProcessXml.Header Header;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.twinfield.com/", Order=0)]
        public System.Xml.XmlNode xmlRequest;
        
        public ProcessXmlDocumentRequest() {
        }
        
        public ProcessXmlDocumentRequest(OAuth2ExampleApp.TwinfieldProcessXml.Header Header, System.Xml.XmlNode xmlRequest) {
            this.Header = Header;
            this.xmlRequest = xmlRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ProcessXmlDocumentResponse", WrapperNamespace="http://www.twinfield.com/", IsWrapped=true)]
    public partial class ProcessXmlDocumentResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.twinfield.com/", Order=0)]
        public System.Xml.XmlNode ProcessXmlDocumentResult;
        
        public ProcessXmlDocumentResponse() {
        }
        
        public ProcessXmlDocumentResponse(System.Xml.XmlNode ProcessXmlDocumentResult) {
            this.ProcessXmlDocumentResult = ProcessXmlDocumentResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ProcessXmlCompressed", WrapperNamespace="http://www.twinfield.com/", IsWrapped=true)]
    public partial class ProcessXmlCompressedRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://www.twinfield.com/")]
        public OAuth2ExampleApp.TwinfieldProcessXml.Header Header;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.twinfield.com/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] xmlRequest;
        
        public ProcessXmlCompressedRequest() {
        }
        
        public ProcessXmlCompressedRequest(OAuth2ExampleApp.TwinfieldProcessXml.Header Header, byte[] xmlRequest) {
            this.Header = Header;
            this.xmlRequest = xmlRequest;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="ProcessXmlCompressedResponse", WrapperNamespace="http://www.twinfield.com/", IsWrapped=true)]
    public partial class ProcessXmlCompressedResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://www.twinfield.com/", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(DataType="base64Binary")]
        public byte[] ProcessXmlCompressedResult;
        
        public ProcessXmlCompressedResponse() {
        }
        
        public ProcessXmlCompressedResponse(byte[] ProcessXmlCompressedResult) {
            this.ProcessXmlCompressedResult = ProcessXmlCompressedResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ProcessXmlSoapChannel : OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ProcessXmlSoapClient : System.ServiceModel.ClientBase<OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlSoap>, OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlSoap {
        
        public ProcessXmlSoapClient() {
        }
        
        public ProcessXmlSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ProcessXmlSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProcessXmlSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ProcessXmlSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlStringResponse OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlSoap.ProcessXmlString(OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlStringRequest request) {
            return base.Channel.ProcessXmlString(request);
        }
        
        public string ProcessXmlString(OAuth2ExampleApp.TwinfieldProcessXml.Header Header, string xmlRequest) {
            OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlStringRequest inValue = new OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlStringRequest();
            inValue.Header = Header;
            inValue.xmlRequest = xmlRequest;
            OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlStringResponse retVal = ((OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlSoap)(this)).ProcessXmlString(inValue);
            return retVal.ProcessXmlStringResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlStringResponse> OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlSoap.ProcessXmlStringAsync(OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlStringRequest request) {
            return base.Channel.ProcessXmlStringAsync(request);
        }
        
        public System.Threading.Tasks.Task<OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlStringResponse> ProcessXmlStringAsync(OAuth2ExampleApp.TwinfieldProcessXml.Header Header, string xmlRequest) {
            OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlStringRequest inValue = new OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlStringRequest();
            inValue.Header = Header;
            inValue.xmlRequest = xmlRequest;
            return ((OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlSoap)(this)).ProcessXmlStringAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlDocumentResponse OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlSoap.ProcessXmlDocument(OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlDocumentRequest request) {
            return base.Channel.ProcessXmlDocument(request);
        }
        
        public System.Xml.XmlNode ProcessXmlDocument(OAuth2ExampleApp.TwinfieldProcessXml.Header Header, System.Xml.XmlNode xmlRequest) {
            OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlDocumentRequest inValue = new OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlDocumentRequest();
            inValue.Header = Header;
            inValue.xmlRequest = xmlRequest;
            OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlDocumentResponse retVal = ((OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlSoap)(this)).ProcessXmlDocument(inValue);
            return retVal.ProcessXmlDocumentResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlDocumentResponse> OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlSoap.ProcessXmlDocumentAsync(OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlDocumentRequest request) {
            return base.Channel.ProcessXmlDocumentAsync(request);
        }
        
        public System.Threading.Tasks.Task<OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlDocumentResponse> ProcessXmlDocumentAsync(OAuth2ExampleApp.TwinfieldProcessXml.Header Header, System.Xml.XmlNode xmlRequest) {
            OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlDocumentRequest inValue = new OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlDocumentRequest();
            inValue.Header = Header;
            inValue.xmlRequest = xmlRequest;
            return ((OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlSoap)(this)).ProcessXmlDocumentAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlCompressedResponse OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlSoap.ProcessXmlCompressed(OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlCompressedRequest request) {
            return base.Channel.ProcessXmlCompressed(request);
        }
        
        public byte[] ProcessXmlCompressed(OAuth2ExampleApp.TwinfieldProcessXml.Header Header, byte[] xmlRequest) {
            OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlCompressedRequest inValue = new OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlCompressedRequest();
            inValue.Header = Header;
            inValue.xmlRequest = xmlRequest;
            OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlCompressedResponse retVal = ((OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlSoap)(this)).ProcessXmlCompressed(inValue);
            return retVal.ProcessXmlCompressedResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlCompressedResponse> OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlSoap.ProcessXmlCompressedAsync(OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlCompressedRequest request) {
            return base.Channel.ProcessXmlCompressedAsync(request);
        }
        
        public System.Threading.Tasks.Task<OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlCompressedResponse> ProcessXmlCompressedAsync(OAuth2ExampleApp.TwinfieldProcessXml.Header Header, byte[] xmlRequest) {
            OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlCompressedRequest inValue = new OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlCompressedRequest();
            inValue.Header = Header;
            inValue.xmlRequest = xmlRequest;
            return ((OAuth2ExampleApp.TwinfieldProcessXml.ProcessXmlSoap)(this)).ProcessXmlCompressedAsync(inValue);
        }
    }
}
