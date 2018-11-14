namespace SkillShare.API.Helpers
{
    public class UserParams
    {
        //paging,sorting,filtering goes here
        private const int MaxPageSize=50;

        public int PageNumber { get; set; }=1; 
        private int pageSize=10;
        public int PageSize
        {
            get { return pageSize;}
            set { pageSize = (value>MaxPageSize)?MaxPageSize:value;}
        }
        
        public int UserId { get; set; }

        public string Skill{get;set;}

        public bool Likees { get; set; }=false;

        public bool Likers { get; set; }=false;
    }
}