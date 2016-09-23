using UnityEngine;
using UnityEngine.UI;
public class Panel : MonoBehaviour
{
    public Text Label;
    public Text State;
    public Text Path;
    public void Set(string label, string state, string path)
    {
        Label.text = label;
        State.text = state;
        Path.text = path;
    }
}
