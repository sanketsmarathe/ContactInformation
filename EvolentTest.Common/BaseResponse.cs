using System.Collections.Generic;

namespace EvolentTest.Common
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public object Result { get; set; }
    }
}
