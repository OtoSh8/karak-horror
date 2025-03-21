// This code is part of the Fungus library (https://github.com/snozbot/fungus)
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace Fungus.EditorUtils
{
    public static class EditorExtensions
    {
        /// <summary>
        /// FindDerivedTypesFromAssembly allows a user to query all of types derived from a
        /// particular Type at runtime.
        /// Example usage:
        ///     foreach (System.Type st in EditorUtility.FindDerivedTypesFromAssembly(System.Reflection.Assembly.GetAssembly(typeof(BaseTimelineEvent)), typeof(BaseTimelineEvent), true))
        /// </summary>
        /// <param name="assembly">The assembly to search in</param>
        /// <param name="baseType">The base Type from which all returned Types derive</param>
        /// <param name="classOnly">If true, only class Types will be returned</param>
        /// <returns></returns>
        public static System.Type[] FindDerivedTypesFromAssembly(this System.Reflection.Assembly assembly, System.Type baseType, bool classOnly = true)
        {
            if (assembly == null)
                Debug.LogError("Assembly must be defined");
            if (baseType == null)
                Debug.LogError("Base type must be defined");

            // Iterate through all available types in the assembly
            var types = assembly.GetTypes().Where(type =>
                {
                    if (classOnly && !type.IsClass)
                        return false;
                    
                    if (baseType.IsInterface)
                    {
                        var it = type.GetInterface(baseType.FullName);

                        if (it != null)
                            return true;
                    }
                    else if (type.IsSubclassOf(baseType))
                    {
                        return true;
                    }

                    return false;
                }
            );

            return types.ToArray();
        }

        /// <summary>
        /// A convenient method for calling the above, but for ALL assemblies.
        /// Example usage:
        ///     List<System.Type> subTypes = EditorUtility.FindDerivedTypes(typeof(BaseTimelineEvent)).ToList();
        /// </summary>
        /// <param name="baseType"></param>
        /// <param name="classOnly"></param>
        /// <returns></returns>
        public static System.Type[] FindDerivedTypes(System.Type baseType, bool classOnly = true)
        {
#if UNITY_2019_2_OR_NEWER
            var results = TypeCache.GetTypesDerivedFrom(baseType).ToArray();
            if (classOnly)
                return results.Where(x => x.IsClass).ToArray();
            else
                return results.ToArray();
#else
            System.Type[] typeArray = new System.Type[0];
            var retval = typeArray.Concat(typeArray);
            foreach (var assem in System.AppDomain.CurrentDomain.GetAssemblies())
            {
                retval = retval.Concat(FindDerivedTypesFromAssembly(assem, baseType, classOnly));
            }
            return retval.ToArray();
#endif
        }

        /// <summary>
        /// Find and return all Unity.Objects that have the target interface
        /// </summary>
        /// <typeparam name="T">Intended to be an interface but will work for any</typeparam>
        /// <returns></returns>
        public static List<T> FindObjectsOfInterface<T>()
        {
            return Object.FindObjectsByType<Object>(FindObjectsSortMode.None).OfType<T>().ToList();
        }
    }
}