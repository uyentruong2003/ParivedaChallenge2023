namespace Pariveda_Challenge
{
    public class rsvpUtility
    {
        private RSVP[] rsvps = new RSVP[500];
        private Event[] events = new Event[500];
        
        public rsvpUtility(RSVP[] rsvps, Event[] events){
            this.rsvps = rsvps;
            this.events = events;
        }
        //GetAllFromFile;
         public void GetAllRSVPsFromFile(){
            //open file
            StreamReader inFile = new StreamReader("rsvps.txt");
            //process file
            RSVP.SetCount(0);
            string line = inFile.ReadLine(); // prime read

            while(line != null){
                // Separate by delimiter '#' to an array 
                string[] temp = line.Split('#');
                // Pass into the constructor
                rsvps[RSVP.GetCount()] = new RSVP(int.Parse(temp[0]),temp[1],int.Parse(temp[2]),bool.Parse(temp[3]),temp[4]);
                RSVP.IncCount();
                line = inFile.ReadLine(); // update read
            }
            //close file
            inFile.Close();
        }
        public void PrintRSVP(){
            for (int i = 0; i< RSVP.GetCount();i++){
                System.Console.WriteLine(rsvps[i].ToString());
            }
        }
        //Add RSVP:
        public void AddRSVP(){
            RSVP newRSVP = new RSVP();
            //Print out the all events & prompt user to enter the eventID
            int eventID = ChooseEvent();
            // SetEventID:
            newRSVP.SetEventID(eventID);
            
            // SetStudentName:
            System.Console.Write("Enter Student's Name: ");
            newRSVP.SetStudentName(Console.ReadLine());
            //SetStudentID:
            AddCWID(ref newRSVP);
            // SetIsMIS & classLevel:
            AddOrEditMISStatus(ref newRSVP);

            // Add new RSVP to the list:
            rsvps[RSVP.GetCount()] = newRSVP;
            RSVP.IncCount();

            // Save to file:
            SaveToFile();
        }

        private void SaveToFile(){
            StreamWriter outFile = new StreamWriter("rsvps.txt");
            for (int i = 0; i<RSVP.GetCount(); i++){
                outFile.WriteLine(rsvps[i].ToFile());
            }
            outFile.Close();
        }
        
        private void AddCWID(ref RSVP newRSVP){
            int cwid = CheckValidCWID();// prime read
            while (!CheckUnique(cwid, newRSVP.GetEventID())){
                System.Console.WriteLine("This CWID is already used to RSVP this event. Please try again.");
                cwid = CheckValidCWID(); //update read
            }
            newRSVP.SetStudentID(cwid);
        }
        private void AddOrEditMISStatus(ref RSVP rsvp){
            //SetIsMIS:
            bool isMIS = CheckIsMIS();
            rsvp.SetIsMIS(isMIS);
            //SetClassLevel:
            string classLevel;
            if (isMIS) classLevel = CheckClassLevel();
            else classLevel = "N/A";
            rsvp.SetClassLevel(classLevel);
        }

        private string CheckClassLevel(){
            // Prompt user for MIS Class:
            string[] classes = {"N/A","MIS 221","MIS 321/330","MIS 405/430", "MIS 431/451","AMP"};
            System.Console.WriteLine("\nMIS Class Levels: MIS 221, MIS 321/330, MIS 405/430, MIS 431/451, AMP");
            // Check if the answer is in the array
            string classLevel = CheckFromList(classes, "Current MIS Class");
            return classLevel;
        }
        private string CheckFromList(string[] list, string listName){
            string input;
            bool found = false;
            do{
                System.Console.Write($"Enter Your {listName}. Enter N/A if not in any: ");
                input = Console.ReadLine();
                for (int i = 0; i < list.Length; i++){
                    if (list[i] == input.ToUpper()){
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
        private bool CheckIsMIS(){
            bool isMIS = false;
            System.Console.Write("Are you MIS major (Yes/No)? ");
            string ans = Console.ReadLine();
            while(ans.ToUpper() != "YES" && ans.ToUpper() != "NO"){
                System.Console.WriteLine("Only accept Yes/No answer. Please try again");
                System.Console.Write("Are you MIS major (Yes/No)? ");
                ans = Console.ReadLine();
            }
            switch(ans.ToUpper()){
                case "YES":
                    isMIS = true;
                    break;
                case "NO":
                    isMIS = false;
                    break;
            }
            return isMIS;
        }
        private int CheckValidCWID(){
            int cwid = CheckInt("Enter Your 8-digit CWID: "); //prime read
            // Check if it's 8-digit
            while(!(cwid.ToString().Length == 8)){
                System.Console.WriteLine("CWID must be 8-digit. Please try again.");
                cwid = CheckInt("Enter Your 8-digit CWID: "); // update read
            }
            return cwid;
        }

        // Check if the CWID has already been used:
        private bool CheckUnique(int cwid, int eventID){
            GetAllRSVPsFromFile();
            bool unique = true;
            for (int i = 0; i< RSVP.GetCount(); i++){
                if (cwid == rsvps[i].GetStudentID() && eventID == rsvps[i].GetEventID()){
                    unique = false;
                    break;
                }
            }
            return unique;
        }
        

        public int ChooseEvent(){
            //Get events from file & print out on screen
            eventUtility eUtility = new eventUtility(events);
            eUtility.GetAllEventsFromFile();
            eUtility.Sort();
            // Prompt user to enter eventID:
            int numInput = CheckInt("Enter an eventID: ");
            int foundIndex = eUtility.SequentialSearch(numInput); // prime read
            while (foundIndex == -1){
                System.Console.WriteLine("EventID does not exist. Please try again.");
                numInput = CheckInt("Enter an eventID: ");
                foundIndex = eUtility.SequentialSearch(numInput); // update read
            }
            return events[foundIndex].GetEventID();
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

        
    
        //Edit RSVP:
        public void EditRSVP(){
            //Print out the all events & prompt user to enter the eventID
            int eventID = ChooseEvent();
            // Prompt for CWID:
            int cwid = CheckValidCWID();// prime read
            // Search:
            int searchIndex = SearchExistingRSVP(eventID, cwid);
            if (searchIndex == -1) {
                System.Console.WriteLine("No RSVP is found with the given eventID and CWID.");
                System.Console.WriteLine("Press any key to go back...");
                Console.ReadKey();
            } else {
                System.Console.WriteLine($"Press any key to edit...");
                Console.ReadKey();
                // Edit:
                // SetStudentName:
                System.Console.Write("Enter Student's Name: ");
                rsvps[searchIndex].SetStudentName(Console.ReadLine());
                //SetStudentID:
                EditCWID(ref rsvps[searchIndex]);
                // SetIsMIS & classLevel:
                AddOrEditMISStatus(ref rsvps[searchIndex]);
                SaveToFile();
            }     
        }
        private void EditCWID(ref RSVP rsvp){
            int cwid = CheckValidCWID();// prime read
            while (cwid != rsvp.GetStudentID() && !CheckUnique(cwid, rsvp.GetEventID())){
                System.Console.WriteLine("This CWID is already used to RSVP this event. Please try again.");
                cwid = CheckValidCWID(); //update read
            }
            rsvp.SetStudentID(cwid);
        }
        private int SearchExistingRSVP(int eventID, int cwid){
            System.Console.WriteLine("Start Searching...");
            GetAllRSVPsFromFile();
            bool found = false;
            int searchIndex = -1;
            for (int i = 0; i< RSVP.GetCount(); i++){
                if (cwid == rsvps[i].GetStudentID() && eventID == rsvps[i].GetEventID()){
                    System.Console.WriteLine(rsvps[i].ToString());
                    found = true;
                    searchIndex = i;
                } 
            }
            return searchIndex;
        }
        

        //Cancel RSVP:
        public void CancelRSVP(){
            //Print out the all events & prompt user to enter the eventID
            int eventID = ChooseEvent();
            // Prompt for CWID:
            int cwid = CheckValidCWID();// prime read
            // Search:
            int searchIndex = SearchExistingRSVP(eventID, cwid);
            if (searchIndex == -1) {
                System.Console.WriteLine("No RSVP is found with the given eventID and CWID.");
                System.Console.WriteLine("Press any key to go back...");
                Console.ReadKey();
            } else {
                System.Console.Write($"Type 'YES' to delete the RSVP above: ");
                if (Console.ReadLine().ToUpper() == "YES"){
                    DeleteFromArray(searchIndex);
                    SaveToFile();
                    System.Console.WriteLine("RSVP removed!");

                } else {
                    System.Console.WriteLine("Delete command canceled");
                }

            }    
        }

        private void DeleteFromArray(int searchIndex){
            RSVP[] temp = new RSVP[rsvps.Length-1];
            // Copy to temp[] the rsvps before the removed one:
            for (int i = 0; i < searchIndex; i++){
                temp[i] = rsvps[i];
            }
            // Copy to temp[] the rsvps after the removed one, excluding the removed rsvp:
            for (int i = searchIndex; i < rsvps.Length-1; i++){
                temp[i] = rsvps[i+1];
            }
            rsvps = temp;
            RSVP.DecCount();
        }
    }
}