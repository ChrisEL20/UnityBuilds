using UnityEngine;
using System.Collections;

public class MaterialSwitchButton : MonoBehaviour {
    [System.Serializable]
    public class Target{
        public GameObject obj;
        public int index;

        public Target()
        {
            this.obj = null;
            this.index = 0;
        }
         
    }

    public Target[] targets;
    public Material material;
    public ObjectMaterialSwitch objMatSwitch;

    public MaterialSwitchButton()
    {
        this.material = null;
        this.objMatSwitch = null;
    }
    

    public void SwitchMaterial()
    {   
        if (this.objMatSwitch != null)
        {
            foreach (Target tar in this.targets)
            {
                this.objMatSwitch.HideWorldUI();
                var meshRender = tar.obj.GetComponent(typeof(MeshRenderer)) as MeshRenderer;
                if (meshRender != null && this.material != null)
                {
                    Material[] mats = new Material[meshRender.materials.Length];
                    for (int i = 0; i < mats.Length; i++)
                    {
                        mats[i] = meshRender.materials[i];
                        if (i == tar.index)
                            mats[tar.index] = this.material;
                    }
                    meshRender.materials = mats;
                    //meshRender.material = this.material;
                }
            }
            
        }
    }
}
