using System.ComponentModel;

class Program
{
    static void Main(string[] args)
    {

    }
}

public class Vehicle
{
    public string Licence { get; set; }
    public bool Pass { get; set; }
    public Vehicle(string licence, bool pass)
    {
        this.Licence = licence;
        this.Pass = pass;
    }
}

public class VehicleTracker
{
    //PROPERTIES
    public string Address { get; set; }
    public int Capacity { get; set; }
    public int SlotsAvailable { get; set; }
    public Dictionary<int, Vehicle> VehicleList { get; set; }

    public VehicleTracker(int capacity, string address)
    {
        this.Capacity = capacity;
        this.Address = address;
        this.SlotsAvailable = capacity;
        this.VehicleList = new Dictionary<int, Vehicle>();

        this.GenerateSlots();
    }

    // STATIC PROPERTIES
    public static string BadSearchMessage = "Error: Search did not yield any result.";
    public static string BadSlotNumberMessage = "Error: No slot with number ";
    public static string SlotsFullMessage = "Error: no slots available.";

    // METHODS
    public void GenerateSlots()
    {
        for (int i = 1; i <= this.Capacity; i++)
        {
            this.VehicleList.Add(i, null);
        }
    }

    public void AddVehicle(Vehicle vehicle)
    {
        foreach (KeyValuePair<int, Vehicle> slot in this.VehicleList)
        {
            if (slot.Value == null)
            {
                this.VehicleList[slot.Key] = vehicle;
                this.SlotsAvailable--;
                return;
            }
        }
        throw new IndexOutOfRangeException(SlotsFullMessage);
    }

    public void RemoveVehicle(string licence)
    {
        try
        {
            int slot = this.VehicleList.First(v => v.Value.Licence == licence).Key;
            this.SlotsAvailable++;
            this.VehicleList[slot] = null;
        }
        catch
        {
            throw new NullReferenceException(BadSearchMessage);
        }
    }

    public void RemoveVehicle(int slotNumber)
    {
        if (slotNumber > this.Capacity || slotNumber <= 0)
        {
            throw new IndexOutOfRangeException(BadSlotNumberMessage);
        }
        else if (VehicleList[slotNumber] == null)
        {
            throw new NullReferenceException(BadSearchMessage);
        }
        else
        {
            this.VehicleList[slotNumber] = null;
            this.SlotsAvailable++;
        }
    }
    public List<Vehicle> ParkedPassholders()
    {
        List<Vehicle> passHolders = new List<Vehicle>();
        foreach (KeyValuePair<int, Vehicle> vehicle in this.VehicleList)
        {
            if (vehicle.Value != null && vehicle.Value.Pass)
            {
                passHolders.Add(vehicle.Value);
            }
        }
        return passHolders;
    }

    public double PassholderPercentage()
    {
        double passHolders = ParkedPassholders().Count();
        double totalVehicle = this.Capacity - this.SlotsAvailable;
        double percentage = (passHolders / totalVehicle) * 100;
        return percentage;
    }
}