using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.IO;

public class HighScores : MonoBehaviour {
   
    public int i = 0;
    private int x = 0;
    public string[,] highscoreArray = new string[5,2];
    public InputField inputField;
    public GameObject inputFieldObject;
    public CanvasGroup mainCanvas;
    public CanvasGroup canvas2;

    // Use this for initialization
    void Start () {
        if (!File.Exists("highScoreNames.txt"))
        {
            FileStream fs = new FileStream("highScoreNames.txt", FileMode.Create);
            fs.Close();
            StreamWriter sw = new StreamWriter("highScoreNames.txt");
            using (sw)
            {
                for (int a = 0; a <= 4; a++)
                {
                    sw.WriteLine("A");
                }
                sw.Close();
            }
        }
        if (!File.Exists("highScoreNumbers.txt"))
        {
            FileStream fs = new FileStream("highScoreNumbers.txt", FileMode.Create);
            fs.Close();
            StreamWriter sw = new StreamWriter("highScoreNumbers.txt");
            using (sw)
            {
                for (int a = 0; a <= 4; a++)
                {
                    sw.WriteLine("0");
                }
                sw.Close();
            }
        }else
        {
            getHighScoreNames();
            getHighScoreNumbers();
        }
        mainCanvas = GameObject.Find("Canvas").GetComponent<CanvasGroup>();
        canvas2= GameObject.Find("Canvas2").GetComponent<CanvasGroup>();
        inputFieldObject = GameObject.Find("InputField");
        checkLastScore();

    }
	
    void getHighScoreNames() //function to retrive names associated with highscores from text file
    {
        //var nameSourceFile = new FileInfo("highScoreNames.txt"); //find text file info
        StreamReader reader = new StreamReader("highScoreNames.txt");
        
            using (reader)
            {
                i = 0;
                while (i <= 4)                                      //read each line of the text file in sequence and 
                {                                                   //store it in the first slot of the 3d array.
                    highscoreArray[i, 0] = reader.ReadLine();
                    Debug.Log(highscoreArray[i, 0]);
                    i++;
                }
            reader.Close();

        }

    }

    void getHighScoreNumbers() //functio to get the corresponding highscores from a text file
    {
        StreamReader reader = new StreamReader("highScoreNumbers.txt");//get the text file info 

        {
            using (reader)
            {
                i = 0;
                while (i <= 4)                                          //sequentially read the textfile line by line
                {                                                       //storing teh scores in the corresponding slot to the name.
                    highscoreArray[i, 1] = reader.ReadLine();
                    i++;
                }
                reader.Close();
            }
        }
       
    }

    void checkLastScore()                                       //check whether the score just earned is larger than an existing one
    {
        x = 0;
        while(x <= 4)
        {
            if (ScoreKeeper.Score >= Int32.Parse(highscoreArray[x, 1]))
            {
                canvas2.alpha = 1f;
                mainCanvas.alpha = 0f;
                break;
            }
            x++;
        }
        //displayHighScores();
    }

    void displayHighScores() //display the highscores on the screen 
    {
        mainCanvas.alpha = 1f;
        canvas2.alpha = 0f;

        i = 1;
        x = 0;

        while (i <= 5)
        {
            
            string nextField = "Name" + i.ToString();
            Debug.Log(nextField);
            Text currentText = GameObject.Find(nextField).GetComponent<Text>();
            currentText.text = highscoreArray[x, 0];
            x++;
            i++;
        }
        i = 1;
        x = 0;
        while (i <= 5)
        {
            string nextField = "Score" +i.ToString();
            Debug.Log(nextField);
            Text currentText = GameObject.Find(nextField).GetComponent<Text>();
            currentText.text = highscoreArray[x, 1];
            x++;
            i++;
        }

    }

    public void UpdateHighscore()  //update the highscores
    {
        updateArray();
        displayHighScores();
        saveHighScoreNames();
        saveHighScoreNumbers();
    }

    void saveHighScoreNames()
    {
        StreamWriter writer = new StreamWriter("highScoreNames.txt");
        using (writer)
        {
            i = 0;
            while (i <= 4)                                      //read each line of the text file in sequence and 
            {                                                   //store it in the first slot of the 3d array.
                writer.WriteLine(highscoreArray[i, 0]);
                i++;
            }
            writer.Close();
        }
    }

    void saveHighScoreNumbers()
    {
        StreamWriter writer = new StreamWriter("highScoreNumbers.txt");
        using (writer)
        {
            i = 0;
            while (i <= 4)                                      //read each line of the text file in sequence and 
            {                                                   //store it in the first slot of the 3d array.
                writer.WriteLine(highscoreArray[i, 1]);
                i++;
            }
            

        }
        writer.Close();
    }

    void updateArray()
    {
        Text newHighscoreName = GameObject.Find("InputFieldText").GetComponent<Text>();
        int y = 4;
        for (int x = 0; x <= 4; x++)
        {
            if (ScoreKeeper.Score > Int32.Parse(highscoreArray[x, 1]))
            {

                if (x == 0)
                {
                    while (y > 0)
                    {
                        highscoreArray[y, 0] = highscoreArray[y - 1, 0];
                        highscoreArray[y, 1] = highscoreArray[y - 1, 1];
                        y--;
                    }
                    highscoreArray[x, 0] = newHighscoreName.text;
                    highscoreArray[x, 1] = ScoreKeeper.Score.ToString();
                    break;
                }
                else if (x == 1)
                {
                    while (y > 1)
                    {
                        highscoreArray[y, 0] = highscoreArray[y - 1, 0];
                        highscoreArray[y, 1] = highscoreArray[y - 1, 1];
                        y--;
                    }
                    highscoreArray[x, 0] = newHighscoreName.text;
                    highscoreArray[x, 1] = ScoreKeeper.Score.ToString();
                    break;
                }
                else if (x == 2)
                {
                    while (y > 2)
                    {
                        highscoreArray[y, 0] = highscoreArray[y - 1, 0];
                        highscoreArray[y, 1] = highscoreArray[y - 1, 1];
                        y--;
                    }
                    highscoreArray[x, 0] = newHighscoreName.text;
                    highscoreArray[x, 1] = ScoreKeeper.Score.ToString();
                    break;

                }
                else if (x == 3)
                {
                    while (y > 3)
                    {
                        highscoreArray[y, 0] = highscoreArray[y - 1, 0];
                        highscoreArray[y, 1] = highscoreArray[y - 1, 1];
                        y--;
                    }
                    highscoreArray[x, 0] = newHighscoreName.text;
                    highscoreArray[x, 1] = ScoreKeeper.Score.ToString();
                    break;
                }
                else if (x == 4)
                {
                    highscoreArray[x, 0] = newHighscoreName.text;
                    highscoreArray[x, 1] = ScoreKeeper.Score.ToString();
                    break;
                }

            }
        }
    }

    void fillArray()
    {
        int a = 0;
        while (a <= 4)
        {
            highscoreArray[i, 0] = " ";
            highscoreArray[1, 1] = "0";
            a++;
        }
        
    }
}

