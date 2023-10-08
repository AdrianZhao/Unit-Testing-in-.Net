
namespace VehicleTrackerTests
{
    [TestClass]
    public class VehicleTrackerTests
    {
        public VehicleTracker CreateVehicleTrackerWithCapacity(int capacity)
        {
            return new VehicleTracker(capacity, "123 Fake St");
        }

        [TestMethod]
        public void VehicleTracker_Initialization_ShouldHaveEmptySlots()
        {
            // Arrange
            int capacity = 10;
            VehicleTracker vt = CreateVehicleTrackerWithCapacity(capacity);
            // Act and Assert
            foreach (Vehicle vehicle in vt.VehicleList.Values)
            {
                Assert.IsNull(vehicle);
            }
        }

        [TestMethod]
        public void VehicleTracker_AddVehicle_ShouldAddToEmptySlot()
        {
            // Arrange
            int capacity = 10;
            VehicleTracker vt = CreateVehicleTrackerWithCapacity(capacity);
            Vehicle vehicleOne = new Vehicle("A01 T22", true);
            // Act
            vt.AddVehicle(vehicleOne);
            // Assert
            Assert.AreEqual(vehicleOne, vt.VehicleList.Values.First());
        }

        [TestMethod]
        public void VehicleTracker_AddVehicle_ShouldThrowExceptionWhenAllSlotsFull()
        {
            // Arrange
            int capacity = 2;
            VehicleTracker vt = CreateVehicleTrackerWithCapacity(capacity);
            Vehicle vehicleOne = new Vehicle("A01 T22", true);
            Vehicle vehicleTwo = new Vehicle("B02 U33", true);
            Vehicle vehicleThree = new Vehicle("C03 V44", true);
            // Act
            vt.AddVehicle(vehicleOne);
            vt.AddVehicle(vehicleTwo);
            // Assert
            Assert.ThrowsException<IndexOutOfRangeException>(() =>
            {
                vt.AddVehicle(vehicleThree);
            });
        }

        [TestMethod]
        public void VehicleTracker_RemoveVehicle_ByLicence_ShouldSetSlotToNull()
        {
            // Arrange
            int capacity = 10;
            VehicleTracker vt = CreateVehicleTrackerWithCapacity(capacity);
            Vehicle vehicleOne = new Vehicle("A01 T22", true);
            vt.AddVehicle(vehicleOne);
            // Act
            vt.RemoveVehicle("A01 T22");
            // Assert
            Assert.IsNull(vt.VehicleList.Values.First());
        }

        [TestMethod]
        public void VehicleTracker_RemoveVehicle_ByLicence_ShouldThrowExceptionWhenLicenceNotFound()
        {
            // Arrange
            int capacity = 10;
            VehicleTracker vt = CreateVehicleTrackerWithCapacity(capacity);
            Vehicle vehicleOne = new Vehicle("A01 T22", true);
            // Act and Assert
            Assert.ThrowsException<NullReferenceException>(() =>
            {
                vt.RemoveVehicle("B02 U33");
            });
        }
        [TestMethod]
        public void VehicleTracker_RemoveVehicle_BySlotNumber_ShouldSetSlotToNull()
        {
            // Arrange
            int capacity = 10;
            VehicleTracker vt = CreateVehicleTrackerWithCapacity(capacity);
            Vehicle vehicleOne = new Vehicle("A01 T22", true);
            vt.AddVehicle(vehicleOne);
            // Act
            vt.RemoveVehicle(1);
            // Assert
            Assert.IsNull(vt.VehicleList.Values.First());
        }

        [TestMethod]
        public void VehicleTracker_RemoveVehicle_BySlotNumber_ShouldThrowExceptionWhenSlotNumberBiggerThanCapacity()
        {
            // Arrange
            int capacity = 10;
            VehicleTracker vt = CreateVehicleTrackerWithCapacity(capacity);
            Vehicle vehicleOne = new Vehicle("A01 T22", true);
            vt.AddVehicle(vehicleOne);
            // Act and Assert
            Assert.ThrowsException<IndexOutOfRangeException>(() =>
            {
                vt.RemoveVehicle(11);
            });
        }

        [TestMethod]
        public void VehicleTracker_RemoveVehicle_BySlotNumber_ShouldThrowExceptionWhenSlotNumberNegative()
        {
            // Arrange
            int capacity = 10;
            VehicleTracker vt = CreateVehicleTrackerWithCapacity(capacity);
            Vehicle vehicleOne = new Vehicle("A01 T22", true);
            vt.AddVehicle(vehicleOne);
            // Act and Assert
            Assert.ThrowsException<IndexOutOfRangeException>(() =>
            {
                vt.RemoveVehicle(-1);
            });
        }

        [TestMethod]
        public void VehicleTracker_RemoveVehicle_BySlotNumber_ShouldThrowExceptionWhenSlotNotFilled()
        {
            // Arrange
            int capacity = 10;
            VehicleTracker vt = CreateVehicleTrackerWithCapacity(capacity);
            // Act and Assert
            Assert.ThrowsException<NullReferenceException>(() =>
            {
                vt.RemoveVehicle(1);
            });
        }

        [TestMethod]
        public void VehicleTracker_ParkedPassholders_ShouldReturnListWithPassholders()
        {
            // Arrange
            int capacity = 10;
            VehicleTracker vt = CreateVehicleTrackerWithCapacity(capacity);
            Vehicle vehicleOne = new Vehicle("A01 T22", true);
            Vehicle vehicleTwo = new Vehicle("B02 U33", false);
            Vehicle vehicleThree = new Vehicle("C03 V44", true);
            vt.AddVehicle(vehicleOne);
            vt.AddVehicle(vehicleTwo);
            vt.AddVehicle(vehicleThree);
            // Act
            List<Vehicle> passholders = vt.ParkedPassholders();
            // Assert
            CollectionAssert.Contains(passholders, vehicleOne);
            CollectionAssert.DoesNotContain(passholders, vehicleTwo);
            CollectionAssert.Contains(passholders, vehicleThree);
            Assert.AreEqual(2, passholders.Count());
        }

        [TestMethod]
        public void VehicleTracker_PassholderPercentage_ShouldCalculatePercentage()
        {
            // Arrange
            int capacity = 10;
            VehicleTracker vt = CreateVehicleTrackerWithCapacity(capacity);
            Vehicle vehicleOne = new Vehicle("A01 T22", true);
            Vehicle vehicleTwo = new Vehicle("B02 U33", false);
            vt.AddVehicle(vehicleOne);
            vt.AddVehicle(vehicleTwo);
            // Act
            double percentage = vt.PassholderPercentage();
            // Assert
            Assert.AreEqual(50, percentage);
        }
    }
}