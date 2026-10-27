using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public static bool Jafoi = false;
    private int quantos;
    void Start()
    {
        PlayerStats.TravarPlayer = true;
    }
    void Update()
    {
        if (Jafoi)
        {
            GameObject.Find("Canvas").GetComponent<UIManagerNOPlayer>().Comecar();
        }
        if (Input.anyKeyDown)
        {
            if (quantos < 100)
            {
                GameObject.Find("Canvas").GetComponent<UIManagerNOPlayer>().Comecar();
                Jafoi = true;
                PlayerStats.TravarPlayer = false;
                GameObject.Find("Dialogue").GetComponent<Dialogo>().cmcTutorial = false;
            }
            else
            {
                quantos++;
            }
        }
    }
}
