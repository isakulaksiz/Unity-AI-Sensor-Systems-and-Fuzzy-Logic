using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AForge;
using AForge.Fuzzy;
using System;

public class FuzzyBot : MonoBehaviour
{
    public Transform player;

    [Header("Distance fuzzy sets")]
    public AnimationCurve nearAC;
    public AnimationCurve medAC;
    public AnimationCurve farAC;
        
    [Header("Speed fuzzy sets")]
    public AnimationCurve slowAC;
    public AnimationCurve mediumAC;
    public AnimationCurve fastAC;

    //FuzzySets
    // distance, speed : near med far, slow, medium, fast

    //Linguistic variables: distance, speed,

    //Database to add rules
    //inference system for defuzzufication.

    public float speed, distance;
    FuzzySet fsNear, fsMed, fsFar;
    FuzzySet fsSlow, fsMedium, fsFast;
    LinguisticVariable lvDistance, lvSpeed;
    Database database;
    InferenceSystem infSystem;

    // Start is called before the first frame update
    void Start()
    {
        Initializate();
    }

    private void Initializate()
    {
        SetDistanceFuzzySets();
        SetSpeedFuzzySets();
        AddRulesTODataBase();
    }



    private void SetDistanceFuzzySets()
    {
        fsNear = new FuzzySet("Near", new TrapezoidalFunction(nearAC.keys[0].time, nearAC.keys[1].time, TrapezoidalFunction.EdgeType.Right));
        fsMed = new FuzzySet("Med", new TrapezoidalFunction(medAC.keys[0].time, medAC.keys[1].time, medAC.keys[2].time, medAC.keys[3].time));
        fsFar = new FuzzySet("Far", new TrapezoidalFunction(farAC.keys[0].time, farAC.keys[1].time, TrapezoidalFunction.EdgeType.Left));

        lvDistance = new LinguisticVariable("Distance", 0, 100);
        lvDistance.AddLabel(fsNear);
        lvDistance.AddLabel(fsMed);
        lvDistance.AddLabel(fsFar);

    }

    private void SetSpeedFuzzySets()
    {
        fsSlow = new FuzzySet("Slow", new TrapezoidalFunction(slowAC.keys[0].time, slowAC.keys[1].time, TrapezoidalFunction.EdgeType.Right));
        fsMedium = new FuzzySet("Medium", new TrapezoidalFunction(mediumAC.keys[0].time, mediumAC.keys[1].time, mediumAC.keys[2].time, mediumAC.keys[3].time));
        fsFast = new FuzzySet("Fast", new TrapezoidalFunction(fastAC.keys[0].time, fastAC.keys[1].time, TrapezoidalFunction.EdgeType.Left));
        lvSpeed = new LinguisticVariable("Speed", 0, 120);

        lvSpeed.AddLabel(fsSlow);
        lvSpeed.AddLabel(fsMedium);
        lvSpeed.AddLabel(fsFast);
    }


    private void AddRulesTODataBase()
    {
        database = new Database();
        database.AddVariable(lvDistance);
        database.AddVariable(lvSpeed);
        SetRules();
    }

    private void SetRules()
    {
        infSystem = new InferenceSystem(database, new CentroidDefuzzifier(120));
        infSystem.NewRule("Rule 1", "IF Distance IS Near THEN Speed IS Slow");
        infSystem.NewRule("Rule 2", "IF Distance IS Med THEN Speed IS Medium");
        infSystem.NewRule("Rule 3", "IF Distance IS Far THEN Speed IS Fast");
    }



    // Update is called once per frame
    void Update()
    {
        Run();
    }

    private void Run()
    {
        distance = Vector3.Distance(player.position, transform.position);
        Vector3 dir = (player.position - transform.position).normalized;
        infSystem.SetInput("Distance", distance);
        speed = infSystem.Evaluate("Speed");
        //Debug.Log(speed);  
        

    }
   

}
