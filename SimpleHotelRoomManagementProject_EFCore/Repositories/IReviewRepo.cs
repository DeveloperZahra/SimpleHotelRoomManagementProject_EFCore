using SimpleHotelRoomManagementProject_EFCore.Models;

namespace SimpleHotelRoomManagementProject_EFCore.Repositories
{
    // Interface defining contract for review repository operations.
    // Classes implementing this interface must provide CRUD functionality for Review entities.
    public interface IReviewRepo
    {
        void AddReview(Review review); //Adds a new Review entity to the database
        void DeleteReview(int reviewId); // Deletes an existing Review from the database by its unique ID.
        List<Review> GetAllReviews(); //Retrieves all Review entities from the database.
        Review GetReviewById(int reviewId); //Retrieves a single Review entity by its unique ID
        void UpdateReview(Review review);
    }
}