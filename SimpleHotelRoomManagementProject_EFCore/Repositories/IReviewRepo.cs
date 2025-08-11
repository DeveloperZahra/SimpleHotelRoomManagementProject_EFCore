using SimpleHotelRoomManagementProject_EFCore.Models;

namespace SimpleHotelRoomManagementProject_EFCore.Repositories
{
    // Interface defining contract for review repository operations.
    // Classes implementing this interface must provide CRUD functionality for Review entities.
    public interface IReviewRepo
    {
        void AddReview(Review review); //Adds a new Review entity to the database
        void DeleteReview(int reviewId);
        List<Review> GetAllReviews();
        Review GetReviewById(int reviewId);
        void UpdateReview(Review review);
    }
}