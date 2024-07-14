using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class DeathParticleScript : MonoBehaviour
{
    private float returnToPoolDelay;
    private void Awake()
    {
        returnToPoolDelay = 5f;
    }
    private void OnEnable()
    {
        HandleDeathParticlePoolReturn(gameObject.tag);
    }
    private async void HandleDeathParticlePoolReturn(string x)
    {
        await Task.Delay(1000);
        if(this.gameObject!=null)
        {
            ObjectPooler.Instance.SendToQueue(this.gameObject, x);
        }
        
    }
}
