// using System.Collections.Generic;
// using System.Collections;
// using UnityEngine.SceneManagement;
// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;
//
// public class FuelManager : MonoBehaviour
// {
//     //플레이어 hp를 싱글톤으로 구현, 씬 이동시 초기화
//     public static FuelManager Instance { get; private set; }
//
//     [Header("Max Fuel 설정")]
//     [SerializeField] private float maxFuel = 25f;
//     [Header("플레이어 오브젝트")]
//     [SerializeField] private GameObject Player;
//     
//     [Header("스테이지")]
//     [SerializeField] private int Stage;
//     private float currentFuel;
//     public float GetttingCurrentFuel() => currentFuel;
//     public float GettingMaxFuel() => maxFuel;
//     private void Awake()
//     {
//         // 싱글톤 기본 구현
//         if (Instance != null && Instance != this)
//         {
//             Destroy(gameObject);
//             return;
//         }
//         Instance = this;
//
//         // 씬 전환 시 파괴되도록 설정 
//         // 시작 시 HP 초기화
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
//         Debug.Log($"플레이어가 {damage} 연료를 사용함. 남은 연료: {currentFuel}");
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
//         Debug.Log("연료 부족.");
//         
//         Player.SetActive(false);
//         
//         Time.timeScale = 0f;
//         
//     }
//     
// }
