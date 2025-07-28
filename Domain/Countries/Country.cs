using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Countries
{
    public class Country
    {
        public string Code { get; private set; } = default!;
        public string Name { get; private set; } = default!;
        public string Region { get; private set; } = default!;

    // Factory method siguiendo patrones DDD
    public static Country Create(string code, string name, string region)
        => new() { Code = code, Name = name, Region = region };
    }
}
