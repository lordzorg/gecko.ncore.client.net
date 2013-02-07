using System;
using System.Collections.Generic;

namespace Gecko.NCore.Client.Querying
{
    internal static class TypeSystem
    {
        internal static Type ResolveElementType(Type type)
        {
            var enumerableType = ResolveEnumerableType(type);
            if (enumerableType == null) 
                return type;

            return enumerableType.GetGenericArguments()[0];
        }

        private static Type ResolveEnumerableType(Type type)
        {
            if (type == null || type == typeof(string))
                return null;

            if (type.IsArray)
                return typeof(IEnumerable<>).MakeGenericType(type.GetElementType());

            if (type.IsGenericType)
            {
                foreach (var genericArgumentType in type.GetGenericArguments())
                {
                    var enumerableType = typeof(IEnumerable<>).MakeGenericType(genericArgumentType);
                    if (enumerableType.IsAssignableFrom(type))
                    {
                        return enumerableType;
                    }
                }
            }
            
            var interfaceTypes = type.GetInterfaces();
            
            if (interfaceTypes.Length > 0)
            {
                foreach (var interfaceType in interfaceTypes)
                {
                    var enumerableType = ResolveEnumerableType(interfaceType);
                    if (enumerableType != null) 
                        return enumerableType;
                }
            }
            
            if (type.BaseType != null && type.BaseType != typeof(object))
            {
                return ResolveEnumerableType(type.BaseType);
            }
            
            return null;
        }
    }
}