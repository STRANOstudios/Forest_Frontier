using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
public class BehaviorTreeState : MonoBehaviour
{
    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        Sim.OnBehaviorChanged += SetText;
    }

    private void OnDisable()
    {
        Sim.OnBehaviorChanged -= SetText;
    }

    public void SetText(string value)
    {
        _text.text = value;
    }
}
