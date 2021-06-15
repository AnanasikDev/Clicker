using UnityEngine;
public class Button : MonoBehaviour
{
    public void onButtonClick()
    {
        ScoreManager.singleton.AddScoreClick();
    }
}
