using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TMPro.Examples;

public class Stage2ClosingTextPopup : MonoBehaviour
{
    [Header("���� ��ҵ�")]
    public TMP_Text ChatText;      // ���� ä���� ������ �ؽ�Ʈ
    public TMP_Text CharacterName; // ĳ���� �̸��� ������ �ؽ�Ʈ
    public GameObject ClosingTextPanel;  // Ŭ��¡ ��ũ��Ʈ �г�
    [SerializeField] private Rocket rocket;
    public Button NextButton;
    public Button SelectButton1;
    public Button SelectButton2;

    [Header("�÷��̾� ������Ʈ")]
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Fuel;

    public static bool istakenStage2 = false;

    [Header("Ŭ��¡ ��ũ��Ʈ ĳ���� ��������Ʈ")]
    public GameObject CharacterPose1; 
    public GameObject CharacterPose2;


    private bool isFullTextDisplayed = false;
    private bool isNextButtonClicked = false;
    public string writerText = "";
    public static bool isFirstTime1 = true;
    void Start()
    {
        
    }

    void Update()
    {

    }

    void Open()
    {
        GameManager.Instance.State = GameManager.GameState.Dialog;
        ClosingTextPanel.SetActive(true);
        Time.timeScale = 0f; // ������ �Ͻ������ϰ� ��ȭ�� ����

        // ��ư ������ ����(�ߺ� ��� ���� ���� ���� RemoveAllListeners)
        NextButton.onClick.RemoveAllListeners();
        
        SelectButton1.onClick.RemoveAllListeners();
        SelectButton2.onClick.RemoveAllListeners();

        // ��ư Ŭ���� ���� ����
        NextButton.onClick.AddListener(OnNextButtonClicked);
        
        SelectButton1.onClick.AddListener(OnSelectButton1Clicked);
        SelectButton2.onClick.AddListener(OnSelectButton2Clicked);

        
        // ���� �ó����� �ڷ�ƾ ����
        StartCoroutine(ClosingText());

    }
    public void OnClear()
    {
        Open();
    }
    void OnNextButtonClicked()
    {
        if (!isFullTextDisplayed)
        {
            isSkipping = true;
        }
        // �̹� ��簡 �� ��������, ���� ���� �Ѿ�� ��ȣ
        else
        {
            isNextButtonClicked = true;
        }
    }
    public void OnSelectButton1Clicked()
    {
        // ���� ��ư ��� ��� Ŭ���Ұ�
        SelectButton1.interactable = false;
        SelectButton2.interactable = false;
        // ���õ� ��ư�� ������ ����ȭ
        StartCoroutine(FadeOutBranchButton(SelectButton1, 1f));
        // �ٸ� ��ư�� �Բ� ������ ����ȭ
        StartCoroutine(FadeOutBranchButton(SelectButton2, 1f));
        
        // ������ 1�� ���� �б� �ó����� �ڷ�ƾ ����
        StartCoroutine(TextSelect1());
    }
    public void OnSelectButton2Clicked()
    {
        // ���� ��ư ��� ��� Ŭ���Ұ�
        SelectButton1.interactable = false;
        SelectButton2.interactable = false;
        // ���õ� ��ư�� ������ ����ȭ
        StartCoroutine(FadeOutBranchButton(SelectButton1, 1f));
        // �ٸ� ��ư�� �Բ� ������ ����ȭ
        StartCoroutine(FadeOutBranchButton(SelectButton2, 1f));

        // ������ 1�� ���� �б� �ó����� �ڷ�ƾ ����
        StartCoroutine(TextSelect2());
    }
    public void OnSkipButtonClicked()
    {
        CloseClosingText();
    }
    public void ExitStage1()
    {
        isFirstTime1 = true;
    }

    public float typingSpeed = 0.02f;
    public bool isSkipping = false;
    IEnumerator NormalChat(string narrator, string narration)
    {
        isFullTextDisplayed = false;
        isSkipping = false;

        int a = 0;
        CharacterName.text = narrator;
        ChatText.text = "";
        writerText = "";

        // �ؽ�Ʈ Ÿ���� ȿ��
        for (a = 0; a < narration.Length; a++)
        {
            if (isSkipping)
            {
                writerText = narration;
                ChatText.text = writerText;
                break;
            }
            writerText += narration[a];
            ChatText.text = writerText;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
        isFullTextDisplayed = true;
        isSkipping = false;
        // Ű�� �ٽ� ���� ������ ������ ���
        isNextButtonClicked = false;
        yield return new WaitUntil(() => isNextButtonClicked);
    }
    IEnumerator FadeOutBranchButton(Button branchButton, float fadeDuration)
    {
        // ���� �ش� ��ư�� Image�� TMP_Text�� �ִٸ� ����
        Image buttonImage = branchButton.GetComponent<Image>();
        TMP_Text buttonText = branchButton.GetComponentInChildren<TMP_Text>();

        if (buttonImage == null && buttonText == null)
        {
            yield break;
        }

        Color startButtonColor = buttonImage != null ? buttonImage.color : Color.white;
        Color startTextColor = buttonText != null ? buttonText.color : Color.white;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            // Time.timeScale�� 0�̾ ���̵尡 ����ǰ� �Ϸ��� unscaledDeltaTime ���
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            float alpha = Mathf.Lerp(1f, 0f, t);

            if (buttonImage != null)
            {
                buttonImage.color = new Color(
                    startButtonColor.r,
                    startButtonColor.g,
                    startButtonColor.b,
                    alpha
                );
            }

            if (buttonText != null)
            {
                buttonText.color = new Color(
                    startTextColor.r,
                    startTextColor.g,
                    startTextColor.b,
                    alpha
                );
            }

            yield return null;
        }

        // ���⼭�� ��ư GameObject�� ���� ����
        // ���� ��Ȱ��ȭ�� CloseClosingText() ������ �̷����
    }
    IEnumerator ClosingText() //("�����ι�", "���")�� �Է�
    {
        yield return StartCoroutine(NormalChat("���ϳ�", "�ȳ�!! �ݰ���!!!���� ���ϳ���"));
        yield return StartCoroutine(NormalChat("���ϳ�", "���� ã���� ���ᱸ��!"));
        yield return StartCoroutine(NormalChat("���ϳ�", "�ƹ��� �ȿ��� �� �˰� ��黷���ݾ�"));
        yield return StartCoroutine(NormalChat("���ϳ�", "�츮 �༺�� �� ������ ���� �ɰžߡ�"));
        yield return StartCoroutine(NormalChat("���ϳ�", "�� ���� ������ ����, ���� ���� ������!"));
        SelectButton1.gameObject.SetActive(true);
        SelectButton2.gameObject.SetActive(true);
    }

    IEnumerator TextSelect1() //("�����ι�", "���")�� �Է�
    {
        //�������� 2 ĳ���� ���� ž�� ����
        istakenStage2 = true;
        Debug.Log(istakenStage2);

        yield return StartCoroutine(NormalChat("���ϳ�", "��ȣ! ������ ���� ����?"));
       
        yield return StartCoroutine(NormalChat("", "���ϳ��� �ų��ٴ� ���� ����� ���Ͽ� �پ� ���ϴ�."));
        CloseClosingText();
    }
    IEnumerator TextSelect2() //("�����ι�", "���")�� �Է�
    {
        
        yield return StartCoroutine(NormalChat("���ϳ�", "����...."));

        yield return StartCoroutine(NormalChat("", "���ϳ��� ���� ���� �ȴ븦 �ٽ� ���ϴ�. �׳��� Ÿ������ ������ �ٽô� ������ ������."));
        CloseClosingText();
    }

    void CloseClosingText()
    {
        SoundManager.Instance.PlayBGM(SoundData.Sound.StageBgm);
        SoundManager.Instance.PlayBGM(SoundData.Sound.MiYeonsi);
        GameManager.Instance.State = GameManager.GameState.Running;
        ClosingTextPanel.SetActive(false); // �г� ��Ȱ��ȭ
        isFirstTime1 = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Stage3");
        //Player.SetActive(true);

        //Fuel.SetActive(true);
        if (istakenStage2)
        {
            rocket.AddPassenger(1);
            Debug.Log(rocket.PassengerCount);
        }

    }
}

