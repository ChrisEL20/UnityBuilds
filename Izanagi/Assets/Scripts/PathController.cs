using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathController : MonoBehaviour {

    public float speed;

    public GameObject target;
    private Rigidbody2D rigidBodyComp;

    public int saveMovementPointsCounter;
    private Vector3 nextMovementPoint;
    private Vector3[] lastMovementPoints;

    public LayerMask rayCastHitLayer;

    public PathController()
    {
        this.speed = 2f;

        this.target = null;
        this.saveMovementPointsCounter = 10;
    }

    void OnEnable()
    {
        this.rigidBodyComp = this.gameObject.GetComponent(typeof(Rigidbody2D)) as Rigidbody2D;
        this.lastMovementPoints = new Vector3[this.saveMovementPointsCounter];
        this.nextMovementPoint = this.gameObject.transform.position;

        if (this.target == null || this.rigidBodyComp == null)
            return;
        this.SetNextMovementPoint();
    }
	
	void FixedUpdate () {
        if (this.target == null || this.rigidBodyComp == null)
            return;

        if (this.IsByTarget())
            return;

        float dist = Vector3.Distance(this.gameObject.transform.position, this.nextMovementPoint);
        if (dist >= 0.3f)
        {
            Vector2 dir = this.nextMovementPoint - this.gameObject.transform.position;
            this.rigidBodyComp.MovePosition(this.rigidBodyComp.position + dir * this.speed * Time.deltaTime);
        }
        else
            this.SetNextMovementPoint();
        

	}

    bool IsByTarget()
    {
        Vector2 currentPos = this.gameObject.transform.position;
        float scaleFactor = this.gameObject.transform.localScale.x;

        RaycastHit2D leftHit = Physics2D.BoxCast(currentPos + (Vector2.left * scaleFactor / 2), new Vector2(scaleFactor / 2, scaleFactor), 0, Vector2.zero, 10f, this.rayCastHitLayer);
        RaycastHit2D rightHit = Physics2D.BoxCast(currentPos + (Vector2.right * scaleFactor / 2), new Vector2(scaleFactor / 2, scaleFactor), 0, Vector2.zero, 10f, this.rayCastHitLayer);
        RaycastHit2D upHit = Physics2D.BoxCast(currentPos + (Vector2.up * scaleFactor / 2), new Vector2(scaleFactor, scaleFactor / 2), 0, Vector2.zero, 10f, this.rayCastHitLayer);
        RaycastHit2D downHit = Physics2D.BoxCast(currentPos + (Vector2.down * scaleFactor / 2), new Vector2(scaleFactor, scaleFactor / 2), 0, Vector2.zero, 10f, this.rayCastHitLayer);

        if (leftHit.collider != null && leftHit.collider.gameObject == this.target ||
            rightHit.collider != null && rightHit.collider.gameObject == this.target ||
            upHit.collider != null && upHit.collider.gameObject == this.target ||
            downHit.collider != null && downHit.collider.gameObject == this.target)
        {
            return true;
        }


        return false;
    }

    void SetNextMovementPoint()
    {
        Vector3 currentPos = this.SnapVectorToGrid(this.gameObject.transform.position);

        List<Vector3> possibleMovementPositions = new List<Vector3>();

        if (!Physics2D.Raycast(currentPos, Vector2.left, 1f, this.rayCastHitLayer))
            possibleMovementPositions.Add(currentPos + Vector3.left);

        if (!Physics2D.Raycast(currentPos, Vector2.right, 1f, this.rayCastHitLayer))
            possibleMovementPositions.Add(currentPos + Vector3.right);

        if (!Physics2D.Raycast(currentPos, Vector2.up, 1f, this.rayCastHitLayer))
            possibleMovementPositions.Add(currentPos + Vector3.up);

        if (!Physics2D.Raycast(currentPos, Vector2.down, 1f, this.rayCastHitLayer))
            possibleMovementPositions.Add(currentPos + Vector3.down);

        for (int i = possibleMovementPositions.Count -1; i >= 0; i--)
        {
            if (this.IsInLastMovementPoints(possibleMovementPositions[i]))
                possibleMovementPositions.Remove(possibleMovementPositions[i]);
        }

        if (possibleMovementPositions.Count > 0)
        {
            float lastDist = Mathf.Infinity;
            Vector3 nearestPoint = this.SnapVectorToGrid(this.gameObject.transform.position);

            for(int i = 0; i < possibleMovementPositions.Count; i++)
            {
                float dist = Vector3.Distance(this.SnapVectorToGrid(this.target.transform.position), possibleMovementPositions[i]);
                if (dist < lastDist)
                {
                    lastDist = dist;
                    nearestPoint = possibleMovementPositions[i];
                }
            }


            this.nextMovementPoint = nearestPoint;
            this.RegistrateMovementPoint(this.nextMovementPoint);
            Debug.Log("Set new MovementPoint");
        }
        else
        {
            this.ResetMovementPointsList();
        }


    }

    bool IsInLastMovementPoints(Vector3 point)
    {
        for(int i = 0; i < this.lastMovementPoints.Length; i++)
        {
            if (this.lastMovementPoints[i] == point)
                return true;
        }
        return false;
    }

    void RegistrateMovementPoint(Vector3 point)
    {
        Vector3 newPos = point;
        for (int i = 0; i < this.lastMovementPoints.Length; i++)
        {
            Vector3 temp = this.lastMovementPoints[i];
            this.lastMovementPoints[i] = newPos;
            newPos = temp;
        }
    }

    public void ResetMovementPointsList()
    {
        this.lastMovementPoints = new Vector3[this.saveMovementPointsCounter];
    }

    Vector3 SnapVectorToGrid(Vector3 target)
    {
        Vector2 pos = target;

        float roundetX = Mathf.Round(pos.x);
        float roundetY = Mathf.Round(pos.y);

        return new Vector3(roundetX, roundetY, 0);
    }
}
