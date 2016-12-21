using UnityEngine;
using System.Collections;

public class TextTypingSound : MonoBehaviour
{
	private AudioSource audioSource;
	private float timer;

	void Awake()
	{
		audioSource = gameObject.AddComponent<AudioSource>();
	}

	public void SetData(AudioClip clip, float delay)
	{
		audioSource.clip = clip;
		audioSource.PlayDelayed(System.Convert.ToUInt64(delay));
	}

	public void Remove()
	{
		timer = 0.1f;
		Destroy(gameObject, timer);
	}

	void Update ()
	{
		if (timer > 0)
		{
			// gently turn the sound off
			audioSource.volume = timer * 10;
		}

		if (audioSource.isPlaying)
			return;

		Destroy(gameObject);
	}
}
