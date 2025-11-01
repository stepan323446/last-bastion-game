using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


namespace Project._Scripts.Managers.Global
{
    public enum CanvasType
    {
        _None,
        Gamehud,
        PauseMenu
    }
    [System.Serializable]
    public struct CanvasOptions
    {
        public CanvasType canvasType;
        public GameObject canvas;
        public bool displayCursor;
        public bool disableControl;
        public bool useBackdrop;
        public bool freezeTime;
        public bool lockSwitchCanvas;
    }
    public class UIManager : SingletonPersistence<UIManager>
    {
        [SerializeField] List<CanvasOptions> _canvasOptions = new List<CanvasOptions>();
        [SerializeField] GameObject _backdropCanvas; 
        [SerializeField] CanvasType _defaultType = CanvasType.Gamehud;

        bool cursorDisplay = false;

        private Dictionary<CanvasType, CanvasOptions> _canvasDict = new Dictionary<CanvasType, CanvasOptions>();
        private CanvasType PreviousCanvasType { get; set;  }
        private CanvasType CurrentCanvasType { get; set; }
        public CanvasOptions CurrentCanvasOptions { get => _canvasDict [CurrentCanvasType]; }
        
        private void Start()
        {
            foreach (CanvasOptions opt in _canvasOptions) 
                _canvasDict.Add(opt.canvasType, opt);
            
            DisplayCanvas(_defaultType, true);
        }

        private void Update()
        {
            // Pause menu
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseMenuSwitcher();
            }
        }

        private void FixedUpdate()
        {
            Cursor.visible = cursorDisplay;
        }
        
        private void CanvasSwitcher()
        {
            // KeyCodes for switching between canvases (Inventory, ...)
        }

        public void DisplayCanvas(CanvasType canvasType, bool ignoreLock = false)
        {
            if (!ignoreLock && _canvasDict[CurrentCanvasType].lockSwitchCanvas)
                return;
                
            PreviousCanvasType = _canvasDict[CurrentCanvasType].canvasType;
            for (int i = 0; i < _canvasOptions.Count; i++)
            {
                CanvasOptions option = _canvasOptions[i];
                
                if (option.canvasType == canvasType)
                {
                    option.canvas.gameObject.SetActive(true);
                    SetBackdropCanvas(option.useBackdrop);
                    SetCursorDisplay(option.displayCursor);
                    Time.timeScale = option.freezeTime ? 0f : 1f;
                    
                    CurrentCanvasType = option.canvasType;
                }
                else
                {
                    option.canvas.gameObject.SetActive(false);
                }
            }
        }

        public void PauseMenuSwitcher()
        {
            if(CurrentCanvasType == CanvasType.PauseMenu)
                DisplayCanvas(PreviousCanvasType, true);
            else
                DisplayCanvas(CanvasType.PauseMenu);
        }
        public void EnableUIManager()
        {
            DisplayCanvas(_defaultType, true);
        }
        public void DisableUIManager()
        {
            DisplayCanvas(CanvasType._None, true);
        }
        public void SetBackdropCanvas(bool display) => _backdropCanvas.gameObject.SetActive(display);
        public void SetCursorDisplay(bool display) => cursorDisplay = display;
    }
}
