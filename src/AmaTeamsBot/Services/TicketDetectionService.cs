using System.Text.RegularExpressions;

namespace AmaTeamsBot.Services;

public interface ITicketDetectionService
{
    IReadOnlyList<string> DetectTickets(string text);
}

public partial class TicketDetectionService : ITicketDetectionService
{
    [GeneratedRegex(@"(?:SD\s*#?\s*|#\s*)(\d{4,8})", RegexOptions.IgnoreCase)]
    private static partial Regex TicketRegex();

    public IReadOnlyList<string> DetectTickets(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return [];

        return TicketRegex()
            .Matches(text)
            .Select(m => m.Groups[1].Value)
            .Distinct()
            .ToList();
    }
}
