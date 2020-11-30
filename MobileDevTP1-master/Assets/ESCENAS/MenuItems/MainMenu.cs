using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour{
   
   bool coop  = true;

   public void startGame(){
       if (coop){
           PlayGame();
       }else
         playGameSolo();
   }

   public void setCoop(){
       coop = true;
   }
    public void setSP(){
       coop = false;
   }

    void PlayGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    void playGameSolo(){
        SceneManager.LoadScene(3);
    }

    public void QuitGame(){
        Application.Quit();
    }

    public float speed = 70f;
    void Update() {
        transform.Rotate(0,speed*Time.deltaTime,0);
    }
    
}
