using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerNOPlayer : MonoBehaviour
{
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject OptionsMenu;
    [SerializeField] GameObject OptionsKeyBinding;
    
    public void CloseGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
    public void OpenGame()
    {
        SceneManager.LoadScene("Screen1");
    }

    public void OpenOptions()
    {
        MainMenu.SetActive(false);
        OptionsMenu.SetActive(true);
    }
    public void CloseOptions()
    {
        MainMenu.SetActive(true);
        OptionsMenu.SetActive(false);
    }

    public void OpenOptionsKeyBinding()
    {
        MainMenu.SetActive(false);
        OptionsMenu.SetActive(false);
        OptionsKeyBinding.SetActive(true);
    }
    public void CloseOptionsKeyBinding()
    {
        OptionsMenu.SetActive(true);
        OptionsKeyBinding.SetActive(false);
    }
}
