using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SocketIO;

public class PlayableCharacter : MonoBehaviour
{
    public SocketIOComponent socket;
    public float id;
    public int selfId;
    public int celebrity;
    public string pname;
    public string sname;
    public int number;
    public float x;
    public float y;
    public bool displayed;

    public TextMesh plName;
    public TextMesh sPlName;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerInit()
    {
        plName.text = pname;
        sPlName.text = sname;
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.GetComponent<PlayableCharacter>() == null)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            //Debug.Log(coll.collider.name);
            Vector3 relativePos = transform.InverseTransformPoint(coll.transform.position);
            if (relativePos.y > 0)
            {
                data["dir"] = "up";
                data["state"] = "false";
                socket.Emit("playerCollidedWall", new JSONObject(data));
            }
            else if (relativePos.y < 0)//коллайдер снизу
            {
                data["dir"] = "down";
                data["state"] = "false";
                socket.Emit("playerCollidedWall", new JSONObject(data));
            }

            if (relativePos.x > 0)
            {
                data["dir"] = "right";
                data["state"] = "false";
                socket.Emit("playerCollidedWall", new JSONObject(data));
            }
            else if (relativePos.x < 0)//коллайдер снизу
            {
                data["dir"] = "left";
                data["state"] = "false";
                socket.Emit("playerCollidedWall", new JSONObject(data));
            }
        }
    }
    void OnCollisionExit(Collision coll)
    {
        if (coll.gameObject.GetComponent<PlayableCharacter>() == null)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            //Debug.Log(coll.collider.name);
            Vector3 relativePos = transform.InverseTransformPoint(coll.transform.position);
            if (relativePos.y > 0)//коллайдер свреху
            {
                data["dir"] = "up";
                data["state"] = "true";
                socket.Emit("playerCollidedWall", new JSONObject(data));
            }
            else if (relativePos.y < 0)//коллайдер снизу
            {
                data["dir"] = "down";
                data["state"] = "true";
                socket.Emit("playerCollidedWall", new JSONObject(data));
            }

            if (relativePos.x > 0)//коллайдер свреху
            {
                data["dir"] = "right";
                data["state"] = "true";
                socket.Emit("playerCollidedWall", new JSONObject(data));
            }
            else if (relativePos.x < 0)//коллайдер снизу
            {
                data["dir"] = "left";
                data["state"] = "true";
                socket.Emit("playerCollidedWall", new JSONObject(data));
            }
        }
    }
}
