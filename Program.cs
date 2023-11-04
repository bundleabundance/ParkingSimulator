using System;
using System.Globalization;

class Program{
    static void Main(){
        while(true){
            try
            {
                 //Get entry and exit time from user
                Console.WriteLine("Enter entry time (hh:mm): ");
                string entryTimeInput = Console.ReadLine();
                Console.WriteLine("Enter exit time (hh:mm): ");
                string exitTimeInput = Console.ReadLine();

                DateTime entryTime, exitTime;

                if (!DateTime.TryParseExact(entryTimeInput, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out entryTime) ||
                    !DateTime.TryParseExact(exitTimeInput, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out exitTime))
                {
                    throw new FormatException("Invalid time format. Please use 'hh:mm' format.");
                }

                // Handle cases where entry time is later than exit time, assuming the car exited the next day
                if (entryTime > exitTime)
                {
                    exitTime = exitTime.AddDays(1);
                }

                // Calculate parking duration
                TimeSpan parkingDuration = exitTime - entryTime;

                /*
                // Check if parking duration exceeds 24 hours
                if (parkingDuration.TotalHours > 24)
                {
                    throw new InvalidOperationException("Parking duration exceeds 24 hours. Please check your entry and exit times.");
                }
                */

                // Calculate parking fee
                decimal parkingFee = CalculateParkingFee(parkingDuration);

                // Display parking duration and fee
                Console.WriteLine($"Parking duration: {parkingDuration.TotalHours:F2} hours");
                Console.WriteLine($"Parking fee: {parkingFee:F2} TL");

                // Allow the user to continue
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Please enter valid time formats.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
            /*
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
            */
        }
    }

    //calculate parking fee
    static decimal CalculateParkingFee(TimeSpan parkingDuration){
        decimal parkingFee = 0;
        double totalHours = parkingDuration.TotalHours;

            if (parkingDuration.TotalHours <= 3){
                parkingFee = (decimal)Math.Ceiling(totalHours) * 3;
            }
            else if (parkingDuration.TotalHours <= 6){
                parkingFee = 9 + (decimal)Math.Ceiling((totalHours - 3)) * 2;
            }   
            else{
                parkingFee = 15 + (decimal)Math.Ceiling((totalHours - 6) / 3) * 5;
            }
        return parkingFee;
    }
}


