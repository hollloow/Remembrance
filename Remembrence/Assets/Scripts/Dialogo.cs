using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Dialogo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI texto;
    public string[] lines;
    public float txtSpeed;
    
    private int index;

    private void Start()
    {
        texto.text = "";
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
        if (index <= lines.Length -1)
        {
            texto.text += "";
            index++;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
