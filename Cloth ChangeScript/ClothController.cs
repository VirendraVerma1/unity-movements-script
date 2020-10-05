using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class ClothController : MonoBehaviour
{
    public Animator PlayerAnim;

    [Header("Weapons")]
    public GameObject WeaponBundle;
    public GameObject WeaponGameObjectUIBundle;
    public GameObject[] PlayerWeapons;

    [Header("Cloth UI")]
    public GameObject TopClothBundle;
    public GameObject BottomClothBundle;

    void Awake()
    {
        saveload.Load();
    }

    void Start()
    {
        SetCloths();
        SetWeapons();
    }

    #region Selecting WeaponCategory

    public void OnWeapondButtonPressed()
    {
        ActivatePanel(WeaponBundle.name);
    }

    public void OnWeaponSelect(string i)
    {
        saveload.weaponsid=i;
        saveload.Save();
        SetWeapons();
    }

    void SetWeapons()
    {
        //disable all the cloths first
        for (int i = 0; i < PlayerWeapons.Length; i++)
        {
            PlayerWeapons[i].gameObject.SetActive(false);
        }

        //set cloth from top
        foreach (Transform child in WeaponGameObjectUIBundle.transform)
        {
            if (child.GetComponent<ClothDetails>().Category == "Weapons" && child.GetComponent<ClothDetails>().ClothID == saveload.weaponsid)
            {
                int clothCode = Convert.ToInt32(saveload.weaponsid);
                PlayerWeapons[clothCode].gameObject.SetActive(true);
                saveload.weaponGOName = gameObject.name;
            }

        }
        SetAnimation();
    }

    #endregion

    #region on Selecting Cloth Category

    public void OnTopButtonPressed()
    {
        ActivatePanel(TopClothBundle.name);
    }

    public void OnBottomButtonPressed()
    {
        ActivatePanel(BottomClothBundle.name);
    }


    public void ActivatePanel(string panelToBeActivated)
    {
        TopClothBundle.SetActive(panelToBeActivated.Equals(TopClothBundle.name));
        BottomClothBundle.SetActive(panelToBeActivated.Equals(BottomClothBundle.name));
        WeaponBundle.SetActive(panelToBeActivated.Equals(WeaponBundle.name));
    }

    #endregion

    #region On Cloth Select

    public void OnTopSelect(string id)
    {
        saveload.clothtopid = id;
        saveload.Save();
        SetCloths();
    }

    public void OnBottomSelect(string id)
    {
        saveload.clothbottomid = id;
        saveload.Save();
        SetCloths();
    }

    #endregion

    #region ClothSetToCharacter

    public GameObject TopClothGameObject;
    public GameObject BottomClothGameObject;

    [Header("Player Cloth")]
    public GameObject PlayerGameObject;
    public GameObject[] TopCloth;
    public GameObject[] BottomCloth;

    void SetCloths()
    {
        //disable all the cloths first
        for (int i = 0; i < TopCloth.Length; i++)
        {
            TopCloth[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < BottomCloth.Length; i++)
        {
            BottomCloth[i].gameObject.SetActive(false);
        }

            //set cloth from top
            foreach (Transform child in TopClothGameObject.transform)
            {
                if (child.GetComponent<ClothDetails>().Category == "Top" && child.GetComponent<ClothDetails>().ClothID == saveload.clothtopid)
                {
                    int clothCode = Convert.ToInt32(saveload.clothtopid);
                    TopCloth[clothCode].gameObject.SetActive(true);
                }

                
            }
            foreach (Transform child in BottomClothGameObject.transform)
            {
                if (child.GetComponent<ClothDetails>().Category == "Bottom" && child.GetComponent<ClothDetails>().ClothID == saveload.clothbottomid)
                {
                    int clothCode = Convert.ToInt32(saveload.clothbottomid);
                    BottomCloth[clothCode].gameObject.SetActive(true);
                }
            }
    }

    #endregion

    #region SetAnimation Using WeaponData

    void SetAnimation()
    {
        if (saveload.weaponsid == "")
        {
            PlayerAnim.SetBool("isIdle", true);
            PlayerAnim.SetBool("isPistolIdle", false);
            PlayerAnim.SetBool("isRifleIdle", false);
        }
        else if (saveload.weaponsid == "0")
        {
            PlayerAnim.SetBool("isPistolIdle", true);
            PlayerAnim.SetBool("isIdle", false);
            PlayerAnim.SetBool("isRifleIdle", false);
        }
        else if (saveload.weaponsid == "1")
        {
            PlayerAnim.SetBool("isRifleIdle", true);
            PlayerAnim.SetBool("isPistolIdle", false);
            PlayerAnim.SetBool("isIdle", false);
        }
    }

    #endregion

    public void OnJoinButtonPressed()
    {
        SceneManager.LoadScene(1);
    }

}