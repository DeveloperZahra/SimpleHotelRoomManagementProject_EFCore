using SimpleHotelRoomManagementProject_EFCore.Models;
using static System.Net.Mime.MediaTypeNames;

namespace SimpleHotelRoomManagementProject_EFCore.Services
{
   // Defines the contract for review-related business operations in the application.
// This interface specifies the methods required for adding, deleting, retrieving, 
// and updating reviews, ensuring that any class implementing it follows a consistent 
// structure for handling review logic
    public interface IReviewServices
    {
        void AddNewReview(int ReviewId, int Rating, string Comment, DateTime ReviewDate, int BookingId, int GuestId);
        void DeleteReview(int reviewId);
        List<Review> GetAllReviews();
        Review GetReviewById(int reviewId);
        void UpdateReview(int reviewId, int rating, string comments);
    }
}