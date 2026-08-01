using System;

public class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, string description, int points)
        : base(name, description, points)
    {
        _isComplete = false;
    }

    // Used when loading from file
    public SimpleGoal(string name, string description, int points, bool completed)
        : base(name, description, points)
    {
        _isComplete = completed;
    }

    public override int RecordEvent()
    {
        if (!_isComplete)
        {
            _isComplete = true;
            return _points;
        }

        return 0;
    }

    public override bool IsComplete()
    {
        return _isComplete;
    }

    public override string GetDetailsString()
    {
        string box = _isComplete ? "[X]" : "[ ]";

        return $"{box} {_name} ({_description})";
    }

    public override string GetStringRepresentation()
    {
        return $"SimpleGoal|{_name}|{_description}|{_points}|{_isComplete}";
    }
}