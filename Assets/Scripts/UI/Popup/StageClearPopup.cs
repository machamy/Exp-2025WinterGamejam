
    using System;
    using DefaultNamespace.UI.Popup;
    using UnityEngine;

    public class StageClearPopup : MonoBehaviour, IClearLisenter
    {
        private void OnEnable()
        {
            
        }
        
        private void OnDisable()
        {
            
        }
        
        public void OnClear()
        {
            gameObject.SetActive(true);
        }
        
        public void GoToMain()
        {
           GameManager.Instance.GoToMain();   
        }
        
        public void GoToNextStage()
        {
            GameManager.Instance.GoToStage(GameManager.Instance.Stage + 1);
        }
    }
