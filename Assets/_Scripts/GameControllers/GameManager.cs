using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

    public static Action<bool> OnGamePause;
    public static Action OnGameLoad;
    public enum GameState{
        Cutscene,
        Paused,
        Running
    }

    public static GameState currentGameState;

    public GameState previousState;
    public static GameManager instance;

    //Controls the game state and there is always an instance of the game manager for other scripts to call
    //The player controller is what will send the input to pause the game, but the game manager does the pausing

    public bool gameIsPaused;
    private void Awake()
    {
        //pause = PlayerInput.
        currentGameState = GameState.Running;
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        OnGameLoad?.Invoke();


    }


    public void PauseGame()
    {
        gameIsPaused = true;
        Time.timeScale = 0;
        OnGamePause?.Invoke(true);
    }

    public void UnpauseGame()
    {
        gameIsPaused = false;
        Time.timeScale = 1;
        OnGamePause?.Invoke(false);
        PlayerController.playerInput.SwitchCurrentActionMap("Player");

        if(currentGameState == GameState.Paused)
        {
            currentGameState = previousState;
        }
    }

   public void ChangeGameState(GameState newState)
    {
        previousState = currentGameState;
        switch(newState)
        {
            case GameState.Paused:
            //Pause the game when the state is changed
                PauseGame();
            //Send a signal to any listeners that care about the game being paused
                
            break;
            case GameState.Cutscene:
            break;
            case GameState.Running:
            //If the game was paused, it is no longer paused
                UnpauseGame();
                
            break;
        }

        currentGameState = newState;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
