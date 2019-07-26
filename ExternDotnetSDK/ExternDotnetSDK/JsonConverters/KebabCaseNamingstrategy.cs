﻿using System.Linq;
using Newtonsoft.Json.Serialization;

namespace ExternDotnetSDK.JsonConverters
{
    public class KebabCaseNamingStrategy : NamingStrategy
    {
        protected override string ResolvePropertyName(string name) => ToKebabCase(name);

        private static string ToKebabCase(string str) => str.ToKebabCase();
    }

    internal static class StringExtensions
    {
        public static string ToKebabCase(this string str) => string
                                                             .Concat(
                                                                 str.Select(
                                                                     (x, i) => i > 0 && char.IsUpper(x)
                                                                         ? "-" + x.ToString()
                                                                         : x.ToString()))
                                                             .ToLower();
    }
}