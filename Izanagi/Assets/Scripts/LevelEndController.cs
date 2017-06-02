using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndController : MonoBehaviour {
    [System.Serializable]
    public class LevelEndPoint{
        public PlayerController.PlayerType targetType;
        public GameObject endPositionGameObject;
        public bool targetIsSnaped;
    }

    [SerializeField]
    public LevelEndPoint[] levelEndPoints;

    public float snapDistance;

    public List<PlayerController> triggeredPlayers;

    public LevelEndController()
    {
        this.snapDistance = 2f;
        this.triggeredPlayers = new List<PlayerController>();
    }

	void Update () {
		if (this.triggeredPlayers.Count >= this.levelEndPoints.Length)
        {
            for (int i = 0; i < this.triggeredPlayers.Count; i++)
            {
                for (int j = 0; j < this.levelEndPoints.Length; j++)
                {
                    if (this.triggeredPlayers[i].type == this.levelEndPoints[j].targetType)
                    {
                        Debug.DrawLine(this.levelEndPoints[j].endPositionGameObject.transform.position, this.triggeredPlayers[i].transform.position);
                        float dist = Vector2.Distance(this.levelEndPoints[j].endPositionGameObject.transform.position, this.triggeredPlayers[i].transform.position);
                        if (dist < this.snapDistance)
                        {
                            this.triggeredPlayers[i].SnapToEndTarget(this.levelEndPoints[j].endPositionGameObject.transform.position);
                            this.levelEndPoints[j].targetIsSnaped = true;
                            break;
                        }
                    }
                }
            }
        }

        if (this.CheckAllCharactersSnaped())
        {
            print("level complete");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController playerCont = other.gameObject.GetComponent(typeof(PlayerController)) as PlayerController;
        if (playerCont != null)
        {
            if (this.triggeredPlayers.Contains(playerCont) == false)
                this.triggeredPlayers.Add(playerCont);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        PlayerController playerCont = other.gameObject.GetComponent(typeof(PlayerController)) as PlayerController;
        if (playerCont != null)
        {
            if (this.triggeredPlayers.Contains(playerCont))
                this.triggeredPlayers.Remove(playerCont);
        }
    }

    public bool CheckAllCharactersSnaped()
    {
        int counter = 0;

        foreach (LevelEndPoint b in this.levelEndPoints)
        {
            if (b.targetIsSnaped)
                counter += 1;
        }
        if (counter < this.levelEndPoints.Length)
            return false;
        else
            return true;
    }
}
