﻿using System;

public abstract class TimerUtil {
    protected float initialTime;
    public float Time { get; set; }
    public bool IsRunning { get; protected set; }

    public float Progress => Time / initialTime;

    public Action OnTimerStart = delegate { };
    public Action OnTimerStop = delegate { };

    protected TimerUtil(float value) {
        initialTime = value;
        IsRunning = true;
    }

    public void Start() {
        Time = initialTime;
        if (!IsRunning) {
            IsRunning = true;
            OnTimerStart.Invoke();
        }
    }

    public void Stop() {
        if (IsRunning) {
            IsRunning = false;
            OnTimerStop.Invoke();
        }
    }

    public void Resume() => IsRunning = true;
    public void Pause() => IsRunning = false;

    public abstract void Tick(float deltaTime);
}