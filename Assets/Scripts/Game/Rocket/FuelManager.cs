// using System.Collections.Generic;
// using System.Collections;
// using UnityEngine.SceneManagement;
// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;
//
// public class FuelManager : MonoBehaviour
// {
//     //�÷��̾� hp�� �̱������� ����, �� �̵��� �ʱ�ȭ
//     public static FuelManager Instance { get; private set; }
//
//     [Header("Max Fuel ����")]
//     [SerializeField] private float maxFuel = 25f;
//     [Header("�÷��̾� ������Ʈ")]
//     [SerializeField] private GameObject Player;
//     
//     [Header("��������")]
//     [SerializeField] private int Stage;
//     private float currentFuel;
//     public float GetttingCurrentFuel() => currentFuel;
//     public float GettingMaxFuel() => maxFuel;
//     private void Awake()
//     {
//         // �̱��� �⺻ ����
//         if (Instance != null && Instance != this)
//         {
//             Destroy(gameObject);
//             return;
//         }
//         Instance = this;
//
//         // �� ��ȯ �� �ı��ǵ��� ���� 
//         // ���� �� HP �ʱ�ȭ
//         currentFuel = maxFuel;
//     }
//
//     public void RestartFuel()
//     {
//         currentFuel = maxFuel;
//     }
//
//
//     public void TakeDamage(float damage)
//     {
//         currentFuel -= damage;
//         Debug.Log($"�÷��̾ {damage} ���Ḧ �����. ���� ����: {currentFuel}");
//
//
//         if (currentFuel <= 0 && Stage == 1)
//         {
//             currentFuel = 0;
//             FuelEmpty();
//         }
//         
//     }
//     public float GetCurrentFuel()
//     {
//         return currentFuel;
//     }
//
//     public float GetMaxFuel()
//     {
//         return maxFuel;
//     }
//
//     private void FuelEmpty()
//     {
//         Debug.Log("���� ����.");
//         
//         Player.SetActive(false);
//         
//         Time.timeScale = 0f;
//         
//     }
//     
// }
