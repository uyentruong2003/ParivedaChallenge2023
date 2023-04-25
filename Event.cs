namespace Pariveda_Challenge
{
    public class Event
    {
        private int eventID;
        private string eventName;
        private string eventDate;
        private string eventTime;
        private string eventLoc;
        private string eventHost;
        
        static private int count;
        public Event(){}
        public Event(int eventID, string eventName, string eventDate, string eventTime, string eventLoc,
                    string eventHost){
            this.eventID = eventID;
            this.eventName = eventName;
            this.eventDate = eventDate;
            this.eventTime = eventTime;
            this.eventLoc = eventLoc;
            this.eventHost = eventHost;
        }
        //Setters:
        public void SetEventID(int eventID){
            this.eventID = eventID;
        }
        public void SetEventName(string eventName){
            this.eventName = eventName;
        }
        public void SetEventDate(string eventDate){
            this.eventDate = eventDate;
        }
        public void SetEventTime(string eventTime){
            this.eventTime = eventTime;
        }
        public void SetEventLoc(string eventLoc){
            this.eventLoc = eventLoc;
        }
        public void SetEventHost(string eventHost){
            this.eventHost = eventHost;
        }
        static public void SetCount(int count){
            Event.count = count;
        }
        
        static public void IncCount(){
            Event.count++;
        }
        static public void DecCount(){
            Event.count--;
        }
        //Getters:
        public int GetEventID(){
            return this.eventID;
        }
        public string GetEventName(){
            return this.eventName;
        }
        public string GetEventDate(){
            return this.eventDate;
        }
        public string GetEventTime(){
            return this.eventTime;
        }
        public string GetEventLoc(){
            return this.eventLoc;
        }
        public string GetEventHost(){
            return this.eventHost;
        }
        static public int GetCount(){
            return Event.count;
        }
        // ToString():
        public override string ToString(){
            return $"EventID: {eventID}\tEvent: {eventName}\tDate: {eventDate}\tTime: {eventTime}\tLocation: {eventLoc}\tHost: {eventHost}";
        }
        public string ToFile(){
            return $"{eventID}#{eventName}#{eventDate}#{eventTime}#{eventLoc}#{eventHost}";
        }
    }
}