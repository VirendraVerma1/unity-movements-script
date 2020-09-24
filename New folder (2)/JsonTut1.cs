using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;
using UnityEngine.UI;

public class JsonTut1 : MonoBehaviour
{
    public Text centerText;
    private string JSONstring;
    //private JsonData stateData;
    void Start()
    {

        // Here we read our file from our game assets as a string field.
        JSONstring = File.ReadAllText(Application.dataPath + "/Resources/Questions.json");
        Debug.Log(Application.dataPath + "/Resources/Questions.json");
        // Now w convert it to JsonData object
        var stateData = JsonMapper.ToObject(JSONstring);
        var questions = stateData["CityName"];

        for (int i = 0; i < questions.Coun t; i++)
        {
            // Obtain the current questions data
            var currentQuestionData = questions[i];
            // Obtain the actual question "How much wood would..."
            var question = currentQuestionData["name"];
            // Obtain the answers "0, 7, 13..."
            var answers = currentQuestionData["population"];
            //currentQuestionData["answers"] = 12332;
            // Obtain the correct answer "27"
            var correct = currentQuestionData["food"];
            var happiness = currentQuestionData["happiness"];
            var note = currentQuestionData["note"];
            // Print the contents to the Debug Console
            print(question+"  "+answers+"  "+correct+" "+happiness+" "+note);
            centerText.text = questions + answers.ToString();
        }
        //stateData["questions"] = questions[0];

        var JSONdata = JsonMapper.ToJson(stateData);
        File.WriteAllText(Application.dataPath + "/Resources/Questions.json", JSONdata);
    }
}
