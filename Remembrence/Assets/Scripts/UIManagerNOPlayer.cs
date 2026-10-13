using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerNOPlayer : MonoBehaviour
{
    [SerializeField] GameObject MainMenu;
    [SerializeField] GameObject OptionsMenu;
    [SerializeField] GameObject OptionsKeyBinding;
    [SerializeField] private GameObject BackGround;
    [SerializeField] private GameObject tutorial;
    
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
        SceneManager.LoadScene("Floresta1");
    }

    public void OpenOptions()
    {
        MainMenu.SetActive(false);
        OptionsMenu.SetActive(true);
        GameObject.Find("Canvas").GetComponent<SlidersAudio>().SettingSliders();
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

    public void CloseMainMenu()
    {
        MainMenu.SetActive(false);
        BackGround.SetActive(false);
        PlayerStats.TravarPlayer = false;
    }

    public void OpenPauseMenu()
    {
        MainMenu.SetActive(true);
        BackGround.SetActive(true);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("TitleScreen");
        PlayerStats.PlayerHp = PlayerStats.PlayerMaxHp;
        PlayerStats.PlayerMana = PlayerStats.PlayerManaMax;
        PlayerStats.RespawnPosMorte = new Vector3(0, 0, 0);
        Tutorial.Jafoi = false;
    }

    public void Comecar()
    {
        tutorial.SetActive(false);
        BackGround.SetActive(false);
    }
}
