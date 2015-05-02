using System;

namespace Fishing.Web.Api.Models
{
    public class PicViewModel
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string ThumbUrl { get; set; }
        public string Description { get; set; }
        public DateTime UploadDate { get; set; }
        public string UploadedBy { get; set; }
    }
}