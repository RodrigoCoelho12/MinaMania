using System.Collections;
using UnityEngine;

[System.Serializable]
public class StatusBar
{
    [Header("[Status Bar] Properties")]	
    [SerializeField] private float _minBarValue = 0f;
    [SerializeField] private float _maxBarValue = 100f;
    [SerializeField] private float _currentValue;

    public float CurrentBarValue
    {
        get => _currentValue;
        set => _currentValue = AdjustToClosestBoundary(value);
    }

    public float MaxBarValue
    {
        get => _maxBarValue;
        set => _maxBarValue = Mathf.Max(_minBarValue + 1f, value); // prevent max from being too small
    }

    public float MinBarValue => _minBarValue;
    public void Init(float maxValue, float currentValue)
    {
        MaxBarValue = maxValue;
        CurrentBarValue = currentValue;
    }

    #region Adjustment Methods

    public void AdjustStatusBarBySum(float portion)
    {
        //MaxBarValue += portion;
        CurrentBarValue += portion;
        _currentValue = AdjustToClosestBoundary(_currentValue);
    }

    public void AdjustStatusBarBySubtraction(float portion)
    {
        //MaxBarValue -= portion;
        CurrentBarValue -= portion;
        _currentValue = AdjustToClosestBoundary(_currentValue);
    }

    public void AdjustStatusBarByMultiplication(float portion)
    {
        //MaxBarValue *= portion;
        CurrentBarValue *= portion;
        _currentValue = AdjustToClosestBoundary(_currentValue);
    }

    public IEnumerator AdjustStatusBarByTime(float portion, float time)
    {
        float startValue = CurrentBarValue;
        float endValue = AdjustToClosestBoundary(CurrentBarValue + portion);
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            CurrentBarValue = Mathf.Lerp(startValue, endValue, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        CurrentBarValue = endValue;
    }

    private float AdjustToClosestBoundary(float value)
    {
        if (value > _maxBarValue)
            return _maxBarValue;
        else if (value < _minBarValue)
            return _minBarValue;
        return value;
    }

    #endregion
}
