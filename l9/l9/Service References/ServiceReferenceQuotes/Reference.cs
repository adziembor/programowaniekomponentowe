﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace l9.ServiceReferenceQuotes {
    using System.Data;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://ws.cdyne.com/", ConfigurationName="ServiceReferenceQuotes.DelayedStockQuoteSoap")]
    public interface DelayedStockQuoteSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ws.cdyne.com/GetQuote", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        l9.ServiceReferenceQuotes.QuoteData GetQuote(string StockSymbol, string LicenseKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ws.cdyne.com/GetQuote", ReplyAction="*")]
        System.Threading.Tasks.Task<l9.ServiceReferenceQuotes.QuoteData> GetQuoteAsync(string StockSymbol, string LicenseKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ws.cdyne.com/GetQuickQuote", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        decimal GetQuickQuote(string StockSymbol, string LicenseKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ws.cdyne.com/GetQuickQuote", ReplyAction="*")]
        System.Threading.Tasks.Task<decimal> GetQuickQuoteAsync(string StockSymbol, string LicenseKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ws.cdyne.com/GetQuoteDataSet", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetQuoteDataSet(string StockSymbols, string LicenseKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://ws.cdyne.com/GetQuoteDataSet", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> GetQuoteDataSetAsync(string StockSymbols, string LicenseKey);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34230")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://ws.cdyne.com/")]
    public partial class QuoteData : object, System.ComponentModel.INotifyPropertyChanged {
        
        private string stockSymbolField;
        
        private decimal lastTradeAmountField;
        
        private System.DateTime lastTradeDateTimeField;
        
        private decimal stockChangeField;
        
        private decimal openAmountField;
        
        private decimal dayHighField;
        
        private decimal dayLowField;
        
        private int stockVolumeField;
        
        private decimal prevClsField;
        
        private string changePercentField;
        
        private string fiftyTwoWeekRangeField;
        
        private decimal earnPerShareField;
        
        private decimal peField;
        
        private string companyNameField;
        
        private bool quoteErrorField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string StockSymbol {
            get {
                return this.stockSymbolField;
            }
            set {
                this.stockSymbolField = value;
                this.RaisePropertyChanged("StockSymbol");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public decimal LastTradeAmount {
            get {
                return this.lastTradeAmountField;
            }
            set {
                this.lastTradeAmountField = value;
                this.RaisePropertyChanged("LastTradeAmount");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public System.DateTime LastTradeDateTime {
            get {
                return this.lastTradeDateTimeField;
            }
            set {
                this.lastTradeDateTimeField = value;
                this.RaisePropertyChanged("LastTradeDateTime");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public decimal StockChange {
            get {
                return this.stockChangeField;
            }
            set {
                this.stockChangeField = value;
                this.RaisePropertyChanged("StockChange");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public decimal OpenAmount {
            get {
                return this.openAmountField;
            }
            set {
                this.openAmountField = value;
                this.RaisePropertyChanged("OpenAmount");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public decimal DayHigh {
            get {
                return this.dayHighField;
            }
            set {
                this.dayHighField = value;
                this.RaisePropertyChanged("DayHigh");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public decimal DayLow {
            get {
                return this.dayLowField;
            }
            set {
                this.dayLowField = value;
                this.RaisePropertyChanged("DayLow");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public int StockVolume {
            get {
                return this.stockVolumeField;
            }
            set {
                this.stockVolumeField = value;
                this.RaisePropertyChanged("StockVolume");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=8)]
        public decimal PrevCls {
            get {
                return this.prevClsField;
            }
            set {
                this.prevClsField = value;
                this.RaisePropertyChanged("PrevCls");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=9)]
        public string ChangePercent {
            get {
                return this.changePercentField;
            }
            set {
                this.changePercentField = value;
                this.RaisePropertyChanged("ChangePercent");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=10)]
        public string FiftyTwoWeekRange {
            get {
                return this.fiftyTwoWeekRangeField;
            }
            set {
                this.fiftyTwoWeekRangeField = value;
                this.RaisePropertyChanged("FiftyTwoWeekRange");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=11)]
        public decimal EarnPerShare {
            get {
                return this.earnPerShareField;
            }
            set {
                this.earnPerShareField = value;
                this.RaisePropertyChanged("EarnPerShare");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=12)]
        public decimal PE {
            get {
                return this.peField;
            }
            set {
                this.peField = value;
                this.RaisePropertyChanged("PE");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=13)]
        public string CompanyName {
            get {
                return this.companyNameField;
            }
            set {
                this.companyNameField = value;
                this.RaisePropertyChanged("CompanyName");
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Order=14)]
        public bool QuoteError {
            get {
                return this.quoteErrorField;
            }
            set {
                this.quoteErrorField = value;
                this.RaisePropertyChanged("QuoteError");
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
    public interface DelayedStockQuoteSoapChannel : l9.ServiceReferenceQuotes.DelayedStockQuoteSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DelayedStockQuoteSoapClient : System.ServiceModel.ClientBase<l9.ServiceReferenceQuotes.DelayedStockQuoteSoap>, l9.ServiceReferenceQuotes.DelayedStockQuoteSoap {
        
        public DelayedStockQuoteSoapClient() {
        }
        
        public DelayedStockQuoteSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DelayedStockQuoteSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DelayedStockQuoteSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DelayedStockQuoteSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public l9.ServiceReferenceQuotes.QuoteData GetQuote(string StockSymbol, string LicenseKey) {
            return base.Channel.GetQuote(StockSymbol, LicenseKey);
        }
        
        public System.Threading.Tasks.Task<l9.ServiceReferenceQuotes.QuoteData> GetQuoteAsync(string StockSymbol, string LicenseKey) {
            return base.Channel.GetQuoteAsync(StockSymbol, LicenseKey);
        }
        
        public decimal GetQuickQuote(string StockSymbol, string LicenseKey) {
            return base.Channel.GetQuickQuote(StockSymbol, LicenseKey);
        }
        
        public System.Threading.Tasks.Task<decimal> GetQuickQuoteAsync(string StockSymbol, string LicenseKey) {
            return base.Channel.GetQuickQuoteAsync(StockSymbol, LicenseKey);
        }
        
        public System.Data.DataSet GetQuoteDataSet(string StockSymbols, string LicenseKey) {
            return base.Channel.GetQuoteDataSet(StockSymbols, LicenseKey);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> GetQuoteDataSetAsync(string StockSymbols, string LicenseKey) {
            return base.Channel.GetQuoteDataSetAsync(StockSymbols, LicenseKey);
        }
    }
}
