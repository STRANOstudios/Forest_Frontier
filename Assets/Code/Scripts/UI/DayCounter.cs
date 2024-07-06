using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
public class DayCounter : MonoBehaviour
{
    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        TimeManager.OnDayChange += OnDayChange;

    }

    private void OnDisable()
    {
        TimeManager.OnDayChange -= OnDayChange;
    }

    private void OnDayChange(float value)
    {
        _text.text = $"{(int)value}";
    }
}
