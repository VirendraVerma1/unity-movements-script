using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetData : MonoBehaviour
{
    private string getMovieDataHome = "https://kreasarapps.000webhostapp.com/Movies/getmovies.php";

    public string[] items;

    void Start()
    {
        GetDataa();
    }

    void GetDataa()
    {
        StartCoroutine(DataHome());
    }

    IEnumerator DataHome()
	{
        WWW www = new WWW(getMovieDataHome);
		yield return www;

        if (www.text != "")
        {
            string itemsDataString = www.text;
			items = itemsDataString.Split (';');

            for (int i = 0; i < items.Length-1; i++)
            {
				string movieName = GetDataValue(items [i], "Name:");
                string movieRating = GetDataValue(items[i], "Rating:");
                string movieYear = GetDataValue(items[i], "Year:");
                string movieCategory = GetDataValue(items[i], "Category:");
                string movieDescription = GetDataValue(items[i], "Description:");
                string movieSize = GetDataValue(items[i], "Size:");
                string movieSizeCompany = GetDataValue(items[i], "SizeCompany:");
                string movieImage = GetDataValue(items[i], "Image:");
                string movieScreenshot1 = GetDataValue(items[i], "Screenshot1:");
                string movieScreenshot2 = GetDataValue(items[i], "Screenshot2:");
                string movieScreenshot3 = GetDataValue(items[i], "Screenshot3:");
                string Torrent = GetDataValue(items[i], "Torrent:");

                print(movieName + "|" + movieRating + "|" + movieYear + "|" + movieCategory + "|" + movieDescription + "|" + movieSize + "|");
            }
        }
    }

    string GetDataValue(string data,string index)
	{
		string value = data.Substring (data.IndexOf (index) + index.Length);
		if (value.Contains ("|"))
			value = value.Remove (value.IndexOf ("|"));
		return value;
	}

}
/*
public void highscoregenerator()
	{
		resultStatusText.text= "Loading...";
		resultStatusText.color = Color.green;
		//print ("high score");
		Image scoreImage = Score.GetComponent<Image> ();
		scoreImage.GetComponent<Image> ().color = new Color32 (236,79,48,255);
		rankText.color = Color.white;
		nameText.color = Color.white;
		scoreText.color = Color.white;
		rankText.text = "Rank";
		nameText.text = "Name";
		scoreText.text = "Score";
		go = (GameObject)Instantiate (Score) as GameObject;

		go.transform.SetParent (scoresTransform.transform);
		go.transform.position = scoresTransform.position;
		go.transform.localScale = scoresTransform.localScale;
		StartCoroutine (highscoresdb());
	}

	IEnumerator highscoresdb()
	{
		
		WWW www=new WWW(gettingallhighscores);
		yield return www;

		if (www.text != "") {
			resultStatusText.text = " ";
			string itemsDataString = www.text;
			items = itemsDataString.Split (';');
			
			namesdb = " ";
			highscoredb = 0;
			iddb = 0;
			saveload.Load ();

			for (int i = 0; i < items.Length-1; i++) {
				namesdb = GetDataValue (items [i], "Name:");
				string temp = GetDataValue (items [i], "Highscore:");
				string tempid=GetDataValue (items [i], "ID:");

				//conversion of string to int highscore
				for (int j = 0; j < temp.Length; j++) {
					char a = temp [j];
					t = t * 10 + ChartoIntConverter (a);
				}

				//conversion of string to int id
				for (int j = 0; j < tempid.Length; j++) {
					char a = tempid [j];
					tt = tt * 10 + ChartoIntConverter (a);
				}


				iddb = tt;



				tt = 0;
				highscoredb= t;
				t = 0;
				if(highscoredb>0){
				rankText.text = (i + 1).ToString();
				nameText.text = namesdb.ToString ();
				scoreText.text = highscoredb.ToString ();

				if ( saveload.id==iddb) {
					rankText.color = Color.blue;
					nameText.color = Color.blue;
					scoreText.color = Color.blue;
				} else {
					rankText.color = Color.white;
					nameText.color = Color.white;
					scoreText.color = Color.white;
				}
				Image scoreImage = Score.GetComponent<Image> ();
				scoreImage.GetComponent<Image> ().color = new Color32 (74,217,58,255);
				go = (GameObject)Instantiate (Score) as GameObject;

				go.transform.SetParent (scoresTransform.transform);
				go.transform.position = scoresTransform.position;
				go.transform.localScale = scoresTransform.localScale;
				}
			}
		} else {
			resultStatusText.text = "Check the connection";
			resultStatusText.color = Color.red;
		}
	}

	string GetDataValue(string data,string index)
	{
		string value = data.Substring (data.IndexOf (index) + index.Length);
		if (value.Contains ("|"))
			value = value.Remove (value.IndexOf ("|"));
		return value;
	}
 * 
 * WWW www = new WWW(imageURL + fileName);
        yield return www;
        Texture2D texture = www.texture;
        //this.GetComponent<Renderer>().material.mainTexture = texture;
        //byte[] bytes = texture.EncodeToJPG();
        //File.WriteAllBytes(Application.persistentDataPath + uploadfileScript.fileName, bytes);

        Rect rec = new Rect(0, 0, texture.width, texture.height);
        tempGO.transform.Find("PropertyPic").GetComponent<Image>().sprite = Sprite.Create(texture, rec, new Vector2(0.5f, 0.5f), 100);
        //rentimages[i].transform.Find("PropertyPic").GetComponent<Image>().sprite = Sprite.Create(texture, rec, new Vector2(0.5f, 0.5f), 100);
        //}
*/