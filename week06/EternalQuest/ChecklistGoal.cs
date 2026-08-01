using System;

public class ChecklistGoal : Goal
{
    private int _target;
    private int _completed;
    private int _bonus;

    public ChecklistGoal(
        string name,
        string description,
        int points,
        int target,
        int bonus)
        : base(name, description, points)
    {
        _target = target;
        _bonus = bonus;
        _completed = 0;
    }

    // Used when loading
    public ChecklistGoal(
        string name,
        string description,
        int points,
        int target,
        int completed,
        int bonus)
        : base(name, description, points)
    {
        _target = target;
        _completed = completed;
        _bonus = bonus;
    }

    public override int RecordEvent()
    {
        if (_completed >= _target)
        {
            return 0;
        }

        _completed++;

        if (_completed == _target)
        {
            return _points + _bonus;
        }

        return _points;
    }

    public override bool IsComplete()
    {
        return _completed >= _target;
    }

    public override string GetDetailsString()
    {
        string box = IsComplete() ? "[X]" : "[ ]";

        return $"{box} {_name} ({_description}) -- Completed {_completed}/{_target}";
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal|{_name}|{_description}|{_points}|{_target}|{_completed}|{_bonus}";
    }
}