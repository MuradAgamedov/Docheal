namespace Doccure.ReviewService.Dtos.ReviewDtos
{
    public class ResultReviewDto
    {
        public string RivewId { get; set; }
        public string DoctorId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserImageUrl { get; set; }
        public string Comment { get; set; }
        public int Raiting { get; set; }
        public bool Recommend { get; set; }
        public int LikeCount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
