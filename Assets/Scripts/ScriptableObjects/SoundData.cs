using UnityEngine;


[CreateAssetMenu(fileName = "SoundData", menuName = "ScriptableObjects/SoundData", order = 1)]
public class SoundData : ScriptableObject
{
    public AudioClip MainBgm;
    public AudioClip StageBgm;
    public AudioClip RocketExplosion;
    public AudioClip RocketBoost;
    public AudioClip GameOver;
    public AudioClip MiYeonsi;
    public AudioClip Ending;
    
    public enum Sound
    {
        MainBgm,
        StageBgm,
        RocketExplosion,
        RocketBoost,
        GameOver,
        MiYeonsi,
        Ending
    }
    
    public AudioClip GetSound(Sound sound)
    {
        switch (sound)
        {
            case Sound.MainBgm:
                return MainBgm;
            case Sound.StageBgm:
                return StageBgm;
            case Sound.RocketExplosion:
                return RocketExplosion;
            case Sound.RocketBoost:
                return RocketBoost;
            case Sound.GameOver:
                return GameOver;
            case Sound.MiYeonsi:
                return MiYeonsi;
            case Sound.Ending:
                return Ending;
            default:
                return null;
        }
    }
}
