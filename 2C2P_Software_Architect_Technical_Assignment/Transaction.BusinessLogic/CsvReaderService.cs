using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Transaction.BusinessLogic
{
    public class CsvReader: ReaderManager
    {


        public CsvReader(Stream stream) : base(stream)
        {

        }

        public override string Read()
        {
            throw new NotImplementedException();
        }

        public override string Verify()
        {
            throw new NotImplementedException();
        }
    }
}
