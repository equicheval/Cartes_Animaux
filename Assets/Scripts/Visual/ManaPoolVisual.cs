using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ManaPoolVisual : MonoBehaviour {

    public Text ProgressTextSun;
    public Text ProgressTextOcean;

	private int availableSunRessources;
	private int availableOceanRessources;

    public int AvailableSunRessources
    {
        get{ return availableSunRessources; }

        set
        {
            ProgressTextSun.text = AvailableSunRessources.ToString();
            availableSunRessources = value;
        }
    }

    public int AvailableOceanRessources
    {
        get{ return availableOceanRessources; }

        set
        {
            ProgressTextSun.text = AvailableOceanRessources.ToString();
			availableOceanRessources = value;
        }
    }
}
