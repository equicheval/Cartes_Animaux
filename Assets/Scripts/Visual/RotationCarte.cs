using UnityEngine;
using System.Collections;

// Script attaché au GameObject d'une carte pour afficher une rotation correcte (avec le dos de carte)

[ExecuteInEditMode]
public class RotationCarte : MonoBehaviour {

	// GameObject parent pour la carte de face
	public RectTransform DevantDeCarte;

	// GameObject parent pour la carte de dos
	public RectTransform DosDeCarte;

	// GameObject vide placé un peu avant la carte de face, au centre de la carte
	public Transform CiblePointDeDevant;

	// Collider 3D attaché à la carte (ne fonctionne pas en 2D)
	public Collider col;

	// Si c'est vrai, affiche le dos de carte
	private bool DosAffiche = false;

	void Update() 
	{
		// Création d'un raycast qui va de la Caméra au point Cible devant la carte.
		// Si il passe à travers le collider de la carte alors on affiche le Dos de la carte

		RaycastHit[] hits;
		hits = Physics.RaycastAll (
			// point d'origine du vecteur 3D -> Caméra
			origin: Camera.main.transform.position,
			/*Direction : 
			 * signe - car Caméra sur axe des Z en -28 donc vecteur serait mal orienté
			 * Normalized : on s'en fout de la longueur, seule la direction compte : on fait donc un vecteur (0,0,1) normalisé
			 * Au final : vecteur de direction Camera -> Cible 
			 */
			direction: (-Camera.main.transform.position + CiblePointDeDevant.position).normalized,
			//DistanceMax permet de vérifier que le vecteur n'est pas infini et s'arrete dès qu'il atteint la Cible
			//.magnitude car conversion en variable float depuis un vecteur3D
			maxDistance: (-Camera.main.transform.position + CiblePointDeDevant.position).magnitude);

		// Vérification que le vecteur touche bien le Collider
		bool PasseATraversLeCollider = false;


		//Pour chaque Raycast, s'il a touché notre Collider, on change la variable
		foreach (RaycastHit h in hits) {
			if (h.collider == col)
				PasseATraversLeCollider = true;
		}

		//Vérification que les 2 variables sont égales sinon on change leur valeur
		if (PasseATraversLeCollider != DosAffiche) {
			DosAffiche = PasseATraversLeCollider;
			if (DosAffiche) {
				DevantDeCarte.gameObject.SetActive (false);
				DosDeCarte.gameObject.SetActive (true);
			} else {
				DevantDeCarte.gameObject.SetActive (true);
				DosDeCarte.gameObject.SetActive (false);
			}
		}
			
	}
}
