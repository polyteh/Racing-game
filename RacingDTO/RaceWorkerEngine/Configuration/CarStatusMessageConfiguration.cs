using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingDTO.RaceWorkerEngine.Configuration
{

    public static class CarStatusMessageConfiguration
    {
        public enum MessageCodes { Failture = 1, FailtureDueToEngineOverheating = 2, BrakingDueToEngineOverheating = 3, SpeedBurst = 4, Finishaed=5};
        private static readonly Dictionary<MessageCodes, string> statusMessageDictionary = new Dictionary<MessageCodes, string>() {
            { MessageCodes.Failture, "Car is damaged in the accident." },
            { MessageCodes.FailtureDueToEngineOverheating, "Engine is overheated. Car is dead." },
            { MessageCodes.BrakingDueToEngineOverheating, "Car needs to brake, engine is practically overheated." },
            { MessageCodes.SpeedBurst, "Great acceleration. Wow." },
            { MessageCodes.Finishaed, "Finished" },
        };
        public static string GetMessage(MessageCodes messCode)
        {
            string statusMessage="Status is unknown";
            if (statusMessageDictionary.TryGetValue(messCode, out statusMessage ))
            {
                return statusMessage;
            }
            return statusMessage;
        }
    }
}
