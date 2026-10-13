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
        if (Input.anyKey)
        {
            if (quantos < 2)
            {
                GameObject.Find("Canvas").GetComponent<UIManagerNOPlayer>().Comecar();
                Jafoi = true;
                PlayerStats.TravarPlayer = false;
            }
            else
            {
                quantos++;
            }
        }
    }
}
