using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

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
