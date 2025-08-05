using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
	[SerializeField]
	private Graphic _target;

	[SerializeField]
	private float _fadeTime = 1;

	[SerializeField]
	private float _fadeOutAlphaValue = 0;

	[SerializeField]
	private float _fadeInAlphaValue = 1;

	private Coroutine _fadeCoroutine;

	public float FadeTime => _fadeTime;

	public void FadeInInstantly()
	{
		Fade(_fadeOutAlphaValue, _fadeInAlphaValue, 0, null);
	}

	public void FadeIn(Action callback)
	{
		Fade(_fadeOutAlphaValue, _fadeInAlphaValue, _fadeTime, callback);
	}

	public void FadeIn()
	{
		FadeIn(null);
	}

	public void FadeOut(Action callback)
	{
		Fade(_fadeInAlphaValue, _fadeOutAlphaValue, _fadeTime, callback);
	}

	public void FadeOut()
	{
		FadeOut(null);
	}

	private void Fade(float startAlpha, float targetAlpha, float fadeTime, Action callback)
	{
		Color color = _target.color;
		color.a = startAlpha;
		_target.color = color;

		if (_fadeCoroutine != null)
		{
			StopCoroutine(_fadeCoroutine);
			_fadeCoroutine = null;
		}
		_fadeCoroutine = StartCoroutine(FadeCoroutine(_target, targetAlpha, fadeTime, callback));
	}

	private IEnumerator FadeCoroutine(Graphic target, float targetAlpha, float fadeTime, Action callback)
	{
		float startAlpha = target.color.a;
		float elapsed = 0f;

		while (elapsed < fadeTime)
		{
			elapsed += Time.deltaTime;
			float alpha = Mathf.Lerp(startAlpha, targetAlpha, fadeTime > 0f ? elapsed / fadeTime : 1f);
			Color c = target.color;
			c.a = alpha;
			target.color = c;
			yield return null;
		}

		Color finalColor = target.color;
		finalColor.a = targetAlpha;
		target.color = finalColor;

		callback?.Invoke();
		_fadeCoroutine = null;
	}
}
