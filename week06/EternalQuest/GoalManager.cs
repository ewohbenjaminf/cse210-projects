using System;
using System.Collections.Generic;
using System.IO;

public class GoalManager
{
    private List<Goal> _goals;
    private int _score;

    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
    }

    public void Start()
    {
        string choice = "";

        while (choice != "6")
        {
            Console.Clear();

            Console.WriteLine("====================================");
            Console.WriteLine("      Eternal Quest Program");
            Console.WriteLine("====================================");
            Console.WriteLine($"Current Score: {_score} points");
            Console.WriteLine();

            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Quit");

            Console.Write("\nSelect an option: ");
            choice = Console.ReadLine();

            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    CreateGoal();
                    break;

                case "2":
                    ListGoals();
                    break;

                case "3":
                    SaveGoals();
                    break;

                case "4":
                    LoadGoals();
                    break;

                case "5":
                    RecordEvent();
                    break;

                case "6":
                    Console.WriteLine("Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }

            if (choice != "6")
            {
                Console.WriteLine();
                Console.Write("Press Enter to continue...");
                Console.ReadLine();
            }
        }
    }

    private void CreateGoal()
    {
        Console.WriteLine("Goal Types");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");

        Console.Write("\nChoose goal type: ");
        string type = Console.ReadLine();

        Console.Write("Goal Name: ");
        string name = Console.ReadLine();

        Console.Write("Description: ");
        string description = Console.ReadLine();

        Console.Write("Points: ");
        int points = int.Parse(Console.ReadLine());

        switch (type)
        {
            case "1":
                _goals.Add(new SimpleGoal(name, description, points));
                break;

            case "2":
                _goals.Add(new EternalGoal(name, description, points));
                break;

            case "3":
                Console.Write("Target Count: ");
                int target = int.Parse(Console.ReadLine());

                Console.Write("Bonus Points: ");
                int bonus = int.Parse(Console.ReadLine());

                _goals.Add(new ChecklistGoal(name, description, points, target, bonus));
                break;

            default:
                Console.WriteLine("Invalid goal type.");
                break;
        }

        Console.WriteLine("\nGoal created successfully.");
    }

    private void ListGoals()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals created yet.");
            return;
        }

        Console.WriteLine("Your Goals:");

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }
    }

    private void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("There are no goals to record.");
            return;
        }

        Console.WriteLine("Select a Goal:");

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetName()}");
        }

        Console.Write("Goal Number: ");
        int choice = int.Parse(Console.ReadLine());

        if (choice < 1 || choice > _goals.Count)
        {
            Console.WriteLine("Invalid selection.");
            return;
        }

        Goal goal = _goals[choice - 1];

        int earned = goal.RecordEvent();

        _score += earned;

        Console.WriteLine($"Congratulations! You earned {earned} points.");
        Console.WriteLine($"Current Score: {_score}");
    }

    private void SaveGoals()
    {
        Console.Write("Filename: ");
        string filename = Console.ReadLine();

        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine(_score);

            foreach (Goal goal in _goals)
            {
                writer.WriteLine(goal.GetStringRepresentation());
            }
        }

        Console.WriteLine("Goals saved successfully.");
    }

    private void LoadGoals()
    {
        Console.Write("Filename: ");
        string filename = Console.ReadLine();

        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            return;
        }

        _goals.Clear();

        string[] lines = File.ReadAllLines(filename);

        _score = int.Parse(lines[0]);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split('|');

            switch (parts[0])
            {
                case "SimpleGoal":

                    _goals.Add(
                        new SimpleGoal(
                            parts[1],
                            parts[2],
                            int.Parse(parts[3]),
                            bool.Parse(parts[4])
                        ));

                    break;

                case "EternalGoal":

                    _goals.Add(
                        new EternalGoal(
                            parts[1],
                            parts[2],
                            int.Parse(parts[3])
                        ));

                    break;

                case "ChecklistGoal":

                    _goals.Add(
                        new ChecklistGoal(
                            parts[1],
                            parts[2],
                            int.Parse(parts[3]),
                            int.Parse(parts[4]),
                            int.Parse(parts[5]),
                            int.Parse(parts[6])
                        ));

                    break;
            }
        }

        Console.WriteLine("Goals loaded successfully.");
    }
}