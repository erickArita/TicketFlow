using RestSharp;
using RestSharp.Authenticators;
using TicketFlow.Common.Exceptions;
using TicketFlow.Services.Email.Dtos;


namespace TicketFlow.Services.Email;

public class MailgunEmailService : IEmailSenderService
{
    private readonly EmailConfigurationDto _config;
    private readonly bool _isDevelopment = false;


    public MailgunEmailService(IConfiguration configuration)
    {
        _config = configuration.GetSection("EmailConfiguration").Get<EmailConfigurationDto>();

        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        if (environment is "Development") _isDevelopment = true;
    }


    #region Private Methods

    private RestRequest _getRequest(string emailTo, string subject, string template)
    {
        if (_isDevelopment)
        {
            emailTo = _config.From;
        }

        var from = _config.From;
        var domain = _config.Domain;

        var request = new RestRequest();
        request.AddParameter("domain", domain, ParameterType.UrlSegment);
        request.Resource = "{domain}/messages";
        request.AddParameter("from", from);
        request.AddParameter("to", emailTo);
        request.AddParameter("subject", subject);
        request.AddParameter("html", template);


        request.Method = Method.Post;

        return request;
    }

    private async Task<bool> _executeRequest(RestRequest request)
    {
        var apiKey = _config.ApiKey;

        var client = new RestClient(new RestClientOptions
        {
            BaseUrl = new Uri("https://api.mailgun.net/v3"),
            Authenticator = new HttpBasicAuthenticator("api", apiKey)
        });

        var response = await client.ExecuteAsync(request);

        if (!response.IsSuccessful)
        {
            throw new TicketFlowException(response.ErrorMessage);
        }

        return response.IsSuccessful;
    }

    #endregion

    public async Task<bool> SendEmailAsync(string emailTo, string subject, string template)
    {
        var request = _getRequest(emailTo, subject, template);

        return await _executeRequest(request);
    }
}