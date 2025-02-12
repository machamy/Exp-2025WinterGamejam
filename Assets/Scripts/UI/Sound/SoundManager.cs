using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : SingletonBehaviour<SoundManager>
{
    // private static SoundManager instance = null;
    // public static SoundManager Instance
    // {
    //     get
    //     {
    //         if (instance == null)
    //         {
    //             return null;
    //         }
    //         return instance;
    //     }
    // }
    // private void Awake()
    // {
    //     if (instance == null)
    //     {
    //         instance = this;
    //         DontDestroyOnLoad(this.gameObject);
    //     }
    //     else
    //     {
    //         Destroy(this.gameObject);
    //     }
    // }

    AudioClip bgmMain; // ���� �����Ŭ��
    AudioClip bgmStage; // �������� �����Ŭ��


    private AudioSource audioSource1; // ����� ������ҽ�, ��������� �����ؼ� �����
    private AudioSource audioSource2; // ȿ���� ������ҽ�, ȿ�������� �����ؼ� �����

    void Start() // ���� ó�� ���۽� ���Ǽ���
    {
        // ���� �÷��̾� �������� ����� bgm�� effect�� Volume���� �ִٸ� �ҷ��´�. ������ ������ ������ ���� ���� �����ϱ� ����.
        if (!PlayerPrefs.HasKey("bgmVolume")) PlayerPrefs.SetFloat("bgmVolume", 1.0f);
        if (!PlayerPrefs.HasKey("effectVolume")) PlayerPrefs.SetFloat("effectVolume", 1.0f);

        // audioSource�� AudioSource ������Ʈ�� �߰�
        audioSource1 = gameObject.AddComponent<AudioSource>();
        audioSource2 = gameObject.AddComponent<AudioSource>();
        audioSource1.loop = true;

        // ����� Ŭ���� ����� �߰�
        bgmMain = Resources.Load<AudioClip>("Sounds/SampleBGM");
        bgmStage = Resources.Load<AudioClip>("Sounds/SampleBossBGM");


        MainBgmOn(); // ���� ���۽� ���θ޴����� ������Bgm ���
    }

    public void MainBgmOn()
    {
        audioSource1.clip = bgmMain;
        audioSource1.volume = PlayerPrefs.GetFloat("bgmVolume"); // �÷��̾����������� bgmVolume �� ��������
        audioSource1.Play();
    }
    public void StageBgmOn()
    {
        audioSource1.clip = bgmStage;
        audioSource1.volume = PlayerPrefs.GetFloat("bgmVolume");
        audioSource1.Play();
    }


    //�ɼ�â ���� �����̴����� �� ����� ������ҽ��� ������ �����ϰ� �� ���� �÷��̾� �������� ����
    public void OnBgmVolumeChange(float volume)
    {
        audioSource1.volume = volume;
        PlayerPrefs.SetFloat("bgmVolume", volume);
    }
    public void OnEffectVolumeChange(float volume)
    {
        audioSource2.volume = volume;
        PlayerPrefs.SetFloat("effectVolume", volume);
    }

    // ���ϴ� ���� ȿ���� �߰� ���� �Լ�
    // SoundManager.Instance.EffectSoundOn("Walk")�� ���� ���
    public void EffectSoundOn(string effectName)
    {
        string effect = "Sounds/" + effectName;
        AudioClip effectClip = Resources.Load<AudioClip>(effect);
        audioSource2.volume = PlayerPrefs.GetFloat("effectVolume"); // �÷��̾����������� effectVolume �� ��������
        audioSource2.clip = effectClip;
        audioSource2.PlayOneShot(effectClip);
    }

    public void EffectSoundOff()
    {
        audioSource2.Stop();
    }
}
