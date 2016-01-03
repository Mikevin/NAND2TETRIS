using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMTranslator.Types
{
    enum MemorySegment
    {
        STATIC,
        THIS,
        LOCAL,
        ARGUMENT,
        THAT,
        CONSTANT,
        POINTER,
        TEMP
    }
}
