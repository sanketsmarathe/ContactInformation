using System;
using System.Collections.Generic;
using System.Text;

namespace EvolentTest.Services
{
    public interface ILogService
    {
        void Information(string message);
        void Warning(string message);
        void Debug(string message);
        void Error(string message);
    }
}
