using UnityEngine;
using TMPro;
public class Upgrade : MonoBehaviour
{
    public float upgradeValue;
    public float multiplyValue;

    public float mpcCost;
    public float mpsCost;

    public short index = 1;
    UpgradeType upgradeType;
    public TextMeshProUGUI mpcCostText;
    public TextMeshProUGUI mpsCostText;
    [SerializeField] string MultTemplate;
    [SerializeField] string AddTemplate;

    char[] valuesTemps = new char[5] { 'k', 'm', 'b', 't', 's'};
    string[] SetValuesAsTemplate()
    {
        string _mpcCost;
        string _mpsCost;
        if (mpcCost >= 1000)
        {
            int mpcFullCostLen = Mathf.RoundToInt(mpcCost).Length();
            int mpcCostLen = mpcFullCostLen - Mathf.RoundToInt(mpcCost / 1000).Length();
            _mpcCost = (System.Math.Round(mpcCost / Mathf.Pow(10, mpcCostLen), 1)).ToString() + valuesTemps[((mpcFullCostLen-1)/3)-1];
        }
        else _mpcCost = mpcCost.ToString();
        if (mpsCost >= 1000)
        {
            int mpsFullCostLen = Mathf.RoundToInt(mpsCost).Length();
            int mpsCostLen = mpsFullCostLen - Mathf.RoundToInt(mpsCost / 1000).Length();
            _mpsCost = (System.Math.Round(mpsCost / Mathf.Pow(10, mpsCostLen), 1)).ToString() + valuesTemps[((mpsFullCostLen - 1) / 3) - 1];
        }
        else _mpsCost = mpsCost.ToString();
        return new string[2] { _mpcCost, _mpsCost };
    }
    private void Start()
    {
        SetUpgrade(upgradeType);
        Draw();
    }
    public void SetUpgrade(UpgradeType type)
    {
        int i;
        switch (type)
        {
            case (UpgradeType.mps):
                i = Random.Range(0, 5);
                if (i > 1)
                {
                    upgradeValue = Random.Range(0.15f * index, 3.5f * index);
                    multiplyValue = 1;
                    mpsCost += upgradeValue * index * 3.5f;
                }
                else
                {
                    multiplyValue = Random.Range(0.1f * index, 3 * index);
                    upgradeValue = 0;
                    mpsCost *= multiplyValue * index * 1.25f;
                }
                mpsCost = Mathf.Round(mpsCost);
                break;

            case (UpgradeType.mpc):
                i = Random.Range(0, 2);
                if (i == 0)
                {
                    upgradeValue = Random.Range(0.2f * index, 4 * index);
                    multiplyValue = 1;
                    mpcCost += upgradeValue * index * 3;
                }
                else
                {
                    multiplyValue = Random.Range(0.1f * index, 3 * index);
                    upgradeValue = 0;
                    mpcCost *= multiplyValue * index * 0.7f;
                }
                mpcCost = Mathf.Round(mpcCost);
                break;
        }
        
        index++;
    }
    public void GetUpgradeMPC()
    {
        if (ScoreManager.singleton.score < mpcCost) return;
        ScoreManager.singleton.score -= mpcCost;
        SetUpgrade(UpgradeType.mpc);
        ScoreManager.singleton.mpc += upgradeValue;
        ScoreManager.singleton.mpc *= multiplyValue;
    }
    public void GetUpgradeMPS()
    {
        if (ScoreManager.singleton.score < mpsCost) return;
        ScoreManager.singleton.score -= mpsCost;
        SetUpgrade(UpgradeType.mps);
        ScoreManager.singleton.mps += upgradeValue;
        ScoreManager.singleton.mps *= multiplyValue;
    }
    public void Draw()
    {
        if (multiplyValue > 1)
        {
            mpcCostText.text = string.Format(MultTemplate, multiplyValue, SetValuesAsTemplate()[0]);
            mpsCostText.text = string.Format(AddTemplate, upgradeValue, SetValuesAsTemplate()[1]);
        }
        else
        {
            mpcCostText.text = string.Format(AddTemplate, upgradeValue, SetValuesAsTemplate()[0]);
            mpsCostText.text = string.Format(AddTemplate, upgradeValue, SetValuesAsTemplate()[1]);
        }
    }
    public enum UpgradeType
    {
        mpc,
        mps
    }
}
public static class Int
{ 
    public static int Length(this int a)
    {
        return a.ToString().Length;
    }
}