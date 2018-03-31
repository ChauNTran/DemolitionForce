//This is here to make sure this audio source volume is always set at the player preferences sound fx volume.
using UnityEngine;
using System.Collections;

public class RegulateAudioSource : MonoBehaviour {

	AudioSource audioSource;

	void Start () {
		audioSource = GetComponent<AudioSource>();
	}
	
	void Update () {
		audioSource.volume = AudioManager.instance.sfxVolumePercent * AudioManager.instance.masterVolumePercent;
	}
}
