using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;

namespace Transaction.BusinessLogic.ViewModels
{
    [XmlRootAttribute(elementName: "Transactions", Namespace = "", IsNullable = false)]
    public class XmlTransactionModel
    {
        [XmlElementAttribute("Transaction")]
        public XmlTransaction[] Transactions;
    }

    public class XmlTransaction
    {
        [XmlAttributeAttribute()]
        public string id;

        public string TransactionDate;
        public PaymentDetails PaymentDetails;
        public string Status;
    }

    public class PaymentDetails
    {
        public string Amount;
        public string CurrencyCode;
    }
}
