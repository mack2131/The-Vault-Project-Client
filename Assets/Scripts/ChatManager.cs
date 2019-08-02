using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketIO;

public class ChatManager : MonoBehaviour
{
    private SocketIOComponent socket;
    //Chat
    public Transform chatContent;
    public Text chatMsgPref;
    public InputField chatInput;

    public bool focused;

    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();

        socket.On("addToChat", RecieveNewChatMsg);
    }

    // Update is called once per frame
    void Update()
    {
        TypeMessage();
    }

    void TypeMessage()
    {
        if (focused && Input.GetKeyDown(KeyCode.Return) && chatInput.text.Length != 0)
        {
            //Debug.Log(chatInput.text);
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["msg"] = chatInput.text;
            socket.Emit("newChatMsg", new JSONObject(data));
            chatInput.text = "";
        }
        else focused = chatInput.isFocused;
    }

    void RecieveNewChatMsg(SocketIOEvent e)
    {
        //Debug.Log("new to chat " + e.data["msg"].str);
        string plName = e.data["playerName"].str;
        string msg = e.data["msg"].str;
        //Debug.Log(msg);
        GameObject chatMsg = Instantiate(chatMsgPref.gameObject, chatContent);
        chatMsg.GetComponent<Text>().text = plName + ": " + msg;
    }
}
