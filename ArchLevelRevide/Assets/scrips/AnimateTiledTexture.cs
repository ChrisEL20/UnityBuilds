using UnityEngine;
using System.Collections;

class AnimateTiledTexture : MonoBehaviour
{
    public int columns;
    public int rows;
    public float framesPerSecond;
    public int materialIndex;
    public bool animateMainTexture;
    public bool animateNormalTexture;

    public AnimateTiledTexture()
    {
        this.columns = 2;
        this.rows = 2;
        this.framesPerSecond = 10f;
        this.materialIndex = 0;
        this.animateMainTexture = true;
        this.animateNormalTexture = false;
    }

    //the current frame to display
    private int index = 0;
    private Renderer rendererComp;

    void Start()
    {
        this.rendererComp = this.gameObject.GetComponent(typeof(Renderer)) as Renderer;
        StartCoroutine(updateTiling());

        //set the tile size of the texture (in UV units), based on the rows and columns
        Vector2 size = new Vector2(1f / this.columns, 1f / this.rows);
        if (this.animateMainTexture == true)
            this.rendererComp.materials[this.materialIndex].SetTextureScale("_MainTex", size);
        if (this.animateNormalTexture == true)
            this.rendererComp.materials[this.materialIndex].SetTextureScale("_BumpMap", size);
    }

    private IEnumerator updateTiling()
    {
        while (true)
        {
            //move to the next index
            this.index++;
            if (this.index >= this.rows * this.columns)
                this.index = 0;

            //split into x and y indexes
            Vector2 offset = new Vector2((float)this.index / this.columns - (this.index / this.columns), (this.index / this.columns) / (float)this.rows);

            if (this.animateMainTexture == true)
                this.rendererComp.materials[this.materialIndex].SetTextureOffset("_MainTex", offset);
            if (this.animateNormalTexture == true)
                this.rendererComp.materials[this.materialIndex].SetTextureOffset("_BumpMap", offset);

            yield return new WaitForSeconds(1f / this.framesPerSecond);
        }

    }
}