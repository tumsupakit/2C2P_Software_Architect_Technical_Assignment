using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Transaction.BusinessLogic
{
    public abstract class ReaderManager
    {
        public ReaderManager(Stream Stream)
        {

        }

        public abstract string Verify();

        public abstract string Read();
    }
}
