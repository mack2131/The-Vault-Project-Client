using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Texture2D playerAtlas;
    public Sprite playerSprite;
    public int frameWidth = 30;
    public int frameHeight = 47;
    private int row;
    private int column;
    public int moveDir;
    private int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>().sprite;
        StartCoroutine(PlayAnimation());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PlayAnimation()
    {
        while(true)
        {
            Sprite tmp;
            switch (moveDir)
            {
                case 1://движение вправо - moveDir = 1
                    {
                        tmp = Sprite.Create(playerAtlas, new Rect(counter * frameWidth, 2 * frameHeight, frameWidth, frameHeight), new Vector2(0.5f, 0.5f));
                        GetComponent<SpriteRenderer>().sprite = tmp;
                        counter++;
                        if (counter >= 9)
                            counter = 0;
                        break;
                    }
                case 2://движение лево - moveDir = 2
                    {
                        tmp = Sprite.Create(playerAtlas, new Rect(counter * frameWidth, 1 * frameHeight, frameWidth, frameHeight), new Vector2(0.5f, 0.5f));
                        GetComponent<SpriteRenderer>().sprite = tmp;
                        counter++;
                        if (counter >= 9)
                            counter = 0;
                        break;
                    }
                case 3://движение вверх - moveDir = 3
                    {
                        tmp = Sprite.Create(playerAtlas, new Rect(counter * frameWidth, 4 * frameHeight + 4, frameWidth, frameHeight), new Vector2(0.5f, 0.5f));
                        GetComponent<SpriteRenderer>().sprite = tmp;
                        counter++;
                        if (counter >= 9)
                            counter = 0;
                        break;
                    }
                case 4://движение вниз - moveDir = 4
                    {
                        tmp = Sprite.Create(playerAtlas, new Rect(counter * frameWidth, 3 * frameHeight + 2, frameWidth, frameHeight), new Vector2(0.5f, 0.5f));
                        GetComponent<SpriteRenderer>().sprite = tmp;
                        counter++;
                        if (counter >= 9)
                            counter = 0;
                        break;
                    }
                default://покой - moveDir = 0
                    {
                        tmp = Sprite.Create(playerAtlas, new Rect(counter * frameWidth, 0, frameWidth, frameHeight), new Vector2(0.5f, 0.5f));
                        GetComponent<SpriteRenderer>().sprite = tmp;
                        counter++;
                        if (counter >= 9)
                            counter = 0;
                        break;
                    }
            }
            yield return new WaitForSeconds(0.3f);
        }
    }
}
