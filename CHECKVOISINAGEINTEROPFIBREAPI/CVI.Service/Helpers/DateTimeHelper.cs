using System;
using System.Collections.Generic;
using System.Text;

namespace CVI.Service.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime? TimeStampToDateTime(string timestamp)
        {
            try
            {
                if (!string.IsNullOrEmpty(timestamp))
                {
                    return DateTime.Parse(timestamp);
                }
                else
                {
                    return null;
                }
                
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
