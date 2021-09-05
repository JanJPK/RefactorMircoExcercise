namespace TDDMicroExercises.TelemetrySystem
{
    public interface ITelemetryClient
    {
        bool OnlineStatus { get; }
        void Connect(string telemetryServerConnectionString);
        void Disconnect();
        string Receive();
        void Send(string message);
    }
}