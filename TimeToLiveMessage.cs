using System;

namespace ClassLibrary4
{
    public interface TimeToLiveMessage : Message
    {
        DateTime TimeToLive { get; }
    }
}