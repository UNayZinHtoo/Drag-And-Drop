using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class MatchAlphabet : MonoBehaviour
{
    private static readonly string[] Columns = new[]
    {
        "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V",
        "W", "X", "Y", "Z"
    };
    private int num1;
    private int num2;
    private int _correct;
    
    private string question;
    private String correct;
    
    private readonly Random _random = new Random();
    
    public TextMeshProUGUI questionText;
    
    public TextMeshProUGUI ans1Text;
    public TextMeshProUGUI ans2Text;
    
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
        question = "";
        num1=_random.Next(26);
        num2=_random.Next(26);
        while (num2==num1)
        {
            num2=_random.Next(26);
        }
        
        for (int k = 0; k < 5; k++)
        {
            question += Columns[num1]+" "+ Columns[num2]+" ";
        }
        
        _correct=_random.Next(2);

        question+=_correct==0?"<color=#FFFF00>?</color>":Columns[num1]+" <color=#FFFF00>?</color>";
        
        correct=_correct==0?Columns[num1]:Columns[num2];
        
        questionText.text = question;
        
        ans1Text.text =Columns[num1];
        ans2Text.text =Columns[num2];
        
        //Debug.Log("=====qus "+qus+"  =====cor "+correct);
    }
    
    public void OnClickAnswer(TextMeshProUGUI ansText)
    {
        String ans =ansText.text;
        if (ans == correct)
        {
            SetUpQuestion();
        }
    }
}