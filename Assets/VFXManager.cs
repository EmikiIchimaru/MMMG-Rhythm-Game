using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : Singleton<VFXManager>
{
    [SerializeField] private ParticleSystem hitVFX;
    public void HitVFX(Vector3 position)
    {
        Color newColor = Color.HSVToRGB(Random.Range(0,360f)/360, 1, 1);
        ParticleSystem vfxPS = Instantiate(hitVFX, position, Quaternion.identity);
        //vfxPS.transform.localScale = new Vector3(1f, 1f, 1f);
        var mainModule = vfxPS.main;
        mainModule.startColor = newColor;
    }
}
