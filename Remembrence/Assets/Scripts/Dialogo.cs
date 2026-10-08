using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Dialogo : MonoBehaviour
{
    [SerializeField] GameObject dialogue;
    [SerializeField] TextMeshProUGUI texto;
    public string[] lines;
    public float txtSpeed;
    public bool travar;
    
    private int index;

    private void OnEnable()
    {
        texto.text = "";
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
        if (travar)
        {
            PlayerStats.TravarPlayer = true;
        } 
        StartDialogue();
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (texto.text == lines[index])
            {
                OnNextLine();
            }
            else
            {
                StopAllCoroutines();
                texto.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            texto.text += c;
            yield return new WaitForSeconds(txtSpeed);
        }   
    }
    void OnNextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            texto.text = "";
            StartCoroutine(TypeLine());
        }
        else
        {
            if (GameObject.Find("PlaceHolder_HitboxChaoDestrutivel"))
            {
                Destroy(GameObject.Find("PlaceHolder_HitboxChaoDestrutivel"));
            }
            if (travar)
            {
                PlayerStats.TravarPlayer = false;
            } 
           dialogue.gameObject.SetActive(false);
        }
    }
}
