using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrambleText : MonoBehaviour
{
    private Text text;
    private string newText;
    private float time;
    
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    public void StartScramble(string newText, float minTime)
    {
        this.newText = newText;
        this.time = time;
        StartCoroutine("ScrambleTo");
    }

    IEnumerator ScrambleTo()
    {
        float startTime = Time.time;
        string temp = text.text;
        while (temp.Length != newText.Length)
        {
            if (temp.Length > newText.Length)
            {
                temp.Substring(0, temp.Length - 1);
            }
            else
            {
                temp = temp + GetRandomCharacter();
            }

            for (int i = 0; i < Random.Range(0, (int) (temp.Length / 3f)); i++)
            {
                int indexToReplace = Random.Range(0, temp.Length);
                temp = temp.Substring(0, indexToReplace - 1) + GetRandomCharacter() + temp.Substring(indexToReplace + 1);
                Debug.Log(temp);
            }
            text.text = temp;
            yield return null;
        }
        text.text = newText;
    }

    private char GetRandomCharacter()
    {
        return System.Convert.ToChar(Random.Range(32, 127));
    }
}
