using TMPro;
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
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
    public void ComecarTutorial()
    {
        tutorial.SetActive(true);
        BackGround.SetActive(true);
    }

    public void MudarIdioma()
    {
        if (PlayerStats.Idioma)
        {
            //ingles
            PlayerStats.Idioma = false;
            GameObject.Find("Idioma").GetComponentInChildren<TextMeshProUGUI>().text = "English";
            GameObject.Find("Button").GetComponentInChildren<TextMeshProUGUI>().text = "Return";
            GameObject.Find("Text Sound Effects").GetComponent<TextMeshProUGUI>().text = "Sound Effects";
            GameObject.Find("Text Music").GetComponent<TextMeshProUGUI>().text = "Music";
            GameObject.Find("Text Master").GetComponent<TextMeshProUGUI>().text = "Master";
        }
        else
        {
            //portugues
            PlayerStats.Idioma = true;
            GameObject.Find("Idioma").GetComponentInChildren<TextMeshProUGUI>().text = "Portugues";
            GameObject.Find("Button").GetComponentInChildren<TextMeshProUGUI>().text = "Voltar";
            GameObject.Find("Text Sound Effects").GetComponent<TextMeshProUGUI>().text = "Effeitos Sonoros";
            GameObject.Find("Text Music").GetComponent<TextMeshProUGUI>().text = "Musica";
            GameObject.Find("Text Master").GetComponent<TextMeshProUGUI>().text = "Mestre";
        }
    }
}
