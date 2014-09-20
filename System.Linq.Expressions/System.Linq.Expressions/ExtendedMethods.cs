using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class ExtendedMethods
{
    public static T To<T>(this Object value)
    {
        return (T)(value);
    }

    public static Boolean Not(this Boolean value)
    {
        return !(value);
    }

    public static Boolean IsNull(this Object value)
    {                                                              
        return Object.ReferenceEquals(null, value);
    }

    public static Boolean Is<T>(this Object value)
    {
        return (value is T);
    }

    public static String Set(this String pattern, params Object[] parameters)
    {
        return String.Format(pattern, parameters);
    }
}
