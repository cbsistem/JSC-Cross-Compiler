﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGames
{
    interface AssemblyReferenceToken :
        ScriptCoreLib.Cards.IAssemblyReferenceToken,
        ScriptCoreLib.Shared.Query.IAssemblyReferenceToken
    {
    }
}
