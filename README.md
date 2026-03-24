# AMA Teams Bot

A Microsoft Teams bot that detects service desk ticket references in channel messages and replies with an Adaptive Card linking directly to the ticket.

## Supported Patterns

| Pattern | Example | Detected Ticket |
|---------|---------|-----------------|
| `SD` + number | `SD12345`, `SD 12345` | 12345 |
| `#` + number | `#12345`, `# 12345` | 12345 |

Ticket numbers must be 4-6 digits.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Bot Framework Emulator](https://github.com/microsoft/BotFramework-Emulator/releases) (for local testing)

## Getting Started

```bash
# Restore and build
dotnet build

# Run locally
dotnet run --project src/AmaTeamsBot
```

The bot listens on `http://localhost:3978/api/messages`.

Open Bot Framework Emulator, connect to that endpoint (leave App ID and Password blank for local dev), and send a message like `Check SD12345`.

## Configuration

| Setting | Environment Variable | Description |
|---------|---------------------|-------------|
| `ServiceDesk:BaseUrl` | `ServiceDesk__BaseUrl` | Base URL for ticket links |
| `MicrosoftAppId` | `MicrosoftAppId` | Azure Bot registration App ID |
| `MicrosoftAppPassword` | `MicrosoftAppPassword` | Azure Bot registration password |
| `MicrosoftAppTenantId` | `MicrosoftAppTenantId` | Azure AD tenant ID |

## Deploy to Azure

1. Create an Azure Bot resource and App Service.
2. Set the messaging endpoint to `https://<your-app>.azurewebsites.net/api/messages`.
3. Configure the app settings listed above.
4. Install the bot in Teams via the Teams Developer Portal.
