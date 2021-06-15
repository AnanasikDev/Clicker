using TMPro;
using UnityEngine;
public class ScoreManager : MonoBehaviour
{
    float _score;
    public float score 
    {
        get { return _score; }
        set 
        {
            _score = value;
            Draw();
        }
    }
    [SerializeField] TextMeshProUGUI scoreText;
    public float mps = 0; // money per second
    public float mpc = 0; // money per click

    public static ScoreManager singleton { get; private set; }
    void Awake() => singleton = this;
    void Start()
    {
        InvokeRepeating("AddScorePassive", 1, 1);
    }
    public void AddScoreClick()
    {
        score += mpc;
        Draw();
    }
    public void AddScorePassive()
    {
        score += mps;
        Draw();
    }
    char[] valuesTemps = new char[5] { 'k', 'm', 'b', 't', 's' };
    public void Draw()
    {
        string _score;
        if (score >= 1000)
        {
            int scoreFullLen = Mathf.RoundToInt(score).Length();
            int scoreLen = scoreFullLen - Mathf.RoundToInt(score / 1000).Length();
            _score = (System.Math.Round(score / Mathf.Pow(10, scoreLen), 1)).ToString() + valuesTemps[((scoreFullLen - 1) / 3) - 1];
        }
        else _score = (System.Math.Round(score, 2)).ToString();
        scoreText.text = _score;
    }
}
