Like Button Feature
This repository contains a "Like" button feature for the Norebase backend engineer technical challenge and includes test cases for core functionality. The goal is to create a "Like" button that can be easily integrated into an article.


Challenge Requirements
Render a "Like" button with the total number of likes when a user visits an article.
Increment the like count by one when the user clicks the button.


Project Overview
This "Like" button feature includes:

Display of the current like count for an article.
Increment the like count when the button is clicked.
The feature includes API endpoints for fetching and updating likes, a database schema, and test cases for validation.


Tech Stack
Backend: ASP.NET Core Web API
Database: In-memory for testing; can be configured for SQL databases in production
Testing: xUnit and Moq


API Endpoints
GET /{id}/likes: Fetches the current like count.
POST /{id}/likes: Increments the like count for a specific article.
Example responses:

GET: { "likes": 5 }
POST: { "likes": 6 }


Database Schema
The database contains a single table for storing like counts:

Table: LikeButtons
Id (int, Primary Key) - Unique identifier for each "Like" button instance.
Likes (int) - Tracks the total number of likes for each item.


Setup Instructions
Clone the Repository:

git clone <[repository-url](https://github.com/8sosa/Norebase-Like-Button)>
Build and Run the Application:

Open the solution in Visual Studio or Visual Studio Code.
Run the application to start the API.


Testing
The project includes tests to validate:

Like Count Retrieval: Verifies that the current like count is retrieved accurately.
Like Incrementing: Ensures the like count increments by 1 with each click.
Error Handling: Validates responses for non-existent "Like" button IDs.

Run the tests with the following command:
dotnet test
//////

Here is the endpoint deployed:
GET https://sosa.bsite.net/api/Like/1/likes - to get the likes
POST https://sosa.bsite.net/api/Like/1/likes to increase the likes

ADDITIONAL INFORMATION
1.  How would the client-side, and server-side + database be structured?

    Client-Side:

    Display Logic: The client renders a like button with the current like count, fetched via an API call when the article loads.
    Event Handling: When the user clicks the like button, an event is triggered to send a POST request to the server to increment the like count.
    UI Update: Use optimistic updates to locally increment the like count for immediate feedback, even before server confirmation. If the server fails to confirm, revert the count.

    Server-Side:

    API Endpoints
    GET /{id}/likes: Fetches the current like count.
    POST /{id}/likes: Increments the like count for a specific article.

    Database:

    Schema: The LikeButtons table includes columns for Id (Primary Key) and Likes.

    
2.  How can you improve the feature to make it more resilient against abuse/exploitation?

    To prevent abuse, we can consider these approaches:

    Rate Limiting: Implement rate limiting that is limiting the like function to one like/second per user.
    Unique User Tracking: Track unique users with an optional user_id field in the LikeButtons table or by creating a separate table for tracking user likes although this involves getting users to register and log in before being able to like.
    Bot Protection: Add CAPTCHA or reCAPTCHA for unusual behavior patterns or sudden activity surges.
    IP/Device Throttling: Track requests per IP or device fingerprint and limit unusual spikes in requests.


3.  How can you improve the feature to make it scale to millions of users and perform without issues?

    To scale to a large number of users the following approaches can be considered:
    
    Horizontal Scaling: Scale application servers horizontally to handle increased load.
    Load Balancers: Add load balancers in front of both caching and application layers.
    Database Optimization: Implement a mix of sharding, replication, and partitioning strategies based on user distribution and article access frequency.
    
4.  How will you scale to a million concurrent users clicking the button at the same time?
    To deal with high concurrency for clicks we can consider the following approaches:

    Distributed Counters: Use Redis or other distributed counters to handle high-frequency updates. Redis’s INCR operation is atomic, so it can handle concurrent increments reliably.
    Sharding: Partition data by article IDs, allowing each partition to scale horizontally.
    Background Processing: Implement a queue (e.g., RabbitMQ, Kafka) for incoming like requests, processing them in batches to reduce the frequency of direct database writes.
    Database Partitioning: Partition data for large datasets across different physical or virtual nodes.

5.  How will you scale to a million concurrent users requesting the article's like count at the same time?
    To deal with high concurrency for reading like counts we can consider the following approaches:
    Caching: Use Redis or a CDN (Content Delivery Network) to cache like counts, refreshing periodically (e.g., every few seconds).
    Read Replicas: Set up read replicas to handle high-volume reads without overloading the main database.
    API Gateway: Utilize an API gateway to load balance across instances and cache responses for high-demand requests, reducing the backend load.



Thank you for the opportunity.
