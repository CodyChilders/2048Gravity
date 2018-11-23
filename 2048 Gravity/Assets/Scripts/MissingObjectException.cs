using System;

class MissingObjectException : Exception
{
    public MissingObjectException (string error) : base(error) { }
    public MissingObjectException(string fmt, params object[] args) : base(string.Format(fmt, args)) { }
}
