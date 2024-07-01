using DG.Tweening;
using UnityEngine;

[DisallowMultipleComponent]
public class SplotchDissolve : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, Min(0f)] private float _fadeDuration = 0.5f;

    [Header("References")]
    [SerializeField] private CanvasGroup _canvas;
    [SerializeField] private Transform _splotch;
    [SerializeField] private ParticleSystem _effect;

    private void OnEnable()
    {
        PauseManager.IsPaused += Normalized;
        MenuController.Resume += Reset;
    }

    private void OnDisable()
    {
        PauseManager.IsPaused -= Normalized;
        MenuController.Resume -= Reset;
    }

    private void Normalized(bool value)
    {
        if (!value) Reset();
        else Dissolve();
    }

    private void Dissolve()
    {
        _canvas.DOFade(1f, _fadeDuration).SetUpdate(true);
        _splotch.DOScale(1f, _fadeDuration).SetUpdate(true);
        _effect.Play();
    }

    private void Reset()
    {
        _canvas.DOFade(0f, _fadeDuration).SetUpdate(true);
        _splotch.DOScale(0.1f, _fadeDuration).SetUpdate(true);
        _effect.Stop();
    }
}
