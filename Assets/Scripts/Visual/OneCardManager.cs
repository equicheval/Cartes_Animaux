using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// holds the refs to all the Text, Images on the card
public class OneCardManager : MonoBehaviour {

    public CardAsset cardAsset;
    public OneCardManager PreviewManager;
    [Header("Text Component References")]
    public Text TextNom;


    public Image ImageRessourceDesert;
    public Image ImageRessourceOcean;

    public Text TextRessourceDesert;
	public Text TextRessourceOcean;


    public Text TextDescription;
    public Text VieText;
    public Text AttaqueText;
	public Text PireEnnemiText;
	public Text MeilleurAllieText;
	public Text TypeText;
	public Image TypeImage;
    [Header ("GameObject References")]
    public GameObject HealthIcon;
    public GameObject AttackIcon;
    [Header("Image References")]
    public Image CardTopRibbonImage;
    public Image CardGraphicImage;
    public Image CardBodyImage;
	public Sprite BackgroundDesert;
	public Sprite BackgroundOcean;
	public Image CardBodyLegendaireImage;
	public Image CardLegendaireIconImage;
    public Image CardFaceGlowImage;
    public Image CardBackGlowImage;

    void Awake()
    {
        if (cardAsset != null)
            ReadCardFromAsset();
    }

    private bool canBePlayedNow = false;
    public bool CanBePlayedNow
    {
        get
        {
            return canBePlayedNow;
        }

        set
        {
            canBePlayedNow = value;

            CardFaceGlowImage.enabled = value;
        }
    }

    public void ReadCardFromAsset()
    {
        // Actions pour chaque carte
        // 1) Applique le style de la carte
		if (cardAsset.Rarete.ToString() == "Legendaire") {
			CardBodyImage.gameObject.GetComponent<Image> ().enabled = false;
			CardBodyLegendaireImage.gameObject.GetComponent<Image> ().enabled = true;
			CardLegendaireIconImage.gameObject.GetComponent<Image> ().enabled = true;
		} else if (cardAsset.Rarete.ToString() == "Classique") {
			if (cardAsset.Set.ToString () == "Desert") {
				CardBodyImage.gameObject.GetComponent<Image> ().enabled = true;
				CardBodyImage.gameObject.GetComponent<Image> ().sprite = BackgroundDesert;
				CardBodyLegendaireImage.gameObject.GetComponent<Image> ().enabled = false;
				CardLegendaireIconImage.gameObject.GetComponent<Image> ().enabled = false;
			} else if (cardAsset.Set.ToString () == "Ocean") {
				CardBodyImage.gameObject.GetComponent<Image> ().enabled = true;
				CardBodyImage.gameObject.GetComponent<Image> ().sprite = BackgroundOcean;
				CardBodyLegendaireImage.gameObject.GetComponent<Image> ().enabled = false;
				CardLegendaireIconImage.gameObject.GetComponent<Image> ().enabled = false;
			}
		}
        // 2) Ajoute Nom de la carte
        TextNom.text = cardAsset.name;
        // 3) Ajoute Couts en Ressources
        if (cardAsset.couteRessourcesDesert == true)
        {
            ImageRessourceDesert.gameObject.GetComponent<Image>().enabled = true;
            TextRessourceDesert.gameObject.GetComponent<Text>().enabled = true;
            TextRessourceDesert.text = cardAsset.CoutEnRessourceDesert.ToString();
        } else {
            ImageRessourceDesert.gameObject.GetComponent<Image>().enabled = false;
            TextRessourceDesert.gameObject.GetComponent<Text>().enabled = false;
        }

        if (cardAsset.couteRessourcesOcean == true)
        {
            TextRessourceOcean.text = cardAsset.CoutEnRessourceOcean.ToString();
            TextRessourceOcean.gameObject.GetComponent<Text>().enabled = true;
            ImageRessourceOcean.gameObject.GetComponent<Image>().enabled = true;
        } else {
            ImageRessourceOcean.gameObject.GetComponent<Image>().enabled = false;
            TextRessourceOcean.gameObject.GetComponent<Text>().enabled = false;
        }
        // 4) Ajoute Description
        TextDescription.text = cardAsset.Description;
        // 5) Change l'image de la carte
		CardGraphicImage.sprite = cardAsset.ImageCarte;



		if (cardAsset.VieMaximum != 0) // c'est un animal
        {
            // Ajoute attaque et vie
			AttaqueText.text = cardAsset.Attaque.ToString();
			VieText.text = cardAsset.VieMaximum.ToString();
			// Ajoute le Type de l'animal s'il existe
			TypeText.text = cardAsset.TypeText;
			TypeImage.sprite = cardAsset.TypeImage;
			PireEnnemiText.text = cardAsset.PireEnnemi;
			MeilleurAllieText.text = cardAsset.MeilleurAllie;
        }

        if (PreviewManager != null)
        {
            // this is a card and not a preview
            // Preview GameObject will have OneCardManager as well, but PreviewManager should be null there
            PreviewManager.cardAsset = cardAsset;
            PreviewManager.ReadCardFromAsset();
        }
    }
}
