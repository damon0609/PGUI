using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Singleton<T> where T:class,new()
{
    private static object obj = new object();

    private static T _instance;

    public static T instance
    {

        get
        {
            if (_instance == null)
            {
                lock (obj)
                {
                    _instance = new T();
                }
            }
            return _instance;
        }
    }


}
