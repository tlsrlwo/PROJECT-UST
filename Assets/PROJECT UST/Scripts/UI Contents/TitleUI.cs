using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UST
{
    public class TitleUI : UIBase //�Ʊ� ���� ��ũ��Ʈ�� ��ӹ��� 
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
