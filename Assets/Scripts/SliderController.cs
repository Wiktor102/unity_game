using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Image Image; // Image
    public Gradient Gradient; // Gradient
    private int _currentValue;// aktualna wartość
    private int _maxValue; // wartość maksymalna
    private Slider _slider; // Slider

    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponentInChildren<Slider>();
        _maxValue = 100;
        _currentValue = 100;
        Image.color = Gradient.Evaluate(1f);

        SetMaxValue(_maxValue);
        Set(_currentValue);
    }

    public void SetMaxValue(int newMaxValue)
    {
        _slider.maxValue = newMaxValue;
    }

    public void Set(int currentValue)
    {
        if (currentValue > _slider.maxValue)
            currentValue = (int)_slider.maxValue;

        _slider.value = currentValue;
        _currentValue = currentValue;
        Image.color = Gradient.Evaluate(_slider.normalizedValue);
    }
}
