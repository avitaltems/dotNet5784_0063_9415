﻿namespace DalTest;
using DalApi;
using DO;
using System.Security.Cryptography;
using System.Xml.Linq;

public static class Initialization
{
    private static IEngineer? s_dalEngineer;
    private static ITask? s_dalTask;
    private static IDependency? s_dalDependency;
    private static RandomGenerator r = new RandomGenerator();

    public static void createEngineers()
    {
        string[] names = new string[10];
        int _id;
        string _email;
        for (int i = 0; i < names.Length; i++)
        {
            names[i] = r.GenerateEngineerName();
        }
        foreach (var _name in names)
        {
            if (s_dalEngineer != null)
            {
                do
                    _id = r.GenerateEngineerID();
                while (s_dalEngineer!.Read(_id) != null);
            }
            else
                _id = r.GenerateEngineerID();
            _email = _name.Split(" ")[0] + "@gmail.com";
            EngineerExperience _level = (EngineerExperience)r.GenerateEngineerLevel();
            Engineer newEngineer = new(_id, _name, _email, _level);
            s_dalEngineer!.Create(newEngineer);
        }
    }
    public static void createTasks()
    {
        string[] descriptions = r.GenerateTaskDescription();
        string[] aliases = r.GenerateTaskAlias();
        double[] weeks = r.GenerateSchedual();
        string _description, _alias;
        bool _mileStone;
        DateTime _crearedAt, _forecastDate, _deadLine;
        EngineerExperience _complexityLevel;
        List<Engineer> list = s_dalEngineer!.ReadAll();
        int _engineerId;
        for (int i = 0; i < descriptions.Length; i++)
        {
            _description = descriptions[i];
            _alias = aliases[i];
            _mileStone = r.GenerateMileStone();
            _crearedAt = DateTime.Now;
            _forecastDate = _crearedAt.AddDays(weeks[i] * 7);
            _deadLine = _forecastDate.AddDays(7);
            _complexityLevel = (EngineerExperience)r.GenerateEngineerLevel();
            _engineerId = r.EngineerForTask(list, _complexityLevel);
            Task newTask = new(0, _description, _crearedAt, _forecastDate, _deadLine, _engineerId, _complexityLevel, _mileStone, _alias);
            s_dalTask!.Create(newTask);
        }
    }
    public static void createDependency()
    {
        List<Task> list = s_dalTask!.ReadAll();
       int _dependentId = r.EngineerForTask(list, _complexityLevel);
        
    }

    public static void DO(IEngineer? dalEngineer)
    {
        s_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL can not be null!");
        createEngineers();
    }
}