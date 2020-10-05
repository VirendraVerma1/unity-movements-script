using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour
{
    public static CharacterInfo characterInfo;

    public int PlayerID;

    private void OnEnable()
    {
        if (CharacterInfo.characterInfo == null)
        {
            CharacterInfo.characterInfo = this;
        }
        else
        {
            if (CharacterInfo.characterInfo != this)
            {

                Destroy(CharacterInfo.characterInfo.gameObject);
                CharacterInfo.characterInfo = this;
            }
        }
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("PlayerID"))
        {
            PlayerID = PlayerPrefs.GetInt("PlayerID");
        }
        else
        {
            PlayerID = Random.Range(111111, 99999999);
            PlayerPrefs.SetInt("PlayerID", PlayerID);
        }
    }
}
