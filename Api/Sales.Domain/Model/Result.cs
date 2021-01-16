using System.Collections.Generic;
using System.Linq;

namespace Sales.Domain.Model
{
    public class Result<T>
    {
        public Result()
        {
            Errors = new List<string>();
        }

        public T Value { get; set; }
        public List<string> Errors { get; set; }
        public bool Success { get { return !Errors.Any(); } }
    }
}
