using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    Text text;
    void Start()
    {
        text = GetComponent<Text>();
        ChangeScoreAndName();
    }
    
    public void ChangeScoreAndName()
    {
        string[] scoreAndName = MainManager.ReturnHighScoreAndName().Split(' ');

        text.text = "Best Score: " + scoreAndName[1] + " Name: " + scoreAndName[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
