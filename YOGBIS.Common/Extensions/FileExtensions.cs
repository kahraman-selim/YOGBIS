using System;

namespace YOGBIS.Common.Extensions
{
    public static class FileExtensions
    {
        public static bool HasFile(this byte[] fileData)
        {
            return fileData != null && fileData.Length > 0;
        }
    }
}
