// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Text;

namespace System.Diagnostics.Internal
{
    // Adapted from https://github.com/aspnet/Common/blob/dev/shared/Microsoft.Extensions.TypeNameHelper.Sources/TypeNameHelper.cs
    internal class TypeNameHelper
    {
        private static readonly Dictionary<Type, string> _builtInTypeNames = new Dictionary<Type, string>
        {
            { typeof(void), "void" },
            { typeof(bool), "bool" },
            { typeof(byte), "byte" },
            { typeof(char), "char" },
            { typeof(decimal), "decimal" },
            { typeof(double), "double" },
            { typeof(float), "float" },
            { typeof(int), "int" },
            { typeof(long), "long" },
            { typeof(object), "object" },
            { typeof(sbyte), "sbyte" },
            { typeof(short), "short" },
            { typeof(string), "string" },
            { typeof(uint), "uint" },
            { typeof(ulong), "ulong" },
            { typeof(ushort), "ushort" }
        };

        /// <summary>
        /// Pretty print a type name.
        /// </summary>
        /// <param name="type">The <see cref="Type"/>.</param>
        /// <param name="fullName"><c>true</c> to print a fully qualified name.</param>
        /// <param name="includeGenericParameterNames"><c>true</c> to include generic parameter names.</param>
        /// <returns>The pretty printed type name.</returns>
        public static string GetTypeDisplayName(Type type, bool fullName = true, bool includeGenericParameterNames = false)
        {
            var builder = new StringBuilder();
            ProcessType(builder, type, new DisplayNameOptions(fullName, includeGenericParameterNames));
            return builder.ToString();
        }

        /// <summary>
        /// Returns a name of given generic type without '`'.
        /// </summary>
        public static string GetTypeNameForGenericType(Type type)
        {
            if (!type.IsGenericType)
            {
                throw new ArgumentException("The given type should be generic", nameof(type));
            }

            var genericPartIndex = type.Name.IndexOf('`');
            Debug.Assert(genericPartIndex >= 0);

            return type.Name.Substring(0, genericPartIndex);
        }

        private static void ProcessType(StringBuilder builder, Type type, DisplayNameOptions options)
        {
            if (type.IsGenericType)
            {
                var genericArguments = type.GetGenericArguments();
                ProcessGenericType(builder, type, genericArguments, genericArguments.Length, options);
            }
            else if (type.IsArray)
            {
                ProcessArrayType(builder, type, options);
            }
            else if (_builtInTypeNames.TryGetValue(type, out var builtInName))
            {
                builder.Append(builtInName);
            }
            else if (type.Namespace == nameof(System))
            {
                builder.Append(type.Name);
            }
            else if (type.IsGenericParameter)
            {
                if (options.IncludeGenericParameterNames)
                {
                    builder.Append(type.Name);
                }
            }
            else
            {
                builder.Append(options.FullName ? type.FullName ?? type.Name : type.Name);
            }
        }

        private static void ProcessArrayType(StringBuilder builder, Type type, DisplayNameOptions options)
        {
            var innerType = type;
            while (innerType.IsArray)
            {
                innerType = innerType.GetElementType();
            }

            ProcessType(builder, innerType, options);

            while (type.IsArray)
            {
                builder.Append('[');
                builder.Append(',', type.GetArrayRank() - 1);
                builder.Append(']');
                type = type.GetElementType();
            }
        }

        private static void ProcessGenericType(StringBuilder builder, Type type, Type[] genericArguments, int length, DisplayNameOptions options)
        {
            var offset = 0;
            if (type.IsNested)
            {
                offset = type.DeclaringType.GetGenericArguments().Length;
            }

            if (options.FullName)
            {
                if (type.IsNested)
                {
                    ProcessGenericType(builder, type.DeclaringType, genericArguments, offset, options);
                    builder.Append('+');
                }
                else if (!string.IsNullOrEmpty(type.Namespace))
                {
                    builder.Append(type.Namespace);
                    builder.Append('.');
                }
            }

            var genericPartIndex = type.Name.IndexOf('`');
            if (genericPartIndex <= 0)
            {
                builder.Append(type.Name);
                return;
            }

            builder.Append(type.Name, 0, genericPartIndex);

            builder.Append('<');
            for (var i = offset; i < length; i++)
            {
                ProcessType(builder, genericArguments[i], options);
                if (i + 1 == length)
                {
                    continue;
                }

                builder.Append(',');
                if (options.IncludeGenericParameterNames || !genericArguments[i + 1].IsGenericParameter)
                {
                    builder.Append(' ');
                }
            }
            builder.Append('>');
        }

        private readonly struct DisplayNameOptions
        {
            public DisplayNameOptions(bool fullName, bool includeGenericParameterNames)
            {
                FullName = fullName;
                IncludeGenericParameterNames = includeGenericParameterNames;
            }

            public bool FullName { get; }

            public bool IncludeGenericParameterNames { get; }
        }
    }
}
