﻿namespace Dal;
using DalApi;
using DO;
using System.Collections.Generic;
using System.Linq;

internal class DependencyImplementation : IDependency
{
    public int Create(Dependency item)
    {
        int id = DataSource.Config.NextDependencyId;
        Dependency newDependency = item with { Id = id };
        DataSource.Dependencies.Add(newDependency);
        return newDependency.Id;
    }

    public void Delete(int id)
    {
        Dependency? newDependency = DataSource.Dependencies.FirstOrDefault(element => element.Id == id);

        if (newDependency == null)
            throw new Exception($"Dependecy with ID = {id} does not exist");
        DataSource.Dependencies.Remove(newDependency);
    }
    public Dependency? Read(int id)
    {
        return DataSource.Dependencies.FirstOrDefault(element => element.Id == id);
    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency?, bool>? filter = null) //stage 2
    {
        if (filter == null)
            return DataSource.Dependencies.Select(item => item);
        else
            return DataSource.Dependencies.Where(filter);
    }

    public void Update(Dependency item)
    {
        Dependency? dependency = DataSource.Dependencies.FirstOrDefault(element => element.Id == item.Id);
        if (dependency == null)
        {
            throw new Exception($"Dependency with ID = {item.Id} does not exist");
        }
        DataSource.Dependencies.Remove(dependency);
        DataSource.Dependencies.Add(item);
    }
}
    

