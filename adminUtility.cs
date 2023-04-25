namespace Pariveda_Challenge
{
    public class adminUtility
    {
        private Admin[] admins = new Admin[10];
        public adminUtility(Admin[] admins){
            this.admins = admins;
        }
        public void GetAllAdminsFromFile(){
            //open file
            StreamReader inFile = new StreamReader("admins.txt");
            //process file
            Admin.SetCount(0);
            string line = inFile.ReadLine(); // prime read

            while(line != null){
                // Separate by delimiter '#' to an array 
                string[] temp = line.Split('#');
                // Pass into the constructor
                admins[Admin.GetCount()] = new Admin(temp[0],temp[1]);
                Admin.IncCount();
                line = inFile.ReadLine(); // update read
            }
            //close file
            inFile.Close();
        }
        public bool VerifyAdmin(string user, string pass){
            GetAllAdminsFromFile();
            bool isVerified = false;
            for (int i = 0; i< Admin.GetCount(); i++){
                if (admins[i].GetUser()==user && admins[i].GetPass()==pass){
                    isVerified = true;
                    break;  
                }
            }
            return isVerified; 
        }
    }
}