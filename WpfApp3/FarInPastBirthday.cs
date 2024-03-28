using System;

namespace WpfApp3
{
    internal class FarInPastBirthday : Exception
    {
        public FarInPastBirthday():base("Date of birth can't be so far in the past!") { }      
    }
}
