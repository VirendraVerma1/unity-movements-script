using UnityEngine;
using System.Collections;
using LitJson;

public class QuestionLoader
{
    public QuestionLoader()
    {
        Debug.Log("sda");
        // Load the Json file and store its contents in a 'JsonData' variable
        var data = JsonLoader.LoadFile(Application.dataPath + "/Resources/Questions.json");
        // Access the "questions" array in the file and store them in a questions variable (JsonData)
        var questions = data["questions"];

        // Loop through all of the contents in the questions variable
        for (int i = 0; i < questions.Count; i++)
        {
            // Obtain the current questions data
            var currentQuestionData = questions[i];
            // Obtain the actual question "How much wood would..."
            var question = currentQuestionData["question"];
            // Obtain the answers "0, 7, 13..."
            var answers = currentQuestionData["answers"];
            // Obtain the correct answer "27"
            var correct = currentQuestionData["correct"];

            // Print the contents to the Debug Console
            Debug.Log(string.Format("Question: {0}\nAnswers: {1}, {2}, {3}, {4}\nCorrect: {5}",
                question, answers[0], answers[1], answers[2], answers[3], correct));
        }
    }
}