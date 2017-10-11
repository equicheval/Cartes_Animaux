using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OneCreatureManager : MonoBehaviour 
{
    public CardAsset cardAsset;
    public OneCardManager PreviewManager;
    [Header("Text Component References")]
    public Text HealthText;
    public Text AttackText;
    [Header("Image References")]
    public Image CreatureGraphicImage;
    public Image CreatureGlowImage;
	public Image CreatureDSImage;
	public Image CreatureTensionImage;
	public Image Mask;

    void Awake()
    {
        if (cardAsset != null)
            ReadCreatureFromAsset();
    }

    private bool canAttackNow = false;
    public bool CanAttackNow
    {
        get
        {
            return canAttackNow;
        }

        set
        {
            canAttackNow = value;

            CreatureGlowImage.enabled = value;
        }
    }

    public void ReadCreatureFromAsset()
    {
        CreatureGraphicImage.sprite = cardAsset.ImageCarte;

        AttackText.text = cardAsset.Attaque.ToString();
        HealthText.text = cardAsset.VieMaximum.ToString();

        if (PreviewManager != null)
        {
            PreviewManager.cardAsset = cardAsset;
            PreviewManager.ReadCardFromAsset();
        }

		if (cardAsset.DernierSouffle == true) {
			CreatureDSImage.gameObject.GetComponent<Image> ().enabled = true;
		} else {
			CreatureDSImage.gameObject.GetComponent<Image> ().enabled = false;
		}

		if (cardAsset.Tension == true) {
			CreatureTensionImage.gameObject.GetComponent<Image> ().enabled = true;
		} else {
			CreatureTensionImage.gameObject.GetComponent<Image> ().enabled = false;
		}
    }	

    public void TakeDamage(int amount, int healthAfter)
    {
        if (amount > 0)
        {
            DamageEffect.CreateDamageEffect(transform.position, amount);
            HealthText.text = healthAfter.ToString();
        }
    }
}
