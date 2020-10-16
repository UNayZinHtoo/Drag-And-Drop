using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using TMPro;
using UnityEngine.SceneManagement;

namespace Example1
{
    public class DragAndDropManager : MonoBehaviour
    {
        public GameObject qus1Image, qus2Image, qus3Image;
        public GameObject qus1Blank, qus2Blank, qus3Blank;
        public TMP_Text qus1Text, qus2Text, qus3Text;
        public AudioSource source;
        public AudioClip[] correct;
        public AudioClip incorrect;

        public Sprite[] qusSptite;

        public String[] qusText;

        //public Question[] question;

        Vector2 qus1InitalPos, qus2InitalPos, qus3InitalPos;

        private Vector2[] intialPos = new Vector2[3];

        private bool qus1Correct = false;
        private bool qus2Correct = false;
        private bool qus3Correct = false;

        private System.Random _random = new System.Random();

        // Start is called before the first frame update
        void Start()
        {
            /*qus1InitalPos = qus1Image.transform.position;
            qus2InitalPos = qus2Image.transform.position;
            qus3InitalPos = qus3Image.transform.position;*/

            intialPos[0] = qus1Image.transform.position;
            intialPos[1] = qus2Image.transform.position;
            intialPos[2] = qus3Image.transform.position;

            prepareQuestion();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                SceneManager.LoadScene("MainScene");
        }

        public void prepareQuestion()
        {
            Refresh();

            var sequence = Enumerable.Range(0, qusSptite.Length - 1).OrderBy(n => n * n * (new System.Random()).Next());

            var enumResult = sequence.Distinct().Take(3);

            int[] result = enumResult.ToArray();

            qus1Image.GetComponent<Image>().sprite = qusSptite[result[0]];
            qus2Image.GetComponent<Image>().sprite = qusSptite[result[1]];
            qus3Image.GetComponent<Image>().sprite = qusSptite[result[2]];

            qus1Text.GetComponent<TMP_Text>().text = qusText[result[0]];
            qus2Text.GetComponent<TMP_Text>().text = qusText[result[1]];
            qus3Text.GetComponent<TMP_Text>().text = qusText[result[2]];

        }

        //for shuffle number from array
        void Shuffle(Vector2[] array)
        {
            int p = array.Length;
            for (int n = p - 1; n > 0; n--)
            {
                int r = _random.Next(1, n);
                Vector2 t = array[r];
                array[r] = array[n];
                array[n] = t;
            }
        }

        public void DragQus1()
        {
            qus1Image.transform.position = Input.mousePosition;
        }

        public void DragQus2()
        {
            qus2Image.transform.position = Input.mousePosition;
        }

        public void DragQus3()
        {
            qus3Image.transform.position = Input.mousePosition;
        }

        public void DropQus1()
        {
            float Distance = Vector3.Distance(qus1Image.transform.position, qus1Blank.transform.position);
            if (Distance < 50 && qus1Correct == false)
            {
                qus1Image.transform.position = qus1Blank.transform.position;
                source.clip = correct[Random.Range(0, correct.Length)];
                qus1Correct = true;
                source.Play();
                if (qus1Correct && qus2Correct && qus3Correct)
                {
                    prepareQuestion();
                }
            }
            else
            {
                //qus1Image.transform.position = qus1InitalPos;
                qus1Image.transform.position = intialPos[0];
                if (!qus1Correct)
                {
                    source.clip = incorrect;
                    source.Play();
                }
            }
        }

        public void DropQus2()
        {
            float Distance = Vector3.Distance(qus2Image.transform.position, qus2Blank.transform.position);
            if (Distance < 50 && qus2Correct == false)
            {
                qus2Image.transform.position = qus2Blank.transform.position;
                source.clip = correct[Random.Range(0, correct.Length)];
                qus2Correct = true;
                source.Play();
                if (qus1Correct && qus2Correct && qus3Correct)
                {
                    prepareQuestion();
                }
            }
            else
            {
                //qus2Image.transform.position = qus2InitalPos;
                qus2Image.transform.position = intialPos[1];
                if (!qus2Correct)
                {
                    source.clip = incorrect;
                    source.Play();
                }
            }
        }

        public void DropQus3()
        {
            float Distance = Vector3.Distance(qus3Image.transform.position, qus3Blank.transform.position);
            if (Distance < 50 && qus3Correct == false)
            {
                qus3Image.transform.position = qus3Blank.transform.position;
                source.clip = correct[Random.Range(0, correct.Length)];
                qus3Correct = true;
                source.Play();
                if (qus1Correct && qus2Correct && qus3Correct)
                {
                    prepareQuestion();
                }
            }
            else
            {
                //up.transform.position = qus3InitalPos;
                qus3Image.transform.position = intialPos[2];
                if (!qus3Correct)
                {
                    source.clip = incorrect;
                    source.Play();
                }
            }
        }

        public void Refresh()
        {
            /*qus1Image.transform.position = qus3InitalPos;
            qus2Image.transform.position = qus1InitalPos;
            qus3Image.transform.position = qus2InitalPos;*/

            Shuffle(intialPos);
            qus1Image.transform.position = intialPos[0];
            qus2Image.transform.position = intialPos[1];
            qus3Image.transform.position = intialPos[2];

            qus1Correct = false;
            qus2Correct = false;
            qus3Correct = false;
        }

    }
}