using UnityEngine;
using System.Collections;

public class DeathMenu : MonoBehaviour {

    void Start()    {
        
    }

    void OnGUI() {
    #if UNITY_STANDALONE || UNITY_WEBPLAYER
		    GUI.skin.button.fontSize = Screen.width/90;
		    int buttonWidth = Screen.width/9;
		    int buttonHeight = buttonWidth/4;
		    int offset = Mathf.FloorToInt(buttonHeight*1.6f);
    #elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE

            Texture2D txt = (Texture2D)Resources.Load("keepPlayingButton");
            GUI.skin.button.fontSize = Screen.width / 20;
            float buttonHeight = (Screen.height * 1.0f / 8.0f);
            float buttonWidth = txt.width* ((Screen.height * 1.0f / 8.0f)/txt.width)*2.0f;
            float offset = buttonHeight/2;
            /*GUI.skin.button.fontSize = Screen.width / 20;
            float buttonHeight = Screen.height * 1 / 10;
            float buttonWidth = buttonHeight * 5;
            float offset = buttonHeight;*/


    #endif
    
            if ( GUI.Button( new Rect(
                Screen.width / 2 - ( buttonWidth / 2 ) ,
                Screen.height * 0.9f / 2 - ( buttonHeight / 2 ) ,
                buttonWidth ,
                buttonHeight
                ) , ( Texture2D ) Resources.Load( "keepPlayingButton" ) ) ) {
                
                GetComponentInParent<PauseScript>().quitDeath();
            }

            if (GUI.Button(new Rect(
                Screen.width / 2 - (buttonWidth / 2),
                Screen.height * 0.9f / 2 - ( buttonHeight / 2 ) + offset*2 ,
                buttonWidth,
                buttonHeight
                ) , ( Texture2D ) Resources.Load( "exitButton" )) ) {

                GetComponentInParent<PauseScript>().quitPause();
                Application.LoadLevel("Menu");
            }


        }
}