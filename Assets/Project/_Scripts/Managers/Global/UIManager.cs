using System;
using System.Collections.Generic;
using UnityEngine;


namespace Project._Scripts.Managers.Global
{
    public enum CanvasType
{
    MainMenu,
    Gamehud,
    PauseMenu,
}
[System.Serializable]
struct CanvasOptions
{
    public CanvasType canvasType;
    public GameObject canvas;
    public bool displayCursor;
    public bool useBackdrop;
    public bool freezeTime;
    public bool lockSwitchCanvas;
}
public class UIManager : SingletonPersistence<UIManager>
{
    [SerializeField] List<CanvasOptions> canvasOptions = new List<CanvasOptions>();
    [SerializeField] GameObject backdropCanvas;
    [SerializeField] CanvasType activeType = CanvasType.Gamehud;

    bool cursorDisplay = false;
    int previousCanvasIndex = -1;
    int currentCanvasIndex = -1;

    private bool IsMainMenu
    {
        get => canvasOptions[currentCanvasIndex].canvasType == CanvasType.MainMenu;
    }

    private CanvasType PreviousCanvas
    {
        get => canvasOptions[previousCanvasIndex].canvasType;
    }
    private CanvasType CurrentCanvas
    {
        get => canvasOptions[currentCanvasIndex].canvasType;
    }

    private void Start()
    {
        DisplayCanvas(activeType);
    }

    private void Update()
    {
        if(!canvasOptions[currentCanvasIndex].lockSwitchCanvas)
            CanvasSwitcher();
        
        // Display PauseMenu if we are not in MainMenu
        if (Input.GetKeyDown(KeyCode.Escape) && !IsMainMenu)
        {
            PauseMenuSwitch();
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

    public void DisplayCanvas(CanvasType canvasType)
    {
        for (int i = 0; i < canvasOptions.Count; i++)
        {
            CanvasOptions option = canvasOptions[i];
            
            if (option.canvasType == canvasType)
            {
                previousCanvasIndex = currentCanvasIndex == -1 ? i : currentCanvasIndex; 
                
                option.canvas.gameObject.SetActive(true);
                SetBackdropCanvas(option.useBackdrop);
                SetCursorDisplay(option.displayCursor);
                Time.timeScale = option.freezeTime ? 0f : 1f;
                
                currentCanvasIndex = i;
            }
            else
            {
                option.canvas.gameObject.SetActive(false);
            }
        }
    }

    public void PauseMenuSwitch()
    {
        // If we are already in PauseMenu, return back to previous saved menu else show pause
        if(CurrentCanvas == CanvasType.PauseMenu)
            DisplayCanvas(PreviousCanvas);
        else
            DisplayCanvas(CanvasType.PauseMenu);
    }
    public void SetBackdropCanvas(bool display) => backdropCanvas.gameObject.SetActive(display);
    public void SetCursorDisplay(bool display) => cursorDisplay = display;
}

}
