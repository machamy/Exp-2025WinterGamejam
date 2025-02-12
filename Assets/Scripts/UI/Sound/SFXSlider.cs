using UnityEngine;
using UnityEngine.UI;

public class SFXSlider : MonoBehaviour
{
    [SerializeField] private Slider sfxSlider;
    private SoundManager soundManager;

    void Start()
    {
        // SoundManager �ν��Ͻ��� ã��
        soundManager = FindObjectOfType<SoundManager>();

        // ����� �����̴� �� �ҷ�����
        float savedVolume = PlayerPrefs.GetFloat("effectVolume", 1.0f); // �⺻���� 1.0f
        if (soundManager != null && sfxSlider != null)
        {
            sfxSlider.value = savedVolume; // ����� ������ �����̴� �ʱ�ȭ
            sfxSlider.onValueChanged.AddListener(SoundManager.Instance.OnEffectVolumeChange);
        }

        // ȿ���� ���� ����
        //SetSFXVolume(savedVolume);
    }

    //void SetSFXVolume(float value)
    //{
    //    if (soundManager != null)
    //    {
    //        soundManager.SetEffectVolume(value); // SoundManager�� ȿ���� ���� ����
    //        PlayerPrefs.SetFloat("SFXVolume", value); // ���� �� ����
    //        PlayerPrefs.Save(); // ����� ������ ��� ����
    //    }
    //}
}
