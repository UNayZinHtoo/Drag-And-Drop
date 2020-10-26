using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class MatchNumber : MonoBehaviour
{
    
    private int num;

    private int m1;
    
    private int m2;

    public TextMeshProUGUI questionText;
    public TextMeshProUGUI numText;
    public TextMeshProUGUI m1Text;
    
    public TextMeshProUGUI ans1Text;
    public TextMeshProUGUI ans2Text;
    public TextMeshProUGUI ans3Text;
    
    
    
    private string question;

    private readonly Random _random = new Random();
    
    // Start is called before the first frame update
    void Start()
    {
        SetUpQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpQuestion()
    {
        num=_random.Next(2,10);
        m1=_random.Next(1,num);
        m2 = num - m1;
        
        int[] result = {m2,m2+1,m2 > 1 ? m2-1 : m2+2 };
        //Shuffle(result);

        question = "which number is match with "+ m1 +" to get " +num+" ?";
        
        //Debug.Log(question+"  Correct Answer" + m2);
        
        questionText.text = question;
        numText.text = num.ToString();
        m1Text.text = m1.ToString();
        
        ans1Text.text =result[0].ToString();
        ans2Text.text =result[1].ToString();
        ans3Text.text =result[2].ToString();
        
    }
    public void Shuffle(int[] array) 
    {
        for (int i = 0; i < array.Length; i++) {
            int r = _random.Next(0, array.Length);
            int temp = array[r];
            array[r] = array[i];
            array[i] = temp;
        }
    }
    public void OnClickAnswer(TextMeshProUGUI ansText)
    {
        int ans =Convert.ToInt32(ansText.text);
        if (ans == m1)
        {
            SetUpQuestion();
        }
    }

}
