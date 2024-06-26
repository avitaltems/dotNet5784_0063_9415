﻿namespace DalTest;
using DalApi;
using DO;
using System.Security.Cryptography;
using System.Xml.Linq;

/// <summary>
/// Provides methods for initializing data in the system.
/// </summary>
public static class Initialization
{
    private static IDal? s_dal;
    private static RandomGenerator r = new RandomGenerator();

    //initialization values for all the lists
    //public static void DO(IDal dal)//stage 2

    /// <summary>
    /// Initializes data for engineers, tasks, and dependencies.
    /// </summary>
    public static void Do() //stage 4
    {
        //s_dal = dal ?? throw new NullReferenceException("DAL object can not be null!"); //stege 2
        s_dal = Factory.Get; //stage 4
        CreateEngineers();
        CreateTasks();
        CreateDependency();
    }

    /// <summary>
    /// Creates engineers with random data and adds them to the system.
    /// </summary>
    public static void CreateEngineers()
    {
        string[] names = new string[50];
        int _id;
        double _cost;
        string _email;
        for (int i = 0; i < names.Length; i++)
        {
            names[i] = r.GenerateEngineerName();
        }
        foreach (var _name in names)
        {
            if (s_dal!.Engineer != null)
            {
                do
                    _id = r.GenerateEngineerID();
                while (s_dal!.Engineer.Read(_id) != null);
            }
            else
                _id = r.GenerateEngineerID();
            _email = _name.Split(" ")[0] + "@exapmle.com";
            _cost = r.GenerateEngineerCost();
            EngineerExperience _level = (EngineerExperience)r.GenerateEngineerLevel();
            Engineer newEngineer = new(_id, _name, _email, _level, _cost);
            s_dal!.Engineer!.Create(newEngineer);
        }
    }

    /// <summary>
    /// Creates tasks with random data and adds them to the system.
    /// </summary>
    public static void CreateTasks()
    {
        string[] descriptions = r.GenerateTaskDescription();
        string[] aliases = r.GenerateTaskAlias();
        double[] weeks = r.GenerateSchedual();
        EngineerExperience _complexityLevel;
        string _description, _alias;
        DateTime _crearedAt, _forecastDate;
        int _engineerId;
        for (int i = 0; i < descriptions.Length; i++)
        {
            _description = descriptions[i];
            _alias = aliases[i];
            _crearedAt = DateTime.Now;
            _forecastDate = _crearedAt.AddDays(weeks[i] * 7);
            _complexityLevel = (EngineerExperience)r.GenerateEngineerLevel();
            _engineerId = r.EngineerForTask(s_dal!.Engineer!.ReadAll().ToList(),_complexityLevel, s_dal!.Task.ReadAll().ToList());
            Task newTask = new(0, _alias, _description, false, _crearedAt, _engineerId, _complexityLevel);
            s_dal!.Task!.Create(newTask);
        }

    }

    /// <summary>
    /// Creates dependencies between tasks and adds them to the system.
    /// </summary>
    public static void CreateDependency()
    {
        List<Task?> list = s_dal!.Task.ReadAll().ToList();
        for (int i = 0; i < 10; i++)
        {
            int _dependentId = r.GenerateDependentTask(list);
            int _dependsOnId = r.GenerateDepensOnTask(list, _dependentId);
            Dependency newDependency = new(0, _dependentId, _dependsOnId);
            s_dal!.Dependency.Create(newDependency);
        }
    }

    

}
