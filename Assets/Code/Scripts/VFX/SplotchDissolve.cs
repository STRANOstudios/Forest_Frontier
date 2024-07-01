using DG.Tweening;
using UnityEngine;

[DisallowMultipleComponent]
public class SplotchDissolve : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _fadeDuration = 0.5f;

    [Header("References")]
    [SerializeField] private CanvasGroup _canvas;
    [SerializeField] private CanvasGroup _canvasMenu;

    [SerializeField] private Transform _splotch;

    [SerializeField] private ParticleSystem _effect;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    public void Dissolve()
    {
        _canvas.DOFade(1f, _fadeDuration);
        _canvasMenu.DOFade(1f, _fadeDuration);
        _splotch.DOScale(1f, _fadeDuration);
        _effect.Play();
    }

    private void Reset()
    {
        _canvas.DOFade(0f, _fadeDuration);
        _canvasMenu.alpha = 0f;

        _splotch.localScale = new(.1f, .1f, .1f);

        _effect.Stop();
    }
}
