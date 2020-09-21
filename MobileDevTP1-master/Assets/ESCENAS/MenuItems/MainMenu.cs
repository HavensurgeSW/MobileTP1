﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour{

    [SerializeField] GameObject creditsPopUp = null;

    public void PlayGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);

    }

    public void QuitGame(){
        Application.Quit();
    }
    public Animator anim;
    
    public void CreditsToggle(){
        creditsPopUp.SetActive(true);
        anim = GetComponent<Animator>();
        anim.Play("HelpPopUp");
    }

}
