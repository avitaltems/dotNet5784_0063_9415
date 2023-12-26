﻿using DO;
namespace BO;

public class Task
{
    public int Id { get; init; }
    public required string Description { get; set; }
    public required string Alias { get; set; } 
    public DateTime CreatedAtDate { get; init; }
    public Status? Status { get; set; } = null;
    public MilestoneInTask? Milestone { get; set; } = null;
    public EngineerExperience? ComplexityLevel { get; set; } = null;
    public DateTime? BaselineStartDate { get; set; } = null;
    public DateTime? StartDate { get; set; } = null;
    public DateTime? ScheduledStartDate { get; set; } = null;
    public DateTime? ForecastDate { get; set; } = null;
    public DateTime? DeadlineDate { get; set; } = null;
    public DateTime? CompleteDate { get; set; } = null;
    public string? Deliverables { get; set; } = null;
    public string? remarks { get; set; } = null;
    public Engineer? Engineer { get; set; } = null;
}
