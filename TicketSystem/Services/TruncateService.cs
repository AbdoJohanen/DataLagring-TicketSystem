namespace TicketSystem.Services;

//SERVICE TO TRUNCATE STRINGS
public static class TruncateService
{
    public static string Truncate(this string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value)) return value;
        return value.Length <= maxLength ? value : value[..maxLength];
    }
}
