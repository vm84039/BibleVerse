using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleVerse.Services.Utility
{
    public interface Ilogger
    {
        void Debug(string message);
        void Info(string message);
        void Warning(string message);
        void Error(string message);
    }
}
