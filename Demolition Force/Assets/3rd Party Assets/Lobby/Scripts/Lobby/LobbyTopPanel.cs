using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Prototype.NetworkLobby
{
    public class LobbyTopPanel : MonoBehaviour
    {
        public bool isInGame = false;

        protected bool isDisplayed = true;
        protected Image panelImage;

        void Start()
        {
            panelImage = GetComponent<Image>();
        }


        void Update()
        {
            if (!isInGame)
                return;

//            if (Input.GetKeyDown(KeyCode.Escape))
//            {
//                ToggleVisibility(!isDisplayed);
//            }
        }

        public void ToggleVisibility(bool visible)
        {
            isDisplayed = visible;
            foreach (Transform t in transform)
            {
                t.gameObject.SetActive(isDisplayed);
            }

            if (panelImage != null)
            {
                panelImage.enabled = isDisplayed;
            }
        }
		public void GoToMainMenu()
		{
			LoadingBarScript loadingBar = FindObjectOfType<LoadingBarScript> ();
			loadingBar.LoadLevel ("Main Menu");
			StartCoroutine (WaitToDestroyLobby());
		}
		IEnumerator WaitToDestroyLobby()
		{
			yield return new WaitForSeconds (1f);
			Destroy (LobbyManager.s_Singleton.gameObject);
		}
    }
}