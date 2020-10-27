using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class CalculateNumbers : MonoBehaviour
{
    private static readonly string[] Columns = new[]
    {
        "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V",
        "W", "X", "Y", "Z"
    };

    enum Operators 
    {
        Sum, 
        Sub, 
        Multi,
        Div
    };
    
    private int num1;

    private int num2;
    
    private int sum;

    public TextMeshProUGUI questionText;
    public TextMeshProUGUI questionText1;
    
    public TextMeshProUGUI ans1Text;
    public TextMeshProUGUI ans2Text;
    public TextMeshProUGUI ans3Text;
    
    [SerializeField]
    private Operators operators;
    
    
    
    private string question;
    private string question1;
    private String op;
    
    private readonly Random _random = new Random();
    
    // Start is called before the first frame update
    void Start()
    {
        SetUpQuestion();
        RamdomCharacter();
        RamdomGreater();
        MatchNumber();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpQuestion()
    {
        num1=_random.Next(1, 10);
        num2=_random.Next(1, 10);
        //sum = num1 + num2;

        switch (operators)
        {
            case Operators.Sum:
                op = "+";
                sum=num1 + num2;
                break;
            case Operators.Sub:
                op = "-";
                if(num1>num2)
                    sum=num1 - num2;
                else
                {
                    sum=num2 - num1;
                    num1 = num2;
                    num2 = -(sum - num1);
                }
                break;
            case Operators.Multi:
                op = "*";
                sum=num1 * num2;
                break;
            case Operators.Div:
                op = "/";
                sum=num1 / num2;
                break;
            
        }
        
        question ="Calculate <color=#ff0000ff>"+num1 + op + num2+"</color>";
        question1 = num1 + "  "+op+"  " + num2+"  =  ?";

        questionText.text = question;
        questionText1.text = question1;
        
        int[] result = {sum,sum+1,sum > 0 ? sum-1 : sum+2 };
        Shuffle(result);

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
        if (ans == sum)
        {
            SetUpQuestion();
            RamdomCharacter();
            RamdomGreater();
            MatchNumber();
        }
    }

    public void RamdomCharacter()
    {
        int i, j,_cor;
        String cor;
        String qus="";
        i=_random.Next(26);
        j=_random.Next(26);
        while (j==i)
        {
            j=_random.Next(26);
        }
        
        for (int k = 0; k < 5; k++)
        {
            qus += Columns[i] + Columns[j];
        }
        _cor=_random.Next(2);

        qus+=_cor==0?"":Columns[i];
        
        cor=_cor==0?Columns[i]:Columns[j];
        Debug.Log("=====qus "+qus+"  =====cor "+cor);
    }
    public void RamdomGreater()
    {
        int i, j,_cor;
        String qus="";
        i=_random.Next(1,10);
        j=_random.Next(1,10);
        while (j==i)
        {
            j=_random.Next(1,10);
        }

        qus = "which number is greater, " + i + " or " + j + " ?";
        _cor = j;
        if (i > j)
            _cor = i;
        
        Debug.Log(qus+"  Correct Answer" + _cor);
    }
    public void MatchNumber()
    {
        int num;
        int m1;
        int m2;
        String qus;
        num=_random.Next(2,10);
        m1=_random.Next(1,num);
        m2 = num - m1;
        
        int[] result = {m2,m2+1,m2 > 1 ? m2-1 : m2+2 };
        Shuffle(result);
        
        for (int k = 0; k < 3; k++)
        {
            Debug.Log("Result=== " + result[k]);
        }
        
        qus = "which number is match with "+ m1 +" to get " +num+" ?";
        
        Debug.Log(qus+"  Correct Answer" + m2);
    }
    
}
