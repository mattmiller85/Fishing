using Fishing.Core;
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
        public bool HasUploadDate { get; set; }
        public string UploadedBy { get; set; }

        public static PicViewModel FromPic(Pic p)
        {
            return new PicViewModel
            {
                Description = p.description,
                Id = p.id,
                Url = p.url,
                ThumbUrl = p.thumburl,
                UploadDate = p.upload_date ?? DateTime.MinValue,
                HasUploadDate = p.upload_date != null,
                UploadedBy = p.User.username
            };
        }
    }

    
}