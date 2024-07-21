
using Biology.G1;
using Managers;
using UnityEngine;

namespace Biology.Microscope
{
    public class MicroscopeSampleOff : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.GetComponent<G1Sample>() != null && other.GetComponent<G1Sample>().SampleID == 1
                                                                 && BiologyStepManager.Instance.Counter.Value == 6)
            {
                other.transform.GetComponent<G1Sample>().SampleOff();
                BiologyStepManager.Instance.UpdateCounter();
                other.transform.SetParent(null);
            }
            
            if (other.transform.GetComponent<G1Sample>() != null && other.GetComponent<G1Sample>().SampleID == 2
                                                                 && BiologyStepManager.Instance.Counter.Value == 10)
            {
                other.transform.GetComponent<G1Sample>().SampleOff();
                BiologyStepManager.Instance.UpdateCounter();
                other.transform.SetParent(null);
            }
            
            if (other.transform.GetComponent<G1Sample>() != null && other.GetComponent<G1Sample>().SampleID == 3
                                                                 && BiologyStepManager.Instance.Counter.Value == 14)
            {
                other.transform.GetComponent<G1Sample>().SampleOff();
                BiologyStepManager.Instance.UpdateCounter();
                other.transform.SetParent(null);
            }
        }
        
    }
}
