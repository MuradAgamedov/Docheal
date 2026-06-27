namespace Doccure.BranchService.Dtos.BranchDtos
{
    public class CreateBranchDto
    {
        public int BranchName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool Status { get; set; }
    }
}
