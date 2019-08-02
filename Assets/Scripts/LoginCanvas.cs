using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketIO;

public class LoginCanvas : MonoBehaviour
{
    private SocketIOComponent socket;
    public GameObject so;
    public GameObject gameManager;

    public InputField loginInput;
    public InputField passInput;
    public Button loginBtn;
    public Button newUserBtn;

    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();
        loginBtn.onClick.AddListener(Login);

        socket.On("logInResponse", Response);
    }

    void Update()
    {
        if (loginInput.text.Length != 0 && passInput.text.Length != 0)
            loginBtn.interactable = true;
        else loginBtn.interactable = false;
    }

    void Login()
    {
        gameManager.SetActive(true);
        Dictionary<string, string> data = new Dictionary<string, string>();
        data["username"] = loginInput.text;
        data["password"] = passInput.text;
        socket.Emit("login", new JSONObject(data));
    }

    void Response(SocketIOEvent e)
    {
        if (e.data["success"] == true)
            Destroy(this.gameObject);
    }
}
