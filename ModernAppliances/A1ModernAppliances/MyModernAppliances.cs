using ModernAppliances.Entities;
using ModernAppliances.Entities.Abstract;
using ModernAppliances.Helpers;

namespace ModernAppliances
{
    /// <summary>
    /// Manager class for Modern Appliances
    /// </summary>
    /// <remarks>Author: Brook</remarks>
    /// <remarks>Date: July 2024</remarks>
    internal class MyModernAppliances : ModernAppliances
    {
        /// <summary>
        /// Option 1: Performs a checkout
        /// </summary>
        public override void Checkout()
        {
            Console.WriteLine("Enter the item number of an appliance: ");
            if (!long.TryParse(Console.ReadLine(), out long itemNumber))
            {
                Console.WriteLine("Invalid input. Please enter a valid item number.");
                return;
            }

            var foundAppliance = Appliances.FirstOrDefault(appliance => appliance.ItemNumber == itemNumber);

            if (foundAppliance == null)
            {
                Console.WriteLine("No appliances found with that item number.");
                return;
            }

            if (!foundAppliance.IsAvailable)
            {
                Console.WriteLine("The appliance is not available to be checked out.");
                return;
            }

            // Assuming Checkout changes the IsAvailable status of the appliance
            // This is a placeholder for the actual checkout logic
            Console.WriteLine("Appliance has been checked out.");
        }

        public override void Find()
        {
            Console.WriteLine("Enter brand to search for:");
            string brand = Console.ReadLine();
            var found = Appliances.Where(appliance => appliance.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase)).ToList();
            DisplayAppliancesFromList(found.Cast<Appliance>().ToList(), 0);
        }

        public override void DisplayRefrigerators()
        {
            Console.WriteLine("Possible options:");
            Console.WriteLine("0 - Any");
            Console.WriteLine("2 - Double doors");
            Console.WriteLine("3 - Three doors");
            Console.WriteLine("4 - Four doors");
            Console.WriteLine("Enter number of doors: ");

            if (!int.TryParse(Console.ReadLine(), out int doors) || doors < 0 || doors > 4)
            {
                Console.WriteLine("Invalid option.");
                return;
            }

            var found = Appliances.OfType<Refrigerator>().Where(r => doors == 0 || r.Doors == doors).ToList();
            DisplayAppliancesFromList(found.Cast<Appliance>().ToList(), 0);
        }

        public override void DisplayVacuums()
        {
            Console.WriteLine("Possible options:");
            Console.WriteLine("0 - Any");
            Console.WriteLine("1 - Residential");
            Console.WriteLine("2 - Commercial");
            Console.WriteLine("Enter grade:");

            string gradeInput = Console.ReadLine();
            string grade = gradeInput switch
            {
                "0" => "Any",
                "1" => "Residential",
                "2" => "Commercial",
                _ => throw new InvalidOperationException("Invalid option.")
            };

            Console.WriteLine("Possible options:");
            Console.WriteLine("0 - Any");
            Console.WriteLine("1 - 18 Volt");
            Console.WriteLine("2 - 24 Volt");
            Console.WriteLine("Enter voltage:");

            if (!short.TryParse(Console.ReadLine(), out short voltage) || voltage < 0 || voltage > 2)
            {
                Console.WriteLine("Invalid option.");
                return;
            }

            var found = Appliances.OfType<Vacuum>().Where(v => grade == "Any" || v.Grade == grade)
                                         .Where(v => voltage == 0 || (voltage == 1 && v.BatteryVoltage == 18) || (voltage == 2 && v.BatteryVoltage == 24))
                                         .ToList();

            DisplayAppliancesFromList(found.Cast<Appliance>().ToList(), 0);
        }

        public override void DisplayMicrowaves()
        {
            Console.WriteLine("Possible options:");
            Console.WriteLine("0 - Any");
            Console.WriteLine("1 - Kitchen");
            Console.WriteLine("2 - Work site");
            Console.WriteLine("Enter room type:");

            char roomType = Console.ReadLine() switch
            {
                "0" => 'A',
                "1" => 'K',
                "2" => 'W',
                _ => throw new InvalidOperationException("Invalid option.")
            };

            var found = Appliances.OfType<Microwave>().Where(m => roomType == 'A' || m.RoomType == roomType).ToList();
            DisplayAppliancesFromList(found.Cast<Appliance>().ToList(), 0);
        }

        public override void DisplayDishwashers()
        {
            Console.WriteLine("Possible options:");
            Console.WriteLine("0 - Any");
            Console.WriteLine("1 - Quietest");
            Console.WriteLine("2 - Quieter");
            Console.WriteLine("3 - Quiet");
            Console.WriteLine("4 - Moderate");
            Console.WriteLine("Enter sound rating:");

            string soundRating = Console.ReadLine() switch
            {
                "0" => "Any",
                "1" => "Qt",
                "2" => "Qr",
                "3" => "Qu",
                "4" => "M",
                _ => throw new InvalidOperationException("Invalid option.")
            };

            var found = Appliances.OfType<Dishwasher>().Where(d => soundRating == "Any" || d.SoundRating == soundRating).ToList();
            DisplayAppliancesFromList(found.Cast<Appliance>().ToList(), 0);
        }

        public override void RandomList()
        {
            Console.WriteLine("Appliance Types");
            Console.WriteLine("0 - Any");
            Console.WriteLine("1 – Refrigerators");
            Console.WriteLine("2 – Vacuums");
            Console.WriteLine("3 – Microwaves");
            Console.WriteLine("4 – Dishwashers");
            Console.WriteLine("Enter type of appliance:");

            if (!int.TryParse(Console.ReadLine(), out int applianceType) || applianceType < 0 || applianceType > 4)
            {
                Console.WriteLine("Invalid option.");
                return;
            }

            Console.WriteLine("Enter number of appliances:");
            if (!int.TryParse(Console.ReadLine(), out int num) || num < 0)
            {
                Console.WriteLine("Invalid number.");
                return;
            }

            var found = Appliances.Where(a => applianceType == 0 || (a.GetType().Name.Equals("Refrigerator") && applianceType == 1) || (a.GetType().Name.Equals("Vacuum") && applianceType == 2) || (a.GetType().Name.Equals("Microwave") && applianceType == 3) || (a.GetType().Name.Equals("Dishwasher") && applianceType == 4)).OrderBy(a => Guid.NewGuid()).Take(num).ToList();
            DisplayAppliancesFromList(found.Cast<Appliance>().ToList(), num);
        }
    }
}
