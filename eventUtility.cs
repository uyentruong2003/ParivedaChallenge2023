namespace Pariveda_Challenge
{
    public class eventUtility
    {
        private Event[] events = new Event[500];
        public eventUtility(Event[] events){
            this.events = events;
        }
        //GetAllFromFile;
         public void GetAllEventsFromFile(){
            //open file
            StreamReader inFile = new StreamReader("events.txt");
            //process file
            Event.SetCount(0);
            string line = inFile.ReadLine(); // prime read

            while(line != null){
                // Separate by delimiter '#' to an array 
                string[] temp = line.Split('#');
                // Pass into the constructor
                events[Event.GetCount()] = new Event(int.Parse(temp[0]),temp[1],temp[2],temp[3],temp[4],temp[5]);
                Event.IncCount();
                line = inFile.ReadLine(); // update read
            }
            //close file
            inFile.Close();
            // print out:
            PrintEvent();
        }
        private void PrintEvent(){
            for (int i = 0; i< Event.GetCount();i++){
                System.Console.WriteLine(events[i].ToString());
            }
        }
        public void AddEvent(){
            GetAllEventsFromFile();
            Sort();
            Event newEvent = new Event();
            // set eventID:
            int eventID = events[Event.GetCount()-1].GetEventID() + 1;
            newEvent.SetEventID(eventID);
            // input information:
            PromptUserInput(ref newEvent);

            //Add to array:
            events[Event.GetCount()] = newEvent;
            Event.IncCount();

            //Save:
            SaveToFile();
        }

        private void SaveToFile(){
            StreamWriter outFile = new StreamWriter("events.txt");
            for (int i = 0; i<Event.GetCount(); i++){
                outFile.WriteLine(events[i].ToFile());
            }
            outFile.Close();
        }

        private void PromptUserInput(ref Event newEvent){
            // set eventName:
            System.Console.Write("Enter event name: ");
            string name = Console.ReadLine();
            newEvent.SetEventName(name);

            // set eventDate:
            System.Console.Write("Enter event date: ");
            string date = Console.ReadLine();
            while (!isValidDate(date)){
                System.Console.WriteLine("Invalid date. Please try again.");
                System.Console.Write("Enter event date: ");
                date = Console.ReadLine();
            }
            newEvent.SetEventDate(date);

            // set eventTime:
            System.Console.Write("Enter event time: ");
            string time = Console.ReadLine();
            while (!isValidTime(time)){
                System.Console.WriteLine("Invalid time. Please try again.");
                System.Console.Write("Enter event time: ");
                time = Console.ReadLine();
            }
            newEvent.SetEventTime(time);

            // set eventLocation:
            System.Console.Write("Enter event location: ");
            string loc = Console.ReadLine();
            newEvent.SetEventLoc(loc);

            // set eventHost:
            string eventHost = CheckHost();
            newEvent.SetEventHost(eventHost);
        }
        private int CheckInt(string prompt){
            System.Console.Write(prompt);
            string input = Console.ReadLine();
            int num = 0;
            while(!(int.TryParse(input, out num))){
                System.Console.WriteLine("Accept number only. Please try again.");
                System.Console.Write(prompt);
                input = Console.ReadLine();
            }
            return num;
        }
        private string CheckHost(){
            // Prompt user for MIS Class:
            string[] affinityGroups = {"Other","AIMS","WIT","CMISS","VIB","MMIS","LGBT++", "Ambassadors"};
            System.Console.WriteLine("\nMIS Affinity Groups: CMISS, WIT, AIMS, Ambassadors, VIB, LGBT++, MMIS");
            // Check if the answer is in the array
            string host = CheckFromList(affinityGroups, "Host");
            return host;
        }
        private string CheckFromList(string[] list, string listName){
            string input;
            bool found = false;
            do{
                System.Console.Write($"Enter event {listName}. Enter Other if not listed: ");
                input = Console.ReadLine();
                for (int i = 0; i < list.Length; i++){
                    if (list[i] == input){
                        found = true;
                        break;
                    }
                }
                if (!found){
                        Console.WriteLine("The input is invalid. Please try again.");
                    }
                
            } while (!found);
            return input;
        }
        private bool isValidTime(string input){
            // Parse the user input into a DateTime object
            DateTime time;
            if (DateTime.TryParse(input, out time)){
                // Convert the time to 24-hour format if it's in AM/PM format
                if (time.ToString("tt") == "PM" && time.Hour != 12)
                {
                    time = time.AddHours(12);
                }
                else if (time.ToString("tt") == "AM" && time.Hour == 12)
                {
                    time = time.AddHours(-12);
                }
                return true;
            } else return false;
        }
        private bool isValidDate(string input){
            // Attempt to parse the input as a DateTime object
            if (DateTime.TryParseExact(input, "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime date)){
                // If the parsing succeeded, check that the parsed date matches the input string
                // This is necessary to ensure that the input string is in the correct format
                return date.ToString("MM/dd/yyyy") == input;
            }

            // If the parsing failed, the input is not a valid date string
            return false;
        }
        
        // Sequential Search:
        public int SequentialSearch(int searchVal){
            for (int i=0; i<Event.GetCount(); i++){
                if(events[i].GetEventID() == searchVal) return i;
            }
            return -1;
        }
        // Selection sort:
        public void Sort (){
            for (int i =0; i < Event.GetCount() -1; i++){
                int min = i;
                for (int j = i + 1; j < Event.GetCount(); j++) {
                    if (events[min].GetEventID()>events[j].GetEventID()) {
                        min = j;
                    }
                }
                if (min != i) {
                    Swap (min, i);
                }
            }
        }

        private void Swap(int x, int y){
            Event temp = events[x];
            events[x] = events [y];
            events[y] = temp;
        }

        public void EditEvent(){
            GetAllEventsFromFile();
            Sort();
            int searchVal = CheckInt("Enter eventID: ");
            int foundIndex = SequentialSearch(searchVal);
            while(foundIndex == -1){
                System.Console.WriteLine("eventID does not exist. Please try again");
                searchVal = CheckInt("Enter eventID: ");
                foundIndex = SequentialSearch(searchVal);
            }
            PromptUserInput(ref events[foundIndex]);
            // Save to file:
            SaveToFile();
        }
        public void CancelEvent(){
            GetAllEventsFromFile();
            Sort();
            int searchVal = CheckInt("Enter eventID: ");
            int foundIndex = SequentialSearch(searchVal);
            while(foundIndex == -1){
                System.Console.WriteLine("eventID does not exist. Please try again");
                searchVal = CheckInt("Enter eventID: ");
                foundIndex = SequentialSearch(searchVal);
            }
            System.Console.WriteLine(events[foundIndex].ToString());
            System.Console.Write($"Type 'YES' to delete the event above: ");
                if (Console.ReadLine().ToUpper() == "YES"){
                    DeleteFromArray(foundIndex);
                    SaveToFile();
                    System.Console.WriteLine("Event removed!");

                } else {
                    System.Console.WriteLine("Delete command canceled");
                }
        }
        private void DeleteFromArray(int foundIndex){
            Event[] temp = new Event[events.Length-1];
            // Copy to temp[] the events before the removed one:
            for (int i = 0; i < foundIndex; i++){
                temp[i] = events[i];
            }
            // Copy to temp[] the events after the removed one, excluding the removed event:
            for (int i = foundIndex; i < events.Length-1; i++){
                temp[i] = events[i+1];
            }
            events = temp;
            Event.DecCount();
        }
    }
}
