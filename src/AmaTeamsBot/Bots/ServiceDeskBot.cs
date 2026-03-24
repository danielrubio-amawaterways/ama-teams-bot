using AmaTeamsBot.Cards;
using AmaTeamsBot.Config;
using AmaTeamsBot.Services;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Options;

namespace AmaTeamsBot.Bots;

public class ServiceDeskBot : ActivityHandler
{
    private readonly ITicketDetectionService _ticketDetection;
    private readonly ServiceDeskOptions _options;

    public ServiceDeskBot(
        ITicketDetectionService ticketDetection,
        IOptions<ServiceDeskOptions> options)
    {
        _ticketDetection = ticketDetection;
        _options = options.Value;
    }

    protected override async Task OnMessageActivityAsync(
        ITurnContext<IMessageActivity> turnContext,
        CancellationToken cancellationToken)
    {
        var text = turnContext.Activity.Text;
        var tickets = _ticketDetection.DetectTickets(text);

        if (tickets.Count == 0)
            return;

        var reply = MessageFactory.Text(string.Empty);
        reply.Attachments = tickets
            .Select(t => TicketCardFactory.CreateTicketCard(t, _options.BaseUrl))
            .ToList();

        await turnContext.SendActivityAsync(reply, cancellationToken);
    }
}
