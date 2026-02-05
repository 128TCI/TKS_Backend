using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Services.Extensions;

public static class ConverterHelper
{
    public static int ToInt(this object obj)
    {
        if (obj is null) return 0;

        return Convert.ToInt32(obj);
    }
    public static long ToBigInt(this object obj)
    {
        if (obj is string && string.IsNullOrEmpty(obj.ToString()))
        {
            obj = 0;

        }
        if (obj is null) return 0;

        return Convert.ToInt64(obj);
    }
    public static decimal ToDecimal(this object obj)
    {
        if (obj is string && string.IsNullOrEmpty(obj.ToString()))
        {
            obj = 0;

        }
        if (obj is null) return 0;

        return Convert.ToDecimal(obj);
    }
    public static string ToSureString(this object obj)
    {
        if (obj is null) return "";

        return Convert.ToString(obj)!;
    }
    public static bool IsStringNullOrEmpty(this object? obj)
    {
        if (obj is null) return true;

        var str = Convert.ToString(obj)!;

        if (string.IsNullOrEmpty(str)) return true;

        return false;
    }
    public static bool ToBoolean(this object obj)
    {
        if (obj is null) return false;

        return Convert.ToBoolean(obj);
    }
    public static DateTime ToDateTime(this object date)
    {
        return Convert.ToDateTime(date);
    }
    public static TimeSpan ToTimeSpan(this string time)
    {
        return DateTime.Parse(time).TimeOfDay;
    }
    public static string ToStringTimeSpan(this TimeSpan time, string format)
    {
        return new DateTime(time.Ticks).ToString(format);
    }
    public static string ToDateTimeString(this object dateTime, string format)
    {
        if (dateTime is null) return "";
        return ToDateTime(dateTime.ToString()!).ToString(format);
    }
    public static string ToCurrency(this object decimalNum)
    {
        if (decimalNum is null) return "";

        decimal num = Convert.ToDecimal(decimalNum);

        return String.Format("{0:N}", num);
    }
    public static long ToFileSize(this long fileSize) => fileSize * 1024 * 1024;
    public static string ToTitleCase(this string input) => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
    public static string StringSplitWord(this string input) => Regex.Replace(input, "([a-z])([A-Z])", "$1 $2");

    public static string? FromExcelToDateTimeString(this object obj, string format = "")
    {
        if (obj is null) return null;
        if (!string.IsNullOrEmpty(format)) format = FormatHelper.MMDDYY;
        if (DateTime.TryParse(obj.ToString(), out _))
        {
            return Convert.ToDateTime(obj).ToString(format);
        }
        else
        {
            if (double.TryParse(obj.ToString(), out _))
            {
                return DateTime.FromOADate(Convert.ToDouble(obj)).ToString(format);
            }
            else
            {
                return null;
            }
        }
    }
    public static DataTable ToDataTables<T>(this IEnumerable<T> data)
    {
        PropertyDescriptorCollection properties =
            TypeDescriptor.GetProperties(typeof(T));
        DataTable table = new();
        foreach (PropertyDescriptor prop in properties)
            table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        foreach (T item in data)
        {
            DataRow row = table.NewRow();
            foreach (PropertyDescriptor prop in properties)
                row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
            table.Rows.Add(row);
        }
        return table;
    }
    public static T AsClassProperty<T>(this IDictionary<string, object> dictionaries)
    {
        return JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(dictionaries))!;
    }
}