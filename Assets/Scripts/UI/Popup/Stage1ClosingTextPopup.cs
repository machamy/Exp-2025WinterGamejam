using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using TMPro.Examples;

public class Stage1ClosingTextPopup : MonoBehaviour
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

    public static bool istakenStage1 = false;

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
        yield return StartCoroutine(NormalChat("점슬이", "엥~~ 어째서 이제야 온거야?"));
        yield return StartCoroutine(NormalChat("점슬이", "뭐~ 구조신호를 보낸건 나, 점슬이야♡"));
        yield return StartCoroutine(NormalChat("점슬이", "그딴거도 우주선이냐구요~ㅋ"));
        yield return StartCoroutine(NormalChat("점슬이", "허~접♡"));
        yield return StartCoroutine(NormalChat("점슬이", "이대로 있다간 우리 행성이 무너질수도 있다는데~"));
        yield return StartCoroutine(NormalChat("점슬이", "점슬이는 오빠 우주선으로 가야하나~?"));
        SelectButton1.gameObject.SetActive(true);
        SelectButton2.gameObject.SetActive(true);
    }

    IEnumerator TextSelect1() //("등장인물", "대사")로 입력
    {

        //스테이지 1 캐릭터 로켓 탑승 여부
        istakenStage1 = true;
        Debug.Log(istakenStage1);
        
        yield return StartCoroutine(NormalChat("점슬이", "흥~♡ 내가 원해서 따라가는게 아니라구!"));
       
        yield return StartCoroutine(NormalChat("", "점슬이는 당신에게 엎혀 우주선에 올라탔습니다."));
        CloseClosingText();
    }
    IEnumerator TextSelect2() //("등장인물", "대사")로 입력
    {
        
        yield return StartCoroutine(NormalChat("점슬이", "이 바보! 무쓸모! 멍청이! 쓰레기! 죽어!"));

        yield return StartCoroutine(NormalChat("", "점슬이는 토라진 얼굴로 당신을 지나쳐 뛰쳐나갑니다."));
        CloseClosingText();
    }

    void CloseClosingText()
    {   
        GameManager.Instance.State = GameManager.GameState.Running;
        ClosingTextPanel.SetActive(false); // 패널 비활성화
        isFirstTime1 = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Stage2");
        //Player.SetActive(true);

        //Fuel.SetActive(true);
        if (istakenStage1)
        {
            rocket.AddPassenger(1);
            Debug.Log(rocket.PassengerCount);
        }
    }
}

