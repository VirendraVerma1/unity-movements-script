using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;
using TMPro;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [Header("Connection Status")]
    public TextMeshProUGUI connectionStatusText;

    [Header("Login Ui Panel")]
    public TMP_InputField playerNameInput;
    public GameObject Login_Ui_panel;

    [Header("Game Option UI Panel")]
    public GameObject GameOptions_UI_Panel;

    [Header("Create room UI Panel")]
    public GameObject CreateRoom_UI_Pannel;
    public TMP_InputField roomNameInputField;
    public TMP_InputField maxplayerInputField;

    [Header("Inside room UI Panel")]
    public GameObject InsideRoom_UI_Pannel;
    public TextMeshProUGUI roomInfoText;
    public GameObject playerListPrefab;
    public GameObject playerListContent;
    public GameObject startGameButton;

    [Header("RoomList UI Panel")]
    public GameObject RoomList_UI_Pannel;
    public GameObject roomListEntryPrefab;
    public GameObject roomListParentGameObject;

    [Header("Join random Room UI Panel")]
    public GameObject JoinRandomRoom_UI_Pannel;

    private Dictionary<string, RoomInfo> cachedRoomList;
    private Dictionary<string, GameObject> roomListGameObjects;
    private Dictionary<int, GameObject> playerListGameObjects;

    List<RoomInfoBean> roomInfoList = new List<RoomInfoBean>();
    public string playerName = "something";

    private void Awake()
    {
        ultimatesave.Load();
        ultimatesave.gameMode = "Game";
        ultimatesave.Save();
        print("Game Mode = " + ultimatesave.gameMode);
    }

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        InitializeName();
        OnGenerateRandomRoomNameButtonPressed();
        //OnLoginButtonClicked();
        print("Player Name = "+ultimatesave.playername);
        //OnQuickPlayButtonPressed();
        StartCoroutine(WaitAndLogin());
    }

    IEnumerator WaitAndLogin()
    {
        yield return new WaitForSeconds(1);
        OnLoginButtonClicked();
    }

    bool joinStart = false;
    void Update()
    {
        connectionStatusText.text = "Connection status : " + PhotonNetwork.NetworkClientState;
        //print("Connection status : " + PhotonNetwork.NetworkClientState);
    }

    //-----------------------MyCode
    public GameObject QuickButton;
    public void OnQuickPlayButtonPressed()
    {
        OnLoginButtonClickedFromQuickPlay();
        roomInfoList.Clear();
        roomListGameObjects = new Dictionary<string, GameObject>();
        PhotonNetwork.AutomaticallySyncScene = true;
        StartCoroutine(WaitAndConnect());
        QuickButton.SetActive(false);
        StartCoroutine(WaitAndShowQuickPlayButton());
    }

    IEnumerator WaitAndShowQuickPlayButton()
    {
        yield return new WaitForSeconds(30);
        QuickButton.SetActive(true);
    }


    #region change name

    [Header("Change Name")]
    public TMP_InputField playernamet;

    void InitializeName()
    {
        ultimatesave.playername = "Player" + Random.Range(111, 999);
        if (ultimatesave.playername == "")
        {
            ultimatesave.playername = "Player" + Random.Range(111, 999);
            playerName = ultimatesave.playername;
        }
        else
        {
            playerName = ultimatesave.playername;
        }
        playernamet.text = ultimatesave.playername;
        ultimatesave.Save();
    }

    public void OnChangeName()
    {
        ultimatesave.playername = playernamet.text.ToString() + "_" + Random.Range(111, 999);
        ultimatesave.Save();
        playernamet.text = ultimatesave.playername;
        playerName = ultimatesave.playername;
        OnLoginButtonClicked();
    }

    #endregion

    #region UI Callbacks

    public void OnLoginButtonClicked()
    {
        if (playerName != "")
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.Disconnect();
            }

            print("game mode player name" + playerName);
            PhotonNetwork.LocalPlayer.NickName = playerName;
            var appsettings = new AppSettings();
            appsettings.AppVersion = ultimatesave.gameMode;
            appsettings.AppIdRealtime = "f3a50035-db1a-400b-b2b5-6cbdceae0696";
            PhotonNetwork.ConnectUsingSettings(appsettings);
            //PhotonNetwork.ConnectUsingSettings();
            //print("game mode connected");
            //StartCoroutine(WaitAndConnect());
            print("connected to sserver");
        }
    }

    public void OnLoginButtonClickedFromQuickPlay()
    {
        if (playerName != "")
        {
            if (!PhotonNetwork.IsConnected)
            {
                print("game mode player name" + playerName);
                PhotonNetwork.LocalPlayer.NickName = playerName;
                var appsettings = new AppSettings();
                appsettings.AppVersion = ultimatesave.gameMode;
                appsettings.AppIdRealtime = "f3a50035-db1a-400b-b2b5-6cbdceae0696";
                PhotonNetwork.ConnectUsingSettings(appsettings);
                //PhotonNetwork.ConnectUsingSettings();
                //print("game mode connected");
                //StartCoroutine(WaitAndConnect());
                print("connected to sserver");
            }
        }
    }

    IEnumerator WaitAndConnect()
    {
        if (joinStart)
        {
            OnJoinRandomRoomButtonClicked();
            yield return new WaitForSeconds(1);
        }
        else
        {
            yield return new WaitForSeconds(1);
            StartCoroutine(WaitAndConnect());
        }


    }

    public void OnRoomCreateButtonClicked()
    {

        string roomName = roomNameInputField.text.ToString();

        if (string.IsNullOrEmpty(roomName))
        {
            roomName = "Room" + Random.Range(1000, 100000);
        }

        RoomOptions roomOptions = new RoomOptions();
        //roomOptions.MaxPlayers = (byte)int.Parse(maxplayerInputField.text.ToString());
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }

    public void OnGenerateRandomRoomNameButtonPressed()
    {
        maxplayerInputField.text = "Room" + Random.Range(1000, 100000);
    }

    public void OnShowRoomListButtonClicked()
    {
        if (!PhotonNetwork.InLobby)
        {
            PhotonNetwork.JoinLobby();
        }
        ActivatePanel(RoomList_UI_Pannel.name);
    }

    public void OnBackButtonClicked()
    {
        if (PhotonNetwork.InRoom)
            PhotonNetwork.LeaveRoom();
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }
        ActivatePanel(GameOptions_UI_Panel.name);
    }

    public GameObject GameStartButton;

    public void OnStartGameButtonClicked()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            GameStartButton.SetActive(false);
            PhotonNetwork.LoadLevel(ultimatesave.gameMode);
            StartCoroutine(ShowMyStartButton());
        }
    }

    IEnumerator ShowMyStartButton()
    {
        yield return new WaitForSeconds(5);
        GameStartButton.SetActive(true);
    }

    #endregion

    #region Photon Callbacks
    public override void OnConnected()
    {
        print("Connected to internet");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        joinStart = false;
    }

    public override void OnConnectedToMaster()
    {
        print(PhotonNetwork.LocalPlayer.NickName + "is connected to photon server");
        ActivatePanel(GameOptions_UI_Panel.name);
        joinStart = true;
    }

    public override void OnCreatedRoom()
    {
        //print("roomCretaed");
    }

    public override void OnJoinedRoom()
    {
        ActivatePanel(InsideRoom_UI_Pannel.name);

        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            startGameButton.SetActive(true);
        }
        else
        {
            startGameButton.SetActive(false);
        }


        roomInfoText.text = "Room name: " + PhotonNetwork.CurrentRoom.Name + " " +
            "/Max: " + PhotonNetwork.CurrentRoom.PlayerCount + "/" +
            PhotonNetwork.CurrentRoom.MaxPlayers;

        if (playerListGameObjects == null)
        {
            playerListGameObjects = new Dictionary<int, GameObject>();
        }

        //Instantiate player list gameObjects
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            GameObject playerListGameObject = Instantiate(playerListPrefab);
            playerListGameObject.transform.SetParent(playerListContent.transform);
            playerListGameObject.transform.localScale = Vector3.one;
            playerListGameObject.transform.localEulerAngles = Vector3.zero;
            playerListGameObject.transform.localPosition = Vector3.zero;


            playerListGameObject.transform.Find("PlayerNameText").GetComponent<TextMeshProUGUI>().text = player.NickName;
            if (player.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                playerListGameObject.transform.Find("PlayerIndicator").gameObject.SetActive(true);
            }
            else
            {
                playerListGameObject.transform.Find("PlayerIndicator").gameObject.SetActive(false);
            }
            playerListGameObjects.Add(player.ActorNumber, playerListGameObject);
        }
        OnStartGameButtonClicked();//-----------------------------------------------------my additional line
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        ClearRoomListView();
        foreach (RoomInfo room in roomList)
        {
            print(room.Name);
            if (!room.IsOpen || !room.IsVisible || room.RemovedFromList)
            {
                int i = 0;
                foreach (RoomInfoBean b in roomInfoList)
                {
                    if (b.roomName == room.Name)
                    {
                        roomInfoList.RemoveAt(i);
                    }
                    i++;
                }
            }
            else
            {
                int i = 0;
                bool flag = false;
                foreach (RoomInfoBean b in roomInfoList)
                {
                    if (b.roomName == room.Name)
                    {
                        b.roomInfo = room;
                        flag = true;
                    }
                    i++;
                }
                if (flag == false)
                {
                    roomInfoList.Add(new RoomInfoBean(room.Name, room));
                }
                
            }
        }

        //print("Cached Room List =" + cachedRoomList.Values.Count);

        foreach (RoomInfoBean b in roomInfoList)
        {
            RoomInfo room = b.roomInfo;
            //print(room.Name);
            GameObject roomListEntryGameObject = Instantiate(roomListEntryPrefab);
            roomListEntryGameObject.transform.SetParent(roomListParentGameObject.transform);

            roomListEntryGameObject.transform.Find("RoomNameText").Find("RoomNameText").GetComponent<TextMeshProUGUI>().text = room.Name;
            // roomListEntryGameObject.transform.Find("RoomPlayerText").GetComponent<TextMeshProUGUI>().text = room.PlayerCount + "/" + room.MaxPlayers;
            roomListEntryGameObject.transform.Find("JoinButton").GetComponent<Button>().onClick.AddListener(() => OnJoinRoomButtonClicked(room.Name));
            // roomListGameObjects.Add(room.Name, roomListEntryGameObject);
            //print("in loop");
            roomListEntryGameObject.transform.localScale = Vector3.one;
            roomListEntryGameObject.transform.localEulerAngles = Vector3.zero;
            roomListEntryGameObject.transform.localPosition = Vector3.zero;
        }
        //print("out loop");
    }

    public override void OnLeftLobby()
    {
        ClearRoomListView();
        //cachedRoomList.Clear();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {

        roomInfoText.text = "Room name: " + PhotonNetwork.CurrentRoom.Name + " " +
            "/Max: " + PhotonNetwork.CurrentRoom.PlayerCount + "/" +
            PhotonNetwork.CurrentRoom.MaxPlayers;

        GameObject playerListGameObject = Instantiate(playerListPrefab);
        playerListGameObject.transform.SetParent(playerListContent.transform);
        playerListGameObject.transform.localScale = Vector3.one;
        playerListGameObject.transform.localPosition = Vector3.zero;


        playerListGameObject.transform.Find("PlayerNameText").GetComponent<TextMeshProUGUI>().text = newPlayer.NickName;
        if (newPlayer.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
        {
            playerListGameObject.transform.Find("PlayerIndicator").gameObject.SetActive(true);
        }
        else
        {
            playerListGameObject.transform.Find("PlayerIndicator").gameObject.SetActive(false);
        }
        playerListGameObjects.Add(newPlayer.ActorNumber, playerListGameObject);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        roomInfoText.text = "Room name: " + PhotonNetwork.CurrentRoom.Name + " " +
           "/Max: " + PhotonNetwork.CurrentRoom.PlayerCount + "/" +
           PhotonNetwork.CurrentRoom.MaxPlayers;

        Destroy(playerListGameObjects[otherPlayer.ActorNumber].gameObject);
        playerListGameObjects.Remove(otherPlayer.ActorNumber);

        if (PhotonNetwork.LocalPlayer.IsMasterClient)
        {
            startGameButton.SetActive(true);
        }
        else
        {
            startGameButton.SetActive(false);
        }
    }

    public override void OnLeftRoom()
    {
        ActivatePanel(GameOptions_UI_Panel.name);

        foreach (GameObject playerListGameObject in playerListGameObjects.Values)
        {
            Destroy(playerListGameObject);
        }
        playerListGameObjects.Clear();
        playerListGameObjects = null;
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        string roomName = "Room " + Random.Range(1000, 100000);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }

    #endregion

    #region Private Methods

    void OnJoinRoomButtonClicked(string _roomName)
    {
        if (PhotonNetwork.InLobby)
        {
            PhotonNetwork.LeaveLobby();
        }
        PhotonNetwork.JoinRoom(_roomName);
    }

    void ClearRoomListView()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("RoomListPrefab");
        foreach (GameObject g in go)
        {
            Destroy(g);
        }
        // roomListGameObjects.Clear();
    }

    public void OnLeaveButtonClicked()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void OnJoinRandomRoomButtonClicked()
    {

        ActivatePanel(JoinRandomRoom_UI_Pannel.name);
        PhotonNetwork.JoinRandomRoom();
    }

    #endregion

    #region Public methods

    public void ActivatePanel(string panelToBeActivated)
    {
        Login_Ui_panel.SetActive(panelToBeActivated.Equals(Login_Ui_panel.name));
        GameOptions_UI_Panel.SetActive(panelToBeActivated.Equals(GameOptions_UI_Panel.name));
        CreateRoom_UI_Pannel.SetActive(panelToBeActivated.Equals(CreateRoom_UI_Pannel.name));
        InsideRoom_UI_Pannel.SetActive(panelToBeActivated.Equals(InsideRoom_UI_Pannel.name));
        RoomList_UI_Pannel.SetActive(panelToBeActivated.Equals(RoomList_UI_Pannel.name));
        JoinRandomRoom_UI_Pannel.SetActive(panelToBeActivated.Equals(JoinRandomRoom_UI_Pannel.name));
    }

    #endregion

}


