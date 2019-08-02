using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketIO;

public class TestMove : MonoBehaviour
{

    private SocketIOComponent socket;

    public Transform playersStorage;

    public GameObject playerPref;
    public float cameraDistance;
    public Text testText;

    public int selfId;
    [System.Serializable]
    public struct Player
    {
        public GameObject sprite;
        public int id;
        public int selfId;
        public int celebrity;
        public string name;
        public string sname;
        public int number;
        public float x;
        public float y;
        public bool displayed;
    }
    
    public List<Player> players = new List<Player>();
    public Player thisPlayer;

    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();
        socket.On("init", TestInit);
        socket.On("update", TestUpd);
    }

    // Update is called once per frame
    void Update()
    {
        ListenInput();
    }

    void TestInit(SocketIOEvent e)
    {
        selfId = int.Parse(e.data.GetField("selfId").str);
        Debug.Log("client selfId: " + selfId);
        //Debug.Log("init");
        //Debug.Log(e.data.GetField("playerPack")[0]["x"]);
        for (int i = 0; i < e.data.GetField("playerPack").Count; i++)
        {
            Player pl = new Player();
            pl.sprite = Instantiate(playerPref);
            pl.displayed = true;
            pl.selfId = int.Parse(e.data.GetField("playerPack")[i]["selfId"].str);
            pl.sprite.name = pl.selfId.ToString();
            if (selfId == pl.selfId)
            {
                Camera.main.transform.SetParent(pl.sprite.transform, false);
                Camera.main.transform.position = new Vector3(pl.sprite.transform.position.x, pl.sprite.transform.position.y, -cameraDistance);
                pl.sprite.GetComponent<PlayableCharacter>().socket = socket;
                thisPlayer = pl;
            }
            pl.sprite.transform.position = new Vector3(e.data.GetField("playerPack")[i]["x"].n, e.data.GetField("playerPack")[i]["y"].n, 0);
            pl.sprite.transform.SetParent(playersStorage, false);
            pl.sprite.GetComponent<PlayableCharacter>().pname = e.data.GetField("playerPack")[i]["name"].str;
            pl.sprite.GetComponent<PlayableCharacter>().id = e.data.GetField("playerPack")[i]["id"].n;
            pl.sprite.GetComponent<PlayableCharacter>().sname = e.data.GetField("playerPack")[i]["sname"].str;
            pl.sprite.GetComponent<PlayableCharacter>().PlayerInit();
            players.Add(pl);
        }
    }

    void TestUpd(SocketIOEvent e)
    {
        //Debug.Log("client players: " + players.Count + "  server player: " + e.data.GetField("playerUpdPack").Count);
        Debug.Log(e.data.GetField("lp"));
        /*
        if (e.data.GetField("playerRemPack").Count > 0)
        {
            //Debug.Log(e.data);
            for(int i = 0; i < e.data.GetField("playerRemPack").Count; i++)
            {
                for(int j = 0; j < players.Count; j++)
                {
                    if (players[j].sprite.GetComponent<PlayableCharacter>().id == e.data.GetField("playerRemPack")[i].n)
                    {
                        Destroy(players[j].sprite);
                        players.RemoveAt(j);
                    }
                }
            }
        }
        if(players.Count != e.data.GetField("playerUpdPack").Count)
        {
            for(int i = e.data.GetField("playerUpdPack").Count - players.Count; i < e.data.GetField("playerUpdPack").Count; i++)
            {
                Player pl = new Player();
                pl.sprite = Instantiate(playerPref);
                pl.displayed = true;
                pl.selfId = int.Parse(e.data.GetField("playerUpdPack")[i]["selfId"].str);
                pl.sprite.name = pl.selfId.ToString();
                pl.sprite.transform.position = new Vector3(e.data.GetField("playerUpdPack")[i]["x"].n, e.data.GetField("playerUpdPack")[i]["y"].n, 0);
                pl.sprite.transform.SetParent(playersStorage, false);
                pl.sprite.GetComponent<PlayableCharacter>().pname = e.data.GetField("playerUpdPack")[i]["name"].str;
                pl.sprite.GetComponent<PlayableCharacter>().id = e.data.GetField("playerUpdPack")[i]["id"].n;
                pl.sprite.GetComponent<PlayableCharacter>().sname = e.data.GetField("playerUpdPack")[i]["sname"].str;
                pl.sprite.GetComponent<PlayableCharacter>().PlayerInit();
                players.Add(pl);
            }
        }
        for(int i = 0; i < e.data.GetField("playerUpdPack").Count; i++)
        {
            players[i].sprite.transform.position = new Vector3(e.data.GetField("playerUpdPack")[i]["x"].n, e.data.GetField("playerUpdPack")[i]["y"].n, 0);
            players[i].sprite.GetComponent<PlayerAnimation>().moveDir = (int)e.data.GetField("playerUpdPack")[i]["moveDir"].n;
        }
        */
    }

    void ListenInput()
    {

        Dictionary<string, string> data = new Dictionary<string, string>();
        //key press
        if (Input.GetKey(KeyCode.W) && !GetComponent<ChatManager>().focused)
        {
            data["inputId"] = "up";
            data["state"] = "true";
            socket.Emit("keyPress", new JSONObject(data));
        }
        else if (Input.GetKey(KeyCode.S) && !GetComponent<ChatManager>().focused)
        {
            data["inputId"] = "down";
            data["state"] = "true";
            socket.Emit("keyPress", new JSONObject(data));
        }
        else if (Input.GetKey(KeyCode.A) && !GetComponent<ChatManager>().focused)
        {
            data["inputId"] = "left";
            data["state"] = "true";
            socket.Emit("keyPress", new JSONObject(data));
        }
        else if (Input.GetKey(KeyCode.D) && !GetComponent<ChatManager>().focused)
        {
            data["inputId"] = "right";
            data["state"] = "true";
            socket.Emit("keyPress", new JSONObject(data));
        }

        //key up
        if (Input.GetKeyUp(KeyCode.W))
        {
            data["inputId"] = "up";
            data["state"] = "false";
            socket.Emit("keyPress", new JSONObject(data));
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            data["inputId"] = "down";
            data["state"] = "false";
            socket.Emit("keyPress", new JSONObject(data));
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            data["inputId"] = "left";
            data["state"] = "false";
            socket.Emit("keyPress", new JSONObject(data));
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            data["inputId"] = "right";
            data["state"] = "false";
            socket.Emit("keyPress", new JSONObject(data));
        }
    }
}