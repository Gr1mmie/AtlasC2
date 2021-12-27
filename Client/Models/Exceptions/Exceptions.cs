using System;

namespace Client.Models
{
    public class AtlasException : Exception
    {
        // https://github.com/FortyNorthSecurity/EDD/blob/master/EDD/Models/EDDException.cs
        public AtlasException(string message) : base(message) { }
        public AtlasException(string message, Exception inner) : base(message, inner) { }

    }
}
