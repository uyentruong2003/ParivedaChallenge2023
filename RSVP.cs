namespace Pariveda_Challenge
{
    public class RSVP
    {
       private int eventID;
        private string studentName;
        private int studentID;
        private bool isMIS;
        private string classLevel; 
        static private int count;
        public RSVP(){}
        public RSVP(int eventID, string studentName, int studentID, bool isMIS, string classLevel){
            this.eventID = eventID;
            this.studentName = studentName;
            this.studentID = studentID;
            this.isMIS = isMIS;
            this.classLevel = classLevel;
        }
        //setters:
        public void SetEventID(int eventID){
            this.eventID = eventID;
        }
        public void SetStudentName(string studentName){
            this.studentName = studentName;
        }
        public void SetStudentID(int studentID){
            this.studentID = studentID;
        }
        public void SetIsMIS(bool isMIS){
            this.isMIS = isMIS;
        }
        public void SetClassLevel(string classLevel){
            this.classLevel = classLevel;
        }
        static public void SetCount(int count){
            RSVP.count = count;
        }
        static public void IncCount(){
            RSVP.count++;
        }
        static public void DecCount(){
            RSVP.count--;
        }
        //getters:
        public int GetEventID(){
            return this.eventID;
        }
        public string GetStudentName(){
            return this.studentName;
        }
        public int GetStudentID(){
            return this.studentID;
        }
        public bool GetIsMIS(){
            return this.isMIS;
        }
        public string GetClassLevel(){
            return this.classLevel;
        }
        static public int GetCount(){
            return RSVP.count;
        }
        //ToString():
        public override string ToString(){
            return $"EventID: {eventID}\tStudent Name: {studentName}\tStudentID: {studentID}\tMIS Major: {isMIS}\tClass: {classLevel}";
        }
        //ToFile():
        public string ToFile(){
            return $"{eventID}#{studentName}#{studentID}#{isMIS}#{classLevel}";
        }
    }
}