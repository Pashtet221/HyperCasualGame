using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkin : MonoBehaviour
{
    [SerializeField] GameObject[] skins ;


    private void Start()
    {
        ChangePlayerSkin();
    }

     private void ChangePlayerSkin () {
      Character character = GameDataManager.GetSelectedCharacter () ;
      if (character.image != null) {
      	 /*[old code]
         playerImage.sprite = character.image ;
         */

         // Get selected character's index:
         int selectedSkin = GameDataManager.GetSelectedCharacterIndex () ;

         // show selected skin's gameobject:
         skins [ selectedSkin ].SetActive (true) ;

         // hide other skins (except selectedSkin) :
         for (int i = 0; i < skins.Length; i++)
            if (i != selectedSkin)
               skins [ i ].SetActive (false) ;
      }
   }
}
