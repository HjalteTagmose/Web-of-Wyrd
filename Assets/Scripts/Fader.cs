using UnityEngine;

public class Fader : MonoBehaviour
{
	public SpriteRenderer sr;
	public float fadeDuration = 1.0f;
	private float timer = 0f;
	private bool fadeIn = false;
	private bool fadeOut = false;
	public Color originalColor;
	public bool fadeOnStart = true;
	public bool clearOnStart = false;

	void Start()
	{
		sr = GetComponent<SpriteRenderer>();
		if (clearOnStart) sr.color = Color.clear;
		if (fadeOnStart) FadeOut(2f);
	}

	public void FadeIn(float duration)
	{
		fadeIn = true;
		fadeOut = false;
		fadeDuration = duration;
		sr.color = Color.clear;
	}

	public void FadeOut(float duration)
	{
		fadeOut = true;
		fadeIn = false;
		fadeDuration = duration;
		sr.color = originalColor;
	}

	void Update()
	{
		if (fadeIn)
		{
			timer += Time.deltaTime;
			sr.color = Color.Lerp(Color.clear, originalColor, timer / fadeDuration);
			if (timer >= fadeDuration)
			{
				fadeIn = false;
				timer = 0f;
			}
		}
		if (fadeOut)
		{
			timer += Time.deltaTime;
			sr.color = Color.Lerp(originalColor, Color.clear, timer / fadeDuration);
			if (timer >= fadeDuration)
			{
				fadeOut = false;
				timer = 0f;
			}
		}
	}
}
