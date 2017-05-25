using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ex2
{
    class SinglePlayer : Player
    {
        public override string ReadAndWriteToServer(string data)
        {
            string result;

            WriteData(data);
            result = ReadData();
            CloseConnection();

            return result;
        }
    }
}
