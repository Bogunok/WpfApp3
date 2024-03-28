using System;

namespace WpfApp3
{
    internal class InvalidMail : Exception
    {
        public InvalidMail():base("Invalid mail!") { }
    }
}
