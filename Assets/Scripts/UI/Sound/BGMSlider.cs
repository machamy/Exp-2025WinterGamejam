using UnityEngine;
using UnityEngine.UI;

public class BGMSlider : MonoBehaviour
{
    // [SerializeField] private Slider bgmSlider;
    // private SoundManager soundManager;
    //
    // void Start()
    // {
    //     // SoundManager �ν��Ͻ��� ã��
    //     soundManager = FindObjectOfType<SoundManager>();
    //
    //     // ����� �����̴� �� �ҷ�����
    //     float savedVolume = PlayerPrefs.GetFloat("bgmVolume", 1.0f); // �⺻���� 1.0f
    //     if (soundManager != null && bgmSlider != null)
    //     {
    //         bgmSlider.value = savedVolume; // ����� ������ �����̴� �ʱ�ȭ
    //         bgmSlider.onValueChanged.AddListener(SoundManager.Instance.OnBgmVolumeChange);
    //     }

        // ������� ���� ����
        //SetBGMVolume(savedVolume);
    // }

    //void SetBGMVolume(float value)
    //{
    //    if (soundManager != null)
    //    {
    //        soundManager.SetBGMVolume(value);
    //        PlayerPrefs.SetFloat("BGMVolume", value); // ���� �� ����
    //        PlayerPrefs.Save(); // ����� ������ ��� ����
    //    }
    //}
}
