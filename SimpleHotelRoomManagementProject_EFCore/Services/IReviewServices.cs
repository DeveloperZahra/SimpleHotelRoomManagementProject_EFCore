using SimpleHotelRoomManagementProject_EFCore.Models;
using System.Buffers.Text;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace SimpleHotelRoomManagementProject_EFCore.Services
{
   // Defines the contract for review-related business operations in the application.
// This interface specifies the methods required for adding, deleting, retrieving, 
// and updating reviews, ensuring that any class implementing it follows a consistent 
// structure for handling review logic
    public interface IReviewServices
    {
        void AddNewReview(int ReviewId, int Rating, string Comment, DateTime ReviewDate, int BookingId, int GuestId); //Creates a new review record using provided details such as ID, rating, comment, date, booking, and guest.
        void DeleteReview(int reviewId); //Removes an existing review by its unique ID
        List<Review> GetAllReviews(); //Retrieves a list of all reviews in the system.
        Review GetReviewById(int reviewId); //Retrieves a specific review based on its unique ID.
        void UpdateReview(int reviewId, int rating, string comments);
    }
}