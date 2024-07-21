
using Biology.G1;
using Managers;
using UnityEngine;

namespace Biology.Microscope
{
    public class MicroscopeSampleChanger : MonoBehaviour
    {
        
        [SerializeField] private Transform sample1Position;
        [SerializeField] private Transform sample2Position;
        [SerializeField] private Transform sample3Position;
        
        [SerializeField] private GameObject sample1;
        [SerializeField] private GameObject sample2;
        [SerializeField] private GameObject sample3;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.GetComponent<G1Sample>() != null && other.GetComponent<G1Sample>().SampleID == 1
                                                                 && BiologyStepManager.Instance.Counter.Value == 7)
            {
                BiologyStepManager.Instance.UpdateCounter();
                sample1.transform.position = sample3Position.position;
                sample2.transform.position = sample1Position.position;
                sample3.transform.position = sample2Position.position;
            }
            
            if (other.transform.GetComponent<G1Sample>() != null && other.GetComponent<G1Sample>().SampleID == 2
                                                                 && BiologyStepManager.Instance.Counter.Value == 11)
            {
                BiologyStepManager.Instance.UpdateCounter();
                sample2.transform.position = sample3Position.position;
                sample3.transform.position = sample1Position.position;
                sample1.transform.position = sample2Position.position;
            }
            
            if (other.transform.GetComponent<G1Sample>() != null && other.GetComponent<G1Sample>().SampleID == 3
                                                                 && BiologyStepManager.Instance.Counter.Value == 15)
            {
                BiologyStepManager.Instance.UpdateCounter();
                sample3.transform.position = sample1Position.position;
            }
        }
        
    }
}
