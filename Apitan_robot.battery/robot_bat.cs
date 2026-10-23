using System;
using System.Collections.Generic;

interface IBattery
{
    int RemainingEnergy { get; }
    void Consume(int amount);
    void Recharge(int amount);
}

class LithiumBattery : IBattery
{
    private int _remainingEnergy = 100;
    public int RemainingEnergy => _remainingEnergy;

    public void Consume(int amount)
    {
        if (amount > 0)
            _remainingEnergy = Math.Max(0, _remainingEnergy - amount);
    }

    public void Recharge(int amount)
    {
        if (amount > 0)
            _remainingEnergy = (int)Math.Min(100L, (long)_remainingEnergy + amount);
    }
}

class SolarBattery : IBattery
{
    private int _remainingEnergy = 100;
    public int RemainingEnergy => _remainingEnergy;

    public void Consume(int amount)
    {
        if (amount > 0)
            _remainingEnergy = Math.Max(0, _remainingEnergy - amount);
    }

    public void Recharge(int amount)
    {
        if (amount > 0)
            _remainingEnergy = (int)Math.Min(100L, (long)_remainingEnergy + amount);
    }
}

abstract class Robot
{
    private readonly IBattery _battery;
    public string Name { get; set; }
    public int RemainingEnergy => _battery.RemainingEnergy;

    public Robot(string name, IBattery battery)
    {
        Name = name;
        _battery = battery;
    }

    public void Recharge(int amount) => _battery.Recharge(amount);
    protected void UseEnergy(int amount) => _battery.Consume(amount);
    public abstract void PerformTask();
}

class CleaningRobot : Robot
{
    public CleaningRobot(string name, IBattery battery) : base(name, battery) { }

    public override void PerformTask()
    {
        UseEnergy(10);
        Console.WriteLine($"{Name} is cleaning. Energy: {RemainingEnergy}");
    }
}

class DeliveryRobot : Robot
{
    public DeliveryRobot(string name, IBattery battery) : base(name, battery) { }

    public override void PerformTask()
    {
        UseEnergy(25);
        Console.WriteLine($"{Name} is delivering. Energy: {RemainingEnergy}");
    }
}

class Program
{
    static void Main()
    {
        List<Robot> robots = new List<Robot>
        {
            new CleaningRobot("Cleaning Robot", new LithiumBattery()),
            new DeliveryRobot("Delivery Robot", new SolarBattery())
        };

        foreach (Robot robot in robots)
        {
            robot.PerformTask();
            robot.Recharge(20);
            Console.WriteLine("After recharge: " + robot.RemainingEnergy);
        }
    }
}