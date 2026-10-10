using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private static bool Jafoi = false;
    private int quantos;
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
            }
            else
            {
                quantos++;
            }
        }
    }
}
