using System;
using System.Collections.Generic;

namespace BasicJS
{
    interface IAbstractID<T>
    {
        void Add();
        void Modify();
        void Erase();
        string Find();
        List<T> List();
        string ListToJson();
        string LogIn();
    }
}
