using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Buttons : MonoBehaviour
{
    public GameController game;
    // Start is called before the first frame update
    void Start()
    {
        
    }

     public void startButton()
    {
        game.st = 1;
        game.sp = 0;
        game.sc = 0;
        game.sd = 0;
    }
    public void StopButton()
    {
        game.st = 0;
        game.sp = 1;
        game.sc = 0;
        game.sd = 0;
    }
    public void ClearButton()
    {
        game.st = 0;
        game.sp = 0;
        game.sc = 1;
        game.sd = 0;
    }
    public void SetDepthButton()
    {
        TextMeshProUGUI txt = game.depthUI;
        game.depth = int.Parse(txt.text.ToString());
        game.st = 0;
        game.sp = 0;
        game.sc = 0;
        game.sd = 1;
    }

}
