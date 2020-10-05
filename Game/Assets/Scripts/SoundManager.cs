using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SoundManager : MonoBehaviour
{

    public static SoundManager main;
    [SerializeField]
    private List<GameSound> sounds;
    void Awake() {
        main = this;
    }

    public void PlaySound(SoundType soundType) {

        GameSound gameSound = sounds.Where(s => s.SoundType == soundType).FirstOrDefault<GameSound>();
        if (gameSound != null) {

            if (gameSound.Sounds.Count > 0) {
                AudioSource audioSource = gameSound.Sounds[Random.Range(0, gameSound.Sounds.Count)];
                audioSource.Play();

            }
        }
    }
}

[System.Serializable]
public class GameSound {
    public SoundType SoundType;
    public List<AudioSource> Sounds;
}

public enum SoundType {
    None,
    Whack,
    Swish,
    Explode,
    NewItem
}