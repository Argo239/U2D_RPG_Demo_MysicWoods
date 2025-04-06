using System;
using System.Collections.Generic;
using System.Timers;
using System.Threading.Tasks;

/// <summary>
/// 通用计时器工具类
/// </summary>
public class TimerTool {
    private readonly Dictionary<int, Timer> timers = new Dictionary<int, Timer>();
    private static int timerIdCounter = 0;

    /// <summary>
    /// 单次延迟执行任务
    /// </summary>
    /// <param name="delay">延迟时间（毫秒）</param>
    /// <param name="action">回调函数</param>
    public static async void StartTimer(int delay, Action action) {
        await Task.Delay(delay);
        action?.Invoke();
    }

    /// <summary>
    /// 创建可控的定时器（间隔执行）
    /// </summary>
    /// <param name="interval">执行间隔（毫秒）</param>
    /// <param name="action">回调函数</param>
    /// <param name="repeatCount">执行次数，-1 为无限循环</param>
    /// <returns>计时器 ID（用于停止）</returns>
    public int StartRepeatingTimer(int interval, Action action, int repeatCount = -1) {
        int timerId = ++timerIdCounter;
        Timer timer = new Timer(interval);
        int executionCount = 0;

        timer.Elapsed += (sender, e) => {
            action?.Invoke();
            if (repeatCount > 0 && ++executionCount >= repeatCount) {
                StopTimer(timerId);
            }
        };

        timer.AutoReset = true;
        timer.Start();
        timers[timerId] = timer;
        return timerId;
    }

    /// <summary>
    /// 取消指定计时器
    /// </summary>
    public void StopTimer(int timerId) {
        if (timers.ContainsKey(timerId)) {
            timers[timerId].Stop();
            timers[timerId].Dispose();
            timers.Remove(timerId);
        }
    }

    /// <summary>
    /// 倒计时任务
    /// </summary>
    /// <param name="duration">倒计时秒数</param>
    /// <param name="onUpdate">每秒更新回调</param>
    /// <param name="onComplete">倒计时结束回调</param>
    public static async void StartCountdown(int duration, Action<int> onUpdate, Action onComplete) {
        for (int i = duration; i > 0; i--) {
            onUpdate?.Invoke(i);
            await Task.Delay(1000);
        }
        onComplete?.Invoke();
    }
}
