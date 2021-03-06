﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //暂停菜单
    public bool isPaused;
    public GameObject pauseMenu;

    //玩家死亡复活相关
    public Vector3 RevivePosition;
    public GameObject PlayerModel;
    public int PlayerLives = 3;
    public GameObject Player1;
    public int P1Lifes = 3;
    private float P1ReviveTimer = 0;
    public GameObject Player2;
    public int P2Lifes = 3;
    private float P2ReviveTimer = 0;
    public float ReviveTime = 3;

    private AudioSource audio;
    public AudioClip loseAud;
    public AudioClip winAud;
    public GameObject WinMenu;
    public GameObject LoseMenu;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        P1Lifes = PlayerLives;
        P2Lifes = PlayerLives;
        audio = GetComponent<AudioSource>();
    }
    public void PauseToggle()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            isPaused = false;
        }
        //Unpause
        else
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            isPaused = true;
        }
        //pause
    }
    void Update()
    {
        ///暂停
        if (ReInput.players.GetSystemPlayer().GetButtonDown("Cancel"))
        {
            PauseToggle();
        }
        if (P1Lifes == 0 && P2Lifes == 0)
        {
            Lose();
        }
    }
    private void FixedUpdate()
    {
        if (!Player1 && P1Lifes > 0)
        {
            P1ReviveTimer += Time.fixedDeltaTime;
            if (P1ReviveTimer >= ReviveTime)
            {
                var player1 = Instantiate(PlayerModel, RevivePosition, Quaternion.identity, null);
                Player1 = player1;
                player1.GetComponent<PlayerMovement>().playerID = 0;
                P1ReviveTimer -= ReviveTime;
                P1Lifes -= 1;
            }
        }
        if (!Player2 && P2Lifes > 0)
        {
            P2ReviveTimer += Time.fixedDeltaTime;
            if (P2ReviveTimer >= ReviveTime)
            {
                var player2 = Instantiate(PlayerModel, RevivePosition, Quaternion.identity, null);
                Player2 = player2;
                player2.GetComponent<PlayerMovement>().playerID = 1;
                P2ReviveTimer -= ReviveTime;
                P2Lifes -= 1;
            }
        }
    }
    public void BacktoMain() 
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
    public void Win() 
    {
        Time.timeScale = 0;
        Debug.Log("YOU WIN!!!");
        audio.PlayOneShot(winAud);
        WinMenu.SetActive(true);
    }
    public void Lose() 
    {
        Time.timeScale = 0;
        Debug.Log("YOU LOSE!!!");
        audio.PlayOneShot(loseAud);
        LoseMenu.SetActive(true);
    }
}
