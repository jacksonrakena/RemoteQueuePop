using System.Numerics;
using Dalamud.Interface.Colors;
using Dalamud.Logging;
using Discord;
using ImGuiNET;
using RemoteQueuePop.Configuration;

namespace RemoteQueuePop.Interface
{
    internal class RemoteQueuePopConfigWindow
    {
        private bool IsOpen = false;
        private RemoteQueuePopConfig _remoteQueuePopConfig;

        public RemoteQueuePopConfigWindow()
        {
            _remoteQueuePopConfig = RemoteQueuePopPlugin.DalamudPluginInterface.GetPluginConfig() as RemoteQueuePopConfig ?? new RemoteQueuePopConfig();
        }

        public void DrawRichPresenceConfigWindow()
        {
            if (!IsOpen)
            {
                return;
            }

            ImGui.SetNextWindowSize(new Vector2(750, 520));
            var imGuiReady = ImGui.Begin("Remote Queue Pop Alerts",
                ref IsOpen);

            if (imGuiReady)
            {
                ImGui.Text("Welcome to the Remote Queue Pop Alert plugin settings.");
                ImGui.Separator();
                ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(1, 3));
                ImGui.Text("You'll need a Discord token from the Discord developer console. Make sure it's in one of your servers, so it can DM you.");
                ImGui.InputText("Discord token", ref _remoteQueuePopConfig.Token, 100);
                ImGui.Separator();
                ImGui.Text("State: " + RemoteQueuePopPlugin.Client.ConnectionState.ToString("G") + "_" + RemoteQueuePopPlugin.Client.LoginState.ToString("G"));

                ImGui.PopStyleVar();

                ImGui.Separator();

                if (ImGui.Button("Start"))
                {
                    RemoteQueuePopPlugin.Client.LoginAsync(TokenType.Bot, _remoteQueuePopConfig.Token).GetAwaiter().GetResult();
                    RemoteQueuePopPlugin.Client.StartAsync();
                }
                if (ImGui.Button("Save and close"))
                {
                    this.Close();
                    RemoteQueuePopPlugin.DalamudPluginInterface.SavePluginConfig(_remoteQueuePopConfig);
                    RemoteQueuePopPlugin.RemoteQueuePopConfig = this._remoteQueuePopConfig;
                    PluginLog.Log("Settings saved.");
                }

                ImGui.End();
            }
        }

        public void Open()
        {
            this.IsOpen = true;
        }

        public void Close()
        {
            this.IsOpen = false;
        }

        public void Toggle()
        {
            this.IsOpen = !this.IsOpen;
        }
    }
}