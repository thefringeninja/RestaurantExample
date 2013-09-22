using System;
using System.Security.Cryptography.X509Certificates;

namespace ClassLibrary4
{
    public interface Message
    {
        Guid CorrelationId { get; }
        Guid CausationId { get; }
        Guid MessageId { get; }
    }
}