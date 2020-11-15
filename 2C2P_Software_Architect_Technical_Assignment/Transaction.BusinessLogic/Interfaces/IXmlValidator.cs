using System;
using System.Collections.Generic;
using System.Text;
using Transaction.BusinessLogic.ViewModels;

namespace Transaction.BusinessLogic.Interfaces
{
    public interface IXmlValidator
    {
        List<string> Validate(XmlTransaction transaction);
    }
}
