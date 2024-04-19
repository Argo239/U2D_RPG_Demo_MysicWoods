using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;
using System.Reflection;

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

    public static bool HasAnimation<T>(string animationState) where T : SetAnimationReferenceAsset {
        FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        foreach (FieldInfo field in fields) {
            if(field.Name.Equals(animationState)){
                return true;
            }
        }
        return false;
    }


}
