using UnityEngine;

namespace Argo_Utils
{
    public static class Utils {

        //private static float time;
        //public static float CountdownTimer(float remainingTime) {
        //    if (remainingTime > 0f) {
        //        time = remainingTime - Time.deltaTime;
        //    }
        //    return time;
        //}

        public static void LogMessage(bool consoleMessage, object message){
            if (consoleMessage) Debug.Log(message); 
        }
    }
}
