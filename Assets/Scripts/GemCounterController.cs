using UnityEngine;
using UnityEngine.UI;

public class GemCounterController : MonoBehaviour
{
    private int _gemsCount;
    private Text _text;

    // Start is called before the first frame update
    void Start()
    {
        _gemsCount = 0;
        _text = GetComponentInChildren<Text>();
        _text.text = _gemsCount.ToString();
    }

    public void Add(int amount = 1)
    {
        if (amount > 0 && amount < 20)
        {
            _gemsCount += amount;
            _text.text = _gemsCount.ToString();
        }

    }

}
