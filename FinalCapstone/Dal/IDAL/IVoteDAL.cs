using FinalCapstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCapstone.Dal.IDAL
{
    public interface IVoteDAL
    {
        //IList<Review> GetAllReviews(string id);
        bool SaveVote(Vote newVote);
    }
}
