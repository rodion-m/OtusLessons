using System.Text;
using MailKit;
using Microsoft.Extensions.Logging;

namespace _19_GC.Mailing
{
    /// <inheritdoc />
    public class MELProtocolLogger : IProtocolLogger
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Log messages via Microsoft Extension Logging (MEL) Microsoft ILogger <see cref="ILogger"/>.
        /// </summary>
        /// <param name="loggerFactory">The logger factory that resolves through Dependency injector (DI)</param>
        public MELProtocolLogger(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("SmtpMimeKit");
        }

        /// <inheritdoc />
        public void LogConnect(Uri uri)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));

            _logger.LogDebug("Connected to {Uri}", uri);
        }

        /// <inheritdoc />
        public void LogClient(byte[] buffer, int offset, int count)
        {
            var message = Encoding.UTF8
                .GetString(buffer)
                .TrimEnd('\0')
                .Replace(Environment.NewLine, "; ");

            _logger.LogTrace("Client: {Message}", message);
        }

        /// <inheritdoc />
        public void LogServer(byte[] buffer, int offset, int count)
        {
            var message = Encoding.UTF8
                .GetString(buffer)
                .TrimEnd('\0')
                .Replace(Environment.NewLine, "; ");

            _logger.LogTrace("Server: {Message}", message);
        }

        public IAuthenticationSecretDetector? AuthenticationSecretDetector { get; set; }

        public void Dispose()
        {
            _logger.LogTrace("Logger disposed");
        }
    }
}