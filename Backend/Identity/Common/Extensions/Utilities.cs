using System;
using System.Collections.Generic;
using Common.Enum;

namespace Common.Extensions
{
    public static class Utilities
    {
        public static string GetEnvironment(EnvironmentType env)
        {
            switch (env)
            {
                case EnvironmentType.Dev:
                {
                    return "Dev";
                }
                case EnvironmentType.Prod:
                {
                    return "Prod";
                }
                case EnvironmentType.Stage:
                {
                    return "Stage";
                }
                default:
                {
                    return "Dev";
                }
            }
        }
        public static string GenerateOtp()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
            var stringChars = new char[6];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var otp = new String(stringChars);
            return otp;
        }
        public static double UpToTwoDecimalPoints(this double num)
        {
            var totalCost = Convert.ToDouble($"{num:0.00}");
            return totalCost;
        }
        public static double UpToTwoDecimalPoints(this decimal num)
        {
            var totalCost = Convert.ToDouble($"{num:0.00}");
            return totalCost;
        }

        public static List<SortModel> GetOrderByList()
        {
            return new List<SortModel> { new SortModel { ColId = "Id", Sort = "desc" } };
        }

        public static List<SortModel> GetOrderByListByCreatedDate()
        {
            return new List<SortModel> { new SortModel { ColId = "CreatedDate", Sort = "desc" } };
        }

        public static List<SortModel> GetOrderByList(string col, string dir)
        {
            return new List<SortModel> { new SortModel { ColId = col, Sort = dir } };
        }
    }
}
