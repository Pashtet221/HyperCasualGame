using UnityEngine;
using EasyUI.PickerWheelUI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OnPickerWheel : MonoBehaviour {
   [SerializeField] private Button uiSpinButton ;
   [SerializeField] private GameObject mainMenuButton ;
   [SerializeField] private Text uiSpinButtonText ;

   [SerializeField] private PickerWheel pickerWheel ;


   private void Start () {
      uiSpinButton.onClick.AddListener (() => {

         uiSpinButton.interactable = false;
         mainMenuButton.SetActive(false);
         uiSpinButtonText.text = "Spinning" ;

         pickerWheel.OnSpinEnd (wheelPiece => {
            Debug.Log (
               @" <b>Index:</b> " + wheelPiece.Index + "           <b>Label:</b> " + wheelPiece.Label
               + "\n <b>Amount:</b> " + wheelPiece.Amount + "      <b>Chance:</b> " + wheelPiece.Chance + "%"
            ) ;
            GameDataManager.AddCoins (wheelPiece.Amount);
            GameSharedUI.Instance.UpdateCoinsUIText();

            uiSpinButton.interactable = true;
            mainMenuButton.SetActive(true);
            uiSpinButtonText.text = "" ;
         }) ;

         pickerWheel.Spin () ;

      }) ;

   }


   public void LoadMainMenu()
   {
      SceneManager.LoadScene("main");
   }

}
