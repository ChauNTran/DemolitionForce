//Audio Manager to manager both sfx and music
//Sam
//how to call from othe scripts
//create public AudioClip shootSound;
// EXample: AudioManager.instance.PlaySound(shootSound, transform pos);
//Access sound group: AudioManager.instance.PlaySound("enemydeath", transform.position
//Note: use PLAYSOUND2D for win noises or collecting objects
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public enum AudioChannel {Master, Sfx, Music};

	[HideInInspector]
	public float masterVolumePercent = 1;
	public float sfxVolumePercent = 1;
	public float musicVolumePercent = 1;

	AudioSource sfx2DSource;
	AudioSource[] musicSources;
	int activeMusicSourceIndex;

	public static AudioManager instance;

	Transform audioListener;
	Transform playerT;

	SoundLibrary library;

	void Awake() {
		//so this game object can exist in all scenes
		if(instance != null){
			Destroy(gameObject);
		}else{

		instance = this;

		DontDestroyOnLoad (gameObject);
		//

		//align audiosource volume with player preferences volume
		masterVolumePercent = PlayerPrefsManager.GetMasterVolume ();
		musicVolumePercent = PlayerPrefsManager.GetSFXVolume ();
		sfxVolumePercent = PlayerPrefsManager.GetMusicVolume ();



		//access to sound library 
		library = GetComponent<SoundLibrary> ();
		//create two audio source componenets so one can play and fade between each other
		musicSources = new AudioSource[2];

			for (int i = 0; i < 2; i++) {
				GameObject newMusicSource = new GameObject ("Music source " + (i + 1));
				musicSources[i] = newMusicSource.AddComponent<AudioSource> ();
				newMusicSource.transform.parent = this.transform;
			}

			GameObject newSfxDsource = new GameObject ("2D sfx source ");
			sfx2DSource = newSfxDsource.AddComponent<AudioSource> ();
			newSfxDsource.transform.parent = this.transform;

			audioListener = FindObjectOfType<AudioListener> ().transform;
			playerT = FindObjectOfType<Camera>().transform;
		}
	}

	void Update(){
		//if player isnt found or if it is gone
		if(playerT != null){
			audioListener.position = playerT.position;
		}
	}
	//Called From Options Manager from pause UI and from Settings scene
	public void SetVolume(float volumePercent, AudioChannel channel){
		switch (channel) {
		case AudioChannel.Master:
			masterVolumePercent = volumePercent;
			break;
		case AudioChannel.Sfx:
			sfxVolumePercent = volumePercent;
			break;
		case AudioChannel.Music:
			musicVolumePercent = volumePercent;
			break;
		}
		if (activeMusicSourceIndex == 0) {
			musicSources [0].volume = musicVolumePercent * masterVolumePercent;
		} else {
			musicSources [1].volume = musicVolumePercent * masterVolumePercent;
		}

		PlayerPrefsManager.SetMasterVolume (masterVolumePercent);
		PlayerPrefsManager.SetMusicVolume (musicVolumePercent);
		PlayerPrefsManager.SetSFXVolume (sfxVolumePercent);

	}
	//PLAYS MUSIC
	public void PlayMusic(AudioClip clip, float fadeDuration = 1){
		
			activeMusicSourceIndex = 1 - activeMusicSourceIndex;
		if (clip != null) {
			musicSources [activeMusicSourceIndex].clip = clip;
			musicSources [activeMusicSourceIndex].Play ();
		} else {
			musicSources [activeMusicSourceIndex].clip = clip;
		}
		StartCoroutine (AnimateMusicCrossfade (fadeDuration));
	}

	//Plays 3D sound fx sounds
	public void PlaySound(AudioClip clip, Vector3 pos) {
		if (clip != null) {
			AudioSource.PlayClipAtPoint (clip, pos, sfxVolumePercent * masterVolumePercent);
		}
	}
	//Plays 3D sound fx from sfx library group
	public void PlaySound (string soundName, Vector3 pos){
		PlaySound (library.GetClipFromName (soundName), pos);
	}
	//Plays 2Dsoundfx from library group
	public void PlayerSound2D (string soundName){
		sfx2DSource.PlayOneShot (library.GetClipFromName (soundName), sfxVolumePercent * masterVolumePercent);
	}
	public void PlayerSound2D(AudioClip clip){
		sfx2DSource.PlayOneShot(clip,sfxVolumePercent * masterVolumePercent);
	}

	IEnumerator AnimateMusicCrossfade(float duration){
		float percent = 0;
		while (percent < 1) {
			percent += Time.deltaTime * 1 / duration; 
			musicSources [activeMusicSourceIndex].volume = Mathf.Lerp (0, musicVolumePercent * masterVolumePercent, percent);
			musicSources [1-activeMusicSourceIndex].volume = Mathf.Lerp (musicVolumePercent * masterVolumePercent,0, percent);
			yield return null;

		}
	}
}
