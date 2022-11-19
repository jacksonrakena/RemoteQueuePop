using Dalamud.Configuration;

namespace RemoteQueuePop.Configuration
{
    class RemoteQueuePopConfig : IPluginConfiguration
    {
        public int Version { get; set; } = 1;

        public string Token = "";
    }
}