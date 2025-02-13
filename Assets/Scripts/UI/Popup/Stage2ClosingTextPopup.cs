using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TMPro.Examples;

public class Stage2ClosingTextPopup : MonoBehaviour
{
    [Header("참조 요소들")]
    public TMP_Text ChatText;      // 실제 채팅이 나오는 텍스트
    public TMP_Text CharacterName; // 캐릭터 이름이 나오는 텍스트
    public GameObject ClosingTextPanel;  // 클로징 스크립트 패널
    [SerializeField] private Rocket rocket;
    public Button NextButton;
    public Button SelectButton1;
    public Button SelectButton2;

    [Header("플레이어 오브젝트")]
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Fuel;

    public static bool istakenStage2 = false;

    [Header("클로징 스크립트 캐릭터 스프라이트")]
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
        Time.timeScale = 0f; // 게임을 일시정지하고 대화만 진행

        // 버튼 리스너 세팅(중복 등록 방지 위해 먼저 RemoveAllListeners)
        NextButton.onClick.RemoveAllListeners();
        
        SelectButton1.onClick.RemoveAllListeners();
        SelectButton2.onClick.RemoveAllListeners();

        // 버튼 클릭시 동작 연결
        NextButton.onClick.AddListener(OnNextButtonClicked);
        
        SelectButton1.onClick.AddListener(OnSelectButton1Clicked);
        SelectButton2.onClick.AddListener(OnSelectButton2Clicked);

        
        // 메인 시나리오 코루틴 시작
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
        // 이미 대사가 다 나왔으면, 다음 대사로 넘어가는 신호
        else
        {
            isNextButtonClicked = true;
        }
    }
    public void OnSelectButton1Clicked()
    {
        // 양쪽 버튼 모두 즉시 클릭불가
        SelectButton1.interactable = false;
        SelectButton2.interactable = false;
        // 선택된 버튼을 서서히 투명화
        StartCoroutine(FadeOutBranchButton(SelectButton1, 1f));
        // 다른 버튼도 함께 서서히 투명화
        StartCoroutine(FadeOutBranchButton(SelectButton2, 1f));
        
        // 선택지 1에 대한 분기 시나리오 코루틴 시작
        StartCoroutine(TextSelect1());
    }
    public void OnSelectButton2Clicked()
    {
        // 양쪽 버튼 모두 즉시 클릭불가
        SelectButton1.interactable = false;
        SelectButton2.interactable = false;
        // 선택된 버튼을 서서히 투명화
        StartCoroutine(FadeOutBranchButton(SelectButton1, 1f));
        // 다른 버튼도 함께 서서히 투명화
        StartCoroutine(FadeOutBranchButton(SelectButton2, 1f));

        // 선택지 1에 대한 분기 시나리오 코루틴 시작
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

        // 텍스트 타이핑 효과
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
        // 키를 다시 누를 때까지 무한정 대기
        isNextButtonClicked = false;
        yield return new WaitUntil(() => isNextButtonClicked);
    }
    IEnumerator FadeOutBranchButton(Button branchButton, float fadeDuration)
    {
        // 만약 해당 버튼에 Image와 TMP_Text가 있다면 참조
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
            // Time.timeScale이 0이어도 페이드가 진행되게 하려면 unscaledDeltaTime 사용
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

        // 여기서는 버튼 GameObject를 끄지 않음
        // 실제 비활성화는 CloseClosingText() 시점에 이루어짐
    }
    IEnumerator ClosingText() //("등장인물", "대사")로 입력
    {
        yield return StartCoroutine(NormalChat("별하나", "안녕!! 반가워!!!나는 별하나야"));
        yield return StartCoroutine(NormalChat("별하나", "나를 찾으러 와줬구나!"));
        yield return StartCoroutine(NormalChat("별하나", "아무도 안오는 줄 알고 잠들뻔했잖아"));
        yield return StartCoroutine(NormalChat("별하나", "우리 행성은 곧 영원한 밤이 될거야…"));
        yield return StartCoroutine(NormalChat("별하나", "내 빛이 꺼지기 전에, 제발 나를 도와줘!"));
        SelectButton1.gameObject.SetActive(true);
        SelectButton2.gameObject.SetActive(true);
    }

    IEnumerator TextSelect1() //("등장인물", "대사")로 입력
    {
        //스테이지 2 캐릭터 로켓 탑승 여부
        istakenStage2 = true;
        Debug.Log(istakenStage2);

        yield return StartCoroutine(NormalChat("별하나", "야호! 모험을 떠나 볼까?"));
       
        yield return StartCoroutine(NormalChat("", "별하나는 신난다는 듯이 당신의 로켓에 뛰어 갑니다."));
        CloseClosingText();
    }
    IEnumerator TextSelect2() //("등장인물", "대사")로 입력
    {
        
        yield return StartCoroutine(NormalChat("별하나", "잘자...."));

        yield return StartCoroutine(NormalChat("", "별하나는 슬픈 듯이 안대를 다시 씁니다. 그녀의 타오르는 눈빛을 다시는 못볼거 같군요."));
        CloseClosingText();
    }

    void CloseClosingText()
    {
        SoundManager.Instance.PlayBGM(SoundData.Sound.StageBgm);
        SoundManager.Instance.PlayBGM(SoundData.Sound.MiYeonsi);
        GameManager.Instance.State = GameManager.GameState.Running;
        ClosingTextPanel.SetActive(false); // 패널 비활성화
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

