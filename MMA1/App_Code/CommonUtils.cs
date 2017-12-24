using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace MMA1.App_Code
{
    public class CommonUtils
    {
        public const string DateMinFormat = "yyyyMMddTHHmm";
        public const string DateSecFormat = "yyyyMMddTHHmmss";

        /// <summary>
        /// Convert a UTC Date String of format yyyyMMddThhmmZ into a Local Date
        /// </summary>
        /// <param name="dateString"></param>
        /// <returns></returns>
        public static DateTime DateTimeStringToDatetime (string dateString)
        {
            //Regex r = new Regex(@"^\d{4}\d{2}\d{2}T\d{2}\d{2}$");
            //if (!r.IsMatch(dateString))
            //{
            //    throw new FormatException(
            //        string.Format("{0} is not the correct format. Should be {1}", dateString, DateMinFormat));
            //}

            //DateTime dt = DateTime.ParseExact(dateString, DateMinFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
            DateTime dt = MMA_Common.CommonUtils.DateTimeStringToDatetime(dateString);
            return dt;
        }

        public static DateTime DateTimeSecStringToDateTime(string dateString)
        {
            Regex r = new Regex(@"^\d{4}\d{2}\d{2}T\d{2}\d{2}\d{2}$");
            if (!r.IsMatch(dateString))
            {
                throw new FormatException(
                    string.Format("{0} is not the correct format. Should be {1}", dateString, DateSecFormat));
            }

            DateTime dt = DateTime.ParseExact(dateString, DateSecFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);

            return dt;
        }

        public static string DateTimeToDateTimeSecString(DateTime dt)
        {
            return dt.ToString(DateSecFormat);
        }

        public static string DateTimeToDateTimeMinString(DateTime dt)
        {
            return dt.ToString(DateMinFormat);
        }

    }

    public static class ExceptionExtention
    {
        public static string GetAllMessages(this Exception e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(e.Message);
            while (e.InnerException != null)
            {
                e = e.InnerException;
                sb.AppendLine(e.Message);
            }
            return sb.ToString();
        }
    }


}