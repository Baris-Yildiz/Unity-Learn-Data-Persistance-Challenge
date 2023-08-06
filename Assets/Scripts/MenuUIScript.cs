using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIScript : MonoBehaviour
{
    InputField nameField;
    public string playerName;

    public static MenuUIScript Instance;

    private void Start()
    {
        Instance = this;
        nameField = GetComponentInChildren<InputField>();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void SaveName(string name)
    {
        playerName = name;
    }
}
