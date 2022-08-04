﻿using System.IO;
using System.Text.RegularExpressions;

namespace Domain.Extensions
{
    public static class ExtensionHelper
    {
        /// <summary>
        /// Get Project Root Directory ex D:\\Work\\Projects\\AuthServer
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string ToApplicationPath(this string fileName)
        {
            var exePath = Path.GetDirectoryName(System.Reflection
                                .Assembly.GetExecutingAssembly().CodeBase);
            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;
            return Path.Combine(appRoot, fileName);
        }
    }
}
