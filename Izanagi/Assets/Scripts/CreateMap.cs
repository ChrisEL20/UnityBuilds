using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour {

    public Texture2D map;

    public GameObject blackTile;
    public GameObject GreyTile;
    public GameObject whiteTile;

    public GameObject blueItem;
    public GameObject redItem;
    public GameObject greenItem;

    public void OnEnable()
    {
        Color color;
        float posX;
        float posY;

        Vector3 spawnPosition;

        int precition = 5;

        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height; y++)
            {
                color = map.GetPixel(x, y);

                //Place Background Objects
                if ((float)Mathf.RoundToInt(color.grayscale*precition)/precition == 0 && this.blackTile != null)
                {
                    posX = (float)(x - (map.width / 2));
                    posY = (float)(y - (map.height / 2 - 1));
                    spawnPosition = new Vector3(posX, posY, 0);
                    GameObject.Instantiate(this.blackTile, spawnPosition, this.blackTile.transform.rotation);
                }

                else if (color.grayscale >= 0.4f && color.grayscale <= 0.6f  && color.r <= 0.8f && color.g <= 0.8f && color.b <= 0.8f && this.GreyTile != null)
                {
                    posX = (float)(x - (map.width / 2));
                    posY = (float)(y - (map.height / 2 - 1));
                    spawnPosition = new Vector3(posX, posY, 0);
                    GameObject.Instantiate(this.GreyTile, spawnPosition, this.GreyTile.transform.rotation);
                }

                else if (this.whiteTile != null)
                {
                    posX = (float)(x - (map.width / 2));
                    posY = (float)(y - (map.height / 2 - 1));
                    spawnPosition = new Vector3(posX, posY, 0);
                    GameObject.Instantiate(this.whiteTile, spawnPosition, this.whiteTile.transform.rotation);
                }

                //PlaceItems
                if (color.r >= 0.98f  && color.g <= 0.5f && color.b <= 0.5f && this.redItem != null)
                {
                    posX = (float)(x - (map.width / 2));
                    posY = (float)(y - (map.height / 2 - 1));
                    spawnPosition = new Vector3(posX, posY, 0);
                    GameObject.Instantiate(this.redItem, spawnPosition, this.redItem.transform.rotation);
                }
                else if (color.g >= 0.98f && color.r <= 0.5f && color.b <= 0.5f && this.greenItem != null)
                {
                    posX = (float)(x - (map.width / 2));
                    posY = (float)(y - (map.height / 2 - 1));
                    spawnPosition = new Vector3(posX, posY, 0);
                    GameObject.Instantiate(this.greenItem, spawnPosition, this.greenItem.transform.rotation);
                }
                else if (color.b >= 0.98f && color.g <= 0.5f && color.r <= 0.5f && this.blueItem != null)
                {
                    posX = (float)(x - (map.width / 2));
                    posY = (float)(y - (map.height / 2 - 1));
                    spawnPosition = new Vector3(posX, posY, 0);
                    GameObject.Instantiate(this.blueItem, spawnPosition, this.blueItem.transform.rotation);
                }
            }
        }

    }


}
