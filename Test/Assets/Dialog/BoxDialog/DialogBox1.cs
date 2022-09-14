using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogBox1 : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    public GameObject click;
    public GameObject dialogSet1;
    public GameObject dialogSet2;


    void Start()
    {
        StartCoroutine(Type());
        dialogSet1.SetActive(true);
        dialogSet2.SetActive(false);
    }

    void Update() 
    {
        if(textDisplay.text == sentences[index]){
            click.SetActive(true);
        }
    }

    IEnumerator Type()
    {       
        char[] s = sentences[index].ToCharArray();

        if (s[s.Length - 1] == '1') //เช็คว่ามีเลข 1 ในท้ายประโยคไหม
        {
            sentences[index] = sentences[index].Replace('1', ' '); //ลบเลข 1 ออกจากประโยคด้วย จะได้ไม่แสดงเลขให้เห็น
            dialogSet1.SetActive(false);
            dialogSet2.SetActive(true);
        }
        else
        {
            dialogSet1.SetActive(true);
            dialogSet2.SetActive(false);
        }

        foreach(char letter in sentences[index].ToCharArray()){
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence()
    {
        click.SetActive(false);

        if(index < sentences.Length - 1){
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else {
            textDisplay.text = "";
            click.SetActive(false);
            dialogSet1.SetActive(false);
            dialogSet2.SetActive(true);
        }
    }
}
