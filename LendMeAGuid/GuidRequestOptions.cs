namespace LendMeAGuid
{
    public class GuidRequestOptions
    {
        public int Count { get; set; } = 1;
        public bool IsUpperCaseRequired { get; set; } = true;
        public bool IsBracketsRequired { get; set; } = false;
        public bool IsHyphensRequired { get; set; } = true;



    }
}
