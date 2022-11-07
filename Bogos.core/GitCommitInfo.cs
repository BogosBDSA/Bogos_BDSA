namespace Bogos.core;

    public class GitCommitInfo{
        public int ?commit{ get; }
        public string ? date { get; }
        public string ? message { get; }

        public string ? name { get; }

        public GitCommitInfo(int commit, string date){
            this.commit = commit;
            this.date = date;
        }
         public GitCommitInfo(int commit, string date, string name, string message){
            this.commit = commit;
            this.date = date;
            this.name = name;   
        }
        
    }