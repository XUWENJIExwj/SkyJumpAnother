using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIParticleProperty : UIProperty
{
    [SerializeField] ParticleSystem particle = null;
    [SerializeField] float particleSizeDefault = 1.0f;
    [SerializeField] private float particleSizeParentCoefficient = 0.0f;
    [SerializeField] float particleSpeedDefault = 1.0f;
    [SerializeField] private float particleSpeedParentCoefficient = 0.0f;

    protected override void Awake()
    {
        base.Awake();
        particleSizeParentCoefficient = particleSizeDefault / ScreenInfo.screenDefaultSize.x;
        particleSpeedParentCoefficient = particleSpeedDefault / ScreenInfo.screenDefaultSize.x;
    }

    // Widthによる拡大縮小
    public override void SetUISizeMatchX()
    {
        base.SetUISizeMatchX();

        SetParticleStartSize();
        SetParticleSimulationSpeed();
    }

    // Heightによる拡大縮小
    public override void SetUISizeMatchY()
    {
        base.SetUISizeMatchY();

        SetParticleStartSize();
        SetParticleSimulationSpeed();
    }

    private void SetParticleStartSize()
    {
        var main = particle.main;
        main.startSize = particleSizeParentCoefficient * Screen.width;
    }

    private void SetParticleSimulationSpeed()
    {
        var main = particle.main;
        main.simulationSpeed = particleSpeedParentCoefficient * Screen.width;
    }
}
