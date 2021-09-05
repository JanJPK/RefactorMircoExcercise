using System;

namespace TDDMicroExercises.TelemetrySystem
{
    public class TelemetryDiagnosticControls
    {
        private const int CheckTransmissionRetryCount = 3;
        private readonly ITelemetryClient _telemetryClient;

        public TelemetryDiagnosticControls() : this(new TelemetryClient())
        { }

        public TelemetryDiagnosticControls(ITelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;
        }

        public string DiagnosticInfo { get; set; } = string.Empty;

        public void CheckTransmission()
        {
            DiagnosticInfo = string.Empty;

            _telemetryClient.Disconnect();

            var retryLeft = CheckTransmissionRetryCount;
            while (_telemetryClient.OnlineStatus == false && retryLeft > 0)
            {
                _telemetryClient.Connect(Consts.TelemetryClientDiagnosticConnectionString);
                retryLeft--;
            }

            if (_telemetryClient.OnlineStatus == false)
            {
                throw new Exception(Consts.Errors.ConnectionError);
            }

            _telemetryClient.Send(Consts.TelemetryClientDiagnosticMessage);
            DiagnosticInfo = _telemetryClient.Receive();
        }
    }
}
