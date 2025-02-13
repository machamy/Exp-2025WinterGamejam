
    using System;
    using DefaultNamespace.UI.Popup;
    using UnityEngine;

    public class StageFailPopup : MonoBehaviour, IFailListener
    {
        private void OnEnable()
        {
            
        }
        
        private void OnDisable()
        {
            
        }
        
        public void OnFail()
        {
            gameObject.SetActive(true);
        }
        
        public void GoToMain()
        {
           GameManager.Instance.GoToMain();   
        }
        
        public void ReplayStage()
        {
            GameManager.Instance.GoToStage(GameManager.Instance.Stage);
        }
        
        public void GoToStage(int stage)
        {
            GameManager.Instance.GoToStage(stage);
        }
    }
