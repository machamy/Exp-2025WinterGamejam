using System.Collections.Generic;
using System.Collections;
using DefaultNamespace.UI.Popup;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class Stage1OpeningTextPopup : MonoBehaviour
{
    [SerializeField]UnityEvent OnComplete = new UnityEvent();
    [Header("참조 요소들")]
    public TMP_Text ChatText;      // 실제 채팅이 나오는 텍스트
    public TMP_Text CharacterName; // 캐릭터 이름이 나오는 텍스트
    public GameObject OpeningTextPanel;  // 오프닝 스크립트 패널
    
    public Button NextButton;
   
    [Header("플레이어 오브젝트")]
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Fuel;
    

    [Header("오프닝 스크립트 캐릭터 스프라이트")]
    public GameObject CharacterPose1; 
    public GameObject CharacterPose2;
   

    private bool isFullTextDisplayed = false;
    private bool isNextButtonClicked = false;
    public string writerText = "";
    
    void Start()
    {
        Open();
           
    }

    void Update()
    {

    }

    void Open()
    {
        GameManager.Instance.State = GameManager.GameState.Dialog;
        OpeningTextPanel.SetActive(true);
        SoundManager.Instance.PlayBGM(SoundData.Sound.MiYeonsi);
        Time.timeScale = 0f; // 게임을 일시정지하고 대화만 진행할 경우 (원하시면 조절)

        // 버튼 리스너 세팅(중복 등록 방지 위해 먼저 RemoveAllListeners)
        NextButton.onClick.RemoveAllListeners();
       
        // 버튼 클릭시 동작 연결
        NextButton.onClick.AddListener(OnNextButtonClicked);
        
        // 메인 시나리오 코루틴 시작
        StartCoroutine(OpeningTextStage1());

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
    
    public void OnSkipButtonClicked()
    {
        CloseOpeningText();
    }
    public void ExitStage1()
    {
        
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

    IEnumerator OpeningTextStage1() //("등장인물", "대사")로 입력
    {
        yield return StartCoroutine(NormalChat("", "외계에서 구조신호가 오고있다."));
        yield return StartCoroutine(NormalChat("주인공", "흐릿하지만 예쁜 외계인인거 같은데..."));
        yield return StartCoroutine(NormalChat("주인공", "나는 외계인이 좋아! 바로 만나러 가자!"));

        if (GameManager.DoTutorial == true)
        {
            yield return StartCoroutine(NormalChat("", "로켓은 별도의 조작이 없으면 계속 앞으로 나갑니다."));
            yield return StartCoroutine(NormalChat("", "로켓을 조작하기 위해서는 연료가 필요합니다."));
            yield return StartCoroutine(NormalChat("", "화면을 슬라이드하면 로켓의 진행 방향이 바뀝니다."));
            yield return StartCoroutine(NormalChat("", "화면을 꾹 누르면 부스터가 발동해 빨라집다."));
            yield return StartCoroutine(NormalChat("", "부스터가 발동된 상태에서는 일부 장애물이 파괴됩니다."));
            GameManager.DoTutorial = false;
        }

        yield return StartCoroutine(NormalChat("", "let's go"));
        CloseOpeningText();
    }

    void CloseOpeningText()
    {
        SoundManager.Instance.PlayBGM(SoundData.Sound.StageBgm);
        GameManager.Instance.State = GameManager.GameState.Running;
        OpeningTextPanel.SetActive(false); // 패널 비활성화
        
        Time.timeScale = 1f;
        
        OnComplete.Invoke();
        //Player.SetActive(true);
        
        //Fuel.SetActive(true);
        
    }
}

