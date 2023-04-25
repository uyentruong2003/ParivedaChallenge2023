namespace Pariveda_Challenge
{
    public class Admin
    {
        private string user;
        private string pass;
        static private int count;
        public Admin(){}
        public Admin(string user, string pass){
            this.user = user;
            this.pass = pass;
        }
        public void SetUser(string user){
            this.user = user;
        }
        public void SetPass(string pass){
            this.pass = pass;
        }
        static public void SetCount(int count){
            Admin.count = count;
        }
        static public void IncCount(){
            Admin.count++;
        }

        public string GetUser(){
            return user;
        }
        public string GetPass(){
            return pass;
        }
        static public int GetCount(){
            return Admin.count;
        }

    }
}