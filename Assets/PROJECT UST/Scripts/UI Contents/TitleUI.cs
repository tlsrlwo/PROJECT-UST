using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UST
{
    public class TitleUI : UIBase //아까 만든 스크립트를 상속받음 
    {
       public void OnClickStartButton()
        {
            Main.Instance.ChangeScene(SceneType.Game); 
        }

        public void OnclickExitButton()
        {
            Debug.Log("Clicked Exit Button");

        }
    }
}
