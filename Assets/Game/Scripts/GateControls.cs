using UnityEngine;

public class GateControls : MonoBehaviour
{
    public void Lower()
    {
        while (this.transform.localPosition.y >= -0.6f)
        {
            this.transform.localPosition -= new Vector3(0, 0.5f * Time.deltaTime, 0);
        }
        return;
    }
}
