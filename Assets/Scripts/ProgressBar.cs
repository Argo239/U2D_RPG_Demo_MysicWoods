using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ProgressBar {
    private float _value;
    private float _maxValue;

    public float topBarValue { get; private set; }
    public float bottomBarValue { get; private set; }

    private float _animationSpeed = 10f;
    private Coroutine _activeCoroutine;

    public ProgressBar(float value, float maxValue) {
        _value = value;
        _maxValue = maxValue;

        // 初始化进度条值
        topBarValue = value / maxValue;
        bottomBarValue = value / maxValue;
    }

    public void Update(float value, float maxValue) {
        _value = value;
        _maxValue = maxValue;

        float targetValue = _value / _maxValue;

        if (_activeCoroutine != null) CoroutineRunner.instance.StopCoroutine(_activeCoroutine);


        if (targetValue > topBarValue) {
            bottomBarValue = targetValue;
            _activeCoroutine = CoroutineRunner.instance.StartCoroutine(AdjustBarWidth(true, targetValue));
        } else if (targetValue < bottomBarValue) {
            topBarValue = targetValue;
            _activeCoroutine = CoroutineRunner.instance.StartCoroutine(AdjustBarWidth(false, targetValue));
        }
    }

    private IEnumerator AdjustBarWidth(bool isAdd, float targetValue) {
        if (isAdd) {
            while (!Mathf.Approximately(topBarValue, targetValue)) {
                topBarValue = Mathf.Lerp(topBarValue, targetValue, Time.deltaTime * _animationSpeed);
                yield return null;
            }
            topBarValue = targetValue;
        } else {
            while (!Mathf.Approximately(bottomBarValue, targetValue)) {
                bottomBarValue = Mathf.Lerp(bottomBarValue, targetValue, Time.deltaTime * _animationSpeed);
                yield return null;
            }
            bottomBarValue = targetValue;
        }
    }
}