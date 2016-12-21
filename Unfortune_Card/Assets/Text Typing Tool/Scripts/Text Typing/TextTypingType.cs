using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TextTypingType : MonoBehaviour
{
	public string m_name;

	public int speedTyping = 200;
	public int speedTypingRandom = 100;

	public float timeEffect = 1;

	public float soundDelay;
	public bool soundPermanently;

	public TextTypingEffect effect;
	public TextTypingCursor cursor;
	public GameObject sounds;

	private List<TextTypingSoundList> soundLetters;
	private TextTypingSoundList soundSpace;
	private TextTypingSoundList soundBrake;
	private TextTypingSoundList soundAny;

	private TextTypingSoundList[] listSounds;

	private TextTypingSoundList curRandomSounds;
	private TextTypingSound curSound;

	public void Init()
	{
		if (sounds == null)
			return;

		listSounds = sounds.GetComponents<TextTypingSoundList>();
		soundLetters = new List<TextTypingSoundList>();

		// forming lists with sounds
		foreach (TextTypingSoundList sound in listSounds)
		{
			switch(sound.letters)
			{
				case "<space>":
					soundSpace = sound;
					break;

				case "<br>":
					soundBrake = sound;
					break;

				case "<any>":
					soundAny = sound;
					break;

				default:
					soundLetters.Add(sound);
					break;
			}
		}

	}

	public void PlaySound(DC_TextTypingLetter letterData)
	{
		if (sounds == null)
			return;

		string letter = letterData.letter.ToString();
		TextTypingSoundList randomSounds = null;

		if (letter == " ")
		{
			// <space>
			randomSounds = soundSpace;
		}

		if (letter == "\n")
		{
			// <br>
			randomSounds = soundBrake;
		}

		if (randomSounds == null && soundLetters.Count > 0)
		{
			// search sound letters
			foreach(TextTypingSoundList item in soundLetters)
			{
				if(item.letters.Contains(letter))
				{
					randomSounds = item;
					break;
				}
			}
		}

		if (randomSounds == null && soundAny != null)
		{
			// We did not find the sound to the character set <any>
			randomSounds = soundAny;
		}

		if (soundPermanently)
		{
			// continuous sound
			if(curRandomSounds != randomSounds)
			{
				// new sound
				curRandomSounds = randomSounds;
				if(curSound != null)
					curSound.Remove();
			}
			else
			{
				// playing old sound
				if(curSound != null)
				{
					// the sound is not ended
					return;
				}
			}
		}

		if(randomSounds != null && randomSounds.clips.Count > 0)
		{
			// select random sound
			GameObject go = new GameObject();
			go.name = "Sound";
			AudioClip clip = randomSounds.clips[Random.Range(0, randomSounds.clips.Count)];
			go.AddComponent<TextTypingSound>().SetData(clip, soundDelay);
			curSound = go.GetComponent<TextTypingSound>();
		}

	}

}
