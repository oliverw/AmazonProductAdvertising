namespace System;

/// <summary>
/// Extension methods for DateTime
/// </summary>
public static class DateTimeExtensions
{
    /// <summary>
    /// Convert to Amazon Date String
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static string ToAmzDateStr(this DateTime date) => date.ToString("yyyyMMddTHHmmssZ");

}
