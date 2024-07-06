using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class StateController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider hungerSlider;
    [SerializeField] private Slider thirstSlider;

    private void OnEnable()
    {
        Sim.OnHungerChanged += SetHunger;
        Sim.OnThirstChanged += SetThirst;
    }

    private void OnDisable()
    {
        Sim.OnHungerChanged -= SetHunger;
        Sim.OnThirstChanged -= SetThirst;
    }

    private void SetHunger(int value)
    {
        SetSliderValue(value, hungerSlider);
    }

    private void SetThirst(int value)
    {
        SetSliderValue(value, thirstSlider);
    }

    private void SetSliderValue(int value, Slider slider)
    {
        // Clamp value between 0 and 100 (assuming value range is 0-100)
        float normalizedValue = Mathf.Clamp01(value / 100f);
        slider.value = normalizedValue;
    }
}
