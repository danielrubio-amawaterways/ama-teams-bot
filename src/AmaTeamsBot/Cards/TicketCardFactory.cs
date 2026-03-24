using AdaptiveCards;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;

namespace AmaTeamsBot.Cards;

public static class TicketCardFactory
{
    public static Attachment CreateTicketCard(string ticketNumber, string baseUrl)
    {
        var url = $"{baseUrl.TrimEnd('/')}/{ticketNumber}";

        var card = new AdaptiveCard(new AdaptiveSchemaVersion(1, 4))
        {
            Body =
            {
                new AdaptiveTextBlock("Service Desk Ticket")
                {
                    Weight = AdaptiveTextWeight.Bolder,
                    Size = AdaptiveTextSize.Medium
                },
                new AdaptiveTextBlock($"Ticket SD-{ticketNumber}")
                {
                    Spacing = AdaptiveSpacing.Small
                }
            },
            Actions =
            {
                new AdaptiveOpenUrlAction
                {
                    Title = $"Open SD-{ticketNumber}",
                    Url = new Uri(url)
                }
            }
        };

        return new Attachment
        {
            ContentType = AdaptiveCard.ContentType,
            Content = JsonConvert.DeserializeObject(card.ToJson())
        };
    }
}
