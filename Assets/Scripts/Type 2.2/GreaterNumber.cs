using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class GreaterNumber : MonoBehaviour
{
    private int num1;
    private int num2;
    private int correct;
    private string question;
    
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
        num1=_random.Next(1,10);
        num2=_random.Next(1,10);
        while (num1==num2)
        {
            num2=_random.Next(1,10);
        }

        question = "which number is greater, " + num1 + " or " + num2 + " ?";
        
        questionText.text = question;
        
        ans1Text.text =num1.ToString();
        ans2Text.text =num2.ToString();
        
        correct = num2;
        if (num1 > num2)
            correct = num1;
        
        //Debug.Log(question+"  Correct Answer" + correct);
    }
    
    public void OnClickAnswer(TextMeshProUGUI ansText)
    {
        int ans =Convert.ToInt32(ansText.text);
        if (ans == correct)
        {
            SetUpQuestion();
        }
    }
}
