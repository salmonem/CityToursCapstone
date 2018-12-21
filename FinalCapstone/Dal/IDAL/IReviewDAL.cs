using FinalCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Dal.IDAL
{
    public interface IReviewDAL
    {
        IList<Review> GetAllReviews(string id);

        bool SaveNewReview(Review newReview);
    }
}
