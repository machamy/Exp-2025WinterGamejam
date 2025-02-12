using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stage1OpeningTextPopup : MonoBehaviour
{
    [Header("���� ��ҵ�")]
    public TMP_Text ChatText;      // ���� ä���� ������ �ؽ�Ʈ
    public TMP_Text CharacterName; // ĳ���� �̸��� ������ �ؽ�Ʈ
    public GameObject OpeningTextPanel;  // ������ ��ũ��Ʈ �г�
    
    public Button NextButton;
   
    [Header("�÷��̾� ������Ʈ")]
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Fuel;
    

    [Header("������ ��ũ��Ʈ ĳ���� ��������Ʈ")]
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
        Time.timeScale = 0f; // ������ �Ͻ������ϰ� ��ȭ�� ������ ��� (���Ͻø� ����)

        // ��ư ������ ����(�ߺ� ��� ���� ���� ���� RemoveAllListeners)
        NextButton.onClick.RemoveAllListeners();
       
        // ��ư Ŭ���� ���� ����
        NextButton.onClick.AddListener(OnNextButtonClicked);
        
        // ���� �ó����� �ڷ�ƾ ����
        StartCoroutine(OpeningTextStage1());

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

    IEnumerator OpeningTextStage1() //("�����ι�", "���")�� �Է�
    {
        yield return StartCoroutine(NormalChat("���ΰ�", "�ܰ迡�� ������ȣ�� ��"));
        yield return StartCoroutine(NormalChat("���ΰ�", "�帴������ ���� �ܰ����ΰ� ����"));
        yield return StartCoroutine(NormalChat("���ΰ�", "�ٷ� ���Ϸ� ���� ���� ���ּ��� �ö�Ž"));
        
        yield return StartCoroutine(NormalChat("", "let's go"));
        CloseOpeningText();
    }

    void CloseOpeningText()
    {
        GameManager.Instance.State = GameManager.GameState.Running;
        OpeningTextPanel.SetActive(false); // �г� ��Ȱ��ȭ
        
        Time.timeScale = 1f;
        
        //Player.SetActive(true);
        
        //Fuel.SetActive(true);
        
    }
}

